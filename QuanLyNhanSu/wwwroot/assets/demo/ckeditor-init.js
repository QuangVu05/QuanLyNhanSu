document.addEventListener("DOMContentLoaded", function () {
    // Kiểm tra xem phần tử có id "noidung" có tồn tại không
    if (document.getElementById("noidung")) {
        CKEDITOR.replace("noidung");
    }
});