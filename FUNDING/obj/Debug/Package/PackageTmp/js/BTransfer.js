var cid;
var check;
var btrans = "";
var jk = false, ak = false, pk = false, sk = false;
var vl_transfer_curr_month;
function ValidBT(e) {
    jk = true;
}
function ValidBT1(e) {
    ak = true;
}
function ValidBT2(e) {
    pk = true;
}
function ValidBT3(e) {
    sk = true;
}
function hideModal(e) {
    jQuery('#' + e).modal('hide');
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
function resetBTran() {
    var txtname = document.getElementById("txtname");
    var txtamt = document.getElementById("txtamt");
    var txtrem = document.getElementById("txtrem");
    var txtcn = document.getElementById("txtcn");
    var file = document.getElementById("file");

    var budgamt = document.getElementById("txtbudgamt");
    var expamt = document.getElementById("txtexpamt");
    var transamt = document.getElementById("txttransamt");
    var sponsermonthyear = document.getElementById("Txtsonsermonthyear");
    var ddlseason = document.getElementById("ddlseason");
    var ddlsponser = document.getElementById("ddlsponser");
    var ddltd = document.getElementById("ddltd");

    //txtname.value = '';
    txtamt.value = '';
    txtrem.value = '';
    txtcn.value = '';
    fudoc.value = '';
    budgamt.value = '';
    expamt.value = '';
    transamt.value = '';
    sponsermonthyear.value = '';

     
     
    //jQuery("#ddlsponser").val(0).change();
    jQuery("#ddlboard").val(0).change();
    jQuery("#ddlcurr").val(0).change();
    jQuery("#ddlseason").val(0).change();
    jQuery("#ddlsponser").val(0).change();
    jQuery("#ddltd").val(0).change();

    jQuery('#txtname').css('border-color', '');
    jQuery('#ddltd').css('border-color', '');
    jQuery('#txtamt').css('border-color', '');
    jQuery('#txtcn').css('border-color', '');
    jQuery('#fudoc').css('border-color', '');
    jQuery("#tbl-smtp1 tbody").empty()
    jQuery("#txtname").prop('disabled', false);
    jQuery("#txtname1").prop('disabled', false);
    jQuery("#txtBID").prop("disabled", false);
    jQuery("#txtbd").prop("disabled", false);
    jQuery('#txtsp').prop("disabled", false);
    jQuery('#txtcur').prop("disabled", false);
    jQuery('#ddltd').prop("disabled", false);
    jQuery('#txtamt').prop("disabled", false);
    jQuery('#txtamt1').prop("disabled", false);
    jQuery('#txtcn').prop("disabled", false);
    jQuery('#txtrem').prop("disabled", false);
    jQuery('#file').prop("disabled", false);


    jQuery('#ddlboard').prop("disabled", false);
    jQuery('#ddlsponser').prop("disabled", false);
    jQuery('#ddlcurr').prop("disabled", false);
    jQuery('#ddlseason').prop("disabled", false);

    lbl.innerHTML = '';
    lblErrorComt.innerHTML = '';
    //lblErrornon.innerHTML = '';
} 
function getSMTPValuesBtansfer() {
    var data = new FormData();

    var tdboard = document.getElementById("ddlboard");
    var tdbvalue = tdboard.options[tdboard.selectedIndex].value;

    var tdsponser = document.getElementById("ddlsponser");
    var tdsvalue = tdsponser.options[tdsponser.selectedIndex].value;

    var tdcurrency = document.getElementById("ddlcurr");
    var txtCUId = tdcurrency.options[tdcurrency.selectedIndex].value;

    var txtname = document.getElementById("txtname");
 //////////////   var txtname1 = document.getElementById("txtname1");
    var td = document.getElementById("ddltd");
    var tdvalue = td.options[td.selectedIndex].value;
    var txtamt = document.getElementById("txtamt");
    var txtamt1 = document.getElementById("txtamt1");
    var txtSTNO = document.getElementById("txtSTNO");
    var txtrem = document.getElementById("txtrem");
    var txtcomt = document.getElementById('txtcomt');
    var txtcn = document.getElementById("txtcn");
    var file = document.getElementById("fudoc").files[0];
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
        txtbd = document.getElementById('txtbd');
        //txtCUId = document.getElementById('txtCUId');
        txtSSID = document.getElementById('txtSSID');
    hdnEmployee = document.getElementById('hdnEmployee');

    var tseason = ddlseason.options[ddlseason.selectedIndex].text;
    var tseasonval = ddlseason.options[ddlseason.selectedIndex].value;

    var eid = jQuery("#txtbd").val();
    var eid = jQuery("#txtCUId").val();
    var eid = jQuery("#txtSSID").val();
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        data.append("fileUpload", file);
        data.append("date", txtname.value.trim());
        data.append("spno", tdsvalue);
        data.append("bdno", tdbvalue);
        data.append("tno", tdvalue);
        data.append("code", txtCUId);
        data.append("amt", txtamt.value.trim());
        data.append("remark", txtrem.value.trim());
        data.append("stno", 0);
        //data.append("date1", txtname1.value.trim());
        data.append("tramt", txtamt1.value.trim());
        data.append("sno", txtSID.value);
        data.append("cmt", txtcomt.value.trim());
        data.append("dummy", check);
        data.append("cheque", txtcn.value.trim());
        data.append("seasonval", tseasonval);
        /*var data = {
            date: txtname.value.trim(),
            bdno: txtbd.value,
            spno: txtSSID.value,
            tno: tdvalue,
            code: txtCUId.value,
            amt: txtamt.value.trim(),
            remark: txtrem.value.trim(),
            stno: txtSTNO.value,
            date1: txtname1.value.trim(),
            tramt: txtamt1.value.trim(),
            sno: txtSID.value,
            cmt: txtcomt.value.trim(),
            dummy: check,
        }*/
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = rows[i].cells[1].innerHTML;
            var newname = txtname.value.trim();
            if (btrans == newname)//userinput
            {
                check = true;
                break;
            }
            if (name.toLowerCase() == newname.toLowerCase().trim() && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", tdsvalue);
            data.append("bdno", tdbvalue);
            data.append("tno", tdvalue);
            data.append("code", txtCUId);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("stno", 0);
            //data.append("date1", txtname1.value.trim());
            data.append("tramt", txtamt1.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
            data.append("seasonval", tseasonval);
            /*var data = {
                date: txtname.value.trim(),
                bdno: txtbd.value,
                spno: txtSSID.value,
                tno: tdvalue,
                code: txtCUId.value,
                amt: txtamt.value.trim(),
                remark: txtrem.value.trim(),
                stno: txtSTNO.value,
                date1: txtname1.value.trim(),
                tramt: txtamt1.value.trim(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }*/
        }
        else {
            /*var data = {
                date: txtname.value.trim(),
                bdno: txtbd.value,
                spno: txtSSID.value,
                tno: tdvalue,
                code: txtCUId.value,
                amt: txtamt.value.trim(),
                remark: txtrem.value.trim(),
                stno: txtSTNO.value,
                date1: txtname1.value.trim(),
                tramt: txtamt1.value.trim(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }*/
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", tdsvalue);
            data.append("bdno", tdbvalue);
            data.append("tno", tdvalue);
            data.append("code", txtCUId);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("stno", 0);
            //data.append("date1", txtname1.value.trim());
            data.append("tramt", txtamt1.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
            data.append("seasonval", tseasonval);
        }
        return data;
    }
}
function getSMTPValuesBtansfer_App() {
    var txtcomt = document.getElementById('txtcomt');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
    txtbd = document.getElementById('txtbd');
    txtCUId = document.getElementById('txtCUId');
    txtSSID = document.getElementById('txtSSID');
    hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    check = true;
    var data = {
        sno: txtSID.value,
        cmt: txtcomt.value.trim(),
    }
    return data;   
}

function Validatebtt(file) {
    result = '';
    var numBytes = document.getElementById(file.id).files[0].size;//2097152
    var filename = document.getElementById(file.id).files[0].name;
    filename = filename.substring(filename.lastIndexOf(".") + 1, filename.length).toLowerCase()
    if (filename != 'xls' && filename != 'xlsx' &&  filename != 'jpg' && filename != 'jpeg' && filename != 'png' && filename != 'gif' && filename != 'pdf') {
        document.getElementById(file.id).value = null;
        result += 'Upload file extension should be Excel or,jpg or jpeg or png or gif or pdf only<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    else if (numBytes > 2097152) {
        document.getElementById(file.id).value = null;
        result += 'Upload file should not exceeds 2 MB<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    return true;
}

function Validatetransferamt(e) {
    result = '';
    var budgamt = jQuery('#txtbudgamt').val().replace(/,/g, ""); // budget Amount
    var balamt = jQuery('#txtamt1').val().replace(/,/g, "");// balance amount
    var transferamt = jQuery('#txtamt').val().replace(/,/g, ""); // Tranfer Amount

    var netamt = Number(budgamt.replace(/,/g, "")) - Number(balamt.replace(/,/g, ""))
    if (vl_transfer_curr_month == "Y") {
        if (Number(transferamt) > Number(balamt)) {
            result += 'Amount Should be less than Balance Amount<br/>';
            jQuery('#txtamt').val('');
            notifyMessage(result, 'danger');
        }
    }
    else {
        if (transferamt == 0) {
            result += 'Amount should be Greaterthan 0<br/>';
            jQuery('#txtamt').val('');
            notifyMessage(result, 'danger');
        }
        else if (Number(transferamt) > Number(netamt)) {
            result += 'Amount Should be less than Balance Amount<br/>';
            jQuery('#txtamt').val('');
            notifyMessage(result, 'danger');
        }
    }
    return true
} 

function validateEmployeeBtansfer_ret() {
    var ddltd = document.getElementById("ddltd"); //Team 
    var txtamt = document.getElementById("txtamt");
    var txtcn = document.getElementById("txtcn");
    var file1 = document.getElementById("fudoc");
    result = '';

    if (txtname.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
    if (txtamt.value.trim().length == 0) {
        result += 'Amount is Required<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    } else if (txtamt.value.trim().length > 23) {
        result += 'Amount Maximum Length is 18.<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    }

    var budgamt = jQuery('#txtbudgamt').val().replace(/,/g, "");
    var famt = txtamt1.value.replace(/,/g, "");
    var tamt = txtamt.value.replace(/,/g, "");
    var netamt = Number(budgamt.replace(/,/g, "")) - Number(famt.replace(/,/g, "")) 

    if ((Number(tamt) > Number(netamt))) {
        result += 'Received Amount should not be grater than Balance Amount<br/>';
    }
    if (txtcn.value.trim().length == 0) {
        result += 'Cheque No Required<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    } else if (txtcn.value.trim().length > 100) {
        result += 'Cheque No Maximum Length is 100.<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtcn.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtcn').css('border-color', 'red');
        }
    }
    if (hdnEmployee.value != 'U') {
        if (file1.value.trim().length == 0) {
            result += 'Upload Document Required<br/>';
            jQuery('#fudoc').css('border-color', 'red');
        }
    }
    return result;
}

////15.11.2021
function checkduplicatechqteam(e) {

    var tchqno = document.getElementById("txtcn").value;
   binddupliacteDetailsteam(tchqno);
}

function checkduplicatechqteamret(e) {

    var trchqno = document.getElementById("txtcn").value;
    binddupliacteDetailsteamret(trchqno);
}

function validateEmployeeBtansfer() {
    var txtname = document.getElementById("txtname");

    var ddlboard = document.getElementById("ddlboard");
    var ddlsponser = document.getElementById("ddlsponser");
    var ddltd = document.getElementById("ddltd"); //Team 
    var txtamt = document.getElementById("txtamt");
    var txtcn = document.getElementById("txtcn");
    var file1 = document.getElementById("fudoc");
    result = '';

    jQuery('#ddlboard').css('border-color', '');
    jQuery('#ddlsponser').css('border-color', '');

    if (ddlboard.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlboard').css('border-color', 'red');
    } else if (ddlboard.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlboard').css('border-color', 'red');
    }
    else { jQuery('#ddlboard').css('border-color', ''); }
    if (ddlsponser.value == 0) {
        result += 'Sponser is Required.<br/>';
        jQuery('#ddlsponser').css('border-color', 'red');
    } else if (ddlsponser.value == 0) {
        result += 'Sponser is Required.<br/>';
        jQuery('#ddlsponser').css('border-color', 'red');
    }
    else {
        jQuery('#ddlsponser').css('border-color', '')
    }

    if (txtname.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
    if (txtamt.value.trim().length == 0) {
        result += 'Amount is Required<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    } else if (txtamt.value.trim().length > 23) {
        result += 'Amount Maximum Length is 18.<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    }
    var budgamt = jQuery('#txtbudgamt').val().replace(/,/g, "");
    var famt = txtamt1.value.replace(/,/g, "");
    var tamt = txtamt.value.replace(/,/g, "");
    if (vl_transfer_curr_month == "Y") {
        if (Number(transferamt) > Number(famt)) {
            result += 'Received Amount should not be grater than Balance Amount<br/>';
        }
    }
    else {
        var netamt = Number(budgamt.replace(/,/g, "")) - Number(famt.replace(/,/g, ""))
        if (Number(tamt) > Number(netamt)) {
            result += 'Received Amount should not be grater than Balance Amount<br/>';
        }
    }
    if (txtcn.value.trim().length == 0) {
        result += 'Cheque No Required<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    } else if (txtcn.value.trim().length > 100) {
        result += 'Cheque No Maximum Length is 100.<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtcn.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtcn').css('border-color', 'red');
        }
    }
    if (hdnEmployee.value != 'U') {
        if (file1.value.trim().length == 0) {
            result += 'Upload Document Required<br/>';
            jQuery('#fudoc').css('border-color', 'red');
        }
    }
    return result;
}
function ChangeValueb(e) {
    var txtamt = document.getElementById("txtamt");
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length - 1;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    txtamt.value = Comma(tot + Number(e.value.replace(/,/g, "")));
}
function callamountb() {
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    return Comma(tot);
}
//return
function getSMTPValues_bt_Ret() {
    var data = new FormData();
    var txtname = document.getElementById("txtname");
    //var txtname1 = document.getElementById("txtname1");
    var td = document.getElementById("ddltd");
    var tdvalue = td.options[td.selectedIndex].value;
    var txtamt = document.getElementById("txtamt");
    var txtamt1 = document.getElementById("txtamt1");
    var txtSTNO = document.getElementById("txtSTNO");
    var txtrem = document.getElementById("txtrem");
    var txtcomt = document.getElementById('txtcomt');
    var txtcn = document.getElementById("txtcn");
    var file = document.getElementById("fudoc").files[0];
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
    txtSID = document.getElementById('txtSID');
    txtbd = document.getElementById('txtbd');
    txtCUId = document.getElementById('txtCUId');
    txtSSID = document.getElementById('txtSSID');
    hdnEmployee = document.getElementById('hdnEmployee');
    var eid = jQuery("#txtbd").val();
    var eid = jQuery("#txtCUId").val();
    var eid = jQuery("#txtSSID").val();
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = decodeHTMLEntities(rows[i].cells[1].innerHTML.toLowerCase().trim());
            var newname = txtname.value.toLowerCase().trim();
            var bd = rows[i].cells[2].innerHTML.toLowerCase().trim();
            var newb = txtbd.value.toLowerCase().trim();
            var tt = rows[i].cells[6].innerHTML;
            var newt = tdvalue;
            var at = rows[i].cells[13].innerHTML.toLowerCase().trim();
            var newa = txtamt.value.toLowerCase().trim();
            if (btrans == newname)//userinput
            {
                check = true;
                break;
            }
            else if (name == newname && jk == true) {
                check = true;
            }
            else if (name == newname && jk == false) {
                check = true;
            }
            else if (name != newname && jk == true) {
                check = false;
            }
            else if (bd == newb && ak == true) {
                check = true;
            }
            else if (bd == newb && ak == false) {
                check = true;
            }
            else if (bd != newb && pk == true) {
                check = false;
            }
            else if (tt == newt && pk == true) {
                check = true;
            }
            else if (tt == newt && pk == false) {
                check = true;
            }
            else if (tt != newt && sk == true) {
                check = false;
            }
            else if (at == newa && sk == true) {
                check = true;
            }
            else if (at == newa && sk == false) {
                check = true;
            }
            else if (at != newa && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
           /* var data = {
                date: txtname.value.trim(),
                bdno: txtbd.value,
                spno: txtSSID.value,
                tno: tdvalue,
                code: txtCUId.value,
                amt: txtamt.value.trim(),
                remark: txtrem.value.trim(),
                stno: txtSTNO.value,
                date1: txtname1.value.trim(),
                tramt: txtamt1.value.trim(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }*/
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", txtSSID.value);
            data.append("bdno", txtbd.value);
            data.append("tno", tdvalue);
            data.append("code", txtCUId.value);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("stno", txtSTNO.value);
         //   data.append("date1", txtname1.value.trim());
            data.append("tramt", txtamt1.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
        }
        else {
            /*var data = {
                date: txtname.value.trim(),
                bdno: txtbd.value,
                spno: txtSSID.value,
                tno: tdvalue,
                code: txtCUId.value,
                amt: txtamt.value.trim(),
                remark: txtrem.value.trim(),
                stno: txtSTNO.value,
                date1: txtname1.value.trim(),
                tramt: txtamt1.value.trim(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }*/
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", txtSSID.value);
            data.append("bdno", txtbd.value);
            data.append("tno", tdvalue);
            data.append("code", txtCUId.value);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("stno", txtSTNO.value);
         //   data.append("date1", txtname1.value.trim());
            data.append("tramt", txtamt1.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
        }
        return data;
    }
}
function getID(e) {
    var table = document.getElementById('tbl-smtp'),
        rows = table.rows,
        rowNumber = 0;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        rowNumber = Index - 1;
    }
}
