var urlGetAll = "/api/Invoices/getall";
var urlGetById = "/api/Invoices/getbyid/";

function refreshData() {
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="3">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="6">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            $.each(response, function (index, element) {
                innerHtml +=
                    '<tr>' +
                    '<td>#000' + element.ID + '</td>' +
                    '<td>' + element.CreateDate + '</td>' +
                    '<td>' + element.Customer + '</td>' +
                    '<td>' + element.Value + '</td>' +
                    '<td>' + element.TaxValue + '%</td>' +
                    '<td>' +
                    '<button class="btn btn-primary btn-xs" id="view-1" onclick="viewItem(' + element.ID + ')">' +
                    '<span class="fa fa-eye"></span>' +
                    '</button>' +
                    '</td>' +
                    '</tr>';
            });
            $('#oht-result-data').html(innerHtml);
        }
    }
}

function viewItem(id) {
    ohtCallServiceToGet(urlGetById + id, loadItemSuccess);
}