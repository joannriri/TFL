let currentSlide = 0;

function showSlide(index) {
    const slides = document.querySelectorAll('.carousel-item');
    const indicators = document.querySelectorAll('.carousel-indicators button');

    // Remove active class from all slides
    slides.forEach((slide) => {
        slide.classList.remove('active');
    });

    // Calculate current slide index
    currentSlide = (index + slides.length) % slides.length; // Loop back if out of bounds
    slides[currentSlide].classList.add('active'); // Set the current slide to active

    // Update indicators
    indicators.forEach((indicator, idx) => {
        indicator.classList.toggle('active', idx === currentSlide);
    });
}

function nextSlide() {
    showSlide(currentSlide + 1);
}

function prevSlide() {
    showSlide(currentSlide - 1);
}

function goToSlide(index) {
    showSlide(index);
}
