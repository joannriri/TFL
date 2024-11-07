document.addEventListener("DOMContentLoaded", function () {
    const deliveryBoxes = document.querySelectorAll(".delivery-select.box");
    const shippingDetails = document.querySelector(".shipping-details");
    const storePickupDetails = document.querySelector(".store-pickup-details");

    const paymentBoxes = document.querySelectorAll(".payment-select.box");
    const bankDetails = document.querySelector(".bank-details");
    const cashDetails = document.querySelector(".cash-details");

    // Helper function to manage visibility of elements
    function toggleVisibility(elementToShow, elementToHide) {
        elementToShow.classList.remove("hidden");
        elementToHide.classList.add("hidden");
    }

    // Delivery Selection
    deliveryBoxes.forEach(box => {
        box.addEventListener("click", function () {
            // Set active state for selected delivery option
            deliveryBoxes.forEach(b => b.classList.remove("active"));
            box.classList.add("active");

            // Toggle visibility based on selected delivery method
            if (box.getAttribute("data-value") === "shipping") {
                toggleVisibility(shippingDetails, storePickupDetails);
            } else if (box.getAttribute("data-value") === "store-pickup") {
                toggleVisibility(storePickupDetails, shippingDetails);
            }
        });
    });

    // Payment Selection
    paymentBoxes.forEach(box => {
        box.addEventListener("click", function () {
            // Set active state for selected payment option
            paymentBoxes.forEach(b => b.classList.remove("active"));
            box.classList.add("active");

            // Toggle visibility based on selected payment method
            if (box.classList.contains('bank-transfer')) {
                toggleVisibility(bankDetails, cashDetails);
            } else if (box.classList.contains('cash-pay')) {
                toggleVisibility(cashDetails, bankDetails);
            }
        });
    });
});
