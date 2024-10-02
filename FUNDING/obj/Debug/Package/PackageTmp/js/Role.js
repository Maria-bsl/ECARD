var cid;
var check;
var jk = false;
function validRole(e) {
    jk = true;
}
function resetRole() {
    var txtname = document.getElementById("txtname");
    var radioValue = jQuery("#rbtrue").prop("checked", true);
    txtname.value = '';
    jQuery("#txtname").prop('disabled', false);
    jQuery("#rbtrue").prop("disabled", false);
    jQuery("#rbfalse").prop("disabled", false);
    lblname.innerHTML = "";
    jQuery('#txtname').css('border-color', '');
}
function showroleModal(e) {
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
                txtname.value = row.cells[2].innerHTML;
                if (txtname.value == "Admin") {
                    jQuery("#txtname").prop('disabled', true);
                    jQuery("#rbtrue").prop("disabled", true);
                    jQuery("#rbfalse").prop("disabled", true);
                }
                else {
                    jQuery("#txtname").prop('disabled', false);
                    jQuery("#rbtrue").prop("disabled", false);
                    jQuery("#rbfalse").prop("disabled", false);
                }
                var flag = row.cells[3].innerHTML;
                if (flag == 'Active') {
                    jQuery("#rbtrue").prop("checked", true);
                    jQuery("#rbfalse").prop("checked", false);
                }
                else if (flag == 'InActive') {
                    jQuery("#rbtrue").prop("checked", false);

                    jQuery("rbfalse").prop("disabled", false);
                } else {
                    jQuery("#rbtrue").prop("checked", false);

                    jQuery("rbfalse").prop("disabled", false);
                }
            }
        }
        hdnEmployee.value = 'U';
    }
    else if (e.value.indexOf('Add') > -1) {
        resetRole();
        jQuery("#loader").css("display", "none");
        jQuery("#btnSubmit").show();
        jQuery("#btnSubmit").prop("show", true).html("Save");
        jQuery("#tbl-smtp1").empty();
        hdnEmployee.value = 'C';
    }
}
function getroleValues() {
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
            name: txtname.value.trim(),
            act: rblGender.val(),
            sno: txtSID.value,
            dummy: check,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var row = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < row.length; i++) {
            var to = row[i].cells[2].innerHTML.toLowerCase();
            var too = txtname.value.toLowerCase().trim();
            if ((to == too) && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                name: txtname.value.trim(),
                act: rblGender.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        else {
            var data =
            {
                name: txtname.value.trim(),
                act: rblGender.val(),
                sno: txtSID.value,
                dummy: check,
            }
        }
        return data;
    }
}
function getroleID(glob) {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateEmployeerole() {
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
        var d = alphaFewsym(txtname.value.trim());
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