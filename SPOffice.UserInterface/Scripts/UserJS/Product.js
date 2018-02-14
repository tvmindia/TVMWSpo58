var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {

        DataTables.ProductTable = $('#productTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProducts(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
                  { "data": "ID", "defaultContent": "<i>-</i>" },
                    { "data": "Code", "defaultContent": "<i>-</i>" },
                      { "data": "OldCode", "defaultContent": "<i>-</i>" },
               { "data": "Name", "defaultContent": "<i>-</i>" },
                  { "data": "Description", "defaultContent": "<i>-</i>" },
                   { "data": "unit.Description", "defaultContent": "<i>-</i>" },
                    { "data": "Rate",render: function (data, type, row) { return roundoff(data, 1); }, "defaultContent": "<i>-</i>" },
               { "data": "commonObj.CreatedDateString", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },

               { className: "text-left", "targets": [1,2,3,4,5,7] },
             { className: "text-center", "targets": [] },
             { className: "text-right", "targets": [6] }

             ]
         });

        $('#ProductTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetAllProducts() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Product/GetAllProducts/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {

            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function openNav(id) {
    var left = $(".main-sidebar").width();
    var total = $(document).width();

    $('.main').fadeOut();
    document.getElementById("myNav").style.left = "3%";
    $('#main').fadeOut();

    if ($("body").hasClass("sidebar-collapse")) {

    }
    else {
        $(".sidebar-toggle").trigger("click");
    }
    if (id != "0") {
        ClearFields();
       
    }

}

function goBack() {
    ClearFields();
    closeNav();
    BindAllProducts();
}

function Save() {

    try {
        //    $('#EmployeeType').val("EMP");
        $("#btnInsertUpdateProduct").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}

function Delete() {
    notyConfirm('Are you sure to delete?', 'DeleteProduct()', '', "Yes, delete it!");
}

function DeleteProduct() {
    try {
        var id = $('#ID').val();
        if (id != '' && id != null) {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Product/DeleteProduct/", data);

            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                notyAlert('success', ds.Message.Message);
                goBack();

            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.Message);
                return 0;
            }
            return 1;
        }

    }
    catch (e) {
        notyAlert('error', e.message);
        return 0;
    }
}

function ClearFields() {

    $("#Code").val("");
    $("#Name").val("");
    $("#ID").val('');
    $("#Code").val("");
    $("#OldCode").val("");
    $("#Name").val("");
    $("#Description").val('');
    $("#UnitCode").val('');
    $("#Rate").val('');

    ResetForm();
    ChangeButtonPatchView("Product", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
}

//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {

    var validator = $("#ProductForm").validate();
    $('#ProductForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}

function BindAllProducts() {
    try {
        DataTables.ProductTable.clear().rows.add(GetAllProducts()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            BindAllProducts();
            notyAlert('success', JsonResult.Record.Message);
            if (JsonResult.Record.ID)
            {
                $("#ID").val(JsonResult.Record.ID);
            }
        
          
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}

function Reset() {
    if ($("#ID").val() == "") {
        ClearFields();
    }
    else {
        FillProductDetails($("#ID").val());
    }
    ResetForm();
}

function GetProductByID(id) {
    try {

        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Product/GetProductDetails/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);

        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

//---------------------------------------Fill Customer Details--------------------------------------------------//
function FillProductDetails(id) {

    ChangeButtonPatchView("Product", "btnPatchAdd", "Edit"); //ControllerName,id of the container div,Name of the action
    var thisItem = GetProductByID(id); //Binding Data
    //Hidden
    $("#ID").val(thisItem.ID);
    $("#Code").val(thisItem.Code);
    $("#OldCode").val(thisItem.OldCode);
    $("#Name").val(thisItem.Name);
    $("#Description").val(thisItem.Description);
    $("#UnitCode").val(thisItem.UnitCode);
    $("#Rate").val(thisItem.Rate);

}

//---------------------------------------Edit Bank--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    openNav("0");
    ResetForm();
    var rowData = DataTables.ProductTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        FillProductDetails(rowData.ID);
    }
}