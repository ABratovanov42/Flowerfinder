// ============================================================
//  Photo lightbox — any img.lightboxable opens full-screen.
//  Click anywhere or press Escape to close.
// ============================================================
(() => {
    const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    let veil = null;

    function open(src, alt) {
        close();
        veil = document.createElement("div");
        veil.className = "lb-veil";
        veil.setAttribute("role", "dialog");
        veil.setAttribute("aria-label", alt || "Photo");
        const img = document.createElement("img");
        img.src = src;
        img.alt = alt || "";
        veil.appendChild(img);
        veil.addEventListener("click", close);
        document.body.appendChild(veil);
        document.body.style.overflow = "hidden";
        if (!reduce) {
            veil.animate([{ opacity: 0 }, { opacity: 1 }], { duration: 220, easing: "ease-out" });
            img.animate(
                [{ transform: "scale(0.92)", opacity: 0 }, { transform: "scale(1)", opacity: 1 }],
                { duration: 320, easing: "cubic-bezier(0.2, 0.8, 0.3, 1)" }
            );
        }
    }

    function close() {
        if (!veil) return;
        veil.remove();
        veil = null;
        document.body.style.overflow = "";
    }

    document.addEventListener("click", (e) => {
        const img = e.target.closest("img.lightboxable");
        if (img) open(img.currentSrc || img.src, img.alt);
    });

    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape") close();
    });
})();
