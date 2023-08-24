function previewImage(selector, elem) {
    var preview = document.querySelector(selector);
    var file = elem.files[0];
    var reader = new FileReader();

    reader.addEventListener("load",
        function () {
            //preview.src = reader.result;
            preview.style.backgroundImage = 'url(' + reader.result + ')';
        },
        false);

    if (file) {
        reader.readAsDataURL(file);
    }
}