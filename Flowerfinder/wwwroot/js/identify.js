// ============================================================
//  Identify page — drop/browse a photo, send it off, and let
//  the guesses bloom in with confidence bars.
// ============================================================
(() => {
    const form = document.getElementById("idForm");
    if (!form) return; // identification not configured

    const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    const analyzeUrl = form.dataset.analyzeUrl;
    const zone = document.getElementById("dropZone");
    const input = document.getElementById("photoInput");
    const preview = document.getElementById("idPreview");
    const previewImg = document.getElementById("previewImg");
    const thinking = document.getElementById("idThinking");
    const results = document.getElementById("idResults");
    const errorBox = document.getElementById("idError");

    let file = null;

    // ---- state helpers ------------------------------------------------
    const show = (el, on) => { el.hidden = !on; };

    function setState(state) {
        show(zone, state === "drop");
        show(preview, state === "preview");
        show(thinking, state === "thinking");
        show(results, state === "results");
    }

    function fail(message) {
        errorBox.textContent = message;
        show(errorBox, true);
        setState(file ? "preview" : "drop");
    }

    // ---- choosing a photo ---------------------------------------------
    function take(f) {
        if (!f) return;
        if (!/^image\/(jpeg|png|webp)$/i.test(f.type)) { fail("That doesn't look like a photo — JPEG, PNG or WebP, please."); return; }
        if (f.size > 10 * 1024 * 1024) { fail("That photo is over 10 MB — try a smaller one."); return; }
        file = f;
        show(errorBox, false);
        if (previewImg.src) URL.revokeObjectURL(previewImg.src);
        previewImg.src = URL.createObjectURL(f);
        setState("preview");
    }

    input.addEventListener("change", () => take(input.files[0]));

    ["dragenter", "dragover"].forEach((t) => zone.addEventListener(t, (e) => {
        e.preventDefault();
        zone.classList.add("drag-over");
    }));
    ["dragleave", "drop"].forEach((t) => zone.addEventListener(t, (e) => {
        e.preventDefault();
        zone.classList.remove("drag-over");
    }));
    zone.addEventListener("drop", (e) => take(e.dataTransfer.files[0]));

    document.getElementById("changeBtn").addEventListener("click", () => {
        input.value = "";
        file = null;
        show(errorBox, false);
        setState("drop");
    });

    // ---- asking the identifier ----------------------------------------
    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        if (!file) return;
        show(errorBox, false);
        setState("thinking");

        const data = new FormData();
        data.append("photo", file);
        data.append("__RequestVerificationToken",
            form.querySelector('input[name="__RequestVerificationToken"]').value);

        let payload;
        try {
            const res = await fetch(analyzeUrl, { method: "POST", body: data });
            payload = await res.json().catch(() => null);
            if (!res.ok) { fail((payload && payload.error) || "Something went wrong — try again."); return; }
        } catch {
            fail("Couldn't reach the identifier — check your connection and try again.");
            return;
        }

        render(payload);
    });

    // ---- showing the answer -------------------------------------------
    function render(payload) {
        const guesses = payload.guesses || [];
        results.innerHTML = "";

        // a staged answer must never be mistaken for a real one
        if (payload.demo) {
            results.insertAdjacentHTML("beforeend",
                '<div class="demo-note">This is a <strong>staged demo answer</strong> — every photo gets this same result. ' +
                'Add a free <a href="https://my.plantnet.org" target="_blank" rel="noopener">PlantNet API key</a> ' +
                "in appsettings.json for real identification.</div>");
        }

        if (!guesses.length) {
            results.innerHTML = '<p class="no-guess">The identifier couldn\'t name this one. Try a closer shot of a single bloom.</p>';
        } else {
            const best = guesses[0];
            results.appendChild(guessCard(best, true));

            if (best.match) {
                results.insertAdjacentHTML("beforeend",
                    '<div class="id-catalog-lead">It\'s in our catalog &mdash; the story, care and growing plan are waiting:</div>');
                results.appendChild(matchCard(best.match));
            } else {
                // "Add to my catalog": the saved find carries the photo, the
                // query carries the names — the Create form arrives pre-filled
                var addUrl = "/Flowers/Create";
                if (payload.record) {
                    addUrl += "?fromRecord=" + payload.record +
                        "&sci=" + encodeURIComponent(best.sci || "") +
                        (best.common ? "&common=" + encodeURIComponent(best.common) : "");
                }
                var lead = document.createElement("div");
                lead.className = "id-catalog-lead";
                lead.innerHTML = 'Not in our catalog yet &mdash; <a class="id-add-link"></a>';
                var link = lead.querySelector(".id-add-link");
                link.href = addUrl;
                link.textContent = payload.record
                    ? "add it with this photo →"
                    : "be the one to add it";
                results.appendChild(lead);
            }

            if (guesses.length > 1) {
                results.insertAdjacentHTML("beforeend", '<div class="also-kicker">It could also be</div>');
                guesses.slice(1).forEach((g) => results.appendChild(guessCard(g, false)));
            }
        }

        results.insertAdjacentHTML("beforeend",
            '<div class="id-again"><button type="button" class="btn-ghost" id="againBtn">Try another photo</button></div>');
        results.querySelector("#againBtn").addEventListener("click", () => {
            input.value = "";
            file = null;
            setState("drop");
        });

        setState("results");

        // confidence bars grow once they're on screen
        requestAnimationFrame(() => {
            results.querySelectorAll(".conf-fill").forEach((bar) => {
                bar.style.width = bar.dataset.score + "%";
            });
        });
        if (!reduce) {
            results.querySelectorAll(".id-guess, .id-catalog-lead, .id-match, .also-kicker").forEach((el, i) => {
                el.animate(
                    [{ opacity: 0, transform: "translateY(14px)" }, { opacity: 1, transform: "none" }],
                    { duration: 450, delay: i * 90, easing: "cubic-bezier(0.2, 0.8, 0.3, 1)", fill: "backwards" }
                );
            });
        }
    }

    function guessCard(g, isBest) {
        const el = document.createElement("div");
        el.className = "id-guess" + (isBest ? " best" : "");
        el.innerHTML =
            '<div class="guess-names">' +
                (g.common ? '<div class="guess-common"></div>' : "") +
                '<div class="guess-sci"></div>' +
            "</div>" +
            '<div class="conf"><div class="conf-track"><div class="conf-fill" data-score="' + Math.max(2, g.score) + '"></div></div>' +
            '<span class="conf-num">' + g.score + "%</span></div>";
        if (g.common) el.querySelector(".guess-common").textContent = g.common;
        el.querySelector(".guess-sci").textContent = g.sci;
        return el;
    }

    function matchCard(m) {
        const a = document.createElement("a");
        a.className = "flower-card id-match";
        a.href = "/Flowers/Details/" + encodeURIComponent(m.id);
        if (m.image) {
            const frame = document.createElement("div");
            frame.className = "photo-frame";
            const img = document.createElement("img");
            img.className = "card-photo loaded";
            img.src = m.image;
            img.alt = m.name;
            frame.appendChild(img);
            frame.insertAdjacentHTML("beforeend", '<span class="view-hint">View &rarr;</span>');
            a.appendChild(frame);
        }
        const h3 = document.createElement("h3");
        h3.textContent = m.name;
        a.appendChild(h3);
        const sci = document.createElement("div");
        sci.className = "sci";
        sci.textContent = m.sci || "";
        a.appendChild(sci);
        const chip = document.createElement("span");
        chip.className = "chip";
        chip.textContent = m.season;
        a.appendChild(chip);
        return a;
    }
})();
