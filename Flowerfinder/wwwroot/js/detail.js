// ============================================================
//  Flower detail page motion — hero parallax, facts that rise
//  in on scroll, and a sticky mini-bar once the title is gone.
// ============================================================
(() => {
    const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

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
