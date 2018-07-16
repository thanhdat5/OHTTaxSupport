var urlGetAll = "/api/accounts/getall";
var urlGetById = "/api/accounts/getbyid/";
var urlDelete = "/api/accounts/delete?id=";
var urlCreate = "/api/accounts/create";
var urlUpdate = "/api/accounts/update";
var urlGetCategory = "/api/Categories/getall";
var urlGetTaxValue = "/api/taxvalues/getall";
getMasterData();
function getMasterData() {
    ohtCallServiceToGet(urlGetCategory, getCategorySuccess);
    ohtCallServiceToGet(urlGetTaxValue, getTaxValueSuccess);
}
function getCategorySuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CategoryName + '</option>';
        }
        $('#oht-Account-Category').html(options);
    }
}
function getTaxValueSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].Value + '</option>';
        }
        $('#oht-Account-TaxValue').html(options);
    }
}

function refreshForm() {
    $('#oht-Account-Code').val(null);
    $('#oht-Account-Category').val(null);
    $('#oht-Account-TaxValue').val(null);
    $('#oht-Account-SH').val("S");
}

function refreshData() {
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="4">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="4">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            $.each(response, function (index, element) {
                innerHtml +=
                    '<tr>' +
                    '<td>' + element.ID + '</td>' +
                    '<td>' + element.AccountCode + '</td>' +
                    '<td>' + element.SH + '</td>' +
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
            $('#oht-Account-ID').val(response.ID);
            $('#oht-Account-Code').val(response.AccountCode);
            $('#oht-Account-Category').val(response.CategoryID);
            $('#oht-Account-TaxValue').val(response.TaxValueID);
            $('#oht-Account-SH').val(response.SH);

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
    var id = $('#oht-Account-ID').val();
    var value = $('#oht-Account-Code').val();
    var Category = $('#oht-Account-Category').val();
    var taxValue = $('#oht-Account-TaxValue').val();
    var sh = $('#oht-Account-SH').val();
    if (value == null || value.trim().length <= 0) {
        ohtToastMessage("error", "Account Code can not be null.");
        $('#oht-Account-Code').focus();
        return;
    }
    if (Category == null || Category.trim().length <= 0) {
        ohtToastMessage("error", "Category can not be null.");
        $('#oht-Account-Category').focus();
        return;
    }
    if (taxValue == null || taxValue.trim().length <= 0) {
        ohtToastMessage("error", "Tax Value can not be null.");
        $('#oht-Account-TaxValue').focus();
        return;
    }
    var obj = {
        ID: id,
        AccountCode: value,
        CategoryID: Category,
        TaxValueID: taxValue,
        SH: sh,
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
        var id = $('#oht-Account-ID').val();
        if (id == null || id.trim().length <= 0) {
            $('#oht-Account-Code').val(null);
            $('#oht-Account-TaxValue').val(null);
            $('#oht-Account-Category').val(null);
            $('#oht-Account-SH').val(null);
        }
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function cancel() {
    $('#oht-Account-ID').val(null);
    refreshForm();
    $('.oht-edit-mode').hide();
    $('.oht-new-mode').show();
    $('#oht-panel-form').toggleClass("widget-color-orange");
} 