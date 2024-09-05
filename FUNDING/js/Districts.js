var cid;
var check;
var jk = false;
var dist = "";
function valid(e) {
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
function resetdist() {
    var txtna = document.getElementById("txtname");
    var ddlreg = document.getElementById("ddlreg");    
    var radios = jQuery('input:radio[name=gender]');
    if (radios.is(':checked') == false) {
        radios.filter('[value=Active]').prop('checked', true);
    }
    txtna.value = '';   
    jQuery("#ddlreg").val(0).change();
    jQuery("#tbl-smtp1 tbody").empty();
    jQuery('#txtname').prop("disabled", false); 
    jQuery("#loader").css("display", "none");
    lblError.innerHTML = '';
    jQuery('#txtname').css('border-color', '');
    jQuery('#ddlreg').css('border-color', '');
}

function getdistrictValues() {
    var txtna = document.getElementById('txtname'),
        dr = document.getElementById("ddlreg");
    var drvalue = ddlreg.options[ddlreg.selectedIndex].value;     
        ddl = dr.options[dr.selectedIndex].text,
        rblreg = jQuery("input[name='gender']:checked"),
        tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
    
    hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable();
    selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            dstname: txtna.value.trim(),
            sno: txtSID.value,
            regid: drvalue,
            regn: ddl,
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
            var newname = txtname.value.trim();
            if (dist == newname) {
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
                dstname: txtna.value.trim(),
                sno: txtSID.value,
                regid: drvalue,
                regn: ddl,
                stats: rblreg.val(),                
                dummy: check,
            }
        }
        else {
            var data = {
                dstname: txtna.value.trim(),
                sno: txtSID.value,
                regid: drvalue,
                regn: ddl,
                stats: rblreg.val(),                
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
function districtborder(e) {
    jQuery('#txtname').css('border-color', '');
}
function districtborder1(e) {
    jQuery('#ddlreg').css('border-color', '');
}
function validatedistrict() {
    var ddlreg = document.getElementById('ddlreg');
    var txtname = document.getElementById('txtname');
    result = '';
    var choosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            choosen = document.forms[0].gender[i].value
        }
    }
    if (txtname.value.trim().length == 0) {
        result += 'District is Required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length > 200) {
        result += 'District Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else {
        var d = alphaFewsymlatest(txtname.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtname').css('border-color', 'red');
        }
    }
    if (ddlreg.value == 0) {
        result += 'Region is Required.<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    } else if (ddlreg.value == 0) {
        result += 'Region is Required.<br/>';
        jQuery('#ddlreg').css('border-color', 'red');
    }
    if (choosen == "") {
        result += 'Status is Required.<br/>';
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
        rowNumber = Index - 1
    }
}