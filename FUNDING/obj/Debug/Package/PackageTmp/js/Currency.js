var cid;
var check;
var valcur = "";
var jk = false;
function validCur(e) {
    jk = true;
}
function resetCur() {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");
    txtcode.value = '';
    txtname.value = '';
    jQuery('#txtcode').css('border-color', '');
    jQuery('#txtname').css('border-color', '');
    lbl1.innerHTML = '';
    lbl2.innerHTML = '';
}
 
function getCurrencyValues() {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");
    txtSID = document.getElementById('txtSID'),
        tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    var check = true;
    var dt = '';
    var dn = '';
    var doublecheck = false;
    var doublechecked = false;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var tblemployee = document.getElementById('tbl-smtp'),
            rows = tblemployee.rows;
        for (var i = 1, row = null; i < rows.length; i++) {
            row = rows[i];
            var code;
            var name;
            var rowCount = $('#tbl-smtp tr').length;
            if (rowCount == 2) {
            }
            else {
                row.cells[1].innerHTML == undefined ? code = '' : code = row.cells[1].innerHTML.toLowerCase().trim();
                row.cells[2].innerHTML == undefined ? name = '' : name = row.cells[2].innerHTML.toLowerCase().trim();
                if (txtcode.value == code) {
                    dt += txtcode.value;
                    doublecheck = true;
                }
                else if (txtname.value.toLowerCase().trim() == name) {
                    dn += txtname.value;
                    doublechecked = true;
                }
            }
        }
        if (doublecheck == true) {
            message = dt + ' Currency code already exists.';
            type = 'danger';
            notifyMessage(message, type);
        }
        else if (doublechecked == true) {
            message = dn + ' Currency name already exists.';
            type = 'danger';
            notifyMessage(message, type);
        }
        else {
            var data = {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
            return data;
        }
    }
    else if (hdnEmployee.value == "U") {
        var table = $('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var name = row[i].cells[2].innerHTML.toLowerCase().trim();
            var newname = txtname.value.toLowerCase().trim();
            if (name == newname && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data =
            {
                code: txtcode.value.trim(),
                cname: txtname.value.trim(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;
    }
}
function getcurrencyID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateEmployeecur() {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");
    result = '';
    if (txtcode.value.trim().length == 0) {
        result += 'Currency Code is required.<br/>';
        jQuery('#txtcode').css('border-color', 'red');
    } else if (txtcode.value.trim().length > 10) {
        result += 'Currency Code Maximum Length is 10.<br/>';
        jQuery('#txtcode').css('border-color', 'red');
    }
    else {
        var d = alpha(txtcode.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtcode').css('border-color', 'red');
        }
    }
    if (txtname.value.trim().length == 0) {
        result += 'Currency Name is required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 100) {
        result += 'Currency Name Maximum Length is 100.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alpha(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }
    return result;
}
function deletecurGrid() {
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