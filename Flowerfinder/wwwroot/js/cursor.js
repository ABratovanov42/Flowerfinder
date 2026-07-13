// ============================================================
//  Flower cursor toggle — the head script applies the saved
//  choice before first paint; this button flips and stores it.
// ============================================================
(() => {
    const btn = document.getElementById("ffCursor");
    if (!btn) return;
    const root = document.documentElement;

    const sync = () =>
        btn.setAttribute("aria-pressed", root.classList.contains("flower-cursor") ? "true" : "false");
    sync();

    btn.addEventListener("click", () => {
        const on = root.classList.toggle("flower-cursor");
        try { localStorage.setItem("ffCursor", on ? "on" : "off"); } catch (e) { }
        sync();
    });
})();
