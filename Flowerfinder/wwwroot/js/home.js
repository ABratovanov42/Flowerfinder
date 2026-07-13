// ============================================================
//  Flowerfinder front page motion (GSAP + ScrollTrigger)
//
//  Scroll story: the hero pins, the headline drifts away and a
//  line-art flower draws itself — stem, leaves, petals inked in
//  one by one — then watercolour washes bloom into the petals,
//  a butterfly flutters down to land on it, and the CTA appears.
//
//  prefers-reduced-motion: the scroll story stays (it only moves
//  when the visitor scrolls), autonomous motion is skipped.
//  Add ?motion=1 to the URL to preview the full experience
//  regardless of the OS setting.
// ============================================================

gsap.registerPlugin(ScrollTrigger);

const forceMotion = new URLSearchParams(location.search).has("motion");
if (forceMotion) document.documentElement.classList.add("force-motion");
const reduceMotion = !forceMotion && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

// ---------------------------------------------------------
//  Prepare every stroke for draw-on animation
// ---------------------------------------------------------
const stemLine = document.getElementById("stemLine");
const leafLines = gsap.utils.toArray(".leaf-line");
const petalLines = gsap.utils.toArray(".petal-line");
const centerRing = document.getElementById("centerRing");

gsap.utils.toArray("#heroFlower .draw").forEach((p) => {
    const len = p.getTotalLength();
    p.style.strokeDasharray = len;
    p.style.strokeDashoffset = len;
});
gsap.set(".center-dot", { opacity: 0 });

// A little sprout pokes out of the ground before any scrolling
stemLine.style.strokeDashoffset = stemLine.getTotalLength() * 0.82;

// ---------------------------------------------------------
//  The scroll story (scrubbed — runs even under reduced motion)
// ---------------------------------------------------------
const story = gsap.timeline({
    scrollTrigger: {
        trigger: "#hero",
        start: "top top",
        end: "+=2800",
        pin: true,
        scrub: 0.5
    }
});

// headline and hint drift away as growth begins
story.to("#heroCopy", { opacity: 0, y: -70, duration: 0.13, ease: "power1.in" }, 0.02);
story.to("#heroHint", { opacity: 0, duration: 0.05 }, 0);
story.to("#heroFlower", { opacity: 0.95, duration: 0.22, ease: "none" }, 0.05);

// stem rises from the sprout
story.to(stemLine, { strokeDashoffset: 0, duration: 0.27, ease: "none" }, 0.05);

// leaves unfurl on the way up
story.to(leafLines[0], { strokeDashoffset: 0, duration: 0.08, ease: "power1.inOut" }, 0.18);
story.to(leafLines[1], { strokeDashoffset: 0, duration: 0.08, ease: "power1.inOut" }, 0.26);

// petals are inked in one by one, starting from the two that
// meet the stem tip and wrapping up and around the bloom
const petalOrder = [4, 5, 3, 6, 2, 7, 1, 8, 0];
petalOrder.forEach((idx, i) => {
    story.to(petalLines[idx], { strokeDashoffset: 0, duration: 0.09, ease: "power1.inOut" }, 0.36 + i * 0.042);
});

// the flower head settles into scale as it blooms
story.fromTo("#heroFlower",
    { scale: 0.94, transformOrigin: "50% 78%" },
    { scale: 1, duration: 0.5, ease: "power1.out" }, 0.36);

// centre ring + seeds complete the line work
story.to(centerRing, { strokeDashoffset: 0, duration: 0.05, ease: "none" }, 0.76);
story.to(".center-dot", { opacity: 1, duration: 0.03, stagger: 0.01 }, 0.79);

// …then watercolour washes bloom into the drawing
story.to(leafLines, { attr: { "fill-opacity": 0.8 }, duration: 0.07, ease: "none" }, 0.8);
petalOrder.forEach((idx, i) => {
    story.to(petalLines[idx], { attr: { "fill-opacity": 0.85 }, duration: 0.06, ease: "none" }, 0.82 + i * 0.009);
});
story.to(centerRing, { attr: { "fill-opacity": 0.9 }, duration: 0.05, ease: "none" }, 0.9);

// a butterfly flutters down and lands on the bloom
// (x and y run on different eases so the flight path curves)
story.fromTo("#butterfly", { opacity: 0 }, { opacity: 1, duration: 0.03, ease: "none" }, 0.88);
story.fromTo("#butterfly", { x: 380 }, { x: 0, duration: 0.1, ease: "none" }, 0.88);
story.fromTo("#butterfly", { y: -170 }, { y: 0, duration: 0.1, ease: "power1.in" }, 0.88);
story.fromTo("#butterfly", { rotation: -14 }, { rotation: 0, duration: 0.1, ease: "power1.out" }, 0.88);

// the call-to-action blooms in last
story.set("#bloomCta", { pointerEvents: "auto" }, 0.93);
story.fromTo("#bloomCta",
    { opacity: 0, y: 24 },
    { opacity: 1, y: 0, duration: 0.07, ease: "power2.out" }, 0.93);

// (Navbar scroll behaviour lives in nav.js — shared by every page)

// ---------------------------------------------------------
//  Autonomous motion — skipped under reduced motion
//  (CSS shows .reveal elements statically in that case)
// ---------------------------------------------------------
if (!reduceMotion) {
    // intro: headline words rise out of their masks, the rest fades up
    gsap.fromTo(".hero h1 .wi",
        { yPercent: 115 },
        { yPercent: 0, duration: 0.9, stagger: 0.09, ease: "power3.out", delay: 0.25 });
    gsap.fromTo("[data-hero]",
        { opacity: 0, y: 26 },
        { opacity: 1, y: 0, duration: 0.9, stagger: 0.14, ease: "power2.out", delay: 0.55 });

    // the flower sways almost imperceptibly, like a breeze
    gsap.to("#heroFlower", {
        rotation: 1.4,
        transformOrigin: "50% 90%",
        duration: 3.6,
        repeat: -1,
        yoyo: true,
        ease: "sine.inOut",
        delay: 2
    });

    // butterfly wings flap gently (visible once it flies in)
    gsap.to("#bfL", { scaleX: 0.5, transformOrigin: "100% 50%", duration: 0.22, repeat: -1, yoyo: true, ease: "sine.inOut" });
    gsap.to("#bfR", { scaleX: 0.5, transformOrigin: "0% 50%", duration: 0.22, repeat: -1, yoyo: true, ease: "sine.inOut" });

    // background washes drift slowly, plus a touch of scroll parallax
    gsap.utils.toArray(".blob").forEach((b, i) => {
        gsap.to(b, {
            x: (i % 2 ? -1 : 1) * gsap.utils.random(30, 70),
            y: (i % 2 ? 1 : -1) * gsap.utils.random(20, 60),
            duration: gsap.utils.random(16, 26),
            repeat: -1,
            yoyo: true,
            ease: "sine.inOut"
        });
        gsap.to(b, {
            yPercent: (i % 2 ? -1 : 1) * 10,
            ease: "none",
            scrollTrigger: { start: 0, end: "max", scrub: 1.5 }
        });
    });

    // a few petals fall gently through the page
    const field = document.getElementById("petalField");
    for (let i = 0; i < 9; i++) {
        const petal = document.createElement("div");
        petal.className = "petal";
        const size = gsap.utils.random(10, 20);
        petal.style.width = size + "px";
        petal.style.height = size * 0.82 + "px";
        petal.style.left = gsap.utils.random(2, 96) + "vw";
        field.appendChild(petal);

        const fall = () => {
            gsap.fromTo(petal,
                { y: -40, x: 0, rotation: gsap.utils.random(0, 360), opacity: 0 },
                {
                    y: window.innerHeight + 60,
                    x: gsap.utils.random(-90, 90),
                    rotation: "+=" + gsap.utils.random(120, 420),
                    opacity: 0.65,
                    duration: gsap.utils.random(11, 20),
                    ease: "none",
                    delay: gsap.utils.random(0, 6),
                    onComplete: fall
                });
        };
        fall();
    }

    // scroll reveals below the hero; transforms are cleared after
    // each reveal so the cards' CSS hover lift keeps working
    gsap.utils.toArray(".reveal:not([data-hero])").forEach((el) => {
        gsap.fromTo(el,
            { opacity: 0, y: 28, scale: 0.98 },
            {
                opacity: 1, y: 0, scale: 1,
                duration: 0.85,
                ease: "power2.out",
                clearProps: "transform",
                scrollTrigger: { trigger: el, start: "top 86%" }
            });
    });

    // the progress vine fills as the visitor moves through the page
    // (explicit fromTo — GSAP must not infer the CSS scaleY(0) start)
    gsap.fromTo(".progress-vine .fill",
        { scaleY: 0 },
        {
            scaleY: 1,
            ease: "none",
            scrollTrigger: { start: 0, end: "max", scrub: 0.4 }
        });

    // mouse parallax: the flower drifts gently opposite the cursor.
    // Targets the wrapper so it never fights the story timeline,
    // which owns the SVG's own transforms.
    if (window.matchMedia("(pointer: fine)").matches) {
        window.addEventListener("mousemove", (e) => {
            const nx = e.clientX / window.innerWidth - 0.5;
            const ny = e.clientY / window.innerHeight - 0.5;
            gsap.to(".hero-flower-wrap", {
                x: nx * 18, y: ny * 12,
                duration: 1.2, ease: "power2.out", overwrite: "auto"
            });
        });

        // magnetic buttons: pull toward the cursor, spring back on leave
        document.querySelectorAll(".btn-bloom, .btn-ghost").forEach((btn) => {
            btn.addEventListener("mousemove", (e) => {
                const r = btn.getBoundingClientRect();
                gsap.to(btn, {
                    x: (e.clientX - r.left - r.width / 2) * 0.22,
                    y: (e.clientY - r.top - r.height / 2) * 0.35,
                    duration: 0.4, ease: "power2.out"
                });
            });
            btn.addEventListener("mouseleave", () => {
                gsap.to(btn, { x: 0, y: 0, duration: 0.6, ease: "elastic.out(1, 0.45)" });
            });
        });
    }
}

// ---------------------------------------------------------
//  "In bloom right now" ribbon: arrows page through it, and on
//  desktop you can grab and drag it. (User-driven, so it stays
//  active under reduced motion.)
// ---------------------------------------------------------
(() => {
    const row = document.getElementById("bloomRow");
    if (!row) return;
    const prev = document.getElementById("bloomPrev");
    const next = document.getElementById("bloomNext");

    const page = () => Math.max(row.clientWidth * 0.8, 260);
    prev.addEventListener("click", () => row.scrollBy({ left: -page(), behavior: "smooth" }));
    next.addEventListener("click", () => row.scrollBy({ left: page(), behavior: "smooth" }));

    // arrows fade out at either end
    const sync = () => {
        prev.disabled = row.scrollLeft < 10;
        next.disabled = row.scrollLeft > row.scrollWidth - row.clientWidth - 10;
    };
    row.addEventListener("scroll", sync, { passive: true });
    window.addEventListener("resize", sync);
    sync();

    // grab-and-drag on fine pointers; a real drag must not "click" a card
    if (window.matchMedia("(pointer: fine)").matches) {
        let down = false, moved = false, startX = 0, startLeft = 0;
        row.addEventListener("pointerdown", (e) => {
            down = true;
            moved = false;
            startX = e.clientX;
            startLeft = row.scrollLeft;
        });
        window.addEventListener("pointermove", (e) => {
            if (!down) return;
            const dx = e.clientX - startX;
            if (Math.abs(dx) > 6 && !moved) {
                moved = true;
                row.classList.add("dragging"); // snap released while dragging
            }
            if (moved) row.scrollLeft = startLeft - dx;
        });
        window.addEventListener("pointerup", () => {
            down = false;
            row.classList.remove("dragging");
        });
        row.addEventListener("click", (e) => {
            if (moved) {
                e.preventDefault();
                e.stopPropagation();
                moved = false;
            }
        }, true);
    }
})();

// In-page anchors glide via CSS scroll-behavior: smooth.
