﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/Company/getall'},
        "columns": [
        { data: 'name', "width": "25%" },
            { data: 'streetaddress', "width": "25%" },
            { data: 'city', "width": "15%" },
            { data: 'state', "width": "10%" },
            { data: 'phonenumber', "width": "10%" },
            { data: 'postalcode', "width": "10%" },
        { data: 'id',
         "render": function (data) {
                     return ` <div class="w-75 btn-group" role="group">
                                    <a href="/admin/Company/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                                    <a onClick=Delete('/admin/Company/delete/${data}') class="btn btn-danger mx-2"><i class="bi bi-trash"></i>Delete</a>
                                    </div>`
            },
            "width": "25%"
            }
    ]
});
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    dataTable.ajax.reload();
                    toaster.success(data.message)
                }
            })
        }
    });
}

