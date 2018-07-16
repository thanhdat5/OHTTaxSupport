var urlGetAll = "/api/products/getall";
var urlGetById = "/api/products/getbyid/";
var urlDelete = "/api/products/delete?id=";
var urlCreate = "/api/products/create";
var urlUpdate = "/api/products/update";
var urlGetUnit = "/api/units/getall";
getMasterData();
function getMasterData() {
    ohtCallServiceToGet(urlGetUnit, getMasterDataSuccess);
}
function getMasterDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].Value + '</option>';
        }
        $('#oht-Product-Unit').html(options);
        $('#oht-Product-Unit2').html('<option value="null"></option>' + options);
    }
}

function refreshForm() {
    $('#oht-Product-Name').val(null);
    $('#oht-Product-Description').val(null);
    $('#oht-Product-Unit').val(null);
    $('#oht-Product-Unit2').val(null);
}

function refreshData() {
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="5">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="5">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            $.each(response, function (index, element) {
                innerHtml +=
                    '<tr>' +
                    '<td>' + element.ID + '</td>' +
                    '<td>' + element.ProductName + '</td>' +
                    '<td>' + element.UnitName + '</td>' +
                    '<td>' + element.Unit2Name + '</td>' +
                    '<td>' +
                    '<button class="btn btn-primary btn-xs" id="edit-1" onclick="editItem(' + element.ID + ')">' +
                    '<span class="fa fa-edit"></span>' +
                    '</button>&nbsp;' +
                    '<button class="btn btn-danger btn-xs" id="delete-1" onclick="deleteItem(' + element.ID + ')">' +
                    '<span class="fa fa-trash"></span>' +
                    '</button>' +
                    '</td>' +
                    '</tr>';
            });
            $('#oht-result-data').html(innerHtml);
        }
    }
}

function editItem(id) {
    ohtCallServiceToGet(urlGetById + id, loadItemSuccess);
}
function loadItemSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null) {
            ohtShowCustomError('Item can not be found.');
        } else {
            // Set value
            $('#oht-Product-ID').val(response.ID);
            $('#oht-Product-Name').val(response.ProductName);
            $('#oht-Product-Description').val(response.Description);
            $('#oht-Product-Unit').val(response.UnitID);
            $('#oht-Product-Unit2').val(response.UnitID2);

            $('.oht-new-mode').hide();
            $('.oht-edit-mode').show();
            $('.oht-key').attr("disabled", "disabled");

            $('#oht-panel-form').toggleClass("widget-color-orange");
        }
    }
}

function deleteItem(id) {
    if (confirm("Are you want to delete this item?")) {
        // Delete item
        ohtCallServiceToDelete(urlDelete + id, deleteItemSuccess);

    }
    return false;
}
function deleteItemSuccess(data) {
    $('*').css('cursor', 'default');
    cancel();
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function saveItem() {
    var id = $('#oht-Product-ID').val();
    var value = $('#oht-Product-Name').val();
    var description = $('#oht-Product-Description').val();
    var unitID = $('#oht-Product-Unit').val();
    var unitID2 = $('#oht-Product-Unit2').val();
    if (value == null || value.trim().length <= 0) {
        ohtToastMessage("error", "Product Name can not be null.");
        $('#oht-Product-Name').focus();
        return;
    }
    if (unitID == null || unitID.trim().length <= 0) {
        ohtToastMessage("error", "Unit can not be null.");
        $('#oht-Product-Unit').focus();
        return;
    }

    var obj = {
        ID: id,
        ProductName: value,
        Description: description,
        UnitID: unitID,
        UnitID2: unitID2,
        IsActive: true
    };
    if (id == null || id.trim().length <= 0) {
        obj.ID = 0;
        ohtCallServiceToAdd(obj, urlCreate, saveItemSuccess);
    } else {
        ohtCallServiceToUpdate(obj, urlUpdate, saveItemSuccess);
    }
}
function saveItemSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var id = $('#oht-Product-ID').val();
        if (id == null || id.trim().length <= 0) {
            $('#oht-Product-Name').val(null);
            $('#oht-Product-Description').val(null);
            $('#oht-Product-Unit').val(null);
            $('#oht-Product-Unit2').val(null);
        }
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function cancel() {
    $('#oht-Product-ID').val(null);
    refreshForm();
    $('.oht-edit-mode').hide();
    $('.oht-new-mode').show();
    $('#oht-panel-form').toggleClass("widget-color-orange");
} 