// ============================================================
//  My garden — the daily check-in list (saves ticks to the
//  server) and the watering can that rains over the bed.
// ============================================================
(() => {
    const forceMotion = new URLSearchParams(location.search).has("motion");
    const reduce = !forceMotion && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    // ---- check-in list -------------------------------------------------
    const list = document.getElementById("careList");
    if (list) {
        const token = list.querySelector('input[name="__RequestVerificationToken"]').value;
        const countEl = document.getElementById("careCount");

        const refreshCount = () => {
            const open = list.querySelectorAll('input[type="checkbox"]:not(:checked)').length;
            countEl.textContent = open === 0 ? "All tended 🌿" : open + " to do";
            if (open === 0 && !reduce) {
                countEl.animate(
                    [{ transform: "scale(1)" }, { transform: "scale(1.25)" }, { transform: "scale(1)" }],
                    { duration: 450, easing: "cubic-bezier(0.2, 0.8, 0.3, 1.4)" });
            }
        };

        list.querySelectorAll('input[type="checkbox"]').forEach((box) => {
            box.addEventListener("change", async () => {
                const row = box.closest(".care-row");
                box.disabled = true;
                try {
                    const data = new FormData();
                    data.append("flowerId", box.dataset.flower);
                    data.append("task", box.dataset.task);
                    data.append("__RequestVerificationToken", token);
                    const res = await fetch("/Flowers/CheckCare", { method: "POST", body: data });
                    if (!res.ok) throw new Error();
                    const { done } = await res.json();
                    box.checked = done;
                    row.classList.toggle("done", done);
                    refreshCount();
                    if (done && !reduce) dropletBurst(row.querySelector(".care-check"));
                } catch {
                    box.checked = !box.checked; // server didn't hear us — undo
                } finally {
                    box.disabled = false;
                }
            });
        });
    }

    // a couple of droplets fall from a freshly ticked box
    function dropletBurst(from) {
        const r = from.getBoundingClientRect();
        for (let i = 0; i < 4; i++) {
            const d = document.createElement("i");
            d.className = "droplet";
            d.style.left = r.left + r.width / 2 + (Math.random() * 18 - 9) + "px";
            d.style.top = r.top + r.height / 2 + "px";
            document.body.appendChild(d);
            d.animate(
                [
                    { transform: "translateY(0) scale(1)", opacity: 0.9 },
                    { transform: "translateY(" + (26 + Math.random() * 22) + "px) scale(0.5)", opacity: 0 }
                ],
                { duration: 480 + Math.random() * 200, easing: "cubic-bezier(0.4, 0, 0.6, 1)", delay: i * 60 }
            ).onfinish = () => d.remove();
        }
    }

    // ---- the watering can ----------------------------------------------
    const can = document.getElementById("waterCan");
    const bed = document.getElementById("bedWrap");
    if (can && bed) {
        let watering = false;
        can.addEventListener("click", () => {
            if (watering || reduce) return;
            watering = true;
            can.classList.add("pouring");

            const bedRect = bed.getBoundingClientRect();
            // rain: droplets fall from just above the bed onto it
            for (let i = 0; i < 26; i++) {
                const d = document.createElement("i");
                d.className = "droplet rain";
                d.style.left = bedRect.left + Math.random() * bedRect.width + "px";
                d.style.top = bedRect.top - 20 + "px";
                document.body.appendChild(d);
                d.animate(
                    [
                        { transform: "translateY(0)", opacity: 0 },
                        { opacity: 0.85, offset: 0.15 },
                        { transform: "translateY(" + (60 + Math.random() * (bedRect.height - 60)) + "px)", opacity: 0 }
                    ],
                    { duration: 700 + Math.random() * 500, easing: "cubic-bezier(0.4, 0, 0.7, 1)", delay: Math.random() * 900 }
                ).onfinish = () => d.remove();
            }

            // the flowers give a grateful little wiggle
            bed.querySelectorAll(".bed-card").forEach((card, i) => {
                card.animate(
                    [
                        { transform: "rotate(0deg)" },
                        { transform: "rotate(1.6deg)" },
                        { transform: "rotate(-1.2deg)" },
                        { transform: "rotate(0.6deg)" },
                        { transform: "rotate(0deg)" }
                    ],
                    { duration: 900, delay: 350 + i * 120, easing: "ease-in-out", composite: "add" }
                );
            });

            setTimeout(() => {
                can.classList.remove("pouring");
                watering = false;
            }, 2100);
        });
    }
})();
