$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable =  $('#dataTable').DataTable({
       "ajax": '/admin/company/getall',
        "columns": [
            {data: 'id', "width": "10%"},
           { data: 'name', width: '10%' },
           { data: 'city', width: '10%' },
           { data: 'country', width: '10%' },
           { data: 'phone', width: '10%' },
            {
                data: 'id', width: "20%", "render": function (data) {
                    return `<div class="d-flex gap-2 align-items-center justify-content-center">
                        <a href="/Admin/Company/Upsert/${data}" class="btn btn-primary p-1 text-white" style="cursor:pointer; width:70px;"> <i class="bi bi-pencil-fill"></i> Edit</a>
                        <a class="btn btn-danger p-1 text-white delete-btn" style="cursor:pointer;"  data-bs-target="#exampleModal" data-id=${data}> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                }
            }
        ]
        ,
        autoWidth: false, 
    });
}
