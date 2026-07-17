// ============================================================
//  Flower detail page motion — hero parallax, facts that rise
//  in on scroll, and a sticky mini-bar once the title is gone.
// ============================================================
(() => {
    // ?motion=1 previews the full experience despite the OS reduced-motion
    // setting — same convention as home.js
    const forceMotion = new URLSearchParams(location.search).has("motion");
    const reduce = !forceMotion && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    // hero photo drifts slower than the page (gentle depth)
    const photo = document.querySelector(".detail-photo");
    if (photo && !reduce) {
        let ticking = false;
        window.addEventListener("scroll", () => {
            if (ticking) return;
            ticking = true;
            requestAnimationFrame(() => {
                // slight zoom keeps the drifting photo's edges inside the hero frame
                photo.style.transform = "translateY(" + Math.min(window.scrollY * 0.1, 60) + "px) scale(1.08)";
                ticking = false;
            });
        }, { passive: true });
    }

    // ---- photo tint: the page glows softly in the flower's own colour ----
    const tintEl = document.querySelector(".detail-tint");
    if (photo && tintEl) {
        const applyTint = () => {
            try {
                const s = 24;
                const c = document.createElement("canvas");
                c.width = s; c.height = s;
                const g = c.getContext("2d", { willReadFrequently: true });
                // sample the middle of the photo — that's where the flower is,
                // not the sky or foliage around the edges
                const w = photo.naturalWidth, h = photo.naturalHeight;
                g.drawImage(photo, w * 0.2, h * 0.2, w * 0.6, h * 0.6, 0, 0, s, s);
                const d = g.getImageData(0, 0, s, s).data;
                let r = 0, gr = 0, b = 0, n = 0;
                for (let i = 0; i < d.length; i += 4) {
                    const R = d[i], G = d[i + 1], B = d[i + 2];
                    const mx = Math.max(R, G, B), mn = Math.min(R, G, B);
                    if (mx - mn < 24 || mx < 40 || mn > 235) continue; // skip greys and extremes
                    r += R; gr += G; b += B; n++;
                }
                if (n > 20) {
                    tintEl.style.setProperty("--tint",
                        Math.round(r / n) + ", " + Math.round(gr / n) + ", " + Math.round(b / n));
                }
            } catch (e) { /* canvas unavailable — the default rose glow stays */ }
        };
        photo.complete && photo.naturalWidth ? applyTint() : photo.addEventListener("load", applyTint, { once: true });
    }

    // ---- petal burst when a flower joins the garden ----
    const gardenForm = document.querySelector(".garden-form");
    const gardenBtn = gardenForm?.querySelector(".garden-btn");
    if (gardenForm && gardenBtn && !gardenBtn.classList.contains("in") && !reduce) {
        gardenForm.addEventListener("submit", (e) => {
            e.preventDefault();
            const rect = gardenBtn.getBoundingClientRect();
            for (let i = 0; i < 10; i++) {
                const p = document.createElement("i");
                p.className = "petal-bit";
                p.style.left = rect.left + rect.width / 2 + "px";
                p.style.top = rect.top + rect.height / 2 + "px";
                document.body.appendChild(p);
                const ang = (Math.PI * 2 * i) / 10 + Math.random() * 0.6;
                const dist = 42 + Math.random() * 48;
                p.animate(
                    [
                        { transform: "translate(0,0) rotate(0deg)", opacity: 1 },
                        { transform: "translate(" + Math.cos(ang) * dist + "px," + (Math.sin(ang) * dist - 24) + "px) rotate(" + (200 + Math.random() * 160) + "deg)", opacity: 0 }
                    ],
                    { duration: 520 + Math.random() * 180, easing: "cubic-bezier(0.2, 0.8, 0.3, 1)" }
                ).onfinish = () => p.remove();
            }
            setTimeout(() => gardenForm.submit(), 460); // let the petals fly first
        });
    }

    // guide icons draw themselves in, like the hero flower on the home
    // page: normalise every stroke, then CSS animates the offset once the
    // section gets its .in class from the observer below
    if (!reduce) {
        document.querySelectorAll(".sec-ico svg").forEach((svg) => {
            svg.querySelectorAll("path, circle, rect").forEach((shape, i) => {
                // same technique as the home hero: dash the stroke to its
                // own length, then the .in rule transitions it to zero
                let len = 60;
                try { len = shape.getTotalLength(); } catch (e) { }
                shape.style.strokeDasharray = len;
                shape.style.strokeDashoffset = len;
                shape.style.transitionDelay = 0.15 + i * 0.14 + "s";
            });
            svg.classList.add("draws");
        });
    }

    // care facts + related cards rise in as they enter the viewport
    const detail = document.querySelector(".flower-detail");
    const items = document.querySelectorAll(".fact, .grow-step, .g-sec, .prob-card, .related-grid .flower-card");
    if (detail && items.length && "IntersectionObserver" in window && !reduce) {
        detail.classList.add("anim");
        items.forEach((el, i) => el.style.setProperty("--d", i % 6));
        const io = new IntersectionObserver((entries) => {
            entries.forEach((en) => {
                if (en.isIntersecting) {
                    en.target.classList.add("in");
                    io.unobserve(en.target);
                }
            });
        }, { rootMargin: "0px 0px -8% 0px" });
        items.forEach((el) => io.observe(el));
    }

    // the growing-plan stem fills as you scroll; nodes light up
    // as the fill reaches them
    const tl = document.querySelector(".grow-timeline");
    if (tl) {
        const steps = Array.from(tl.querySelectorAll(".grow-step"));
        if (reduce) {
            steps.forEach((s) => s.classList.add("lit")); // static, fully grown
        } else {
            let tick2 = false;
            const update = () => {
                const r = tl.getBoundingClientRect();
                // the stem tracks a point ~72% down the viewport
                const p = Math.min(1, Math.max(0, (window.innerHeight * 0.72 - r.top) / r.height));
                tl.style.setProperty("--grow", p.toFixed(4));
                const fillY = r.top + 10 + p * (r.height - 36); // track insets: 10 top, 26 bottom
                steps.forEach((s) => {
                    const nr = s.querySelector(".grow-node").getBoundingClientRect();
                    s.classList.toggle("lit", nr.top + nr.height / 2 <= fillY);
                });
                tick2 = false;
            };
            tl.style.setProperty("--grow", "0");
            window.addEventListener("scroll", () => {
                if (!tick2) {
                    tick2 = true;
                    requestAnimationFrame(update);
                }
            }, { passive: true });
            update();
        }
    }

    // guide contents rail: highlight the section being read
    const toc = document.getElementById("guideToc");
    if (toc) {
        const links = Array.from(toc.querySelectorAll('a[href^="#"]'));
        const targets = links
            .map((a) => document.getElementById(a.getAttribute("href").slice(1)))
            .filter(Boolean);
        let tick3 = false;
        const spy = () => {
            let current = targets[0];
            for (const t of targets) {
                if (t.getBoundingClientRect().top < window.innerHeight * 0.35) current = t;
            }
            links.forEach((a) =>
                a.classList.toggle("on", current && a.getAttribute("href") === "#" + current.id));
            tick3 = false;
        };
        window.addEventListener("scroll", () => {
            if (!tick3) {
                tick3 = true;
                requestAnimationFrame(spy);
            }
        }, { passive: true });
        spy();
    }

    // mini-bar appears when the title scrolls out of view
    const bar = document.getElementById("miniBar");
    const title = document.querySelector(".flower-detail h1");
    if (bar && title && "IntersectionObserver" in window) {
        new IntersectionObserver(([en]) => {
            bar.classList.toggle("on", !en.isIntersecting && en.boundingClientRect.top < 0);
        }).observe(title);
    }
})();
