// ============================================================
//  Photo lightbox — any img.lightboxable opens full-screen,
//  growing out of the exact spot you clicked (FLIP) and
//  shrinking back into it on close. Escape or click closes.
// ============================================================
(() => {
    const forceMotion = new URLSearchParams(location.search).has("motion");
    const reduce = !forceMotion && window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    let veil = null;
    let source = null;

    // where the full-size photo should sit: centred, contained
    function targetRect(w, h) {
        const maxW = innerWidth * 0.94, maxH = innerHeight * 0.94;
        const scale = Math.min(maxW / w, maxH / h);
        const tw = w * scale, th = h * scale;
        return { left: (innerWidth - tw) / 2, top: (innerHeight - th) / 2, width: tw, height: th };
    }

    function open(img) {
        close(true);
        source = img;
        const from = img.getBoundingClientRect();
        const to = targetRect(img.naturalWidth || from.width, img.naturalHeight || from.height);

        veil = document.createElement("div");
        veil.className = "lb-veil";
        veil.setAttribute("role", "dialog");
        veil.setAttribute("aria-label", img.alt || "Photo");
        const big = document.createElement("img");
        big.src = img.currentSrc || img.src;
        big.alt = img.alt || "";
        Object.assign(big.style, {
            position: "fixed",
            left: to.left + "px", top: to.top + "px",
            width: to.width + "px", height: to.height + "px"
        });
        veil.appendChild(big);
        veil.addEventListener("click", () => close());
        document.body.appendChild(veil);
        document.body.style.overflow = "hidden";

        if (!reduce) {
            veil.animate([{ opacity: 0 }, { opacity: 1 }], { duration: 240, easing: "ease-out" });
            big.animate(
                [frameAt(from, to), { transform: "none", borderRadius: "14px" }],
                { duration: 380, easing: "cubic-bezier(0.2, 0.8, 0.3, 1)" }
            );
        }
    }

    // the transform that maps the final position back onto the thumbnail
    function frameAt(from, to) {
        return {
            transform: "translate(" + (from.left - to.left) + "px," + (from.top - to.top) + "px) " +
                       "scale(" + from.width / to.width + "," + from.height / to.height + ")",
            transformOrigin: "top left",
            borderRadius: "40px"
        };
    }

    function close(instant) {
        if (!veil) return;
        const v = veil;
        veil = null;
        document.body.style.overflow = "";
        const big = v.querySelector("img");

        if (instant || reduce || !source || !document.contains(source)) {
            v.remove();
            return;
        }

        const from = source.getBoundingClientRect();
        const to = big.getBoundingClientRect();
        v.animate([{ opacity: 1 }, { opacity: 0 }], { duration: 300, easing: "ease-in", fill: "forwards" });
        big.animate(
            [{ transform: "none" }, frameAt(from, to)],
            { duration: 320, easing: "cubic-bezier(0.4, 0, 0.6, 1)", fill: "forwards" }
        ).onfinish = () => v.remove();
        setTimeout(() => v.remove(), 700); // belt and braces if frames stall
    }

    document.addEventListener("click", (e) => {
        const img = e.target.closest("img.lightboxable");
        if (img) open(img);
    });

    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape") close();
    });
})();
