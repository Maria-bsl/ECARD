var doublemailemp;
var doubleuser;
var doubleumobile;
var cid;
var check;
var jk = false;
var doublemobemp = true;
var doublecheckemail = true;
var doublecheckusername = true;
function validDet(e) {
    jk = true;
}
function checkduplicate(e) {
    var name = document.getElementById('txtuname').value;
    binddupliacteDetails(name);
}
function checkduplicateempid(e) {
    var name = document.getElementById('txtempid').value;
    binddupliacteDetailsempid(name);
}
function selectteam(e) {

    //var vl_mode = jQuery("#ddldesg").val();
    var vl_mode = ddldesg.options[ddldesg.selectedIndex].text;
    if (vl_mode == "Team") {
        jQuery("#ddlteam").attr("disabled", false);
    }
    else {
        jQuery("#ddlteam").attr("disabled", true);
        jQuery("#ddlteam").val(0).change();
    }

}
function ValidateEmailemp(e) {
  //  doublecheckemail = true;
    var email = jQuery('#txtEmail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    if (reg.test(email)) {
        lblErroremail1.innerHTML = "";
        doublemailemp = true;
        jQuery('#lblErroremail1').hide();
        e.style.borderColor = "";
        binddupliacteDetailsEmail(email);
    }
    else {
        doublemailemp = false;
        jQuery('#lblErroremail1').show();
        error = "Invalid Email ID";
        lblErroremail1.innerHTML = "Invalid Email ID";
        jQuery('#lblErroremail1').css('color', 'Red');
        e.style.borderColor = "red";
        //binddupliacteDetailsEmail(email);
    }
}
function mobileNumEmpValiadte(e) {
    var mobileNum = jQuery('#txtMobile').val();
  //  var prefix = jQuery('#txtMobprefix').val();
    var prefix = jQuery('#txtMobprefix').html();
    var newmob = prefix + mobileNum.toString(); 
    var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    //if (mobileNum.substr(0000, 4) != 0000 || mobileNum == '') {
    if (newmob.substr(0000, 4) != 0000 || newmob == '') {
        if (validateMobNum.test(newmob) && newmob.length == 12) {
            lblError2.innerHTML = "";
            e.style.borderColor = "";
            doublemobemp = true;
        }
        else {
           // jQuery("#lblError2").show();
            doublemobemp = false;
            lblError2.innerHTML = "";
            error = "Mobile Number should be 12 digits";
            //jQuery('#lblError2').css('color', 'Red');
            //lblError2.innerHTML = "Mobile Number.should be 12 digits";
            //e.style.borderColor = "red";
        }
    }
    else {
        jQuery("#lblError2").show();
        doublemobemp = false;
        error = "Invalid Mobile Number.";
        lblError2.innerHTML = "Invalid Mobile Number.";
        jQuery('#lblError2').css('color', 'Red');
        e.style.borderColor = "red";
    }
    binddupliactemobileno(newmob);
}
function resetchru() {
    var txtempid = document.getElementById('txtempid');
    var txtf = document.getElementById('txtfirstname');
    var txtm = document.getElementById('txtmiddle');
    var txtl = document.getElementById('txtlast');
    var ddesg = document.getElementById('ddldesg');
    var txtEmail = document.getElementById('txtEmail');
    var txtmo = document.getElementById('txtMobile');
    var txtunsr = document.getElementById('txtuname');
    var radioValue = jQuery('input[name="gender"]').prop('checked', false);
    var dteam = document.getElementById('ddlteam');
    txtempid.value = '';
    txtf.value = '';
    ddesg.selectedIndex = 0;
   // dteam.selectedIndex = 0;

    txtm.value = '';
    txtl.value = '';
    txtEmail.value = '';
    txtmo.value = '';
    radioValue = '';
    txtunsr.value = ''
    jQuery('#txtempid').css('border-color', '');
    jQuery('#txtfirstname').css('border-color', '');
    jQuery('#txtmiddle').css('border-color', '');
    jQuery('#txtlast').css('border-color', '');
    jQuery('#txtuname').css('border-color', '');
    jQuery('#ddldesg').css('border-color', '');
    jQuery('#txtEmail').css('border-color', '');
    jQuery('#txtMobile').css('border-color', '');
    lblid.innerHTML = "";
    lblf.innerHTML = "";
    lbll.innerHTML = "";
    lblu.innerHTML = "";
    lblErroremail1.innerHTML = "";
    lblError2.innerHTML = "";
  
}
function getSMTPValuesdet() {
    if (doublemobemp == false) {
        jQuery("#loader").css("display", "none");
        message = error;
        type = 'danger';
        notifyMessage(message, type);
    }
    else {
        jQuery("#loader").css("display", "block");
        var txtempid = document.getElementById('txtempid');
        var txtf = document.getElementById('txtfirstname');
        var txtm = document.getElementById('txtmiddle');
        var txtl = document.getElementById('txtlast');
        var ddesg = document.getElementById('ddldesg');
        var txtEmail = document.getElementById('txtEmail');
        var newmob  = document.getElementById('txtMobile');
        var txtunsr = document.getElementById('txtuname');
        var bnvalue = ddesg.options[ddesg.selectedIndex].value;
        txtSID = document.getElementById('txtSID');
        rblGender = jQuery("input[name='gender']:checked");
        hdnEmployee = document.getElementById('hdnEmployee');

        if (ddldesg.options[ddldesg.selectedIndex].text == "Team") {
            var dteam = ddlteam.options[ddlteam.selectedIndex].value;
        }
        else {
            var dteam = 0;
        }

        //var prefix = jQuery('#txtMobprefix').val();
        //var txtmo = prefix + newmob.value; 
        //   var prefix = document.getElementById("txtMobprefix").innerText.toString() ;//jQuery('#txtMobprefix').val();
        var txtmo = document.getElementById("txtMobprefix").innerText + newmob.value;


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
                empid: txtempid.value.trim(),
                fname: txtf.value.trim(),
                mname: txtm.value.trim(),
                lname: txtl.value.trim(),
                desg: bnvalue,
                email: txtEmail.value.trim(),
                mobile: txtmo,
                user: txtunsr.value.trim(),
                gender: rblGender.val(),
                team: dteam,
                sno: txtSID.value,
                dummy: check,
            }
            return data;
        }
        else if (hdnEmployee.value == "U") {
            var table = $('#tbl-smtp').DataTable();
            var row = table.rows({ page: 'all' }).nodes();
            for (var i = 0; i < row.length; i++) {
                var name = row[i].cells[1].innerHTML.toLowerCase().trim();
                var newname = txtempid.value.toLowerCase().trim();
                if (name == newname && jk == true) {
                    check = false;
                }
                else { }
            }
            if (check == true) {
                var data = {
                    empid: txtempid.value.trim(),
                    fname: txtf.value.trim(),
                    mname: txtm.value.trim(),
                    lname: txtl.value.trim(),
                    desg: bnvalue,
                    email: txtEmail.value.trim(),
                    mobile: txtmo,
                    user: txtunsr.value.trim(),
                    sno: txtSID.value,
                    gender: rblGender.val(),
                    team: dteam,
                    dummy: check,
                }
            }
            else {
                var data = {
                    fname: txtf.value.trim(),
                    mname: txtm.value.trim(),
                    lname: txtl.value.trim(),
                    desg: bnvalue,
                    email: txtEmail.value.trim(),
                    mobile: txtmo,
                    user: txtunsr.value.trim(),
                    sno: txtSID.value,
                    gender: rblGender.val(),
                    team: dteam,
                    dummy: check,
                }
            }
            return data;
        }
    }
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
function validateEmployeeemp() {
    var txtempid = document.getElementById('txtempid');
    var txtSEmID = document.getElementById('txtSEmID');
    var txtfirstname = document.getElementById('txtfirstname');
    var txtlast = document.getElementById('txtlast');
    var ddldesg = document.getElementById('ddldesg');
    var txtEmail = document.getElementById('txtEmail');
    var txtMobile = document.getElementById('txtMobile');
    var txtuname = document.getElementById('txtuname');
    var ddlteam = document.getElementById('ddlteam');
    //var mobileNum = jQuery('#txtMobile').val();
    //var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
    //var a = validateMobNum.test(mobileNum);
    var email = jQuery('#txtEmail').val();
    var reg = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;
    var b = reg.test(email);
    rblGender = jQuery("input[name='gender']:checked");

    var prefix = jQuery('#txtMobprefix').html();
    
    var newmob = prefix + txtMobile.value.toString(); 
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtempid.value.trim().length == 0) {
        result += 'Employee ID is required.<br/>';
        jQuery('#txtempid').css('border-color', 'red');
    }
    else if (txtempid.value.trim().length > 50) {
        result += 'Employee ID Maximum Length is 50.<br/>';
        jQuery('#txtempid').css('border-color', 'red');
    } else {
        var d = alpha(txtempid.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtempid').css('border-color', 'red');
        }
    }
    if (txtfirstname.value.trim().length == 0) {
        result += 'First Name is required.<br/>';
        jQuery('#txtfirstname').css('border-color', 'red');
    } else if (txtfirstname.value.trim().length > 50) {
        result += 'First Name Maximum Length is 50.<br/>';
        jQuery('#txtfirstname').css('border-color', 'red');
    } else {
        var d = alpha(txtfirstname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtfirstname').css('border-color', 'red');
        }
    }
    if (txtlast.value.trim().length == 0) {
        result += 'Last Name is required.<br/>';
        jQuery('#txtlast').css('border-color', 'red');
    } else if (txtlast.value.trim().length > 50) {
        result += 'Last Name Maximum Length is 50.<br/>';
        jQuery('#txtlast').css('border-color', 'red');
    } else {
        var d = alpha(txtlast.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtlast').css('border-color', 'red');
        }
    }
    if (txtuname.value.trim().length == 0) {
        result += 'User Name is required.<br/>';
        jQuery('#txtuname').css('border-color', 'red');
    } else if (txtuname.value.trim().length > 50) {
        result += 'User Name Maximum Length is 50.<br/>';
        jQuery('#txtuname').css('border-color', 'red');
    } else {
        var d = alpha(txtuname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtuname').css('border-color', 'red');
        }
    }
    if (ddldesg.value == 0) {
        result += 'Role is required.<br/>';
        jQuery('#ddldesg').css('border-color', 'red');
    } else if (ddldesg.value == 0) {
        result += 'Role is required.<br/>';
        jQuery('#ddldesg').css('border-color', 'red');
    }
    if (ddldesg.value == 4) {
        if (ddlteam.value  == 0) {
            result += 'Team is required.<br/>';
          //  jQuery('#ddlteam').css('border-color', 'red');
        }
        else if (ddlteam.value == "") {
            result += 'Team is required.<br/>';
           // jQuery('#ddlteam').css('border-color', 'red');
        }
    }

    //if (txtMobile.value.trim().length == 0) {
    if (newmob.trim().length == 0) {
        result += 'Mobile Number is required.<br/>';
        jQuery('#txtMobile').css('border-color', 'red');
    } else if (doublemobemp == false) {
        result += 'Invalid Mobile Number/Mobile Number Should be 12 digits<br/>';
        jQuery('#txtMobile').css('border-color', 'red');
    }
    if (txtEmail.value.trim().length == 0) {
        result += 'Email ID is required.<br/>';
        jQuery('#txtEmail').css('border-color', 'red');
    } else if (txtEmail.value.trim().length > 100) {
        result += 'Email ID Maximum Length is 100.<br/>';
        jQuery('#txtEmail').css('border-color', 'red');
    } 
    
    if (chosen == "") {
        result += 'Status is Required.<br/>';
    }
    if (doubleuser == false) {
        result += "Employee ID Already Exists<br/>";
    }
    if (doublecheckemail == false) {
        if (txtEmail.value != txtSEmID.value) {
            result += "Email ID Already Exists.<br/>";
        }
     }
    if (doubleumobile == false) {
        jQuery("#loader").css("display", "none");
        result += "Mobile No Already Exists.<br/>";

    }
    if (doublecheckusername == false) {
        jQuery("#loader").css("display", "none");
        result += "User Name Already Exists.<br/>";
    }
    return result;
}
function deleteSMTPGrid() {
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