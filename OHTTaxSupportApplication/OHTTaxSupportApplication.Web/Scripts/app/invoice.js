var urlGetAll = "/api/Invoices/getall";
var urlGetSearch = "/api/Invoices/filter";
var urlGetById = "/api/Invoices/getbyid/";

function refreshData() {
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="6">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function searchData() {
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (validateFilter(fromDate, toDate)) {
        $('#oht-result-data').html('<tr class="oht-loading"><td colspan="6">Loading...</td></tr>');
        ohtCallServiceToGet(urlGetSearch + "?fromDate=" + fromDate + "&toDate=" + toDate, refreshDataSuccess);
    }
}

function validateFilter(fromDate, toDate) {
    if (isDate(fromDate) && isDate(toDate)) {
        if (new Date(fromDate) > new Date(toDate)) {
            alert("To date can not be less than from date.");
            return false;
        } else {
            return true;
        }
    } else {
        return false;
    }
}

function isDate(str) {
    if (str != null && str.trim().length > 0) {
        var dateValue = new Date(str);
        var day = dateValue.getDate();
        var month = dateValue.getMonth() + 1;
        var year = dateValue.getFullYear();

        var strDay = day < 10 ? "0" + day : day;
        var strMonth = month < 10 ? "0" + month : month;

        var strNewDate = strMonth + "/" + strDay + "/" + year;
        var strNewDate2 = year + "-" + strMonth + "-" + strDay;
        if (strNewDate != str && strNewDate2 != str) {
            alert("Date invalid");
            return false;
        }
    }
    return true;
}

$('#btnReset').click(function () {
    $('#txtFromDate').val("");
    $('#txtToDate').val("");
    refreshData();
});
$('#btnSearch').click(function () {
    searchData();
});
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="7">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            var currentId = 0;
            $.each(response, function (index, element) {
                if (element.ID != currentId) {
                    innerHtml += "<tr><td style='background: #d4eaff;' colspan='7'><b>Date created: " + element.CreateDate + "</b>";
                    //innerHtml += '<button class="btn btn-default btn-xs pull-right" id="view-1" onclick="viewItem(' + element.ID + ')">' +
                    //    '<span class="fa fa-eye"></span>' +
                    //    '</button></td></tr>';
                    innerHtml += '</td></tr>';
                    currentId = element.ID;
                }
                innerHtml +=
                    '<tr>' +
                    '<td>#000' + element.ID + '</td>' +
                    '<td>' + element.CreateDate + '</td>' +
                    '<td>' + (element.InOut == true ? "In" : "Out") + '</td>' +
                    '<td>' + element.Customer + '</td>' +
                    '<td>' + element.Value + '</td>' +
                    '<td>' + element.TaxValue + '%</td>' +
                    '<td></td>' +
                    '</tr>';
            });
            $('#oht-result-data').html(innerHtml);
        }
    }
}

function viewItem(id) {
    ohtCallServiceToGet(urlGetById + id, loadItemSuccess);
}
$('#btnReport').click(function () {
    //var fromDate = $('#txtFromDate').val();
    //var toDate = $('#txtToDate').val();
    //$('#lblFromDate').text(fromDate);
    //$('#lblToDate').text(toDate);
    //if (validateFilter(fromDate, toDate)) {
    //    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="7">Loading...</td></tr>');
    //    ohtCallServiceToGet(urlGetSearch + "?fromDate=" + fromDate + "&toDate=" + toDate, exportData);
    //}
});

function exportData(data) {
    searchData();
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#report-content').html('<p>No record found.</p>');
            $('#reportModal').modal('show');
        } else {
            var rs = "";
            var cpht = 0;
            var dtht = 0;

            var categories = [];
            var dttn19 = 0;
            var dttn0 = 0;
            var dtnn7 = 0;

            for (var i = 0; i < response.length; i++) {
                var item = response[i];
                item.Value = (item.Value == null || item.Value == "") ? 0 : item.Value.replace(',', '');
                if (item.InOut == false) {
                    cpht += (parseFloat(item.Value));
                } else {
                    if (item.TaxValue == "0") {
                        dttn0 += (parseFloat(item.Value));
                    }
                    if (item.TaxValue == "19") {
                        dttn19 += (parseFloat(item.Value));
                    }
                    if (item.TaxValue == "7") {
                        dtnn7 += (parseFloat(item.Value));
                    }
                    dtht += (parseFloat(item.Value));
                }
            }
            rs += '<tr><th>Chi phí hàng tháng</th><td>' + cpht + ' € (Total)</td></tr>';

            rs += '<tr><th>Doanh thu</th><td>' + dtht + ' € (Total)</td></tr><tr><td class="text-right">Trong nước 19%</td><td>' + dttn19 + ' €</td></tr><tr><td class="text-right">Trong nước 0%</td><td>' + dttn0 + ' €</td></tr><tr><td class="text-right">Nước ngoài</td><td>' + dtnn7 + ' €</td></tr>';

            rs += '<tr><th>ket qua dong (vari.)</th><td>' + (cpht - dtht) + ' €</td></tr>';
            debugger
            $('#report-content').html(rs);
            $('#reportModal').modal('show');
        }
    }
}