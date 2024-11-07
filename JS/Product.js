function incrementQuantity() {
    const input = document.querySelector(".quantity-input");
    const formQuantity = document.getElementById("formQuantity");
    let quantity = parseInt(input.value);
    const max = parseInt(input.max) || Infinity; // Default max to Infinity if not set

    if (quantity < max) {
        quantity++;
        input.value = quantity;
        formQuantity.value = quantity;
    }
}

function decrementQuantity() {
    const input = document.querySelector(".quantity-input");
    const formQuantity = document.getElementById("formQuantity");
    let quantity = parseInt(input.value);

    if (quantity > 1) {
        quantity--;
        input.value = quantity;
        formQuantity.value = quantity;
    }
}

function selectVariant(variantId) {
    // Deselect all variants first
    const variants = document.querySelectorAll('.variant-option');
    variants.forEach(variant => {
        variant.classList.remove('selected');
    });

    // Select the clicked variant
    const selectedVariant = document.getElementById(`variant-${variantId}`);
    selectedVariant.classList.add('selected');

    // Update the hidden input field with the selected variant ID
    document.getElementById('selectedVariantId').value = variantId;
}


