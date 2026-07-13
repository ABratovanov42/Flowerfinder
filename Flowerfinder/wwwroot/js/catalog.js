// ============================================================
//  Catalog page motion — live search with FLIP (cards glide to
//  their new grid spots), a rolling odometer count, and the
//  wilting-flower empty state when nothing matches.
// ============================================================
(() => {
    const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    // --- filters apply themselves — no need to press the button
    document.querySelectorAll("#catFilters select").forEach((s) =>
        s.addEventListener("change", () => s.form.submit()));

    const grid = document.querySelector(".flower-grid");
    const searchBox = document.querySelector('#catFilters input[name="q"]');
    const countLine = document.querySelector(".cat-count");
    const emptyBox = document.getElementById("catEmpty");

    // ---------------------------------------------------------
    //  Rolling count: each digit is a reel of 0-9 that spins
    // ---------------------------------------------------------
    let odo, suffix;
    if (countLine) {
        const m = countLine.textContent.trim().match(/^(\d+)\s*(.*)$/);
        if (m) {
            countLine.innerHTML = '<span class="odo" aria-hidden="true"></span><span class="odo-suffix"></span>';
            countLine.setAttribute("aria-live", "polite");
            odo = countLine.querySelector(".odo");
            suffix = countLine.querySelector(".odo-suffix");
            setCount(parseInt(m[1], 10), m[2]);
        }
    }

    function setCount(n, text) {
        if (!odo) return;
        suffix.textContent = " " + text;
        const digits = String(n).split("");
        while (odo.children.length > digits.length) odo.removeChild(odo.firstChild);
        while (odo.children.length < digits.length) {
            const reel = document.createElement("span");
            reel.className = "odo-digit";
            const strip = document.createElement("span");
            strip.className = "odo-strip";
            for (let d = 0; d <= 9; d++) {
                const s = document.createElement("span");
                s.textContent = d;
                strip.appendChild(s);
            }
            reel.appendChild(strip);
            odo.insertBefore(reel, odo.firstChild);
        }
        digits.forEach((d, i) => {
            odo.children[i].firstChild.style.transform = "translateY(" + (-d * 1.1) + "em)";
        });
    }

    // ---------------------------------------------------------
    //  Live search with FLIP: measure, re-flow, glide
    // ---------------------------------------------------------
    if (!grid || !searchBox) return;
    const cards = Array.from(grid.querySelectorAll(".flower-card"));

    let debounce;
    searchBox.addEventListener("input", () => {
        clearTimeout(debounce);
        debounce = setTimeout(() => applyFilter(searchBox.value.trim().toLowerCase()), 90);
    });

    function applyFilter(term) {
        // the entrance stagger has done its job; filtering owns the cards now
        grid.classList.add("settled");
        if (grid.style.display === "none") grid.style.display = "";

        // First: where is everybody?
        const first = new Map();
        cards.forEach((c) => {
            if (c.style.display !== "none") first.set(c, c.getBoundingClientRect());
        });

        // re-flow the grid
        let shown = 0;
        const entries = [], survivors = [];
        cards.forEach((c) => {
            const name = c.querySelector("h3").textContent.toLowerCase();
            const sci = (c.querySelector(".sci")?.textContent || "").toLowerCase();
            const hit = !term || name.includes(term) || sci.includes(term);
            const wasShown = c.style.display !== "none";
            c.style.display = hit ? "" : "none";
            if (hit) {
                shown++;
                (wasShown ? survivors : entries).push(c);
            }
        });

        setCount(shown, (shown === 1 ? "flower" : "flowers") + (term ? " found" : " in the catalog"));
        if (emptyBox) emptyBox.hidden = shown !== 0;
        if (shown === 0) grid.style.display = "none";

        if (reduce || shown === 0) return;

        // Last + Invert + Play: survivors glide, newcomers bloom in
        survivors.forEach((c) => {
            const f = first.get(c);
            if (!f) return;
            const l = c.getBoundingClientRect();
            const dx = f.left - l.left, dy = f.top - l.top;
            if (dx || dy) {
                c.animate(
                    [{ transform: "translate(" + dx + "px," + dy + "px)" }, { transform: "none" }],
                    { duration: 380, easing: "cubic-bezier(0.4, 0, 0.2, 1)" }
                );
            }
        });
        entries.forEach((c, i) => {
            c.animate(
                [{ opacity: 0, transform: "scale(0.92) translateY(10px)" }, { opacity: 1, transform: "none" }],
                { duration: 320, delay: Math.min(i * 25, 200), easing: "cubic-bezier(0.2, 0.8, 0.3, 1)", fill: "backwards" }
            );
        });
    }
})();
