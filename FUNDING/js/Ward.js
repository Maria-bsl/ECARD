var cid;
var check;
var ward = "";
var jk = false, ak = false;
function validW(e) {
    jk = true;
}
function validW1(e) {
    ak = true;
}
function resetSMTPward() {
    var txtname = document.getElementById('txtname'),
        ddlreg = document.getElementById('ddlreg'),
        ddldist = document.getElementById('ddldno');
    var radios = jQuery('input:radio[name=gender]');
    if (radios.is(':checked') == false) {
        radios.filter('[value=Active]').prop('checked', true);
    }
    jQuery("#ddlreg").val(0).change();
    jQuery("#ddldno").val(0).change();
    txtname.value = '';  
    jQuery('#txtname').css('border-color', '');
    jQuery('#ddlreg').css('border-color', '');
    jQuery('#ddldno').css('border-color', '');
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtname').prop("disabled", false);
    lblErrorWard.innerHTML = '';
}

function getSMTPValuesward() {
    var txtward = document.getElementById('txtname'),
        txtSID = document.getElementById('txtDID'),
        dr = document.getElementById('ddlreg'),       
        ddldist = document.getElementById('ddldno');  
    var rsno = ddlreg.options[ddlreg.selectedIndex].value,
        ddl = dr.options[dr.selectedIndex].text,      
        dsno = ddldist.options[ddldist.selectedIndex].value,
        dds = ddldist.options[ddldist.selectedIndex].text,
        rblreg = jQuery("input[name='gender']:checked"),    
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
            ward_name: txtname.value.trim(),
            region_id: rsno,
            regname: ddl,
            district_sno: dsno,
            disname: dds,
            sno: txtSID.value,
            ward_status: rblreg.val(),
            dummy: check,
        }
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = decodeHTMLEntities(rows[i].cells[1].innerHTML.toLowerCase());
            var newname = txtname.value.toLowerCase().trim();
            var disno = rows[i].cells[4].innerHTML;
            var risno = rows[i].cells[2].innerHTML;
            if (ward == newname)//userinput
            {
                check = true;
                break;
            }
            if (name == newname && disno == dsno && ak == true && risno == rsno) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                ward_name: txtname.value.trim(),
                region_id: rsno,
                regname: ddl,
                district_sno: dsno,
                disname: dds,
                sno: txtSID.value,
                ward_status: rblreg.val(),
                dummy: check,
            }
        }
        else {
            var data = {
                ward_name: txtname.value.trim(),
                region_id: rsno,
                regname: ddl,
                district_sno: dsno,
                disname: dds,              
                sno: txtSID.value,
                ward_status: rblreg.val(),
                dummy: check,
            }
        }
        return data;
    }   
}
function validateEmployeeward() {
    var txtward = document.getElementById("txtname");
    ddlreg = document.getElementById('ddlreg');
    ddldno = document.getElementById('ddldno');
    txtSID = document.getElementById('txtDID');    
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (txtward.value.trim().length == 0) {
        result += 'Ward is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 200) {
        result += 'Ward Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtward.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }     
    if (ddlreg.value == 0) {
        result += 'Region is Required<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    } else if (ddlreg.value == 0) {
        result += 'Region is Required.<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    }
    if (ddldno.value == 0) {
        result += 'District is Required<br/>';
        jQuery('#ddldno').css('border-color', 'red');
    } else if (ddldno.value == 0) {
        result += 'District is Required.<br/>';
        jQuery('#ddldno').css('border-color', 'red');
    }
    if (chosen == "") {
        result += 'Status is Required<br/>';
    }
    return result;
}
function deleteRowForEmployeeGrid() {
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected');
    table.row(selectedRow).remove().draw();
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
function getID(e) {
    var table = document.getElementById('tbl-smtp'),
        rows = table.rows,
        rowNumber = 0;
    for (var Index = 1, row = null; Index < rows.length; Index++) {
        row = rows[Index];
        rowNumber = Index - 1;
    }
}
