window.addEventListener('DOMContentLoaded', event => {
    // Simple-DataTables
    // https://github.com/fiduswriter/Simple-DataTables/wiki

    const datatablesSimple = document.getElementById('datatablesSimple');
    
    if (datatablesSimple) {
        /* new simpleDatatables.DataTable(datatablesSimple);*/
        const dataTable = new simpleDatatables.DataTable(datatablesSimple, {
            language: {
                search: "Tìm kiếm",  // Thay đổi văn bản tìm kiếm
                zeroRecords: "Không có dữ liệu",  // Thay đổi thông báo khi không có kết quả
                emptyTable: "Không có dữ liệu trong bảng",  // Thay đổi thông báo khi bảng trống
                infoEmpty: "Không có dữ liệu",  // Thay đổi thông báo khi không có bản ghih
            }
         });
       
        
        
     
    }
});
