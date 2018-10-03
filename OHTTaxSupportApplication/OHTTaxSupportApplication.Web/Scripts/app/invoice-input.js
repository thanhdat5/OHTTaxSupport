var urlDelete = "/api/invoices/setinactive?id=";
var urlCreate = "/api/invoices/create";
var urlUpdate = "/api/customers/update";

var urlCreateDetail = "/api/InvoiceDetails/create";
var urlGetAllDetailById = "/api/InvoiceDetails/getbyinvoiceid/";
var urlDeleteDetail = "/api/InvoiceDetails/setinactive?id=";

var urlSave = "/api/invoices/update";
var urlSaveAll = "/api/invoices/saveAll";

var tabsCount = 0;

var customersDDL = '';
var taxValuesDDL = '';
var departmentsDDL = '';
var categoriesDDL = '';
var unitsDDL = '';
var productsDDL = '';

var tempCustomersDDL = '';
var tempTaxValuesDDL = '';
var tempDepartmentsDDL = '';
var tempCategoriesDDL = '';
var tempUnitsDDL = '';
var tempProductsDDL = '';

var urlGetCustomers = "/api/customers/getall";
var urlGetTaxValues = "/api/taxvalues/getall";
var urlGetDepartments = "/api/departments/getall";
var urlGetCategories = "/api/categories/getall";
var urlGetUnits = "/api/units/getall";
var urlGetProducts = "/api/products/getall";

getMasterData();
function getMasterData() {
    ohtCallServiceToGet(urlGetCustomers, getCustomersSuccess);
}
function getCustomersSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempCustomersDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CustomerName + '</option>';
        }
        customersDDL = options;
        ohtCallServiceToGet(urlGetTaxValues, getTaxValuesSuccess);
    }
}
function getTaxValuesSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempTaxValuesDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].Value + '</option>';
        }
        taxValuesDDL = options;
        ohtCallServiceToGet(urlGetDepartments, getDepartmentsSuccess);
    }
}
function getDepartmentsSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempDepartmentsDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].DepartmentName + '</option>';
        }
        departmentsDDL = options;
        ohtCallServiceToGet(urlGetCategories, getCategoriesSuccess);
    }
}
function getCategoriesSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempCategoriesDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CategoryName + '</option>';
        }
        categoriesDDL = options;
        ohtCallServiceToGet(urlGetUnits, getUnitsSuccess);
    }
}
function getUnitsSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempUnitsDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].Value + '</option>';
        }
        unitsDDL = options;
        ohtCallServiceToGet(urlGetProducts, getProductsSuccess);
    }
}
function getProductsSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        tempProductsDDL = response;
        var options = '<option value="">--Please select--</option>';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].ProductName + '</option>';
        }
        productsDDL = options;
    }
}

var invoicesList = [];
$(function () {
    $(".invoice-manager .nav-tabs").on("click", "a", function (e) {
        e.preventDefault();
        if (!$(this).hasClass('add-invoice')) {
            $(this).tab('show');
        }
    })
        .on("click", "span", function () {
            var that = this;
            if (confirm('Are you sure you want to delete this invoice?')) {
                // Call service to delete invoice.
                var selectedId = that.id.replace('remove-invoice-', '');
                $('*').css('cursor', 'wait');
                $.ajax({
                    method: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: urlDelete + selectedId,
                    success: function (data) {
                        $('*').css('cursor', 'default');
                        if (data.Code != 0) {
                            ohtShowCustomError(data.Message);
                        } else {
                            var anchor = $(that).siblings('a');
                            $(anchor.attr('href')).remove();
                            $(that).parent().remove();
                            tabsCount--;
                            if ($(".invoice-manager .nav-tabs li").children('a').length > 1) {
                                $(".invoice-manager .nav-tabs li").children('a').first().click();
                            }

                            var tempLst = [];
                            for (var i = 0; i < invoicesList.length; i++) {
                                if (invoicesList[i].id != selectedId) {
                                    tempLst.push(invoicesList[i]);
                                }
                            }
                            invoicesList = tempLst;
                            ohtShowSuccess(data.Message);
                        }
                    },
                    error: function (error) {
                        ohtShowError(error);
                    }
                });
            }
        })
        .on("click", "i.fa-save", function () {
            var that = this;
            var selectedId = that.id.replace('save-invoice-', '');
            
            var postData = [];
            for (var i = 0; i < invoicesList.length; i++) {
                if (invoicesList[i].id == selectedId) {
                    postData.push(invoicesList[i]);
                    break;
                }
            }
            if (postData.length<=0 || postData[0].details == null || postData[0].details.length <= 0) {
                ohtShowCustomError("Please add more invoice details.");
                return;
            }

            $('*').css('cursor', 'wait');
            $.ajax({
                method: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(postData),
                url: urlSaveAll,
                success: function (data) {
                    $('*').css('cursor', 'default');
                    if (data.Code != 0) {
                        ohtShowCustomError(data.Message);
                    } else {
                        var anchor = $(that).siblings('a');
                        $(anchor.attr('href')).remove();
                        $(that).parent().remove();
                        tabsCount--;
                        if ($(".invoice-manager .nav-tabs li").children('a').length > 1) {
                            $(".invoice-manager .nav-tabs li").children('a').first().click();
                        }
                        var tempLst = [];
                        for (var i = 0; i < invoicesList.length; i++) {
                            if (invoicesList[i].id != selectedId) {
                                tempLst.push(invoicesList[i]);
                            }
                        }
                        invoicesList = tempLst;
                        ohtShowSuccess(data.Message);
                    }
                },
                error: function (error) {
                    ohtShowError(error);
                }
            });
        });
    $('.add-invoice').click(function (e) {
        e.preventDefault();
        var that = this;
        // call service to add new invoice
        $('*').css('cursor', 'wait');
        var postData = {
            ID: 1,
            TypeID: 1,
            CreatedDate: null,
            Value: null,
            CustomerID: null,
            TaxValueID: null,
            Status: 0,
            IsActive: true
        };
        $.ajax({
            method: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(postData),
            url: urlCreate,
            success: function (data) {
                $('*').css('cursor', 'default');
                if (data.Code != 0) {
                    ohtShowCustomError(data.Message);
                } else {
                    var id = data.Result.ID;

                    invoicesList.push({ id: id, details: [] });

                    var tabId = 'invoice_' + id;
                    $(that).closest('li').before('<li><a href="#invoice_' + id + '">#000' + id + '</a> <span id="remove-invoice-' + id + '"><i class="fa fa-trash"></i></span><i class="fa fa-save text-success" id="save-invoice-' + id + '" style="position: absolute;right: 35px;top: 0;z-index: 9;font-size: 18px;padding:9px 10px;cursor:pointer"></i></li>');

                    var tabContent = "<div class='tab-pane' id='" + tabId + "'>";
                    //###################################################
                    tabContent += "<div class='row'>";
                    tabContent += "<div class='col-md-3 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Date Created</label><input type='date' class='form-control' id='txtDateCreated-" + id + "' name='txtDateCreated-" + id + "' /><span class='text-danger' id='txtDateCreated-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='col-md-3 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>In/Out&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboInOut-" + id + "' name='cboInOut-" + id + "'><option value=''>--Please select--</option><option value='true'>In</option><option value='false'>Out</option></select><span class='text-danger' id='cboInOut-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='hidden-lg hidden-md hidden-sm'><div style='clear:both;width:100%;height:10px;'></div></div>";

                    tabContent += "<div class='col-md-3 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Customer&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboCustomer-" + id + "' name='cboCustomer-" + id + "'>" + customersDDL + "</select><span class='text-danger' id='cboCustomer-" + id + "_error'></span></div>";
                    tabContent += "</div>";


                    tabContent += "<div class='col-md-3 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Total</label><input type='text' disabled='disabled' class='form-control' id='txtTotal-" + id + "' name='txtTotal-" + id + "' value='0' /><span class='text-danger' id='txtTotal-" + id + "_error'></span></div>";
                    tabContent += "</div>";
                    tabContent += "</div>";
                    //###################################################

                    // Details

                    //###################################################
                    tabContent += "<div class='panel panel-info' id='detailsPanel_" + id + "'>";
                    tabContent += "<div class='panel-heading'><h4 class='panel-title' id='titleDetailsPanel_" + id + "'>Add invoice details</h4></div>";
                    tabContent += "<div class='panel-body'>";
                    //###################################################
                    tabContent += "<div class='row'>";
                    tabContent += "<div class='col-md-6 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Product&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboProduct-" + id + "' name='cboProduct-" + id + "'>" + productsDDL + "</select><span class='text-danger' id='cboProduct-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Unit&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboUnit-" + id + "' name='cboUnit-" + id + "'>" + unitsDDL + "</select><span class='text-danger' id='cboUnit-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='hidden-lg hidden-md hidden-sm'><div style='clear:both;width:100%;height:10px;'></div></div>";

                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Quantity&nbsp;<span class='text-danger'>*</span></label><input type='number' class='form-control' id='txtQuantity-" + id + "' name='txtQuantity-" + id + "' /><span class='text-danger' id='txtQuantity-" + id + "_error'></span></div>";
                    tabContent += "</div>";


                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Tax Value&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboTaxValue-" + id + "' name='cboTaxValue-" + id + "'>" + taxValuesDDL + "</select><span class='text-danger' id='cboTaxValue-" + id + "_error'></span></div>";
                    tabContent += "</div>";
                    tabContent += "</div>";
                    //###################################################


                    //###################################################
                    tabContent += "<div class='row'>";
                    tabContent += "<div class='col-md-6 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Department&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboDepartment-" + id + "' name='cboDepartment-" + id + "'>" + departmentsDDL + "</select><span class='text-danger' id='cboDepartment-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>Category&nbsp;<span class='text-danger'>*</span></label><select class='form-control' id='cboCategory-" + id + "' name='cboCategory-" + id + "'>" + categoriesDDL + "</select><span class='text-danger' id='cboCategory-" + id + "_error'></span></div>";
                    tabContent += "</div>";

                    tabContent += "<div class='hidden-lg hidden-md hidden-sm'><div style='clear:both;width:100%;height:10px;'></div></div>";

                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group'><label>NET.€&nbsp;<span class='text-danger'>*</span></label><input type='number' onkeyup='totalNET(" + id + ")' onmouseup='totalNET(" + id + ")' class='form-control' id='txtNET-" + id + "' name='txtNET-" + id + "' /><span class='text-danger' id='txtNET-" + id + "_error'></span></div>";
                    tabContent += "</div>";


                    tabContent += "<div class='col-md-2 col-sm-6'>";
                    tabContent += "<div class='form-group text-right'><label>Action</label><div class=''><button type='button' class='btn btn-warning btn-sm' id='btnSaveInvoiceDetails-" + id + "' name='btnSaveInvoiceDetails-" + id + "' onclick='saveInvoiceDetails(" + id + ")'>Add</button>&nbsp;<button type='button' class='btn btn-default btn-sm' id='btnCancelInvoiceDetailsForm-" + id + "' name='btnCancelInvoiceDetailsForm-" + id + "' onclick='cancelInvoiceDetailsForm(" + id + ")'>Cancel</button></div></div>";
                    tabContent += "</div>";
                    tabContent += "</div>";
                    //###################################################




                    tabContent += '<div class="widget-box widget-color-blue">';
                    tabContent += '<div class="widget-header">';
                    tabContent += '<h5 class="widget-title bigger lighter">';
                    tabContent += '<i class="ace-icon fa fa-table"></i>';
                    tabContent += 'Invoice details';
                    tabContent += '</h5>';

                    tabContent += '<div class="widget-toolbar widget-toolbar-light no-border">';
                    tabContent += '<select id="simple-colorpicker-1" class="hide">';
                    tabContent += '<option selected="" data-class="blue" value="#307ECC">#307ECC</option>';
                    tabContent += '<option data-class="blue2" value="#5090C1">#5090C1</option>';
                    tabContent += '<option data-class="blue3" value="#6379AA">#6379AA</option>';
                    tabContent += '<option data-class="green" value="#82AF6F">#82AF6F</option>';
                    tabContent += '<option data-class="green2" value="#2E8965">#2E8965</option>';
                    tabContent += '<option data-class="green3" value="#5FBC47">#5FBC47</option>';
                    tabContent += '<option data-class="red" value="#E2755F">#E2755F</option>';
                    tabContent += '<option data-class="red2" value="#E04141">#E04141</option>';
                    tabContent += '<option data-class="red3" value="#D15B47">#D15B47</option>';
                    tabContent += '<option data-class="orange" value="#FFC657">#FFC657</option>';
                    tabContent += '<option data-class="purple" value="#7E6EB0">#7E6EB0</option>';
                    tabContent += '<option data-class="pink" value="#CE6F9E">#CE6F9E</option>';
                    tabContent += '<option data-class="dark" value="#404040">#404040</option>';
                    tabContent += '<option data-class="grey" value="#848484">#848484</option>';
                    tabContent += '<option data-class="default" value="#EEE">#EEE</option>';
                    tabContent += '</select><div class="dropdown dropdown-colorpicker">		<a data-toggle="dropdown" class="dropdown-toggle" data-position="auto" href="#" aria-expanded="false"><span class="btn-colorpicker" style="background-color: rgb(48, 126, 204);"></span></a><ul class="dropdown-menu dropdown-caret dropdown-menu-right"><li><a class="colorpick-btn selected" href="#" style="background-color:#307ECC;" data-color="#307ECC"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#5090C1;" data-color="#5090C1"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#6379AA;" data-color="#6379AA"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#82AF6F;" data-color="#82AF6F"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#2E8965;" data-color="#2E8965"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#5FBC47;" data-color="#5FBC47"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#E2755F;" data-color="#E2755F"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#E04141;" data-color="#E04141"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#D15B47;" data-color="#D15B47"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#FFC657;" data-color="#FFC657"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#7E6EB0;" data-color="#7E6EB0"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#CE6F9E;" data-color="#CE6F9E"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#404040;" data-color="#404040"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#848484;" data-color="#848484"></a></li><li><a class="colorpick-btn" href="#" style="background-color:#EEE;" data-color="#EEE"></a></li></ul></div>';
                    tabContent += '</div>';
                    tabContent += '</div>';

                    tabContent += '<div class="widget-body">';
                    tabContent += '<div class="widget-main no-padding">';
                    tabContent += '<div class="table-reponsive" style="overflow:auto">';
                    tabContent += '<table class="table table-striped table-bordered">';
                    tabContent += '<thead>';
                    tabContent += '<tr>';
                    tabContent += '<th class="oht-th-id">Date</th>';
                    tabContent += '<th>Invoice #</th>';
                    tabContent += '<th>Customer</th>';
                    tabContent += '<th>Department</th>';
                    tabContent += '<th>Category</th>';
                    tabContent += '<th>Product</th>';
                    tabContent += '<th>NET.€</th>';
                    tabContent += '<th>Quantity</th>';
                    tabContent += '<th>Tax</th>';
                    tabContent += '<th>Action</th>';
                    tabContent += '</tr>';
                    tabContent += '</thead>';
                    tabContent += '<tbody id="oht-invoice-data-' + id + '"></tbody>';
                    tabContent += '</table>';
                    tabContent += '</div>';
                    tabContent += '</div>';
                    tabContent += '</div>';
                    tabContent += '</div>';

                    //###################################################
                    tabContent += "</div>";
                    tabContent += "</div>";
                    //###################################################

                    tabContent += "</div>";
                    $('.tab-content').append(tabContent);
                    tabsCount++;
                    $('.invoice-manager .nav-tabs li:nth-child(' + tabsCount + ') a').click();


                    $('#simple-colorpicker-' + id).ace_colorpicker({ pull_right: true }).on('change', function () {
                        var color_class = $(that).find('option:selected').data('class');
                        var new_class = 'widget-box';
                        if (color_class != 'default') new_class += ' widget-color-' + color_class;
                        $(that).closest('.widget-box').attr('class', new_class);
                    });

                    $('#txtDateCreated-' + id).val(convertToDate(data.Result.CreatedDate));
                    resetFormDetail(id);
                }
            },
            error: function (error) {
                ohtShowError(error);
            }
        });
    });
});

function convertToDate(date) {
    var xDate = new Date(date);
    var year = xDate.getUTCFullYear();
    var month = (xDate.getMonth() + 1);
    var date = xDate.getDate();
    if (month < 10) {
        month = "0" + month;
    }
    return year + "-" + month + "-" + date;
}

function resetValidate(id) {
    var blankStr = "";
    $('#cboInOut-' + id + "_error").text(blankStr);
    $('#cboProduct-' + id + "_error").text(blankStr);
    $('#cboUnit-' + id + "_error").text(blankStr);
    $('#txtQuantity-' + id + "_error").text(blankStr);
    $('#cboTaxValue-' + id + "_error").text(blankStr);
    $('#cboDepartment-' + id + "_error").text(blankStr);
    $('#cboCategory-' + id + "_error").text(blankStr);
    $('#txtNET-' + id + "_error").text(blankStr);
    $('#cboCustomer-' + id + "_error").text(blankStr);
}


function saveInvoiceDetails(id) {
    resetValidate(id);

    var errRequired = 'Please input data for this field.';
    var isValid = true;
    var txtDateCreated = $('#txtDateCreated-' + id).val();
    var cboInOut = $('#cboInOut-' + id).val();
    var cboProduct = $('#cboProduct-' + id).val();
    var cboUnit = $('#cboUnit-' + id).val();
    var txtQuantity = $('#txtQuantity-' + id).val();
    var cboTaxValue = $('#cboTaxValue-' + id).val();
    var cboDepartment = $('#cboDepartment-' + id).val();
    var cboCategory = $('#cboCategory-' + id).val();
    var txtNET = $('#txtNET-' + id).val();
    var cboCustomer = $('#cboCustomer-' + id).val();

    if (cboInOut == null || cboInOut.trim().length <= 0) {
        $('#cboInOut-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboProduct == null || cboProduct.trim().length <= 0) {
        $('#cboProduct-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboUnit == null || cboUnit.trim().length <= 0) {
        $('#cboUnit-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (txtQuantity == null || txtQuantity.trim().length <= 0) {
        $('#txtQuantity-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboTaxValue == null || cboTaxValue.trim().length <= 0) {
        $('#cboTaxValue-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboDepartment == null || cboDepartment.trim().length <= 0) {
        $('#cboDepartment-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboCategory == null || cboCategory.trim().length <= 0) {
        $('#cboCategory-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (txtNET == null || txtNET.trim().length <= 0) {
        $('#txtNET-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (cboCustomer == null || cboCustomer.trim().length <= 0) {
        $('#cboCustomer-' + id + "_error").text(errRequired);
        isValid = false;
    }
    if (!isValid) {
        return;
    };

    var cboProductName = tempProductsDDL.find(function (element) { return element.ID == cboProduct; }).ProductName;
    var cboTaxValueValue = tempTaxValuesDDL.find(function (element) { return element.ID == cboTaxValue; }).Value;
    var cboDepartmentName = tempDepartmentsDDL.find(function (element) { return element.ID == cboDepartment; }).DepartmentName;
    var cboCategoryName = tempCategoriesDDL.find(function (element) { return element.ID == cboCategory; }).CategoryName;
    var cboCustomerName = tempCustomersDDL.find(function (element) { return element.ID == cboCustomer; }).CustomerName;
    var cboUnitNameName = tempUnitsDDL.find(function (element) { return element.ID == cboUnit; }).Value;
    var postModel = {
        Date: txtDateCreated,
        CustomerID: cboCustomer,
        CustomerName: cboCustomerName,
        DepartmentName: cboDepartmentName,
        CategoryName: cboCategoryName,
        ProductName: cboProductName,
        TaxValueValue: cboTaxValueValue,

        TaxValue: cboTaxValue,
        ID: 0,
        InOut: cboInOut,
        InvoiceID: id,
        ProductID: cboProduct,
        UnitID: cboUnit,
        Value: Number(txtNET),
        Quanlity: txtQuantity,
        DepartmentID: cboDepartment,
        CategoryID: cboCategory,
        IsActive: true
    };

    for (var i = 0; i < invoicesList.length; i++) {
        if (invoicesList[i].id == id) {
            if (!isEdit) {
                postModel.ID = invoicesList[i].details.length;
                invoicesList[i].details.push(postModel);
                ohtShowSuccess("The record was added successfully.");
            } else {
                for (var j = 0; j < invoicesList[i].details.length; j++) {
                    if (invoicesList[i].details[j].ID == idEdit) {
                        postModel.ID = idEdit;
                        invoicesList[i].details[j] = postModel;
                        ohtShowSuccess("The record was saved successfully.");
                        cancelInvoiceDetailsForm(id);
                        break;
                    }
                }
            }
            resetFormDetail(id);
            refreshDataDetail(id);
        }
    }
}

function resetFormDetail(id) {
    $('#cboInOut-' + id).val("");
    $('#cboProduct-' + id).val("");
    $('#cboUnit-' + id).val("");
    $('#txtQuantity-' + id).val("0");
    $('#cboTaxValue-' + id).val("");
    $('#cboDepartment-' + id).val("");
    $('#cboCategory-' + id).val("");
    $('#txtNET-' + id).val("0");
    $('#cboCustomer-' + id).val("");
}

function deleteItem(invoiceID, id) {
    if (confirm("Are you want to delete this item?")) {
        for (var i = 0; i < invoicesList.length; i++) {
            if (invoicesList[i].id == invoiceID) {
                var iDetails = [];
                for (var j = 0; j < invoicesList[i].details.length; j++) {
                    if (invoicesList[i].details[j].ID != id) {
                        invoicesList[i].details[j].ID = iDetails.length;
                        iDetails.push(invoicesList[i].details[j]);
                    }
                }
                invoicesList[i].details = iDetails;
            }
        }
        refreshDataDetail(invoiceID);
        cancelInvoiceDetailsForm(invoiceID);
        ohtShowSuccess("The record was removed successfully.");
    }
    return false;
}

var isEdit = false;
var idEdit = 0;
function editItem(invoiceID, id) {
    $('#btnSaveInvoiceDetails-' + invoiceID).text("Save");
    $('#detailsPanel_' + invoiceID).addClass('panel-warning');
    $('#titleDetailsPanel_' + invoiceID).text('Edit invoice #' + invoiceID + ' details, item #' + id);
    isEdit = true;
    idEdit = id;
    resetValidate(id);
    for (var i = 0; i < invoicesList.length; i++) {
        if (invoicesList[i].id == invoiceID) {

            for (var j = 0; j < invoicesList[i].details.length; j++) {
                if (invoicesList[i].details[j].ID == id) {
                    var detail = invoicesList[i].details[j];

                    $('#txtDateCreated-' + invoiceID).val(detail.Date);
                    $('#cboInOut-' + invoiceID).val(detail.InOut);
                    $('#cboProduct-' + invoiceID).val(detail.ProductID);
                    $('#cboUnit-' + invoiceID).val(detail.UnitID);
                    $('#txtQuantity-' + invoiceID).val(detail.Quanlity);
                    $('#cboTaxValue-' + invoiceID).val(detail.TaxValue);
                    $('#cboDepartment-' + invoiceID).val(detail.DepartmentID);
                    $('#cboCategory-' + invoiceID).val(detail.CategoryID);
                    $('#txtNET-' + invoiceID).val(detail.Value);
                    $('#cboCustomer-' + invoiceID).val(detail.CustomerID);
                }
            }
        }
    }
}

function cancelInvoiceDetailsForm(id) {
    if (isEdit) {
        isEdit = false;
        idEdit = 0;
        $('#btnSaveInvoiceDetails-' + id).text("Add");
        $('#detailsPanel_' + id).removeClass('panel-warning');
        $('#titleDetailsPanel_' + id).text('Add invoice details');
    }
    resetFormDetail(id);
}

function refreshDataDetail(id) {
    var invoiceIndex = -1;
    for (var i = 0; i < invoicesList.length; i++) {
        if (invoicesList[i].id == id) {
            invoiceIndex = i;
            break;
        }
    }
    totalNET(id);
    if (invoiceIndex == -1) {
        $('#oht-invoice-data-' + id).html('<tr class="oht-loading"><td colspan="10">No record found.</td></tr>');
        $('#oht-invoice-data-' + id).html(innerHtml);
        return;
    }
    if (invoicesList[invoiceIndex].details == null || invoicesList[invoiceIndex].details.length <= 0) {
        $('#oht-invoice-data-' + id).html('<tr class="oht-loading"><td colspan="10">No record found.</td></tr>');
    } else {
        var innerHtml = '';
        $.each(invoicesList[invoiceIndex].details, function (index, element) {
            innerHtml +=
                '<tr>' +
                '<td>' + element.Date + '</td>' +
                '<td>#000' + element.InvoiceID + '</td>' +
                '<td>' + element.CustomerName + '</td>' +
                '<td>' + element.DepartmentName + '</td>' +
                '<td>' + element.CategoryName + '</td>' +
                '<td>' + element.ProductName + '</td>' +
                '<td>' + element.Value + '</td>' +
                '<td>' + element.Quanlity + '</td>' +
                '<td>' + element.TaxValueValue + '</td>' +
                '<td>' +
                '<button class="btn btn-primary btn-xs" id="edit-1" onclick="editItem(' + element.InvoiceID + ',' + element.ID + ')">' +
                '<span class="fa fa-pencil"></span>' +
                '</button>' +
                '&nbsp;<button class="btn btn-danger btn-xs" id="delete-1" onclick="deleteItem(' + element.InvoiceID + ',' + element.ID + ')">' +
                '<span class="fa fa-trash"></span>' +
                '</button>' +
                '</td>' +
                '</tr>';
        });
        $('#oht-invoice-data-' + id).html(innerHtml);
    }
}

function saveAll() {
    if (invoicesList == null || invoicesList.length <= 0) {
        ohtShowCustomError("At least one invoice created.");
        return;
    }
    // call service to save all invoices
    $('*').css('cursor', 'wait');
    $.ajax({
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(invoicesList),
        url: urlSaveAll,
        success: function (data) {
            $('*').css('cursor', 'default');
            if (data.Code != 0) {
                ohtShowCustomError(data.Message);
            } else {
                window.location = "/Invoices";
            }
        },
        error: function (error) {
            ohtShowError(error);
        }
    });
}

function showHistory() {
    window.location = "/Export";
}

function totalNET(invoiceID) {
    var total = Number($('#txtNET-' + invoiceID).val());
    try {
        for (var i = 0; i < invoicesList.length; i++) {
            if (invoicesList[i].id == invoiceID) {
                for (var j = 0; j < invoicesList[i].details.length; j++) {
                    total += Number(invoicesList[i].details[j].Value);
                }
            }
        }
    } catch (e) {
        total = 0;
    }
    $('#txtTotal-' + invoiceID).val(Number(total).toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
}