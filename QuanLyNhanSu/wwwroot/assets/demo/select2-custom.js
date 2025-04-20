// select2-custom.js

// Hàm khởi tạo Select2 cho các phần tử <select> với id truyền vào
function initializeSelect2(selectId, options = {}) {
    $(selectId).select2({
        placeholder: options.placeholder || "Chọn giá trị", // Đặt placeholder khi chưa chọn
        allowClear: options.allowClear !== undefined ? options.allowClear : true,  // Cho phép xóa lựa chọn
        width: options.width || '100%',    // Chiều rộng của dropdown
        dropdownAutoWidth: options.dropdownAutoWidth !== undefined ? options.dropdownAutoWidth : true, // Tự động điều chỉnh chiều rộng dropdown
        maximumSelectionLength: options.maximumSelectionLength || 10,  // Số lượng tối đa các mục có thể chọn
        dropdownCssClass: options.dropdownCssClass || "bigdrop"  // Tạo lớp CSS để chỉnh kiểu thanh cuộn
    });
    $(selectId).next('.select2-container').find('.select2-selection--single').css({
        'border': 'none',
        'box-shadow': 'none'
    });
}

// Hàm khởi tạo select2 cho các phần tử khi document sẵn sàng
$(document).ready(function () {
    $('select').each(function () {
        let selectId = '#' + $(this).attr('id'); // Lấy ID của select
        let hideSearch = $(this).data('hide-search') === true; // Kiểm tra có ẩn thanh tìm kiếm không

        initializeSelect2(selectId, {
            placeholder: "Chọn giá trị",
            multiple: false,
           dropdownCssClass: "custom-drop",
            minimumResultsForSearch: hideSearch ? Infinity : 0 // Ẩn tìm kiếm nếu có data-hide-search
        });

        // Thêm class "form-control" cho select2 container
       $(this).next('.select2-container').addClass('form-control');
    });

   
});
