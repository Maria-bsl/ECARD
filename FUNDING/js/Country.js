var cid;
var check;
var cou = "";
var jk = false;
function valid(e) {
    jk = true;
}
//function maxcou(e) {
//    var txtname = document.getElementById("txtname");
//    var name = txtname.value;
//    if (name.length > 200) {
//        lblErrorCou.innerHTML = "Maximum Length Exceeded";
//        jQuery('#lblErrorCou').css('color', 'Red');
//    }
//    else {
//        lblErrorCou.innerHTML = '';
//    }
//}
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
function resetCou() {
    var txtname = document.getElementById('txtname');
    txtSID = document.getElementById('txtSID');
    txtSID =
    txtname.value = '';
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtname').prop("disabled", false);
    lblErrorCou.innerHTML = '';
    jQuery('#txtname').css('border-color', '');
}

function getSMTPValuesCou() {
    var txtname = document.getElementById('txtname');   
        txtSID = document.getElementById('txtSID');
    hdnEmployee = document.getElementById('hdnEmployee');
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
            country_name: txtname.value.trim(),
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
            if (cou == newname)//userinput
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
                country_name: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data = {
                country_name: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;
    }   
}
function validateEmployeecou() {
    var txtname = document.getElementById("txtname");   
    txtSID = document.getElementById('txtDID');
    result = '';   
    if (txtname.value.trim().length == 0) {
        result += 'Country is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 200) {
        result += 'Country Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
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
        rowNumber = Index - 1
    }
}