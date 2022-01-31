var dataTable;

jQuery(document).ready(function () {
    loadDataTable;

})

function loadDataTable() {

    dataTable = jQuery('#tblData').dataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "displayOrder", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                           <div class="text-center">
            
                             <a href="/Admin/category/Upsert/${data}" class='btn btn-success text-while' style='curser:pointer,width:100px;'>

                             <i class="far fa-edit"></i> Edit</a> &nbsp;

                          <a onClick=Delete("/Admin/category/Delete/${data}") class='btn btn-success text-while' style='curser:pointer,width:100px;'>

                             <i class="far fa-trash-alt"></i> Delete</a>
                             </div>

                                `;


                },
                "width": "30%"
            }

        ],
        "language" :{
            "emptyTable":"No records found"
        },
        "width":"100%"
    })
}