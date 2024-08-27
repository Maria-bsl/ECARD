var cid;
var check;
var jk = false;
var expapprovalflag = "";
var vl_cntr =1;
function validExp(e) {
    jk = true;
}
function showDiv() {
    jQuery("#btnSubmit").click(function () {
        jQuery(this).hide();
        jQuery("#loader").css("display", "block");
    }, 2000);
}
function PagerClick(index) {
    document.getElementById("hfCurrentPageIndex").value = index;
    document.forms[0].submit();
}
function hideModal(e) {
    jQuery('#' + e).modal('hide');
}

function hideBackdrop() {
    $(".modal-backdrop").hide();
}


function Validatespnmst(file) {
    result = '';
    var numBytes = document.getElementById(file.id).files[0].size;//2097152
    var filename = document.getElementById(file.id).files[0].name;
    filename = filename.substring(filename.lastIndexOf(".") + 1, filename.length).toLowerCase()
    if (filename != 'xls' && filename != 'xlsx' && filename != 'jpg' && filename != 'jpeg' && filename != 'png' && filename != 'gif' && filename != 'pdf') {
        document.getElementById(file.id).value = null;
        result += 'Upload file extension should be Excel or jpg or jpeg or png or gif or pdf only<br/>';
        jQuery('#fudocmst').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    else if (numBytes > 2097152) {
        document.getElementById(file.id).value = null;
        result += 'Upload file should not exceeds 2 MB<br/>';
        jQuery('#fudocmst').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    return true;
}
 
function Validateexp(file) {
     
    result = '';
   
    jQuery("#txtufilename").val('');
    jQuery("#txtufilepath").val('');
    var numBytes = document.getElementById(file.id).files[0].size;//2097152
    var filename = document.getElementById(file.id).files[0].name;
    filename = filename.substring(filename.lastIndexOf(".") + 1, filename.length).toLowerCase()
    if (filename != 'xls' && filename != 'xlsx' && filename != 'jpg' && filename != 'jpeg' && filename != 'png' && filename != 'gif' && filename != 'pdf') {
        document.getElementById(file.id).value = null;
        result += 'Upload file extension should be Excel or jpg or jpeg or png or gif or pdf only<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    else if (numBytes > 2097152) {
        document.getElementById(file.id).value = null;
        result += 'Upload file should not exceeds 2 MB<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    jQuery("#txtufilename").val(document.getElementById(file.id).files[0].name);
    coptfilefolder(fudoc);  //07.12.2021
    return true;
}
function resetExpm() {
    txtSID = document.getElementById('txtSID');
    txtSID = 0;
  //  jQuery("#txtdates").val('');
    //jQuery("#ddltd").val('0');
    jQuery("#txtrem").val('');
    jQuery("#txtbt").val("");
    jQuery("#txtsp").val("");
    jQuery("#txtcur").val("");
    jQuery("#txtamt").val("");
    jQuery("#txtname1").val("");
    jQuery("#txtspend").val("");
    jQuery("#txtbal").val("");
    jQuery("#txtexpamt").val('');
    jQuery("#txtdetrem").val('');
    jQuery("#txtBTNO").val('');
    jQuery("#txtBID").val('');
    jQuery("#txtSSID").val('');
    jQuery("#txtCUId").val('');
    jQuery("#ddlexpcatg").val(0).change();
    fudocmst.value = '';
    fudoc.value = '';
    jQuery("#txtufilename").val('');
    jQuery("#txtufilepath").val('');
    var ddlseason = document.getElementById("ddlseason");
    jQuery("#ddlseason").val(0).change();

    var tRows = document.getElementById('tblchruch');
    var rows = tRows.rows;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        row.cells[0].innerHTML = Index;
    }
    jQuery("#txtdates").prop("disabled", false);
    jQuery("#ddltd").prop("disabled", false);
    jQuery("#txtrem").prop("disabled", false);
    jQuery('#txtbt').prop("disabled", false);
    jQuery('#txtsp').prop("disabled", false);
    jQuery('#txtcur').prop("disabled", false);
    jQuery('#txtamt').prop("disabled", false);
    jQuery('#txtname1').prop("disabled", false);
    jQuery('#txtspend').prop("disabled", false);
    jQuery('#txtbal').prop("disabled", false);
    jQuery('#txtexpamt').prop("disabled", false);
    jQuery('#txtdetrem').prop("disabled", false);
    jQuery('#ddlseason').prop("disabled", false);
    jQuery("#tblchruch  TBODY").children().not(":last").remove();
    jQuery('#txtdates').css('border-color', '');
    jQuery('#ddltd').css('border-color', '');
    jQuery("#txtrem").css('border-color', '');
    //jQuery('#txttype').css('border-color', '');
    //jQuery('#txtfile').css('border-color', '');
    lblErrorrem.innerHTML = '';
    //lbltype.innerHTML = '';
    //lblfile.innerHTML = '';
}
//function getexpvaluesm() {
//    var txtdt = document.getElementById("txtdate");
//    var dt = document.getElementById('ddltd');
//    var dtvalue = dt.options[dt.selectedIndex].value;
//    var txtrem = document.getElementById("txtrem");
//    //var txttype = document.getElementById("txttype");
//    //var txtfile = document.getElementById('txtfile');
//    var txtcomt = document.getElementById('txtcomt');
//    tblemployee = document.getElementById('tbl-smtp'),
//        rows = tblemployee.rows,
//        txtSID = document.getElementById('txtSID');
//    hdnEmployee = document.getElementById('hdnEmployee');
//    var table = jQuery('#tbl-smtp').DataTable(),
//        selectedRow = table.rows('.selected').data();
//    var orderArr = [];
//    orderArr.length = 0;
//    //var formData = new FormData();
//    jQuery.each(jQuery("#tblchruch tbody tr"), function ($) {
//        orderArr.push({
//            B_T_T_Det_Sno: jQuery(this).find('td:eq(1) input').val(),
//            Board_Det_Sno: jQuery(this).find('td:eq(2) input').val(),
//            Sp_Det_Sno: jQuery(this).find('td:eq(4) input').val(),
//            Currency_Code: jQuery(this).find('td:eq(6) input').val(),
//            Transfer_Date: jQuery(this).find('td:eq(8) input').val(),
//            Transfer_Amount: jQuery(this).find('td:eq(9) input').val().replace(/,/g, ""),
//            Spend_Amount: jQuery(this).find('td:eq(10) input').val().replace(/,/g, ""),
//            Balance_Amount: jQuery(this).find('td:eq(11) input').val().replace(/,/g, ""),
//            Expenses_Amount: jQuery(this).find('td:eq(12) input').val().replace(/,/g, ""),
//            Exp_Det_Remarks: jQuery(this).find('td:eq(13) input').val(),
//            File_Name: jQuery(this).find('td:eq(14) input').val(),
//            //File_Name : jQuery(this).find("fudoc").files[0],
//            //data.append("B_T_T_Det_Sno", jQuery(this).find('td:eq(1) input').val()),
//            //data.append("Board_Det_Sno", jQuery(this).find('td:eq(2) input').val()),
//            //data.append("Sp_Det_Sno", jQuery(this).find('td:eq(4) input').val()),
//            //data.append("Currency_Code", jQuery(this).find('td:eq(6) input').val()),
//            //data.append("Transfer_Date", jQuery(this).find('td:eq(8) input').val()),
//            //data.append("Transfer_Amount", jQuery(this).find('td:eq(9) input').val().replace(/,/g, "")),
//            //data.append("Spend_Amount", jQuery(this).find('td:eq(10) input').val().replace(/,/g, "")),
//            //data.append("Balance_Amount", jQuery(this).find('td:eq(11) input').val().replace(/,/g, "")),
//            //data.append("Expenses_Amount", jQuery(this).find('td:eq(12) input').val().replace(/,/g, "")),
//            //data.append("Exp_Det_Remarks", jQuery(this).find('td:eq(13) input').val()),
//            //data.append("fileUpload", file),
//        });
//    });
//    var check = true;
//    if (hdnEmployee.value == "C") {
//        txtSID.value = '0';
//        jk = false;
//        var data = {
//            date: txtdt.value.trim(),
//            tdno: dtvalue,
//            rem: txtrem.value.trim(),
//            //type: txttype.value.trim(),
//            //file: txtfile.value.trim(),
//            sno: txtSID.value,
//            dummy: check,
//            lastrow: orderArr.length,
//            details: orderArr,
//            cmt: txtcomt.value.trim()
//        }
//        //data.append("date", txtdt.value.trim());
//        //data.append("tdno", dtvalue);
//        //data.append("rem", txtrem.value.trim());
//        //data.append("sno", txtSID.value);
//        //data.append("cmt", txtcomt.value.trim());
//        //data.append("dummy", check);
//        //data.append("lastrow", orderArr.length);
//        //data.append("details", orderArr);
//        //return data;
//    }
//    else if (hdnEmployee.value == "U") {
//        var table = jQuery('#tbl-smtp').DataTable();
//        var rows = table.rows({ page: 'all' }).nodes();
//        if (check == true) {
//            var data = {
//                date: txtdt.value.trim(),
//                tdno: dtvalue,
//                rem: txtrem.value.trim(),
//                //type: txttype.value.trim(),
//                //file: txtfile.value.trim(),
//                sno: txtSID.value,
//                dummy: check,
//                lastrow: orderArr.length,
//                details: orderArr,
//                cmt: txtcomt.value.trim()
//            }
//        }
//        else {
//            var data = {
//                date: txtdt.value.trim(),
//                tdno: dtvalue,
//                rem: txtrem.value.trim(),
//                //type: txttype.value.trim(),
//                //file: txtfile.value.trim(),
//                sno: txtSID.value,
//                dummy: check,
//                lastrow: orderArr.length,
//                details: orderArr,
//                cmt: txtcomt.value.trim()
//            }
//        }
//        return data;
//    }
//}
function getexpvaluesmret() {
    var txtdt = document.getElementById("txtdate");
    var dt = document.getElementById('ddltd');
    var dtvalue = dt.options[dt.selectedIndex].value;
    var txtrem = document.getElementById("txtrem");
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById('txtfile');
    var txtcomt = document.getElementById('txtcomt');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
    hdnEmployee = document.getElementById('hdnEmployee');
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    var orderArr = [];
    //var data = new FormData();
    //formData = new Array();
    orderArr.length = 0;
    jQuery.each(jQuery("#tblchruch tbody tr"), function ($) {
        orderArr.push({
            B_T_T_Det_Sno: jQuery(this).find('td:eq(1) input').val(),
            Board_Det_Sno: jQuery(this).find('td:eq(2) input').val(),
            Sp_Det_Sno: jQuery(this).find('td:eq(4) input').val(),
            Currency_Code: jQuery(this).find('td:eq(6) input').val(),
            //Transfer_Date: jQuery(this).find('td:eq(8) input').val(),
            Transfer_Amount: jQuery(this).find('td:eq(8) input').val().replace(/,/g, ""),
            Spend_Amount: jQuery(this).find('td:eq(9) input').val().replace(/,/g, ""),
            Balance_Amount: jQuery(this).find('td:eq(10) input').val().replace(/,/g, ""),
            Expenses_Amount: jQuery(this).find('td:eq(11) input').val().replace(/,/g, ""),
            exp_cat_sno: jQuery(this).find('td:eq(12) option:selected').val(),
            Exp_Det_Remarks: jQuery(this).find('td:eq(13) input').val(),
            File_Name: jQuery(this).find('td:eq(14) input')./*get[0].files*/val(),
            tFile_Name: jQuery(this).find('td:eq(16) input').val(), //10.12.2021
            tFile_path: jQuery(this).find('td:eq(17) input').val(), //10.12.2021
            Dumval: jQuery(this).find('td:eq(0)').html()
        });
    });
    var check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            date: txtdt.value.trim(),
            tdno: dtvalue,
            rem: txtrem.value.trim(),
            //type: txttype.value.trim(),
            //file: txtfile.value.trim(),
            sno: txtSID.value,
            dummy: check,
            lastrow: orderArr.length,
            details: orderArr,
            //datas.append("fileUpload", file),
            cmt: txtcomt.value.trim()
        }
        //data.append("date", txtdt.value.trim());
        //data.append("tdno", dtvalue);
        //data.append("rem", txtrem.value.trim());
        //data.append("sno", txtSID.value);
        //data.append("cmt", txtcomt.value.trim());
        //data.append("dummy", check);
        //data.append("lastrow", orderArr.length);
        //data.append("details", orderArr);
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        if (check == true) {
            var data = {
                date: txtdt.value.trim(),
                tdno: dtvalue,
                rem: txtrem.value.trim(),
                //type: txttype.value.trim(),
                //file: txtfile.value.trim(),
                sno: txtSID.value,
                dummy: check,
                lastrow: orderArr.length,
                details: orderArr,
                cmt: txtcomt.value.trim()
            }
        }
        else {
            var data = {
                date: txtdt.value.trim(),
                tdno: dtvalue,
                rem: txtrem.value.trim(),
                //type: txttype.value.trim(),
                //file: txtfile.value.trim(),
                sno: txtSID.value,
                dummy: check,
                lastrow: orderArr.length,
                details: orderArr,
                cmt: txtcomt.value.trim()
            }
        }
        return data;
    }
}
function getexpvaluesm() {
    var txtdt = document.getElementById("txtdates");
    var dt = document.getElementById('ddltd');
    var dtvalue = dt.options[dt.selectedIndex].value;
    var txtrem = document.getElementById("txtrem");


    var dt2 = document.getElementById('ddlseason');
    var tseasonval = dt2.options[dt2.selectedIndex].value;
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById('txtfile');
    var txtcomt = document.getElementById('txtcomt');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
    hdnEmployee = document.getElementById('hdnEmployee');
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    var orderArr = [];
    //var data = new FormData();
    //formData = new Array();
    orderArr.length = 0;
    jQuery.each(jQuery("#tblchruch tbody tr"), function ($) {
        orderArr.push({
            B_T_T_Det_Sno: jQuery(this).find('td:eq(1) input').val(),
            Board_Det_Sno: jQuery(this).find('td:eq(2) input').val(),
            Sp_Det_Sno: jQuery(this).find('td:eq(4) input').val(),
            Currency_Code: jQuery(this).find('td:eq(6) input').val(),
            //Transfer_Date: jQuery(this).find('td:eq(8) input').val(),
            Transfer_Amount: jQuery(this).find('td:eq(8) input').val().replace(/,/g, ""),
            Spend_Amount: jQuery(this).find('td:eq(9) input').val().replace(/,/g, ""),
            Balance_Amount: jQuery(this).find('td:eq(10) input').val().replace(/,/g, ""),
            Expenses_Amount: jQuery(this).find('td:eq(11) input').val().replace(/,/g, ""),
            exp_cat_sno: jQuery(this).find('td:eq(12) option:selected').val(),
            Exp_Det_Remarks: jQuery(this).find('td:eq(13) input').val(),
            File_Name: jQuery(this).find('td:eq(14) input')./*get[0].files*/val(),
            tFile_Name: jQuery(this).find('td:eq(15) input').val(),
            tFile_path: jQuery(this).find('td:eq(16) input').val(),
            Dumval: jQuery(this).find('td:eq(0)').html()
        });
    });
    var check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            date: txtdt.value.trim(),
            tdno: dtvalue,
            rem: txtrem.value.trim(),
            seasonno:tseasonval,
            //type: txttype.value.trim(),
            //file: txtfile.value.trim(),
            sno: txtSID.value,
            dummy: check,
            lastrow: orderArr.length,
            details: orderArr,
            //datas.append("fileUpload", file),
            cmt: txtcomt.value.trim()
        }
        //data.append("date", txtdt.value.trim());
        //data.append("tdno", dtvalue);
        //data.append("rem", txtrem.value.trim());
        //data.append("sno", txtSID.value);
        //data.append("cmt", txtcomt.value.trim());
        //data.append("dummy", check);
        //data.append("lastrow", orderArr.length);
        //data.append("details", orderArr);
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        if (check == true) {
            var data = {
                date: txtdt.value.trim(),
                tdno: dtvalue,
                rem: txtrem.value.trim(),
                seasonno: tseasonval,
                //type: txttype.value.trim(),
                //file: txtfile.value.trim(),
                sno: txtSID.value,
                dummy: check,
                lastrow: orderArr.length,
                details: orderArr,
                cmt: txtcomt.value.trim()
            }
        }
        else {
            var data = {
                date: txtdt.value.trim(),
                tdno: dtvalue,
                rem: txtrem.value.trim(),
                seasonno: tseasonval,
                //type: txttype.value.trim(),
                //file: txtfile.value.trim(),
                sno: txtSID.value,
                dummy: check,
                lastrow: orderArr.length,
                details: orderArr,
                cmt: txtcomt.value.trim()
            }
        }
        return data;
    }
}

function AddExpNewRowm_ret(e) {
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(1) input').val();
        var bal = jQuery(table).eq(i).find('td:eq(10) input').val().replace(/,/g, "");
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val().replace(/,/g, "");
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val().replace(/,/g, "");
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '') {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = 'Duplicate Row.Please Remove.<br>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = true;
                return;
            }
            else if (Number(exp) > Number(bal)) {
                message = 'Expenses Amount should not exceed Balance Amount<br/>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = true;
                return;
            }
            else {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                duplicaterow = true;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
    if (duplicaterow == true) {
        if (rt == false) {
            $('#tblchruch tr:last').show();
            var tr = '<button class="btn btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
            rt = true;
        }
        else {
            var txtBTNO = jQuery("#txtBTNO");
            var txtBID = jQuery("#txtBID");
            var txtbd = jQuery("#txtbd");
            var txtsp = jQuery("#txtsp");
            var txtSSID = jQuery("#txtSSID");
            var txtCUId = jQuery("#txtCUId");
            var txtcur = jQuery("#txtcur");
            var txtname1 = jQuery("#txtname1");
            var txtamt = jQuery("#txtamt");
            var txtspend = jQuery("#txtspend");
            var txtbal = jQuery("#txtbal").val().replace(/,/g, "");// jQuery("#txtbal");
            var txtexpamt = jQuery("#txtexpamt").val().replace(/,/g, "");
            var txtdetrem = jQuery("#txtdetrem").val();
            var fudoc = jQuery("#fudoc").val();
            var tmpfile = jQuery("#txtufilename").val();
            var tmpfilepath = jQuery("#txtufilepath").val();
            var expdtlid = jQuery("#txtdtlid").val();
            var dlvalue = jQuery("#ddlexpcatg").val();
            if (txtBID.val() != '' && txtexpamt != '' && dlvalue != 0) {
                if (Number(txtexpamt) <= Number(txtbal)) {
                    var tBody = jQuery("#tblchruch > TBODY")[0];
                    var row = tBody.insertRow(0);
                    var cell = jQuery(row.insertCell(0));
                    var cell1 = jQuery(row.insertCell(1));
                    var t1 = document.createElement("input");
                    t1.id = "txtBTNO1";
                    t1.value = txtBTNO.val();
                    t1.className = "form-control d-none";
                    cell1.append(t1);

                    var cell2 = jQuery(row.insertCell(2));
                    var t2 = document.createElement("input");
                    t2.id = "txtbd1";
                    t2.value = txtbd.val();
                    t2.className = "form-control d-none";
                    cell2.append(t2);

                    var cell3 = jQuery(row.insertCell(3));
                    var t3 = document.createElement("input");
                    t3.id = "txtBID1";
                    t3.value = txtBID.val();
                    t3.className = "form-control";
                    t3.readOnly = true;
                    t3.onblur = function () {
                        bindSponseId(this);
                    }
                    var span = document.createElement('span');
                    span.innerHTML = '<button type="button" class="btn btn-success btn-sm btn-biz_logic" id="btnSearch" onclick="getdata()">Search</button>';
                    cell3.append(t3);
                    //cell3.append(span);
                    var cell4 = jQuery(row.insertCell(4));
                    var t4 = document.createElement("input");
                    t4.id = "txtSSID1";
                    t4.value = txtSSID.val();
                    t4.className = "form-control d-none";
                    cell4.append(t4);
                    var cell5 = jQuery(row.insertCell(5));
                    var t5 = document.createElement("input");
                    t5.id = "txtsp1";
                    t5.value = txtsp.val();
                    t5.className = "form-control";
                    t5.readOnly = true;
                    cell5.append(t5);
                    var cell6 = jQuery(row.insertCell(6));
                    var t6 = document.createElement("input");
                    t6.id = "txtCUId1";
                    t6.className = "form-control d-none";
                    t6.value = txtCUId.val();
                    cell6.append(t6);
                    var cell7 = jQuery(row.insertCell(7));
                    var t7 = document.createElement("input");
                    t7.id = "txtcur1";
                    t7.value = txtcur.val();
                    t7.className = "form-control";
                    t7.readOnly = true;
                    cell7.append(t7);

                    var cell8 = jQuery(row.insertCell(8));
                    var t8 = document.createElement("input");
                    t8.id = "txtamt1";
                    t8.value = txtamt.val();
                    t8.className = "form-control";
                    t8.readOnly = true;
                    t8.onpaste = function () {
                        return false;
                    }
                    cell8.append(t8);

                    var cell9 = jQuery(row.insertCell(9));
                    var t9 = document.createElement("input");
                    t9.id = "txtspend1";
                    t9.value = txtspend.val();
                    t9.className = "form-control";
                    t9.readOnly = true;
                    t9.onpaste = function () {
                        return false;
                    }
                    cell9.append(t9);

                    var cell10 = jQuery(row.insertCell(10));
                    var t10 = document.createElement("input");
                    t10.id = "txtbal1";
                    t10.value = txtbal;
                    t10.className = "form-control";
                    t10.readOnly = true;
                    t10.onpaste = function () {
                        return false;
                    }
                    cell10.append(t10);

                    var cell11 = jQuery(row.insertCell(11));
                    var t11 = document.createElement("input");
                    t11.id = "txtexpamt1";
                    t11.readOnly = true;
                    t11.value = txtexpamt == undefined ? '' : txtexpamt;
                    t11.className = "form-control";
                    t11.onpaste = function () {
                        return false;
                    }
                    cell11.append(t11);

                    var cell12 = jQuery(row.insertCell(12));
                    var t12 = document.createElement("select");
                    t12.className = "form-control";
                    for (var i = 0; i < array[0].length; i++) {
                        var option = document.createElement("option");
                        option.value = array[0][i].SNO;
                        option.text = array[0][i].category_description;
                        t12.appendChild(option);
                    }
                    t12.id = "txtddlexpcat1";
                    t12.disabled = true;
                    t12.value = dlvalue == undefined ? 0 : dlvalue;
                    cell12.append(t12);

                    var cell13 = jQuery(row.insertCell(13));
                    var t13 = document.createElement("input");
                    t13.id = "txtdetrem1";
                    t13.value = txtdetrem == undefined ? '' : txtdetrem;
                    t13.className = "form-control";
                    t13.placeholder = "Remarks";
                    t13.onpaste = function () {
                        return false;
                    }
                    cell13.append(t13);

                    var cell14 = jQuery(row.insertCell(14));
                    var t14 = document.createElement("input");
                    t14.id = "fudoc1";
                    t14.value = fudoc == undefined ? '' : fudoc;
                    t14.className = "form-control";
                    t14.type = "file";
                    t14.disabled = true;

                    cell14.append(t14);

                    var cell15 = jQuery(row.insertCell(15));
                    var t15 = document.createElement("input");
                    t15.id = "tmpfile1";
                    t15.value = tmpfile;
                    t15.className = "form-control";
                    t15.disabled = true;
                    cell15.append(t15);

                    var cell16 = jQuery(row.insertCell(16));
                    var t16 = document.createElement("input");
                    t16.id = "tmpfilepath1";
                    t16.value = tmpfilepath;
                    t16.disabled = true;
                    t16.className = "form-control";
                    cell16.append(t16);

                     
                    var cell17 = jQuery(row.insertCell(17));
                    var t18 = document.createElement("input");
                    t18.id = "txtview1";
                    t18.readOnly = true;
                    t18.value = "View";
                    t18.className = "form-control";
                    cell17.append(t18);
                   
                    cell18 = jQuery(row.insertCell(18));
                    var btnRemove = jQuery('<button class="btn btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this); return false;" ><i class="fas fa-trash" aria-hidden="true"></i> Delete</button>');
                    cell18.append(btnRemove);
                    //09.09.2021
                    
                   

                    jQuery('#tblchruch tr td:nth-child(2)').hide();
                    jQuery('#tblchruch tr td:nth-child(3)').hide();
                    jQuery('#tblchruch tr td:nth-child(5)').hide();
                    jQuery('#tblchruch tr td:nth-child(7)').hide();
                    jQuery('#tblchruch tr td:nth-child(9)').hide();
                    jQuery('#tblchruch tr td:nth-child(10)').hide(); 
                    
                    jQuery('#txtBTNO').val("");
                    jQuery('#txtbd').val("");
                    jQuery('#txtBID').val("");
                    jQuery('#txtsp').val("");
                    jQuery('#txtSSID').val("");
                    jQuery('#txtcur').val("");
                    jQuery('#txtCUId').val("");
                    jQuery('#txtname1').val("");
                    jQuery("#txtamt").val("");
                    jQuery("#txtspend").val("");
                    jQuery("#txtbal").val("");
                    jQuery("#txtexpamt").val('');
                    jQuery("#txtdetrem").val('');
                    jQuery("#txtdtlid").val('');
                    jQuery("#ddlexpcatg").val(0).change();
                    jQuery("#fudoc").val("")
                    jQuery("#txtufilename").val("");
                    jQuery("#txtufilepath").val("");
                    var tRows = document.getElementById('tblchruch');
                    var rows = tRows.rows;
                    for (var Index = 1, row = null; Index < rows.length; Index++) {
                        row = rows[Index];
                        row.cells[0].innerHTML = Index;
                    }
                }
                else {
                    message = 'Expenses Amount should not exceed Balance Amount.';
                    type = 'danger';
                    notifyMessage(message, type);
                }
            }
            else {
                message = 'Required Details are missing.</br>';
                type = 'danger';
                notifyMessage(message, type);
            }
        }
    }
}

function AddExpNewRowm(e) {

    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(1) input').val();
        var bal = jQuery(table).eq(i).find('td:eq(10) input').val().replace(/,/g, "");
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val().replace(/,/g, "");
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val().replace(/,/g, "");
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '') {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = 'Duplicate Row.Please Remove.<br>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = true;
                return;
            }
            else if (Number(exp) > Number(bal)) {
                message = 'Expenses Amount should not exceed Balance Amount<br/>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = true;
                return;
            }
            else {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                duplicaterow = true;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
    if (duplicaterow == true) {
        if (rt == false) {
            $('#tblchruch tr:last').show();
            var tr = '<button class="btn btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
            rt = true;
        }
        else {
            var txtBTNO = jQuery("#txtBTNO");
            var txtBID = jQuery("#txtBID");
            var txtbd = jQuery("#txtbd");
            var txtsp = jQuery("#txtsp");
            var txtSSID = jQuery("#txtSSID");
            var txtCUId = jQuery("#txtCUId");
            var txtcur = jQuery("#txtcur");
         ///   var txtname1 = jQuery("#txtname1"); //NR
            var txtamt = jQuery("#txtamt");
            var txtspend = jQuery("#txtspend");
            var txtbal = jQuery("#txtbal").val().replace(/,/g, "");//jQuery("#txtbal");
            var txtexpamt = jQuery("#txtexpamt").val().replace(/,/g, "");
            var txtdetrem = jQuery("#txtdetrem").val();
            var fudoc = jQuery("#fudoc").val();
            var tmpfile = jQuery("#txtufilename").val();
            var tmpfilepath = jQuery("#txtufilepath").val();
            var dlvalue = jQuery("#ddlexpcatg").val();
            if (txtBID.val() != '' && txtexpamt != 0 && dlvalue != 0) {
                if (Number(txtexpamt) <= Number(txtbal)) {
                    vl_cntr += 1;

                    var tBody = jQuery("#tblchruch > TBODY")[0];
                    var row = tBody.insertRow(0);
                    var cell = jQuery(row.insertCell(0));
                    var cell1 = jQuery(row.insertCell(1));
                    var t1 = document.createElement("input");
                    t1.id = "txtBTNO1";
                    t1.value = txtBTNO.val();
                    t1.className = "form-control d-none";
                    cell1.append(t1);

                    var cell2 = jQuery(row.insertCell(2));
                    var t2 = document.createElement("input");
                    t2.id = "txtbd1";
                    t2.value = txtbd.val();
                    t2.className = "form-control d-none";
                    cell2.append(t2);

                    var cell3 = jQuery(row.insertCell(3));
                    var t3 = document.createElement("input");
                    t3.id = "txtBID1";
                    t3.value = txtBID.val();
                    t3.className = "form-control";
                    t3.readOnly = true;
                    t3.onblur = function () {
                        bindSponseId(this);
                    }
                    var span = document.createElement('span');
                    span.innerHTML = '<button type="button" class="btn btn-success btn-sm btn-biz_logic" id="btnSearch" onclick="getdata()">Search</button>';
                    cell3.append(t3);
                   // cell3.append(span);

                    var cell4 = jQuery(row.insertCell(4));
                    var t4 = document.createElement("input");
                    t4.id = "txtSSID1";
                    t4.value = txtSSID.val();
                    t4.className = "form-control d-none";
                    cell4.append(t4);

                    var cell5 = jQuery(row.insertCell(5));
                    var t5 = document.createElement("input");
                    t5.id = "txtsp1";
                    t5.value = txtsp.val();
                    t5.className = "form-control";
                    t5.readOnly = true;
                    cell5.append(t5);

                    var cell6 = jQuery(row.insertCell(6));
                    var t6 = document.createElement("input");
                    t6.id = "txtCUId1";
                    t6.className = "form-control d-none";
                    t6.value = txtCUId.val();
                    cell6.append(t6);

                    var cell7 = jQuery(row.insertCell(7));
                    var t7 = document.createElement("input");
                    t7.id = "txtcur1";
                    t7.value = txtcur.val();
                    t7.className = "form-control";
                    t7.readOnly = true;
                    cell7.append(t7);

                    //////NR
                    //var cell8 = jQuery(row.insertCell(8));
                    //var t8 = document.createElement("input");
                    //t8.id = "txtname11";
                    //t8.value = txtname1.val();
                    //t8.className = "form-control";
                    //t8.readOnly = true;
                    //cell8.append(t8);
                    //////NR

                    var cell8 = jQuery(row.insertCell(8));
                    var t8 = document.createElement("input");
                    t8.id = "txtamt1";
                    t8.value = txtamt.val();
                    t8.className = "form-control";
                    t8.readOnly = true;
                    t8.onpaste = function () {
                        return false;
                    }
                    cell8.append(t8);

                    var cell9 = jQuery(row.insertCell(9));
                    var t9 = document.createElement("input");
                    t9.id = "txtspend1";
                    t9.value = txtspend.val();
                    t9.className = "form-control";
                    t9.readOnly = true;
                    t9.onpaste = function () {
                        return false;
                    }
                    cell9.append(t9);

                    var cell10 = jQuery(row.insertCell(10));
                    var t10 = document.createElement("input");
                    t10.id = "txtbal1";
                    t10.value = txtbal ;
                    t10.className = "form-control";
                    t10.readOnly = true;
                    t10.onpaste = function () {
                        return false;
                    }
                    cell10.append(t10);

                    var cell11 = jQuery(row.insertCell(11));
                    var t11 = document.createElement("input");
                    t11.id = "txtexpamt1";
                    t11.readOnly = true;
                    t11.value = txtexpamt == undefined ? '' : txtexpamt;
                    t11.className = "form-control";
                    t11.onpaste = function () {
                        return false;
                    }
                    cell11.append(t11);

                    var cell12 = jQuery(row.insertCell(12));
                    var t12 = document.createElement("select");
                    t12.className = "form-control";
                    for (var i = 0; i < array[0].length; i++) {
                        var option = document.createElement("option");
                        option.value = array[0][i].SNO;
                        option.text = array[0][i].category_description;
                        t12.appendChild(option);
                    }
                    t12.id = "txtddlexpcat1";
                    t12.disabled = true;
                    t12.value = dlvalue == undefined ? 0 : dlvalue;
                    cell12.append(t12);

                    var cell13 = jQuery(row.insertCell(13));
                    var t13 = document.createElement("input");
                    t13.id = "txtdetrem1";
                    t13.value = txtdetrem == undefined ? '' : txtdetrem;
                    t13.className = "form-control";
                    t13.placeholder = "Remarks";
                    t13.disabled = true;
                    t13.onpaste = function () {
                        return false;
                    }
                    cell13.append(t13);

                   
                    
                    var cell14 = jQuery(row.insertCell(14));
                    var t14 = document.createElement("input");
                    t14.id = "fudoc1";
                    t14.value = fudoc == undefined ? '' : fudoc;
                    t14.className = "form-control";
                    t14.type = "file";
                    t14.disabled = true;
                    cell14.append(t14);

                    
                    var cell15 = jQuery(row.insertCell(15));
                    var t15 = document.createElement("input");
                    t15.id = "tmpfile1";
                    t15.value = tmpfile ;
                    t15.className = "form-control";
                    cell15.append(t15);

                    //copyfiletofolder(fudoc);  //07.12.2021

                    var cell16 = jQuery(row.insertCell(16));
                    var t16 = document.createElement("input");
                    t16.id = "tmpfilepath1";
                    t16.value = tmpfilepath;
                    t16.className = "form-control";
                    cell16.append(t16);

                    
                   // alert(jQuery("#txtufilename").val());
                    


                    cell17 = jQuery(row.insertCell(17));
                    var btnRemove = jQuery('<button class="btn btn-outline-dark btn-sm" onclick="RemoveExpm(this); return false;" ><i class="fas fa-trash" aria-hidden="true"></i> Delete</button>');
                    cell17.append(btnRemove);

                    jQuery('#tblchruch tr td:nth-child(2)').hide();
                    jQuery('#tblchruch tr td:nth-child(3)').hide();
                    jQuery('#tblchruch tr td:nth-child(5)').hide();
                    jQuery('#tblchruch tr td:nth-child(7)').hide();
                    jQuery('#tblchruch tr td:nth-child(9)').hide(); //transfer amt hide
                    jQuery('#tblchruch tr td:nth-child(10)').hide(); // Spent amt hide
                    jQuery('#tblchruch tr td:nth-child(17)').hide(); 

                    jQuery('#txtBTNO').val("");
                    jQuery('#txtbd').val("");
                    jQuery('#txtBID').val("");
                    jQuery('#txtsp').val("");
                    jQuery('#txtSSID').val("");
                    jQuery('#txtcur').val("");
                    jQuery('#txtCUId').val("");
                   //// jQuery('#txtname1').val("");
                    jQuery("#txtamt").val("");
                    jQuery("#txtspend").val("");
                    jQuery("#txtbal").val("");
                    jQuery("#txtexpamt").val('');
                    jQuery("#txtdetrem").val('');
                    jQuery("#ddlexpcatg").val(0).change();
                    jQuery("#fudoc").val("")
                    jQuery("#txtufilename").val("");
                    jQuery("#txtufilepath").val("");
                    var tRows = document.getElementById('tblchruch');
                    var rows = tRows.rows;
                    for (var Index = 1, row = null; Index < rows.length; Index++) {
                        row = rows[Index];
                        row.cells[0].innerHTML = Index;
                    }
                    alert(vl_cntr);
                }
                else {
                    message = 'Expenses Amount should not exceed Balance Amount.';
                    type = 'danger';
                    notifyMessage(message, type);
                }
            }
            else {
                message = 'Required Details are missing.</br>';
                type = 'danger';
                notifyMessage(message, type);
            }
        }
    }
}


function Addviewfile(e) {
    var row = e.closest('tr').rowIndex;
    var d = Number(row) - 1;
    var filename = jQuery('#tblchruch tbody tr:eq(' + d + ')').find('td:eq(14) input').val();
    alert(filename);
    window.open("google.com"); 

    

}

function RemoveExpm_ret(button) {
    var x = confirm("Are you sure you want to Delete?");
    if (x) {
        var tRows = document.getElementById('tblchruch');
        var row1 = jQuery(button).closest("TR");
        var name1 = jQuery("TD", row1).eq(0).html();
        var len1 = tRows.rows.length - 1;
        if (len1 == name1) {
            if (len1 != 1) {
                $('#tblchruch tr:last').hide();
                rt = false;
                duplicaterow = true;
                jQuery('#txtBTNO').val('').change();
                jQuery('#txtBID').val('').change();
                jQuery('#txtbd').val('').change();
                jQuery('#txtsp').val('').change();
                jQuery('#txtSSID').val('').change();
                jQuery('#txtCUId').val('').change();
                jQuery('#txtcur').val('').change();
                jQuery('#txtname1').val('').change();
                jQuery('#txtamt').val('').change();
                jQuery('#txtspend').val('').change();
                jQuery('#txtbal').val('').change();
                jQuery('#txtexpamt').val('');
                jQuery('#txtdetrem').val('');
            }
        } else {
            if (len1 >= 2) {
                var row2 = jQuery(button).closest("TR");
                var table2 = jQuery("#tblchruch")[0];
                table2.deleteRow(row2[0].rowIndex);
            }
        }
        var hd = $("#tblchruch tr:last").is(":visible")
        if (hd == false) {
            if (len1 == 3 || len1 == 2) {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddExpNewRowm_ret(this)"><i class="fa fa-plus"></i>AddNewRow</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            } else {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddExpNewRowm_ret(this)"><i class="fa fa-plus"></i>AddNewRow</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            }
        }
        $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
        var rows = tRows.rows;
        for (var Index = 1, row = null; Index < rows.length; Index++) {
            row = rows[Index];
            row.cells[0].innerHTML = Index;
            var d = Number(Index) - 1;
            jQuery('#tblchruch tbody tr:eq(' + d + ')').css('backgroundColor', 'White');
        }
        if (len1 != 1) {
            bindSponsedeatils_recalc_ret();
        }
    };
}


function RemoveExpm(button) {
    var x = confirm("Are you sure you want to Delete?");
    if (x) {
        var tRows = document.getElementById('tblchruch');
        var row1 = jQuery(button).closest("TR");
        var name1 = jQuery("TD", row1).eq(0).html();
        var len1 = tRows.rows.length - 1;
        if (len1 == name1) {
            if (len1 != 1) {
                $('#tblchruch tr:last').hide();
                rt = false;
                duplicaterow = true;
                jQuery('#txtBTNO').val('').change();
                jQuery('#txtBID').val('').change();
                jQuery('#txtbd').val('').change();
                jQuery('#txtsp').val('').change();
                jQuery('#txtSSID').val('').change();
                jQuery('#txtCUId').val('').change();
                jQuery('#txtcur').val('').change();
                jQuery('#txtname1').val('').change();
                jQuery('#txtamt').val('').change();
                jQuery('#txtspend').val('').change();
                jQuery('#txtbal').val('').change();
                jQuery('#txtexpamt').val('');
                jQuery("#ddlexpcatg").val(0).change();
                jQuery('#txtdetrem').val('');
            }
        } else {
            if (len1 >= 2) {
                var row2 = jQuery(button).closest("TR");
                var table2 = jQuery("#tblchruch")[0];
                table2.deleteRow(row2[0].rowIndex);
            }
        }
        var hd = $("#tblchruch tr:last").is(":visible")
        if (hd == false) {
            if (len1 == 3 || len1 == 2) {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddExpNewRowm(this)"><i class="fa fa-plus"></i>AddNewRow</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            } else {
                var tr = '<button type="button" id="btnAdd" class="btn btn-biz_logic btn-sm" value="Add" onclick="AddExpNewRowm(this)"><i class="fa fa-plus"></i>AddNewRow</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="btn  btn-outline-dark btn-sm" onclick="RemoveExpm_ret(this);return false;"><i class="fas fa-trash" aria-hidden="true"></i>Delete</button>';
            }
        }
        $("#tblchruch").find("tr:last").prev().find("td:last").html(tr);
        var rows = tRows.rows;
        for (var Index = 1, row = null; Index < rows.length; Index++) {
            row = rows[Index];
            row.cells[0].innerHTML = Index;
            var d = Number(Index) - 1;
            jQuery('#tblchruch tbody tr:eq(' + d + ')').css('backgroundColor', 'White');
        }

        //// recalculate board balances 10.06.2021
       // if (len1 != 1) {
            bindSponsedeatils_recalc();
        //    }
        
        ////over 
    };
}

function getexpvaluesm_frmdata() {
    var data = new FormData();
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var totalFiles = jQuery(table + ':nth-child(' + i + ')').find('td:eq(14) input')[0].files[0];
        data.append("fileUpload", totalFiles);
    }
    return data;
}

function validateExpensemRet() {
    var txtdate = document.getElementById("txtdate");
    var ddltd = document.getElementById('ddltd');
    var txtrem = document.getElementById("txtrem");
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById("txtfile");
    var txtexpamt = jQuery("#txtexpamt").val();
    result = '';
    if (txtdate.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtdates').css('border-color', 'red');
    } else if (txtdate.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtdates').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team Name is Required<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team Nameis Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
    
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    var hdval = $("#tblchruch tr:last").is(":visible");
    var tt;
    if (hdval == false) {
        tt = Number(count) - 1;
    }
    else { tt = Number(count); }
    for (var i = 0; i < tt; i++) {
        var expamt = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(11) input').val().replace(/,/g, "");
        var bal = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(10) input').val().replace(/,/g, "");
        var dlvalue = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(12) option:selected').val();//jQuery("#ddlexpcatg").val();
        if (expamt == '') {
            result += 'Required Details are missing.<br/>';
        }
        else if (expamt == "0") {
            result += 'Required Details are missing.<br/>';
        }
        else if (dlvalue == "0") {
            result += 'Required Details are missing.<br/>';
        }
        else if (Number(expamt) > Number(bal)) {
            result += 'Expenses Amount should not exceed Balance Amount<br/>';
        }
        else { }
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(2) input').val();
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val().replace(/,/g, "");
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(2) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val().replace(/,/g, "");
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '') {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                result += 'Duplicate Row.Please Remove.<br>';
                //type = 'danger';
                //notifyMessage(message, type);
                duplicaterow = false;
                //return;
            } else {
                duplicaterow = true;
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
    return result;
}
function validateExpensem() {
    var txtdates = document.getElementById("txtdates");
    var ddltd = document.getElementById('ddltd');
    var txtrem = document.getElementById("txtrem");
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById("txtfile");
    var txtexpamt = jQuery("#txtexpamt").val();
    var file1 = document.getElementById("fudocmst");

    result = '';
    if (txtdates.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtdates').css('border-color', 'red');
    } else if (txtdates.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtdates').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team Name is Required<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team Nameis Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
    if (hdnEmployee.value != 'U') {
        if (file1.value.trim().length == 0) {
            result += 'Upload Master Document Required<br/>';
            jQuery('#fudocmst').css('border-color', 'red');
        }
    }
   
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    var hdval = $("#tblchruch tr:last").is(":visible");
    var tt;
    if (hdval == false) {
        tt = Number(count) - 1;
    }
    else { tt = Number(count); }
    for (var i = 0; i < tt; i++) {
        var expamt = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(11) input').val().replace(/,/g, "");
        var bal = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(10) input').val().replace(/,/g, "");
        var dlvalue = jQuery('#tblchruch tbody tr:eq(' + i + ')').find('td:eq(12) option:selected').val();
        //var expcat = jQuery("#ddlexpcat > option:selected").attr("value");
        if (expamt == '' ) {
            result += 'Required Details are missing.<br/>';
        }
        else if (expamt == "0") {
            result += 'Required Details are missing.<br/>';
        }
        else if (dlvalue == "0") {
            result += 'Required Details are missing.<br/>';
        }
        else if (Number(expamt) > Number(bal)) {
            result += 'Expenses Amount should not exceed Balance Amount<br/>';
        }
        else { }
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(2) input').val();
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val().replace(/,/g, "");
        var expcatg = jQuery(table).eq(i).find('td:eq(12) option:selected').val();
    //    var ddlexpcat = jQuery(table).eq(i).find('td:eq(12) option:selected').val();
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(2) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val().replace(/,/g, "");
            var expcatg1 = jQuery(table).eq(j).find('td:eq(12) option:selected').val()
           // var ddlexpcat1 = jQuery(table).eq(i).find('td:eq(12) option:selected').val();
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '' && expcatg1 == expcatg ) {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                result += 'Duplicate Row.Please Remove.<br>';
                //type = 'danger';
                //notifyMessage(message, type);
                duplicaterow = false;
                //return;
            } else {
                duplicaterow = true;
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }
    return result;
}
function getSMTPID(glob) {
    jQuery.noConflict();
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function ChangeValuee(e) {
    var txtamt = document.getElementById("txtamt");
    var txtspend = document.getElementById("txtspend");
    var txtbal = document.getElementById("txtbal");
    var txtexpamt = document.getElementById("txtexpamt");
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length - 1;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    txtamt.value = Comma(tot + Number(e.value.replace(/,/g, "")));
    txtspend.value = Comma(tot + Number(e.value.replace(/,/g, "")));
    txtbal.value = Comma(tot + Number(e.value.replace(/,/g, "")));
    txtexpamt.value = Comma(tot + Number(e.value.replace(/,/g, "")));
}
function callamounte() {
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    return Comma(tot);
}
 
function viewfilejsret(e) {
    var row = e.closest('tr').rowIndex;
    var d = Number(row) - 1;
    var filename = jQuery('#tblchruch tbody tr:eq(' + d + ')').find('td:eq(14) input').val();
    var rowval = jQuery('#tblchruch tbody tr:eq(' + d + ')').find('td:eq(17) input').val();



    if (filename != "null") {
        var a = document.getElementById('afu' + rowval);
        a.href = '/ExpensesMasterApp/OpenFile?fname=' + filename;
    }
}

function viewfilejs(e) {
    var row = e.closest('tr').rowIndex;
    var d = Number(row) - 1;
    var filename = jQuery('#tblchruch tbody tr:eq(' + d + ')').find('td:eq(14) input').val();
    var rowval = jQuery('#tblchruch tbody tr:eq(' + d + ')').find('td:eq(15) input').val();



    if (filename != "") {
        var a = document.getElementById('afu'+rowval); 
        a.href = '/ExpensesMasterApp/OpenFile?fname=' + filename ;
    }
}

//approve
function validateExpenseApp() {
    result = "";
    var txtdate = document.getElementById("txtdate");
    var ddltd = document.getElementById('ddltd');
    var txtrem = document.getElementById("txtrem");
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById("txtfile");
    var txtexpamt = jQuery("#txtexpamt").val();
    result = '';
    if (txtdate.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtdate').css('border-color', 'red');
    } else if (txtdate.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtdate').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team Name is Required<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team Nameis Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
  
    //if (txttype.value.trim().length == 0) {
    //    result += 'Content Type is Required<br/>';
    //    jQuery('#txttype').css('border-color', 'red');
    //} else if (txttype.value.trim().length > 100) {
    //    result += 'Content Type Maximum Length is 100.<br/>';
    //    jQuery('#txttype').css('border-color', 'red');
    //} else {
    //    var d = alphaFewsymlatest(txttype.value.trim());
    //    if (d != false) {
    //        result += d;
    //        jQuery('#txttype').css('border-color', 'red');
    //    }
    //}
    //if (txtfile.value.trim().length == 0) {
    //    result += 'File Name is Required<br/>';
    //    jQuery('#txtfile').css('border-color', 'red');
    //} else if (txtfile.value.trim().length > 100) {
    //    result += 'File Name Maximum Length is 100.<br/>';
    //    jQuery('#txtfile').css('border-color', 'red');
    //} else {
    //    var d = alphaFewsymlatest(txtfile.value.trim());
    //    if (d != false) {
    //        result += d;
    //        jQuery('#txtfile').css('border-color', 'red');
    //    }
    //}
    var hd = $("#tblchruch tr:last").is(":visible");
    if (hd == true) {
        if (txtexpamt == '') {
            result += 'Required Details are missing.';
        }
        ////if (Number(famt) < Number(tamt)) {
        ////    result += 'Expenses Amount should not be less than Balance Spent<br/>';
        ////}
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length - 1;
    for (var i = 1; i <= count; i++) {
        var expamt = jQuery(table).eq(i).find('td:eq(11) input').val();
        if (expamt == '') {
            result += 'Required Details are missing.';
        }
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(1) input').val();
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val();
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val();
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '') {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = 'Duplicate Row.Please Remove.<br>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = false;
                return;
            } else {
                duplicaterow = true;
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }

    
   
    return result;
}


function validateExpenseAppret() {
    result = "";
    var txtdate = document.getElementById("txtdate");
    var ddltd = document.getElementById('ddltd');
    var txtrem = document.getElementById("txtrem");
    //var txttype = document.getElementById("txttype");
    //var txtfile = document.getElementById("txtfile");
    var txtexpamt = jQuery("#txtexpamt").val();
    result = '';
    if (txtdate.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtdate').css('border-color', 'red');
    } else if (txtdate.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtdate').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team Name is Required<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team Nameis Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }

    var hd = $("#tblchruch tr:last").is(":visible");
    if (hd == true) {
        if (txtexpamt == '') {
            result += 'Required Details are missing.';
        }
        ////if (Number(famt) < Number(tamt)) {
        ////    result += 'Expenses Amount should not be less than Balance Spent<br/>';
        ////}
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length - 1;
    for (var i = 1; i <= count; i++) {
        var expamt = jQuery(table).eq(i).find('td:eq(11) input').val();
        if (expamt == '') {
            result += 'Required Details are missing.';
        }
    }
    var table = "#tblchruch tr";
    var count = jQuery(table).not("thead tr").length;
    for (var i = 1; i <= count; i++) {
        var bd = jQuery(table).eq(i).find('td:eq(1) input').val();
        var exp = jQuery(table).eq(i).find('td:eq(11) input').val();
        for (var j = i + 1; j <= count; j++) {
            var bd1 = jQuery(table).eq(j).find('td:eq(1) input').val();
            var exp1 = jQuery(table).eq(j).find('td:eq(11) input').val();
            if (bd == bd1 && exp == exp1 && bd != '' && exp != '') {
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'Red');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'Red');
                message = 'Duplicate Row.Please Remove.<br>';
                type = 'danger';
                notifyMessage(message, type);
                duplicaterow = false;
                return;
            } else {
                duplicaterow = true;
                var m = Number(i) - 1;
                var s = Number(j) - 1;
                jQuery('#tblchruch tbody tr:eq(' + m + ')').css('backgroundColor', 'White');
                jQuery('#tblchruch tbody tr:eq(' + s + ')').css('backgroundColor', 'White');
            }
        }
    }

     

    return result;
}



