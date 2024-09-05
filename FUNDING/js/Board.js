var cid;
var check;
var board = "";
var doublemobb = true;
var doublecheckemail = true;
var doubleemail;
var jk = false;
function validBoard(e) {
    jk = true;
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
function ValidateEmailBoard(e) {
    var email = jQuery('#txtemail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    if (reg.test(email)) {
        lblErroremail.innerHTML = "";
        doubleemail = true;
        e.style.borderColor = "";
    }
    else {
        doubleemail = false;
        jQuery('#lblErroremail').show();
        error = "Invalid Email";
        lblErroremail.innerHTML = "Invalid Email";
        jQuery('#lblErroremail').css('color', 'Red');
        e.style.borderColor = "red";
    }
}
function mobileNumBoardValidate(e) {
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    if (mobileNum.substr(0, 4) != 0000 || mobileNum == '') {
        if (validateMobNum.test(mobileNum) && mobileNum.length == 12) {
            lblErrormob.innerHTML = "";
            e.style.borderColor = "";
            jQuery('#lblErrormob').hide();
            doublemobb = true;
        }
        else {
            doublemobb = false;
            lblErrormob.innerHTML = "";
           // jQuery('#lblErrormob').show();
            error = "Mobile should be 12 digits";
            //lblErrormob.innerHTML = "Mobile should be 12 digits";
            //jQuery('#lblErrormob').css('color', 'Red');
            //e.style.borderColor = "red";
        }
    }
    else {
        doublemobb = false;
        jQuery('#lblErrormob').show();
        error = "Invalid Mobile Number";
        lblErrormob.innerHTML = " Invalid Mobile Number";
        jQuery('#lblErrormob').css('color', 'Red');
        e.style.borderColor = "red";
    }
} 
function resetBoard() {
    var txtname = document.getElementById("txtname");
    var txtpobox = document.getElementById("txtpobox");
    var txtadd = document.getElementById("txtadd");
    var ddlreg = document.getElementById("ddlreg");
    var ddldist = document.getElementById("ddldist");
    var ddlward = document.getElementById("ddlward");
    var txtper = document.getElementById("txtper");
    var txtmob = document.getElementById("txtmob");
    var txtemail = document.getElementById("txtemail");
    txtname.value = '';
    txtpobox.value = '';
    txtadd.value = '';
    txtper.value = '';
    txtmob.value = '';
    txtemail.value = '';
    jQuery("#ddlreg").val(0).change();
    jQuery("#ddldist").val(0).change();
    jQuery("#ddlward").val(0).change();
    jQuery('#txtname').css('border-color', '');
    jQuery('#txtpobox').css('border-color', '');
    jQuery('#txtadd').css('border-color', '');
    jQuery('#ddlreg').css('border-color', '');
    jQuery('#ddldist').css('border-color', '');
    jQuery('#ddlward').css('border-color', '');
    jQuery('#txtper').css('border-color', '');
    jQuery('#txtmob').css('border-color', '');
    jQuery('#txtemail').css('border-color', '');
    jQuery("#tbl-smtp1 tbody").empty()
    jQuery("#txtname").prop('disabled', false);
    jQuery("#txtpobox").prop("disabled", false);
    jQuery('#txtadd').prop("disabled", false);
    jQuery('#ddlreg').prop("disabled", false);
    jQuery('#ddldist').prop("disabled", false);
    jQuery('#ddlward').prop("disabled", false);
    jQuery('#txtper').prop("disabled", false);
    jQuery('#txtmob').prop("disabled", false);
    jQuery('#txtemail').prop("disabled", false);
    lblname.innerHTML = '';
    lbladd.innerHTML = '';
    lblper.innerHTML = '';
    lblErrormob.innerHTML = '';
    lblErroremail.innerHTML = '';
    lblErrorComt.innerHTML = '';
} 

function getSMTPValuesBoard() {
    if (doublemobb == false) {
        jQuery("#loader").css("display", "none");
        message = error;
        type = 'danger';
        notifyMessage(message, type);
    }
    else {
        var txtname = document.getElementById("txtname");
        var txtpobox = document.getElementById("txtpobox");
        var txtadd = document.getElementById("txtadd");
        var reg = document.getElementById("ddlreg");
        var regvalue = reg.options[reg.selectedIndex].value;
        var dist = document.getElementById("ddldist");
        var distvalue = dist.options[dist.selectedIndex].value;
        var ward = document.getElementById("ddlward");
        var wardvalue = ward.options[ward.selectedIndex].value;
        var txtper = document.getElementById("txtper");
        var txtmob = document.getElementById("txtmob");
        var txtemail = document.getElementById("txtemail");
        var txtcomt = document.getElementById('txtcomt');
        tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows,
            txtSID = document.getElementById('txtSID');
        hdnEmployee = document.getElementById('hdnEmployee');
        rblreg = jQuery("input[name='gender']:checked");
        if (txtSID.value == '') {
            txtSID.value = '0';
        }
        var table = jQuery('#tbl-smtp').DataTable(),
            selectedRow = table.rows('.selected').data();
        check = true;
        if (hdnEmployee.value == "C") {
            txtSID.value = '0';
            jk = false;
            var data = {
                name: txtname.value.trim(),
                pobox: txtpobox.value.trim(),
                add: txtadd.value.trim(),
                regid: regvalue,
                dsno: distvalue,
                wdno: wardvalue,
                person: txtper.value.trim(),
                mob: txtmob.value.trim(),
                email: txtemail.value.trim(),
                status: rblreg.val(),
                sno: txtSID.value,
                cmt: txtcomt.value.trim(),
                dummy: check,
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {
            var table = jQuery('#tbl-smtp').DataTable();
            var rows = table.rows({ page: 'all' }).nodes();
            for (var i = 0; i < rows.length; i++) {
                var name = decodeHTMLEntities(rows[i].cells[1].innerHTML);
                var newname = txtname.value.trim();
                if (board == newname)//userinput
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
                var data = {
                    name: txtname.value.trim(),
                    pobox: txtpobox.value.trim(),
                    add: txtadd.value.trim(),
                    regid: regvalue,
                    dsno: distvalue,
                    wdno: wardvalue,
                    person: txtper.value.trim(),
                    mob: txtmob.value.trim(),
                    email: txtemail.value.trim(),
                    status: rblreg.val(),
                    sno: txtSID.value,
                    cmt: txtcomt.value.trim(),
                    dummy: check,
                }
            }
            else {
                var data = {
                    name: txtname.value.trim(),
                    pobox: txtpobox.value.trim(),
                    add: txtadd.value.trim(),
                    regid: regvalue,
                    dsno: distvalue,
                    wdno: wardvalue,
                    person: txtper.value.trim(),
                    mob: txtmob.value.trim(),
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
function validateEmployeeBoard() {
    var txtname = document.getElementById("txtname");
    var txtpobox = document.getElementById("txtpobox");
    var txtadd = document.getElementById("txtadd");
    var ddlreg = document.getElementById("ddlreg");
    var ddldist = document.getElementById("ddldist");
    var ddlward = document.getElementById("ddlward");
    var txtper = document.getElementById("txtper");
    var txtmob = document.getElementById("txtmob");
    var txtemail = document.getElementById("txtemail");
    var mobileNum = jQuery('#txtmob').val();
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    var a = validateMobNum.test(mobileNum);
    var email = jQuery('#txtemail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    var b = reg.test(email);
    result = '';
    if (txtname.value.trim().length == 0) {
        result += ' Name is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 200) {
        result += ' Name Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }
    if (txtpobox.value.trim().length == 0) {
        result += 'PostBox is Required<br/>';
        jQuery('#txtpobox').css('border-color', 'red');
    } else if (txtpobox.value.trim().length == 0) {
        result += 'PostBox is Required.<br/>';
        jQuery('#txtpobox').css('border-color', 'red');
    }
    if (txtadd.value.trim().length == 0) {
        result += 'Address is Required<br/>';
        jQuery('#txtadd').css('border-color', 'red');
    } else if (txtadd.value.trim().length > 200) {
        result += 'Address Maximum Length is 200.<br/>';
        jQuery('#txtadd').css('border-color', 'red');
    }
    else {
        var d = alphaFewsAddress(txtadd.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtadd').css('border-color', 'red');
        }
    }
    if (ddlreg.value == 0) {
        result += 'Region is Required.<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    } else if (ddlreg.value == 0) {
        result += 'Region is Required.<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    }
    if (ddldist.value == 0) {
        result += 'District is Required.<br/>';
        jQuery('#ddldist').css('border-color', 'red');
    } else if (ddldist.value == 0) {
        result += 'District is Required.<br/>';
        jQuery('#ddldist').css('border-color', 'red');
    }
    if (ddlward.value == 0) {
        result += 'Ward is Required.<br/>';
        jQuery('#ddlward').css('border-color', 'red');
    } else if (ddlward.value == 0) {
        result += 'Ward is Required.<br/>';
        jQuery('#ddlward').css('border-color', 'red');
    }
    if (txtper.value.trim().length == 0) {
        result += 'Contact Person is Required<br/>';
        jQuery('#txtper').css('border-color', 'red');
    } else if (txtper.value.trim().length > 100) {
        result += 'Contact Person Maximum Length is 100.<br/>';
        jQuery('#txtper').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtper.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtper').css('border-color', 'red');
        }
    }
    if (txtmob.value.trim().length == 0) {
        result += ' Mobile is Required<br/>';
        jQuery('#txtmob').css('border-color', 'red');
    } else if (doublemobb == false) {
        result += 'Invalid Mobile/Mobile Should be 12 digits<br/>';
        jQuery('#txtmob').css('border-color', 'red');
    }
    if (txtemail.value.trim().length == 0) {
        result += 'Email is Required<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    } else if (txtemail.value.trim().length > 50) {
        result += 'Email Maximum Length is 50.<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    } else if (doubleemail == false) {
        result += 'Invalid Email.<br/>';
        jQuery('#txtemail').css('border-color', 'red');
    }
    return result;
}
//approve
function showBoardDModal(e) {
    txtSID = document.getElementById('txtSID');
    var tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    doublemobb = true;
    jk = false;
    if (e.value.indexOf('Edit') > -1) {
        jQuery("#txtcomt").val('');
        jQuery("#lblmsg").hide();
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[15].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                txtname.value = decodeHTMLEntities(row.cells[1].innerHTML);
                txtpobox.value = decodeHTMLEntities(row.cells[2].innerHTML);
                txtadd.value = decodeHTMLEntities(row.cells[3].innerHTML);
                jQuery("#ddlreg").val(row.cells[4].innerHTML).change();
                jQuery("#ddldist").val(row.cells[6].innerHTML).change();
                jQuery("#ddlward").val(row.cells[8].innerHTML).change();
                txtSname.value = row.cells[6].innerHTML;
                txtWname.value = row.cells[8].innerHTML;
                txtper.value = decodeHTMLEntities(row.cells[10].innerHTML);
                txtmob.value = row.cells[11].innerHTML;
                txtemail.value = decodeHTMLEntities(row.cells[12].innerHTML);

                var flag = row.cells[14].innerHTML;
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

function getSMTPValues_bd_Ret() {
    if (doublemobb == false) {
        jQuery("#loader").css("display", "none");
        message = error;
        type = 'danger';
        notifyMessage(message, type);
    }
    else {
        var txtname = document.getElementById("txtname");
        var txtpobox = document.getElementById("txtpobox");
        var txtadd = document.getElementById("txtadd");
        var reg = document.getElementById("ddlreg");
        var regvalue = reg.options[reg.selectedIndex].value;
        var dist = document.getElementById("ddldist");
        var distvalue = dist.options[dist.selectedIndex].value;
        var ward = document.getElementById("ddlward");
        var wardvalue = ward.options[ward.selectedIndex].value;
        var txtper = document.getElementById("txtper");
        var txtmob = document.getElementById("txtmob");
        var txtemail = document.getElementById("txtemail");
        var txtcomt = document.getElementById('txtcomt');
        tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows,
            txtSID = document.getElementById('txtSID');
        hdnEmployee = document.getElementById('hdnEmployee');
        rblreg = jQuery("input[name='gender']:checked");
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
                if (sponser == newname)//userinput
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
                else { }
            }
            if (check == true) {
                var data = {
                    name: txtname.value.trim(),
                    pobox: txtpobox.value.trim(),
                    add: txtadd.value.trim(),
                    regid: regvalue,
                    dsno: distvalue,
                    wdno: wardvalue,
                    person: txtper.value.trim(),
                    mob: txtmob.value.trim(),
                    email: txtemail.value.trim(),
                    status: rblreg.val(),
                    sno: txtSID.value,
                    cmt: txtcomt.value.trim(),
                    dummy: check,
                }
            }
            else {
                var data = {
                    name: txtname.value.trim(),
                    pobox: txtpobox.value.trim(),
                    add: txtadd.value.trim(),
                    regid: regvalue,
                    dsno: distvalue,
                    wdno: wardvalue,
                    person: txtper.value.trim(),
                    mob: txtmob.value.trim(),
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
function getID(e) {
    var table = document.getElementById('tbl-smtp'),
        rows = table.rows,
        rowNumber = 0;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        rowNumber = Index - 1;
    }
}
