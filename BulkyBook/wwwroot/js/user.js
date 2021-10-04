var dataTable;

$(document).ready(function () {
    loadTable();
});

function loadTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd"},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
                        <div class="text-center">
                            <a onclick=LockOrUnlock('${data.id}') class="btn btn-success" style="cursor:pointer;width:140px;">
                                Unlock this user
                            </a>
                        </div>
                        `;
                    }
                    else {
                        return `
                        <div class="text-center">
                            <a onclick=LockOrUnlock('${data.id}') class="btn btn-danger" style="cursor:pointer;width:140px;">
                                Lock this user
                            </a>
                        </div>
                        `;
                    }
                },
                "width": "25%"
            }
        ],
    });
}

function LockOrUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/User/LockOrUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                $('#tblData').dataTable().api().ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}





