var cid;
var king = true;
function ValidateEmail() {
    var email = document.getElementById('txtEmail').value;
    var reg = /([a-zA-Z0-9]+)([\_\.\-{1}])?([a-zA-Z0-9]+)\@@([a-zA-Z0-9]+)([\.])([a-zA-Z\.]+)/g;
    if (reg.test(email)) {
        lblError.innerHTML = "";
        king = true;
    }
    else {
        //lblError.innerHTML = "Invalid email address.";
        jQuery('#lblError').css('color', 'Red');
        king = false;
    }
}
//function checkduplicate(e) {
//    jQuery(document).ready(function () {
//        $("#txtUname").keydown(function (event) {
//            if (event.keyCode == 32) {
//                event.preventDefault();
//            }
//        });
//    });
//    var name = document.getElementById('txtUname').value;
//    binddupliacteDetails(name);
//}
function resetSMTP() {
    var txtEmail = document.getElementById('txtEmail');
    var txtAdd = document.getElementById('txtAdd');
    var txtPort = document.getElementById('txtPort');
    var txtUname = document.getElementById('txtUname');
    txtPass = document.getElementById('txtPass');
    var radios = jQuery('input:radio[name=gender]');
    if (radios.is(':checked') == false) {
        radios.filter('[value=Active]').prop('checked', true);
    }
    txtEmail.value = '';
    txtAdd.value = '';
    txtPort.value = '';
    txtUname.value = '';
    txtPass.value = '';
}

function getSMTPValuessmtp() {
    var txtEmail = document.getElementById('txtEmail'),
        txtAdd = document.getElementById('txtAdd'),
        txtPort = document.getElementById('txtPort'),
        txtUname = document.getElementById('txtUname'),
        txtPass = document.getElementById('txtPass'),
        txtSID = document.getElementById('txtSID'),
        rblGender = jQuery("input[name='gender']:checked"),
        tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    king = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            from_address: txtEmail.value.trim(),
            smtp_address: txtAdd.value.trim(),
            smtp_port: txtPort.value.trim(),
            smtp_uname: txtUname.value.trim(),
            smtp_pwd: txtPass.value.trim(),
            sno: txtSID.value,
            gender: rblGender.val(),
        }
        return data;
    }
    else if (hdnEmployee.value == "U" && king == true) {
        var data = {
            from_address: txtEmail.value.trim(),
            smtp_address: txtAdd.value.trim(),
            smtp_port: txtPort.value.trim(),
            smtp_uname: txtUname.value.trim(),
            smtp_pwd: txtPass.value.trim(),
            sno: txtSID.value,
            gender: rblGender.val(),
        }
        return data;
    }
    //return data;
}
function getSMTPID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateEmployeesmtp() {
    var txtEmail = document.getElementById('txtEmail');
    var txtAdd = document.getElementById('txtAdd');
    var txtPort = document.getElementById('txtPort');
    var txtUname = document.getElementById('txtUname');
    var txtPass = document.getElementById('txtPass');
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'Email ID is Required.<br/>';
    }
    if (chosen == "") {
        result += 'Ssl Enable is Required .<br/>';
    }
    if (txtAdd.value.trim().length == 0) {
        result += 'Smtp Address is Required.<br/>';
    }
    if (txtPort.value.trim().length == 0) {
        result += 'Smtp Port is Required.<br/>';
    }
    if (txtUname.value.trim().length == 0) {
        result += 'Username is Required.<br/>';
    }
    if (txtPass.value.trim().length == 0) {
        result += 'Password is Required .<br/>';
    }
    return result;
}