var cid;
var check;
var valteam = "";
var doublemoble = true;
var doublecheckemail = true;
var doubleemail;
var jk = false;
function validteam(e) {
    jk = true;
}
function resetTeam() {
    txtSID = document.getElementById('txtSID');
    txtSID = 0;
    jQuery("#txtname").val('');
    jQuery("#txtmem").val('');
    jQuery("#txtprsn").val('');
    jQuery("#txtmob").val('');
    jQuery("#txtemail").val('');
    jQuery('#txtmem').css('border-color', '');
    jQuery('#txtname').css('border-color', '');
    jQuery('#txtprsn').css('border-color', '');
    jQuery('#txtmob').css('border-color', '');
    jQuery('#txtemail').css('border-color', '');
    lbl1.innerHTML = '';
    lbl2.innerHTML = '';
    lbl3.innerHTML = '';
    lblErrormob1.innerHTML = '';
    lblErroremail1.innerHTML = '';
}

function getTeamDetailsValues() {
    if (doublemoble == false) {
        jQuery("#loader").css("display", "none");
        message = error;
        type = 'danger';
        notifyMessage(message, type);
    }
    else {
        var txtmem = document.getElementById('txtmem');
        var txtname = document.getElementById('txtname');
        var txtprsn = document.getElementById('txtprsn');
        var txtmob = document.getElementById('txtmob');
        var txtemail = document.getElementById('txtemail');
        var txtcomt = document.getElementById('txtcomt');
        txtSID = document.getElementById('txtSID'),
            tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows,
            hdnEmployee = document.getElementById('hdnEmployee');
        rblreg = jQuery("input[name='gender']:checked");
        if (txtSID.value == '') {
            txtSID.value = '0';
        }
        var table = jQuery('#tbl-smtp').DataTable(),
            selectedRow = table.rows('.selected').data();
        var check = true;
        if (hdnEmployee.value == "C") {
            txtSID.value = '0';
            jk = false;
            var data = {
                name: txtname.value.trim(),
                members: txtmem.value.trim(),
                person: txtprsn.value.trim(),
                mobile: txtmob.value.trim(),
                email: txtemail.value.trim(),
                status: rblreg.val(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {
            var table = $('#tbl-smtp').DataTable();
            var row = table.rows({ page: 'all' }).nodes();
            for (var i = 0; i < row.length; i++) {
                var name = row[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtname.value.toLowerCase().trim();
                if (name == newname && jk == true) {
                    check = false;
                }
                else { }
            }
            if (check == true) {
                var data = {
                    name: txtname.value.trim(),
                    members: txtmem.value.trim(),
                    person: txtprsn.value.trim(),
                    mobile: txtmob.value.trim(),
                    email: txtemail.value.trim(),
                    status: rblreg.val(),
                     sno: txtSID.value,
                    cmt: txtcomt.value.trim(),
                    dummy: check,
                }
            }
            else {
                var data =
                {
                    name: txtname.value.trim(),
                    members: txtmem.value.trim(),
                    person: txtprsn.value.trim(),
                    mobile: txtmob.value.trim(),
                    email: txtemail.value.trim(),
                    status: rblreg.val(),
                    sno: txtSID.value,
                    cmt: txtcomt.value.trim(),
                    dummy: check,
                }
            }
            return data;
        }
    }
}
//approve
function showAppTeamDetailsModal(e) {
    valteam = "";
    var txtmem = document.getElementById('txtmem');
    var txtname = document.getElementById('txtname');
    var txtprsn = document.getElementById('txtprsn');
    var txtmob = document.getElementById('txtmob');
    var txtemail = document.getElementById('txtemail');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    doublemoble = true;
    jk = false;
    if (e.value.indexOf('Edit') > -1) {
        jQuery("#lblmsg").hide();
        jQuery('#txtcomt').val("");
        jQuery("#btnSubmit").html("Approve");
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[8].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                txtmem.value = row.cells[2].innerHTML;
                txtname.value = decodeHTMLEntities(row.cells[1].innerHTML);
                txtprsn.value = decodeHTMLEntities(row.cells[3].innerHTML);
                txtmob.value = row.cells[4].innerHTML;
                txtemail.value = decodeHTMLEntities(row.cells[5].innerHTML);
                valcur = decodeHTMLEntities(row.cells[1].innerHTML);

                var flag = row.cells[7].innerHTML;
                if (flag == 'Active') {
                    jQuery("#rbtrue").prop("checked", true);
                    jQuery("#rbfalse").prop("checked", false);
                }
                else if (flag == 'InActive') {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", true);
                }
                else {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", false);
                }
                bindCommentDetails(txtSID.value);
                break;
            }
        }
        hdnEmployee.value = 'U';
        bindCommentDetails(jQuery(e).data('sid'));
        jQuery("#tblcmt").show();
    }
}
//return

function getTeamDetailsID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateTeam() {
    var txtmem = document.getElementById('txtmem');
    var txtname = document.getElementById('txtname');
    var txtprsn = document.getElementById('txtprsn');
    var txtmob = document.getElementById('txtmob');
    var txtemail = document.getElementById('txtemail');
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    var a = validateMobNum.test(mobileNum);
    var email = jQuery('#txtemail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    var b = reg.test(email);
    result = '';
    if (txtname.value.trim().length == 0) {
        result += 'Team Name is required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 200) {
        result += 'Team Name Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alpha(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }
    if (txtmem.value.trim().length == 0) {
        result += 'Team Members is required.<br/>';
        jQuery('#txtmem').css('border-color', 'red');
    } else if (txtmem.value.trim().length > 3) {
        result += 'Team Members Maximum Length is 3.<br/>';
        jQuery('#txtmem').css('border-color', 'red');
    }
    if (txtprsn.value.trim().length == 0) {
        result += 'Contact Person is required.<br/>';
        jQuery('#txtprsn').css('border-color', 'red');
    } else if (txtprsn.value.trim().length > 100) {
        result += 'Contact Person Maximum Length is 100.<br/>';
        jQuery('#txtprsn').css('border-color', 'red');
    }
    else {
        var d = alpha(txtprsn.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtprsn').css('border-color', 'red');
        }
    }
    if (txtmob.value.trim().length == 0) {
        result += ' Mobile is Required<br/>';
        jQuery('#txtmob').css('border-color', 'red');
    } else if (doublemoble == false) {
       // result += error + ".<br/>";
        result += 'Invalid Mobile Number/Mobile Number Should be 12 digits<br/>';
        jQuery('#txtmob').css('border-color', 'red');
    }
    if (txtemail.value.trim().length == 0) {
        result += 'Email is Required<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    } else if (txtemail.value.trim().length > 50) {
        result += 'Email Maximum Length is 50.<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    } else if (doubleemail == false) {
        result += 'Invalid Email<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    }
    return result;
}
function deleteteamGrid() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');
    table.row(selectedRow).remove().draw();
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
function ValidateEmailTeam(e) {
    var email = jQuery('#txtemail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    if (reg.test(email)) {
        lblErroremail1.innerHTML = "";
        doubleemail = true;
    }
    else {

        doubleemail = false;
        jQuery('#lblErroremail1').show();
        error = "Invalid Email";
        lblErroremail1.innerHTML = "Invalid Email";
        jQuery('#lblErroremail1').css('color', 'Red');
        e.style.borderColor = "red";
    }
}
function mobileNumTeamValidate1(e) {
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (mobileNum.substr(0, 4) != 0000 || mobileNum == '') {
        if (validateMobNum.test(mobileNum) && mobileNum.length == 12) {
            lblErrormob1.innerHTML = "";
            doublemoble = true;
            e.style.borderColor = "";
            jQuery('#lblErrormob1').hide();
        }
        else {
            doublemoble = false;
            lblErrormob1.innerHTML = "";
           // jQuery('#lblErrormob1').show();
            error = "Mobile should be 12 digits";
            //lblErrormob1.innerHTML = "Mobile should be 12 digits";
            //jQuery('#lblErrormob1').css('color', 'Red');
            //e.style.borderColor = "red";
        }
    }
    else {
        doublemoble = false;
        jQuery('#lblErrormob1').show();
        error = "Invalid Mobile";
        lblErrormob1.innerHTML = " Invalid Mobile";
        jQuery('#lblErrormob1').css('color', 'Red');
        e.style.borderColor = "red";
    }
} 
function textteam(e) {
    jQuery('#txtname').css('border-color', '');
}
function textteam1(e) {
    jQuery('#txtmem').css('border-color', '');
}
function textteam2(e) {
    jQuery('#txtprsn').css('border-color', '');
}
function textteam3(e) {
    jQuery('#txtmob').css('border-color', '');
}
function textteam4(e) {
    jQuery('#txtemail').css('border-color', '');
}