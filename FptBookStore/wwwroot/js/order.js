var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadTable("GetOrderList?status=inprocess");
    }
    else {
        if (url.includes("pending")) {
            loadTable("GetOrderList?status=pending");
        }
        else {
            if (url.includes("compeleted")) {
                loadTable("GetOrderList?status=compeleted");
            }
            else {
                if (url.includes("rejected")) {
                    loadTable("GetOrderList?status=rejected");
                }
                else {
                    loadTable("GetOrderList?status=all");
                }
            }
        }
    }
});

function loadTable(url) {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "type": "GET",
            "url": "/Admin/Order/" + url
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Order/Detail/${data}" class="btn btn-success" style="cursor:pointer;">
                                Detail
                            </a>
                        </div>
                    `;
                },
                "width": "5%"
            }
        ],
    });
}
