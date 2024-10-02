var cid;
var check;
var valc = "";
var jk = false;
function validSec(e) {
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
//function maxsec(e) {
//    var txtname = document.getElementById("txtname");
//    var name = txtname.value;
//    if (name.length > 500) {
//        lblErrorSec.innerHTML = "Maximum Length Exceeded";
//        jQuery('#lblErrorSec').css('color', 'Red');
//    }
//    else {
//        lblErrorSec.innerHTML = '';
//    }
//}
function resetSMTPques() {
    var txtname = document.getElementById('txtname');
    txtSID = document.getElementById('txtSID');
    var radios = jQuery('input:radio[name=gender]');
    if (radios.is(':checked') == false) {
        radios.filter('[value=Active]').prop('checked', true);
    }
    txtname.value = '';
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtname').prop("disabled", false);
    jQuery('#txtname').css('border-color', '');
    lblErrorSec.innerHTML = '';
}

function getSMTPValuesques() {  
    var txtname = document.getElementById('txtname'); 
    txtSID = document.getElementById('txtSID'),
        rblreg = jQuery("input[name='gender']:checked"),
        hdnEmployee = document.getElementById('hdnEmployee');
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
            status: rblreg.val(),
            sno: txtSID.value,
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
            if (valc == newname)//userinput
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
                status: rblreg.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data = {
                name: txtname.value.trim(),
                status: rblreg.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;
    }  
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
function validateEmployeeques() {
    var txtname = document.getElementById('txtname'),  
        txtSID = document.getElementById('txtSID');
    result = '';   
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtname.value.trim().length == 0) {
        result += 'Security Question is Required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 500) {
        result += 'Security Question Maximum Length is 500.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest2(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }   
    if (chosen == "") {
        result += ' Status is Required.<br/>';
    }
    return result;
}
function deleteRowForEmployeeGrid() {
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
