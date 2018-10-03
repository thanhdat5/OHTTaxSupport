var urlGetSearch = "/api/Invoices/filter";

$('#btnSearch').click(function () {
    searchData();
});
function searchData() {
    var fromDate = $('#txtFromDate').val();
    var toDate = $('#txtToDate').val();
    if (
        (fromDate == null || fromDate == "" || fromDate.toString() == "invaid date")
        && (toDate == null || toDate == "" || toDate.toString() == "invaid date")
    ) {
        alert('At least one date field selected.');
        return;
    }
    if (validateFilter(fromDate, toDate)) {
        $('#report-content').html("<tr><td colspan='2'>Loading...</td></tr>");
        ohtCallServiceToGet(urlGetSearch + "?fromDate=" + fromDate + "&toDate=" + toDate, searchDataSuccess);
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

function formatDate(str) {
    if (str != null && str.trim().length > 0) {
        var dateValue = new Date(str);
        var day = dateValue.getDate();
        var month = dateValue.getMonth() + 1;
        var year = dateValue.getFullYear();

        var strDay = day < 10 ? "0" + day : day;
        var strMonth = month < 10 ? "0" + month : month;

        var strNewDate = strMonth + "/" + strDay + "/" + year;
        var strNewDate2 = year + "-" + strMonth + "-" + strDay;
        return strNewDate;
    }
    return str;
}

$('#btnReset').click(function () {
    $('#txtFromDate').val("");
    $('#txtToDate').val("");
    $('#report-content').html("");
    $('#chart-content').html("");
});

function searchDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#report-content').html('<p style="padding:10px;">No record found.</p>');
            $('#reportModal').modal('show');
        } else {
            var rs = "";
            var rsCPHT = "";
            var cpht = 0;
            var dtht = 0;

            var categories = [];
            var dttn19 = 0;
            var dttn0 = 0;
            var dtnn7 = 0;

            // Doanh thu
            for (var i = 0; i < response.length; i++) {
                var item = response[i];
                item.Value = (item.Value == null || item.Value == "") ? 0 : item.Value.replace(',', '');

                if (item.InOut == true) {
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

            // Chi phí
            var arrCpht = response.filter(function (el) {
                return el.InOut == false;
            });
            var categoriesName = getDistinctCategory(arrCpht);
            var arrCphtResult = [];
            for (var j = 0; j < categoriesName.length; j++) {
                var catName = categoriesName[j];
                var catValue = 0;
                for (var k = 0; k < arrCpht.length; k++) {
                    var item = arrCpht[k];

                    if (item.Category == catName) {
                        item.Value = (item.Value == null || item.Value == "") ? 0 : item.Value.replace(',', '');
                        catValue += Number(item.Value);
                    }
                }
                arrCphtResult.push({ catName: catName, catValue: catValue });
            }

            for (var j = 0; j < arrCphtResult.length; j++) {
                cpht += Number(arrCphtResult[j].catValue);
                rsCPHT += '<tr><td class="text-right">' + arrCphtResult[j].catName + '</td><td>' + Number(arrCphtResult[j].catValue).toLocaleString() + ' €</td></tr>';
            }

            rs += '<tr><th style="width:200px">Costs</th><td>' + cpht.toLocaleString() + ' € (Total)</td></tr>';
            rs += rsCPHT;

            rs += '<tr><th>Revenue</th><td>' + dtht.toLocaleString() + ' € (Total)</td></tr><tr><td class="text-right">Revenue domestic (19%)</td><td>' + dttn19.toLocaleString() + ' €</td></tr><tr><td class="text-right">Revenue domestic (0%)</td><td>' + dttn0.toLocaleString() + ' €</td></tr><tr><td class="text-right">Revenue overseas (7%)</td><td>' + dtnn7.toLocaleString() + ' €</td></tr>';

			rs += '<tr><th>Result (vari.)</th><td>' + (dtht - cpht).toLocaleString() + ' €</td></tr>';

            $('#report-content').html(rs);

            // Draw chart

            var fromDate = $('#txtFromDate').val();
            var toDate = $('#txtToDate').val();
            fromDate = (fromDate != null && fromDate != "" && fromDate.toUpperCase != "INVALID DATE") ? formatDate(fromDate) : formatDate((new Date()).toString());
            toDate = (toDate != null && toDate != "" && toDate.toUpperCase != "INVALID DATE") ? formatDate(toDate) : formatDate((new Date()).toString());
            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(function drawAnnotations() {
                var data = google.visualization.arrayToDataTable([
                    ['Type', 'Total'],
                    ['Costs', cpht],
                    ['Revenue', (dttn0 + dtnn7 + dttn19)],
                ]);

                var options = {
                    title: "Statistics from " + fromDate + " to " + toDate
                };

                var chart = new google.visualization.PieChart(document.getElementById('myChart'));
                chart.draw(data, options);
            });


        }
    }
}

function getDistinctCategory(array) {
    var flags = [], output = [], l = array.length, i;
    for (i = 0; i < l; i++) {
        if (flags[array[i].Category]) continue;
        flags[array[i].Category] = true;
        output.push(array[i].Category);
    }
    return output;
}