// ============================================================
//  Shared micro-interactions (every page)
//
//  Flower cards tilt gently toward the cursor — a few degrees,
//  Apple TV poster style — while a soft specular highlight
//  follows the pointer across the photo. Fine pointers only,
//  and never under reduced motion.
// ============================================================
(() => {
    if (!window.matchMedia("(pointer: fine)").matches) return;
    if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) return;

    const MAX_TILT = 4.5; // degrees

    document.querySelectorAll(".flower-card").forEach((card) => {
        const frame = card.querySelector(".photo-frame");
        let raf = null;

        card.addEventListener("mousemove", (e) => {
            if (frame) {
                const fr = frame.getBoundingClientRect();
                frame.style.setProperty("--mx", (((e.clientX - fr.left) / fr.width) * 100).toFixed(1) + "%");
                frame.style.setProperty("--my", (((e.clientY - fr.top) / fr.height) * 100).toFixed(1) + "%");
            }
            if (raf) return;
            raf = requestAnimationFrame(() => {
                const r = card.getBoundingClientRect();
                const px = (e.clientX - r.left) / r.width;
                const py = (e.clientY - r.top) / r.height;
                // short linear chase while tracking; the CSS spring takes over on leave
                card.style.transition = "transform 0.12s ease-out, box-shadow 0.25s";
                card.style.transform =
                    "perspective(700px) translateY(-5px)" +
                    " rotateX(" + ((0.5 - py) * MAX_TILT).toFixed(2) + "deg)" +
                    " rotateY(" + ((px - 0.5) * MAX_TILT).toFixed(2) + "deg)";
                raf = null;
            });
        });

        card.addEventListener("mouseleave", () => {
            card.style.transition = "";
            card.style.transform = "";
        });
    });
})();
