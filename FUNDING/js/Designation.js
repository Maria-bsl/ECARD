var cid;
var check;
var desg = "";
var jk = false;
function validDeg(e) {
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
function resetDesignation() {
    var txtdesg = document.getElementById("txtdesg");
    txtdesg.value = '';
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtdesg').prop("disabled", false);
    jQuery('#txtdesg').css('border-color', '');
    lblError.innerHTML = '';
}

function getdesignationValues() {
    var txtdesg = document.getElementById("txtdesg");
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
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
            desg: txtdesg.value.trim(),
            sno: txtSID.value,
            dummy: check,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = decodeHTMLEntities(rows[i].cells[1].innerHTML.trim());
            var newname = txtdesg.value.trim();
            if (desg == newname) {
                check = true;
                break;
            }
            if (name.toLowerCase() == newname.toLowerCase() && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                desg: txtdesg.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data = {
                desg: txtdesg.value.trim(),
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
function validateDesignation() {
    var txtdesg = document.getElementById('txtdesg');
    result = '';
    if (txtdesg.value.trim().length == 0) {
        result += 'Designation is Required.<br/>';
        jQuery('#txtdesg').css('border-color', 'red');
    } else if (txtdesg.value.trim().length > 200) {
        result += 'Designation Maximum Length is 200.<br/>';
        jQuery('#txtdesg').css('border-color', 'red');
    } else {
        var d = alphaFewsymlatest(txtdesg.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtdesg').css('border-color', 'red');
        }
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