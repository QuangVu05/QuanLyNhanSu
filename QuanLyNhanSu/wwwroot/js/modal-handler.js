function showModal(url, title) {
    $.get(url, function (data) {
        $("#modalTitle").text(title);
        $("#modalContent").html(data);
        $("#modalForm").modal("show");
    });
}

$(document).on("submit", "form[data-ajax='true']", function (e) {
    e.preventDefault();
    var form = $(this);
    var url = form.attr("action");

    $.post(url, form.serialize(), function (response) {
        if (response.success) {
            showAlert("success", response.successMessage);
            $("#modalForm").modal("hide");
            setTimeout(() => location.reload(), 2000);
        } else {
            if (response.errorMessage) {
                showAlert("danger", response.errorMessage);
            }
            $("#modalContent").html(response);
        }
    });
});

/*function showModal(url, title) {
    $.get(url, function (data) {
        $('#modalTitle').text(title);
        $('#modalContent').html(data);
        $('#modalForm').modal('show'); 
        if ($.validator && $.validator.unobtrusive) {
            $.validator.unobtrusive.parse($('#modalForm'));
        }
    });
}

function submitForm(formId, url) {
    $.post(url, $('#' + formId).serialize(), function (data) {
        let formData = $('#' + formId).serialize();
        console.log("Dữ liệu gửi đi:", formData); // Kiểm tra dữ liệu trước khi gửi
        if (data.success) {
            showAlert('success', data.message);

            // Ẩn modal và reload sau 2 giây
            $('#modalForm').modal('hide');  
            setTimeout(() => location.reload(), 2000); // Giảm từ 5000ms -> 2000ms để UX tốt hơn
        } else {
            $('#modalContent').html(data); 
        }
    }).fail(function (xhr) {
        $('#modalContent').html(xhr.responseText); // Xử lý lỗi nếu có
    });
}*/