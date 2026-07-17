// ============================================================
//  Magnetic buttons — the big CTAs lean gently toward the
//  cursor and spring back when it leaves.
// ============================================================
(() => {
    const forceMotion = new URLSearchParams(location.search).has("motion");
    const reduce = !forceMotion && window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    if (reduce || !matchMedia("(pointer: fine)").matches) return;

    document.querySelectorAll(".btn-bloom, .ff-cta, .btn-find").forEach((btn) => {
        btn.style.willChange = "transform";

        btn.addEventListener("mousemove", (e) => {
            const r = btn.getBoundingClientRect();
            const dx = (e.clientX - r.left - r.width / 2) / (r.width / 2);
            const dy = (e.clientY - r.top - r.height / 2) / (r.height / 2);
            btn.style.transition = "transform 0.1s";
            btn.style.transform = "translate(" + dx * 5 + "px," + dy * 4 + "px)";
        });

        btn.addEventListener("mouseleave", () => {
            btn.style.transition = "transform 0.45s cubic-bezier(0.2, 0.8, 0.3, 1.4)";
            btn.style.transform = "";
        });
    });
})();
