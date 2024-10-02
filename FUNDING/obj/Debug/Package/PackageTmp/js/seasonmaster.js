
var desg = "";
function validateseason() {
    var txtseason = document.getElementById('txtseason');
    result = '';
    if (txtseason.value.trim().length == 0) {
        result += 'Season is Required.<br/>';
        jQuery('#txtcatg').css('border-color', 'red');
    } else if (txtseason.value.trim().length > 200) {
        result += 'Season Maximum Length is 200.<br/>';
        jQuery('#txtseason').css('border-color', 'red');
    } else {
        var d = alphaFewsymlatest(txtseason.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtseason').css('border-color', 'red');
        }
    }
    return result;
}

function resetseason() {
    var txtseason = document.getElementById("txtseason");
    txtseason.value = '';
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtseason').prop("disabled", false);
    jQuery('#txtseason').css('border-color', '');
    lblError.innerHTML = '';
}

function getseasonValues() {
    var txtseason = document.getElementById("txtseason");
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
            season: txtseason.value.trim(),
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
            var newname = txtseason.value.trim();
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
                season: txtseason.value.trim(),
                sno: txtSID.value,
                stats: rblreg.val(),
                dummy: check,
            }
        }
        else {
            var data = {
                season: txtseason.value.trim(),
                sno: txtSID.value,
                stats: rblreg.val(),
                dummy: check,
            }
        }
        return data;
    }
}