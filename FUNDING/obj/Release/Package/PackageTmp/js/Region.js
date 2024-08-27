var cid;
var check;
var jk = false;
var regi = "";
function validrgion(e) {//??
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
function resetRegion() {
    var RegName = document.getElementById("txtname");
    var ddlcode = document.getElementById("ddlcode");
    document.getElementById("txtname").style.borderColor = "";
    document.getElementById("ddlcode").style.borderColor = "";
    var radios = jQuery('input:radio[name=gender]');
    if (radios.is(':checked') == false) {
        radios.filter('[value=Active]').prop('checked', true);
    }
    //$('#btnAdd').click(function () {
    //    $('input[value=Active]').prop('checked', false);
    //});
    RegName.value = '';
    jQuery("#ddlcode").val(0).change();
    //document.forms[0].reset();
    jQuery("#tbl-smtp1 tbody").empty();                                      
    jQuery('#txtname').prop("disabled", false);
   
}
function GetRegion() {//validation for null
    var RegName = document.getElementById('txtname');
    ddlcode = document.getElementById('ddlcode'),

        txtSID = document.getElementById('txtSID');
        var rsno = ddlcode.options[ddlcode.selectedIndex].value,
        ddl = ddlcode.options[ddlcode.selectedIndex].text,
        

       rb = jQuery("input[name='gender']:checked");
       tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
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

            name: RegName.value.trim(),
            sno: txtSID.value,
            status: rb.val(),
            csno: rsno,
            CountryName: ddl,
            dummy: check,
        }


        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = decodeHTMLEntities(rows[i].cells[1].innerHTML);
            var newname = RegName.value.trim();
            if (regi == newname) {
                check = true;
                break;
            }
            //var CountryName = rows[i].cells[2].innerHTML;
            //var status = rows[i].cells[4].innerHTML;
            // var s = rb.val();
            if (name.toLowerCase().trim() == newname.toLowerCase().trim() && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            var data = {
                name: RegName.value.trim(),
                sno: txtSID.value,
                status: rb.val(),
                csno: rsno,
                CountryName: ddl,
                dummy: check,
            }
        }
        else {
            var data = {
                name: RegName.value.trim(),
                sno: txtSID.value,
                status: rb.val(),
                csno: rsno,
                CountryName: ddl,
                dummy: check,
            }
        }
        return data;
    }
    else if (hdnEmployee.value == "D") {//??
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

function getDistID(glob) {//appp is in use
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    if (hdnEmployee.value == "D") {
        var data = {
            sno: glob,
        }
        return data;
    }
}
function validateRegionMaster() {
    var RegName = document.getElementById("txtname");
    var ddlcode = document.getElementById("ddlcode");
    result = '';
    var chosen = "";
    var len = document.forms[0].gender.length;
    for (i = 0; i < len; i++) {
        if (document.forms[0].gender[i].checked) {
            chosen = document.forms[0].gender[i].value
        }
    }
    if (RegName.value.trim().length == 0) {
        result += 'Region is Required<br/>';
        document.getElementById("txtname").style.borderColor = "red";
    }
    else if (RegName.value.trim().length > 200) {
        result += 'Region Maximum Length is 200.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(RegName.value.trim());
        if (d != false) {
            result += d;
            document.getElementById("txtname").style.borderColor = "red";
        }
    }
    if (ddlcode.value == 0) {
        result += 'Country is Required<br/>';
        document.getElementById("ddlcode").style.borderColor = "red";
    }
    //if (chosen == "") {
    //    result += 'Status is Required<br/>';
    //}
    return result;
}
function deleteCountGrid() {//r u sure u want to delete
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