var cid;
var check;
var jk = false;
function validEmail(e) {
    jk = true;
}
var today, datepicker;
today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
$('#txtdate').datepicker({
    uiLibrary: 'bootstrap4',
    format: 'dd/mm/yyyy',
    iconsLibrary: 'fontawesome',
    weekStartDay: 1,
});
function resetSMTPemail() {
    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');
    var txtsubloc = document.getElementById('txtsubloc');
    var txtloc = document.getElementById('txtloctext');
    ddlflow.selectedIndex = 0;
    txttext.value = '',
    txtsub.value = '',
    txtsubloc.value = '',
    txtloc.value = '';
}

function getSMTPValuesemail() {
    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');
    var txtsubloc = document.getElementById('txtsubloc');
    var txtloc = document.getElementById('txtloctext');
    txtSID = document.getElementById('txtSID'),
    hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            flow: ddlflow.value,
            text: txttext.value.trim(),
            loctext: txtloc.value.trim(),
            sub: txtsub.value.trim(),
            subloc: txtsubloc.value.trim(),
            sno: txtSID.value,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var data = {
            flow: ddlflow.value,
            text: txttext.value.trim(),
            loctext: txtloc.value.trim(),
            sub: txtsub.value.trim(),
            subloc: txtsubloc.value.trim(),
            sno: txtSID.value,
        }
        return data;
    }
    else if (hdnEmployee.value == "D") {
        var data = {
            employeeId: selectedRow[0][2],
            employeeName: selectedRow[0][3],
            jobId: 0,
            joined: selectedRow[0][5],
            salary: selectedRow[0][6],
            deptId: 0,
            active: 0,
            opType: hdnEmployee.value,
        }
        return data;
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
function validateEmployeeemail() {
    var txttext = document.getElementById('txtEtext');
    var ddlflow = document.getElementById('ddlflow');
    var txtsub = document.getElementById('txtsub');
    result = '';
    if (ddlflow.value == 0) {
        result += 'Flow Id is Required .<br/>';
    }
    if (txtsub.value.trim().length == 0) {
        result += 'Subject is Required .<br/>';
    }
    if (txttext.value.trim().length == 0) {
        result += ' Email Text is Required.<br/>';
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