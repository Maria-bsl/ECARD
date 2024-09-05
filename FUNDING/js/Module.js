var cid;
var check;
var jk = false;
function validMod(e) {
    jk = true;
}
function resetmod() {
    var txtname = document.getElementById("txtname");
    var radioValue = jQuery("#rbtrue").prop("checked", true);
    txtname.value = '';
    lblname.innerHTML = "";
    jQuery('#txtname').css('border-color', '');
}
function showmodModal(e) {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById('txtname');
    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    if (e.value.indexOf('Edit') > -1) {
        lblname.innerHTML = "";
        jQuery('#txtname').css('border-color', '');
        jQuery("#btnSubmit").prop("show", true).html("Update");
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[4].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                txtcode.value = row.cells[1].innerHTML;
                txtname.value = row.cells[2].innerHTML;
                var flag = row.cells[3].innerHTML;
                if (flag == 'Active') {
                    jQuery("#rbtrue").prop("checked", true);
                    jQuery("#rbfalse").prop("checked", false);
                }
                else if (flag == 'InActive') {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", true);
                } else {
                    jQuery("#rbtrue").prop("checked", false);
                    jQuery("#rbfalse").prop("checked", false);
                }
            }
        }
        hdnEmployee.value = 'U';
    }
    else if (e.value.indexOf('Add') > -1) {
        resetmod();
        jQuery("#loader").css("display", "none");
        jQuery("#btnSubmit").show();
        jQuery("#tbl-smtp1").empty();
        hdnEmployee.value = 'C';
    }
}
function getmodValues() {
    var txtcode = document.getElementById('txtcode');
    var txtname = document.getElementById("txtname");
    rblGender = jQuery("input[name='gender']:checked");
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
        var data = {
            code: txtcode.value,
            name: txtname.value.trim(),
            act: rblGender.val(),
            sno: txtSID.value,
            dummy: check,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        for (var i = 0; i < rows.length; i++) {
            var to = rows[i].cells[2].innerHTML;
            var too = txtname.value;
            var codes = txtcode.value;
            if ((to == too) && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                code: txtcode.value,
                name: txtname.value.trim(),
                act: rblGender.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data =
            {
                sno: txtSID.value,
                dummy: check,
            }
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
function validateEmployeemod() {
    var txtname = document.getElementById("txtname");
    result = '';
    if (txtname.value.trim().length == 0) {
        result += 'Description is required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else if (txtname.value.trim().length > 100) {
        result += ' Description Maximum Length is 100.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }else {
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