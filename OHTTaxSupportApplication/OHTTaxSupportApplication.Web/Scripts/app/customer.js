var urlGetAll = "/api/customers/getall";
var urlGetById = "/api/customers/getbyid/";
var urlDelete = "/api/customers/delete?id=";
var urlCreate = "/api/customers/create";
var urlUpdate = "/api/customers/update";
var urlGetCompany = "/api/companies/getall";
var urlGetCustomerType = "/api/customertypes/getall";
getMasterData();
function getMasterData() {
    ohtCallServiceToGet(urlGetCompany, getCompanySuccess);
    ohtCallServiceToGet(urlGetCustomerType, getCustomerTypeSuccess);
}
function getCompanySuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CompanyName + '</option>';
        }
        $('#oht-Customer-Company').html(options);
    }
}
function getCustomerTypeSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CustomerTypeName + '</option>';
        }
        $('#oht-Customer-Type').html(options);
    }
}

function refreshForm() {
    $('#oht-Customer-Name').val(null);
    $('#oht-Customer-Type').val(null);
    $('#oht-Customer-Company').val(null);
    $('#oht-Customer-Addess').val(null);
    $('#oht-Customer-Phone').val(null);
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
                    '<td>' + element.CustomerName + '</td>' +
                    '<td>' + element.CustomerType + '</td>' +
                    '<td>' + element.Company + '</td>' +
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
            $('#oht-Customer-ID').val(response.ID);
            $('#oht-Customer-Name').val(response.CustomerName);
            $('#oht-Customer-Type').val(response.CustomerTypeID);
            $('#oht-Customer-Company').val(response.CompanyID);
            $('#oht-Customer-Address').val(response.Address);
            $('#oht-Customer-Phone').val(response.PhoneNumber);

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
    var id = $('#oht-Customer-ID').val();
    var value = $('#oht-Customer-Name').val();
    var ctype = $('#oht-Customer-Type').val();
    var company = $('#oht-Customer-Company').val();
    var address = $('#oht-Customer-Address').val();
    var phone = $('#oht-Customer-Phone').val();
    if (value == null || value.trim().length <= 0) {
        ohtToastMessage("error", "Customer Name can not be null.");
        $('#oht-Customer-Name').focus();
        return;
    }
    if (ctype == null || ctype.trim().length <= 0) {
        ohtToastMessage("error", "Customer type can not be null.");
        $('#oht-Customer-Type').focus();
        return;
    }
    if (company == null || company.trim().length <= 0) {
        ohtToastMessage("error", "Company can not be null.");
        $('#oht-Customer-Company').focus();
        return;
    }
    var obj = {
        ID: id,
        CustomerName: value,
        CustomerTypeID: ctype,
        CompanyID: company,
        Address: address,
        PhoneNumber: phone,
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
        var id = $('#oht-Customer-ID').val();
        if (id == null || id.trim().length <= 0) {
            $('#oht-Customer-Name').val(null);
            $('#oht-Customer-Type').val(null);
            $('#oht-Customer-Company').val(null);
            $('#oht-Customer-Address').val(null);
            $('#oht-Customer-Phone').val(null);
        }
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function cancel() {
    $('#oht-Customer-ID').val(null);
    refreshForm();
    $('.oht-edit-mode').hide();
    $('.oht-new-mode').show();
    $('#oht-panel-form').toggleClass("widget-color-orange");
} 