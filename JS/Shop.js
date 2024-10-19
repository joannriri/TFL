// JavaScript to toggle filter and sort section
document.getElementById("filter-toggle").onclick = function () {
    var filterSortDiv = document.getElementById("filter-sort");
    if (filterSortDiv.style.display === "none") {
        filterSortDiv.style.display = "block"; // Show
    } else {
        filterSortDiv.style.display = "none"; // Hide
    }
};
