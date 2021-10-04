var dataTable;

$(document).ready(function () {
    loadTable();
});

function loadTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "75%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Upsert/${data}" class="btn btn-success" style="cursor:pointer;">
                                Edit
                            </a>

                            <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger" style="cursor:pointer;">
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





