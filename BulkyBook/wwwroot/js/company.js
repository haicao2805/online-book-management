var dataTable;

$(document).ready(function () {
    loadTable();
});

function loadTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "11%" },
            { "data": "streetAddress", "width": "11%" },
            { "data": "city", "width": "11%" },
            { "data": "state", "width": "11%" },
            { "data": "postalCode", "width": "11%" },
            { "data": "phoneNumber", "width": "11%" },
            {
                "data": "isAuthorized",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`;
                    }
                    else {
                        return `<input type="checkbox" disabled />`;
                    }
                },
                "width": "9%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Company/Upsert/${data}" class="btn btn-success" style="cursor:pointer;">
                                Edit
                            </a>

                            <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger" style="cursor:pointer;">
                                Delete
                            </a>
                        </div>
                    `;
                },
                "width": "25%"
            }
        ],
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    $('#tblData').dataTable().api().ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        })
    });
}





