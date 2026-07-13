// ============================================================
//  Site-wide navigation motion (Apple-inspired, Flowerfinder warmth)
//  — the bar compacts on scroll, a soft pill glides between links
//    on hover, the Catalog dropdown unfolds over a blurred page,
//    and on phones a burger opens a full-screen menu.
// ============================================================
(() => {
    const nav = document.getElementById("ffNav");
    if (!nav) return;

    const finePointer = window.matchMedia("(pointer: fine)").matches;

    // --- bar compacts once the page starts scrolling
    const onScroll = () => nav.classList.toggle("nav-scrolled", window.scrollY > 40);
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });

    // --- hover pill glides between links
    const wrap = nav.querySelector(".ff-links-wrap");
    const pill = nav.querySelector(".ff-pill");
    if (wrap && pill && finePointer) {
        let visible = false;
        wrap.querySelectorAll(".ff-links a").forEach((a) => {
            a.addEventListener("mouseenter", () => {
                const r = a.getBoundingClientRect();
                const w = wrap.getBoundingClientRect();
                pill.style.width = r.width + "px";
                pill.style.transform = "translateX(" + (r.left - w.left) + "px)";
                if (!visible) {
                    // first hover: appear in place; later hovers: slide over
                    pill.classList.add("instant");
                    void pill.offsetWidth;
                    pill.classList.remove("instant");
                    visible = true;
                }
                pill.classList.add("on");
            });
        });
        wrap.addEventListener("mouseleave", () => {
            pill.classList.remove("on");
            visible = false;
        });
    }

    // --- Catalog dropdown + page veil (hover on desktop, focus for keyboards)
    const drop = document.getElementById("ffDrop");
    const trigger = document.getElementById("ffCatalogItem");
    if (drop && trigger) {
        let closeTimer;
        const open = () => {
            clearTimeout(closeTimer);
            document.body.classList.add("drop-open");
        };
        const close = () => {
            closeTimer = setTimeout(() => document.body.classList.remove("drop-open"), 140);
        };
        if (finePointer) {
            [trigger, drop].forEach((el) => {
                el.addEventListener("mouseenter", open);
                el.addEventListener("mouseleave", close);
            });
        }
        // keyboard: tabbing onto Catalog (or into the panel) opens it, tabbing out closes it
        [trigger, drop].forEach((el) => {
            el.addEventListener("focusin", open);
            el.addEventListener("focusout", (e) => {
                if (!trigger.contains(e.relatedTarget) && !drop.contains(e.relatedTarget)) close();
            });
        });
        document.addEventListener("keydown", (e) => {
            if (e.key === "Escape") document.body.classList.remove("drop-open");
        });
    }

    // --- mobile: burger toggles the full-screen menu
    const burger = document.getElementById("ffBurger");
    if (burger) {
        burger.addEventListener("click", () => {
            const isOpen = document.body.classList.toggle("menu-open");
            burger.setAttribute("aria-expanded", isOpen);
        });
        document.querySelectorAll("#ffMobile a").forEach((a) =>
            a.addEventListener("click", () => document.body.classList.remove("menu-open")));
    }
})();

// ============================================================
//  First-visit loader: let the flower bloom, then fade away
// ============================================================
(() => {
    const root = document.documentElement;
    if (!root.classList.contains("ff-loading")) return;
    const finish = () => {
        setTimeout(() => {
            root.classList.add("ff-done");
            setTimeout(() => root.classList.remove("ff-loading", "ff-done"), 650);
            try { sessionStorage.setItem("ffSeen", "1"); } catch (e) { }
        }, 1150); // let the bloom finish before fading
    };
    if (document.readyState === "complete") finish();
    else window.addEventListener("load", finish);
})();

// ============================================================
//  Shared-element page transitions: a card's photo morphs into
//  the detail page's hero (View Transitions API; harmless no-op
//  in browsers without it).
// ============================================================
(() => {
    // clicking a flower card: tag its photo as "the" transitioning element
    document.querySelectorAll("a.flower-card").forEach((a) => {
        a.addEventListener("click", () => {
            const img = a.querySelector(".card-photo");
            if (!img) return;
            // only one element per page may carry the name
            const hero = document.querySelector(".detail-photo");
            if (hero) hero.style.viewTransitionName = "none";
            img.style.viewTransitionName = "flower-photo";
        });
    });

    // navigating back from a detail page: morph the hero into the right card
    window.addEventListener("pagereveal", (e) => {
        if (!e.viewTransition || !window.navigation || !navigation.activation) return;
        const from = navigation.activation.from;
        if (!from) return;
        const match = new URL(from.url).pathname.match(/\/Flowers\/Details\/(\d+)$/i);
        if (!match) return;
        const img = document.querySelector('a.flower-card[href$="/Details/' + match[1] + '"] .card-photo');
        if (!img) return;
        img.style.viewTransitionName = "flower-photo";
        e.viewTransition.finished.then(() => { img.style.viewTransitionName = ""; });
    });
})();

// ============================================================
//  Command palette: Ctrl/Cmd+K (or the nav search button) opens
//  a floating search over the whole catalog. Data is fetched
//  once per page and kept in memory only.
// ============================================================
(() => {
    const palette = document.getElementById("palette");
    if (!palette) return;
    const input = document.getElementById("paletteInput");
    const list = document.getElementById("paletteResults");
    let flowers = null;   // fetched lazily on first open
    let sel = 0;

    const quickLinks = [
        { name: "Identify a flower", sci: "Snap or drop a photo", href: "/Identify", icon: "camera" },
        { name: "Browse the catalog", sci: "Every flower we know", href: "/Flowers", icon: "grid" }
    ];

    const open = async () => {
        palette.hidden = false;
        document.body.classList.add("palette-open");
        input.value = "";
        input.focus();
        if (!flowers) {
            try {
                flowers = await (await fetch("/Flowers/SearchData")).json();
            } catch { flowers = []; }
        }
        render("");
    };

    const close = () => {
        palette.hidden = true;
        document.body.classList.remove("palette-open");
    };

    function render(term) {
        const rows = [];
        if (!term) {
            quickLinks.forEach((q) => rows.push(q));
        }
        (flowers || [])
            .filter((f) => !term
                || f.name.toLowerCase().includes(term)
                || (f.sci || "").toLowerCase().includes(term))
            .slice(0, term ? 8 : 5)
            .forEach((f) => rows.push({ name: f.name, sci: f.sci, href: "/Flowers/Details/" + f.id, image: f.image }));

        sel = 0;
        list.innerHTML = "";
        if (rows.length === 0) {
            list.innerHTML = '<div class="palette-none">Nothing blooms by that name.</div>';
            return;
        }
        rows.forEach((r, i) => {
            const a = document.createElement("a");
            a.className = "palette-row" + (i === 0 ? " sel" : "");
            a.href = r.href;
            a.setAttribute("role", "option");
            if (r.image) {
                const img = document.createElement("img");
                img.src = r.image;
                img.alt = "";
                a.appendChild(img);
            } else {
                a.insertAdjacentHTML("beforeend",
                    '<span class="row-ico" aria-hidden="true"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.7" stroke-linecap="round">' +
                    (r.icon === "camera"
                        ? '<path d="M23 19a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h4l2-3h6l2 3h4a2 2 0 0 1 2 2z"/><circle cx="12" cy="13" r="4"/>'
                        : '<rect x="3" y="3" width="7" height="7" rx="1.5"/><rect x="14" y="3" width="7" height="7" rx="1.5"/><rect x="3" y="14" width="7" height="7" rx="1.5"/><rect x="14" y="14" width="7" height="7" rx="1.5"/>') +
                    "</svg></span>");
            }
            const copy = document.createElement("span");
            copy.className = "row-copy";
            const nm = document.createElement("span");
            nm.className = "row-name";
            nm.textContent = r.name;
            copy.appendChild(nm);
            if (r.sci) {
                const sc = document.createElement("span");
                sc.className = "row-sci";
                sc.textContent = r.sci;
                copy.appendChild(sc);
            }
            a.appendChild(copy);
            a.addEventListener("mousemove", () => select(i));
            list.appendChild(a);
        });
    }

    function select(i) {
        const rows = list.querySelectorAll(".palette-row");
        if (!rows.length) return;
        sel = Math.max(0, Math.min(i, rows.length - 1));
        rows.forEach((r, j) => r.classList.toggle("sel", j === sel));
        rows[sel].scrollIntoView({ block: "nearest" });
    }

    input.addEventListener("input", () => render(input.value.trim().toLowerCase()));
    input.addEventListener("keydown", (e) => {
        if (e.key === "ArrowDown") { e.preventDefault(); select(sel + 1); }
        else if (e.key === "ArrowUp") { e.preventDefault(); select(sel - 1); }
        else if (e.key === "Enter") {
            const row = list.querySelectorAll(".palette-row")[sel];
            if (row) row.click();
        }
    });

    document.addEventListener("keydown", (e) => {
        if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === "k") {
            e.preventDefault();
            palette.hidden ? open() : close();
        } else if (e.key === "Escape" && !palette.hidden) {
            close();
        }
    });

    document.getElementById("ffSearchBtn")?.addEventListener("click", open);
    document.getElementById("paletteVeil").addEventListener("click", close);
})();

// ============================================================
//  Theme toggle: day <-> dusk, revealed by a circle that grows
//  out of the button (View Transitions API; plain fade elsewhere)
// ============================================================
(() => {
    const btn = document.getElementById("ffTheme");
    if (!btn) return;
    const root = document.documentElement;

    const apply = () => {
        const dark = root.classList.toggle("dark");
        try { localStorage.setItem("ffTheme", dark ? "dark" : "light"); } catch (e) { }
    };

    btn.addEventListener("click", () => {
        const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
        if (!document.startViewTransition || reduce) { apply(); return; }

        // theme-wipe folds the named elements (nav, detail photo) back
        // into the root snapshot so the circle covers the whole page
        root.classList.add("theme-wipe");
        const r = btn.getBoundingClientRect();
        const x = r.left + r.width / 2;
        const y = r.top + r.height / 2;

        const vt = document.startViewTransition(apply);
        vt.ready.then(() => {
            const radius = Math.hypot(
                Math.max(x, window.innerWidth - x),
                Math.max(y, window.innerHeight - y)
            );
            root.animate(
                { clipPath: [`circle(0px at ${x}px ${y}px)`, `circle(${radius}px at ${x}px ${y}px)`] },
                { duration: 550, easing: "cubic-bezier(0.4, 0, 0.2, 1)", pseudoElement: "::view-transition-new(root)" }
            );
        }).catch(() => { });
        vt.finished.finally(() => root.classList.remove("theme-wipe"));
    });
})();

// ============================================================
//  Slow-navigation loader: when a page change takes more than a
//  beat, the bloom appears instead of a frozen page. Fast loads
//  never see it — the timer is outrun by the next document.
// ============================================================
(() => {
    if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) return;
    const root = document.documentElement;
    let timer;

    const arm = () => {
        clearTimeout(timer);
        timer = setTimeout(() => root.classList.add("ff-navload"), 450);
    };

    document.addEventListener("click", (e) => {
        if (e.defaultPrevented || e.button !== 0 || e.metaKey || e.ctrlKey || e.shiftKey || e.altKey) return;
        const a = e.target.closest("a[href]");
        if (!a || a.target === "_blank" || a.hasAttribute("download")) return;
        const url = new URL(a.href, location.href);
        if (url.origin !== location.origin) return;
        // in-page anchors don't navigate
        if (url.pathname === location.pathname && url.search === location.search && url.hash) return;
        arm();
    });

    // catalog filters and admin forms count as navigations too
    document.addEventListener("submit", (e) => {
        if (!e.defaultPrevented) arm();
    });

    // coming back via bfcache (or a cancelled navigation): stand down
    window.addEventListener("pageshow", () => {
        clearTimeout(timer);
        root.classList.remove("ff-navload");
    });
})();

// ============================================================
//  Photos: swap the loading shimmer off once each image arrives
// ============================================================
(() => {
    document.querySelectorAll("img.card-photo, img.detail-photo").forEach((img) => {
        if (img.complete && img.naturalWidth > 0) img.classList.add("loaded");
        else img.addEventListener("load", () => img.classList.add("loaded"), { once: true });
    });
})();
