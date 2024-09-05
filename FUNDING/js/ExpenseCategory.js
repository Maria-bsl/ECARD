


function validateexpcategory() {
    var txtdesg = document.getElementById('txtcatg');
    result = '';
    if (txtdesg.value.trim().length == 0) {
        result += 'Category is Required.<br/>';
        jQuery('#txtcatg').css('border-color', 'red');
    } else if (txtdesg.value.trim().length > 200) {
        result += 'Category Maximum Length is 200.<br/>';
        jQuery('#txtcatg').css('border-color', 'red');
    } else {
        var d = alphaFewsymlatest(txtdesg.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtcatg').css('border-color', 'red');
        }
    }
    return result;
}

function resetexpensecategory() {
    var txtdesg = document.getElementById("txtcatg");
    txtcatg.value = '';
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtcatg').prop("disabled", false);
    jQuery('#txtcatg').css('border-color', '');
    lblError.innerHTML = '';
}

function getexpcategoryValues() {
    var txtcatg = document.getElementById("txtcatg");
    tblemployee = document.getElementById('tbl-smtp'),
        rblreg = jQuery("input[name='gender']:checked"),
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
            catg: txtcatg.value.trim(),
            sno: txtSID.value,
            stats: rblreg.val(), 
            dummy: check,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = decodeHTMLEntities(rows[i].cells[1].innerHTML.trim());
            var newname = txtcatg.value.trim();
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
                catg: txtcatg.value.trim(),
                sno: txtSID.value,
                stats: rblreg.val(), 
                dummy: check,
            }
        }
        else {
            var data = {
                catg: txtcatg.value.trim(),
                sno: txtSID.value,
                stats: rblreg.val(), 
                dummy: check,
            }
        }
        return data;
    }
}