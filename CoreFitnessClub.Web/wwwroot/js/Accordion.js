document.addEventListener("DOMContentLoaded", () => {
    const accordionBoxes = document.querySelectorAll(".accordion-box");

    accordionBoxes.forEach(box => {
        box.addEventListener("click", () => {
            const valueNumber = box.getAttribute("data-value");
            const correspondingDescription = document.querySelector(
                `.accordion-descriptions[data-value="${valueNumber}"]`
            );
            const isCurrentlyActive = box.classList.contains("active");

            accordionBoxes.forEach(otherBox => {
                const otherValueNumber = otherBox.getAttribute("data-value");
                const otherDescription = document.querySelector(
                    `.accordion-descriptions[data-value="${otherValueNumber}"]`
                );

                otherBox.classList.remove("active");

                if (otherDescription) {
                    otherDescription.classList.remove("active");
                }
            });

            if (!isCurrentlyActive && correspondingDescription) {
                box.classList.add("active");
                correspondingDescription.classList.add("active");
            }
        });
    });
});