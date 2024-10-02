var cid;
var check;
var sbtrans = "";
var jk = false, ak = false, pk = false, sk = false;
var boardtransferamt = true;
function ValidSBT(e) {
    jk = true;
}
function ValidSBT1(e) {
    ak = true;
}
function ValidSBT2(e) {
    pk = true;
}
function ValidSBT3(e) {
    sk = true;
}
function hideModal(e) {
    jQuery('#' + e).modal('hide');
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
function resetSBTran() {
    var txtname = document.getElementById("txtname");
    var ddlbd = document.getElementById("ddlbd");
    var ddlcur = document.getElementById("ddlcur");
    var ddlsp = document.getElementById("ddlsp");
    var txtamt = document.getElementById("txtamt");
    var txtrem = document.getElementById("txtrem");
    var txtcn = document.getElementById("txtcn");
    var file = document.getElementById("file");
    var ddlseason = document.getElementById("ddlseason");
    //txtname.value = '';
    txtamt.value = '';
    txtrem.value = '';
    txtcn.value = '';
    fudoc.value = '';
    jQuery("#ddlbd").val(0).change();
    jQuery("#ddlcur").val(0).change();
    jQuery("#ddlsp").val(0).change();
    jQuery("#ddlseason").val(0).change();
    jQuery('#txtname').css('border-color', '');
    jQuery('#ddlbd').css('border-color', '');
    jQuery('#ddlsp').css('border-color', '');
    jQuery('#ddlcur').css('border-color', '');
    jQuery('#txtamt').css('border-color', '');
    jQuery('#txtrem').css('border-color', '');
    jQuery('#txtcn').css('border-color', '');
    jQuery('#fudoc').css('border-color', '');
    jQuery("#tbl-smtp1 tbody").empty()
    jQuery("#txtname").prop('disabled', false);
    jQuery("#ddlbd").prop("disabled", false);
    jQuery('#ddlsp').prop("disabled", false);
    jQuery('#ddlcur').prop("disabled", false);
    jQuery('#txtamt').prop("disabled", false);
    jQuery('#txtrem').prop("disabled", false);
    jQuery('#txtcn').prop("disabled", false);
    jQuery('#file').prop("disabled", false);
    jQuery('#ddlseason').prop("disabled", false);
    lbl.innerHTML = '';
    lblErrorrem.innerHTML = '';
    lblErrorComt.innerHTML = '';
    lblErrorcn.innerHTML = '';
}
function getSMTPValuesSBtansfer() {
    var data = new FormData();
    var txtname = document.getElementById("txtname");
    var bd = document.getElementById("ddlbd");
    var bdvalue = bd.options[bd.selectedIndex].value;
    var cur = document.getElementById("ddlcur");
    var curvalue = cur.options[cur.selectedIndex].value;
    var sp = document.getElementById("ddlsp");
    var spvalue = sp.options[sp.selectedIndex].value;
    var txtamt = document.getElementById("txtamt");
    var txtrem = document.getElementById("txtrem");
    var txtcomt = document.getElementById('txtcomt');
    var txtcn = document.getElementById("txtcn");
    var file = document.getElementById("fudoc").files[0];

    var tseason = ddlseason.options[ddlseason.selectedIndex].text;
    var tseasonval = ddlseason.options[ddlseason.selectedIndex].value;

    tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        txtSID = document.getElementById('txtSID');
    //hdnEmployee = document.getElementById('hdnEmployee');
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var table = jQuery('#tbl-smtp').DataTable(),
        selectedRow = table.rows('.selected').data();
    check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        data.append("fileUpload", file);
        data.append("date", txtname.value.trim());
        data.append("spno", spvalue);
        data.append("bdno", bdvalue);
        data.append("code", curvalue);
        data.append("amt", txtamt.value.trim());
        data.append("remark", txtrem.value.trim());
        data.append("sno", txtSID.value);
        data.append("cmt", txtcomt.value.trim());
        data.append("dummy", check);
        data.append("cheque", txtcn.value.trim());
        data.append("seasonval", tseasonval);
        /*var data = {
            date: txtname.value.trim(),
            spno: spvalue,
            bdno: bdvalue,
            code: curvalue,
            amt: txtamt.value.trim(),
            remark: txtrem.value.trim(),
            sno: txtSID.value,
            cmt: txtcomt.value.trim(),
            dummy: check,
        }*/
        return data;
    }
    else if (hdnEmployee.value == "U") {
        var table = jQuery('#tbl-smtp').DataTable();
        var rows = table.rows({ page: 'all' }).nodes();
        for (var i = 0; i < rows.length; i++) {
            var name = rows[i].cells[1].innerHTML.toLowerCase().trim();
            var newname = txtname.value.toLowerCase().trim();
            var bd = rows[i].cells[4].innerHTML;
            var newb = bdvalue;
            var sp = rows[i].cells[2].innerHTML;
            var news = spvalue; 
            var at = rows[i].cells[8].innerHTML.toLowerCase().trim();
            var newa = txtamt.value.toLowerCase().trim();
            if (sbtrans == newname)//userinput
            {
                check = true;
                break;
            }
            //if (name.toLowerCase() == newname.toLowerCase().trim() && bd == bdvalue && sp == spvalue && ak == true && at.toLowerCase() == txtamt.value.toLowerCase().trim()) {
            //    check = false;
            //}
            else if (name == newname && jk == true) {
                check = true;
            }
            else if (name == newname && jk == false) {
                check = true;
            }
            else if (name != newname && jk == true) {
                check = false;
            }
            else if (bd == newb && ak == true) {
                check = true;
            }
            else if (bd == newb && ak == false) {
                check = true;
            }
            else if (bd != newb && pk == true) {
                check = false;
            }
            else if (sp == news && pk == true) {
                check = true;
            }
            else if (sp == news && pk == false) {
                check = true;
            }
            else if (sp != news && sk == true) {
                check = false;
            }
            else if (at == newa && sk == true) {
                check = true;
            }
            else if (at == newa && sk == false) {
                check = true;
            }
            else if (at != newa && jk == true) {
                check = false;
            }
            else { }
        }
        if (check == true) {
            
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", spvalue);
            data.append("bdno", bdvalue);
            data.append("code", curvalue);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
            data.append("seasonval", tseasonval);
        }
        else {
            data.append("fileUpload", file);
            data.append("date", txtname.value.trim());
            data.append("spno", spvalue);
            data.append("bdno", bdvalue);
            data.append("code", curvalue);
            data.append("amt", txtamt.value.trim());
            data.append("remark", txtrem.value.trim());
            data.append("sno", txtSID.value);
            data.append("cmt", txtcomt.value.trim());
            data.append("dummy", check);
            data.append("cheque", txtcn.value.trim());
            data.append("seasonval", tseasonval);
        }
        return data;
    }
}
//approve
function showAppModalbstran(e) {
    var txtname = document.getElementById("txtname");
    var ddlbd = document.getElementById("ddlbd");
    var ddlcur = document.getElementById("ddlcur");
    var ddlsp = document.getElementById("ddlsp");
    var txtamt = document.getElementById("txtamt");
    var txtrem = document.getElementById("txtrem");
    var txtcn = document.getElementById("txtcn");
    txtSID = document.getElementById('txtSID');
    var tblemployee = document.getElementById('tbl-smtp'),
        rows = tblemployee.rows,
        hdnEmployee = document.getElementById('hdnEmployee');
    jk = false;
    if (e.value.indexOf('Edit') > -1) {
        jQuery("#btnSubmit").html("Approve");
        jQuery("#lblmsg").hide();
        jQuery('#txtcomt').val("");
        for (var i = 0, row = null; i < rows.length; i++) {
            row = rows[i];
            if (row.cells[14].innerHTML.indexOf(e.id) > -1) {
                txtSID.value = jQuery(e).data('sid');
                jQuery("#ddlsp").val(row.cells[1].innerHTML).change();
                jQuery("#ddlbd").val(row.cells[3].innerHTML).change();
                jQuery("#ddlcur").val(row.cells[6].innerHTML).change();
                txtname.value = row.cells[5].innerHTML;
                txtamt.value = row.cells[8].innerHTML;
                txtrem.value = decodeHTMLEntities(row.cells[9].innerHTML);
                jQuery("#ddlseason").val(row.cells[10].innerHTML).change();
                txtcn.value = row.cells[12].innerHTML;
                var fvalue = row.cells[13].innerHTML;
                if (fvalue != "null") {
                    var a = document.getElementById('afu');
                    a.href = '/Sponser_Transfer_B_Approve/DownloadDoc?Id=' + row.cells[12].innerHTML + '&no=' + txtSID.value;
                }
                break;
            }
        }
        hdnEmployee.value = 'U';
        bindCommentDetails(jQuery(e).data('sid'));
        jQuery("#tblcmt").show();
    }
}
//return

function Validatespn(file) {
    result = '';
    var numBytes = document.getElementById(file.id).files[0].size;//2097152
    var filename = document.getElementById(file.id).files[0].name;
    filename = filename.substring(filename.lastIndexOf(".") + 1, filename.length).toLowerCase()
    if (filename != 'xls' && filename != 'xlsx' && filename != 'jpg' && filename != 'jpeg' && filename != 'png' && filename != 'gif' && filename != 'pdf') {
        document.getElementById(file.id).value = null;
        result += 'Upload file extension should be jpg or jpeg or png or gif or pdf only<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }
    else if (numBytes > 2097152) {
        document.getElementById(file.id).value = null;
        result += 'Upload file should not exceeds 2 MB<br/>';
        jQuery('#fudoc').css('border-color', 'red');
        notifyMessage(result, 'danger');
    }

    return true;
}
////15.11.2021
function checkduplicatechq(e) {

    var chqno = document.getElementById("txtcn").value;
    binddupliacteDetails(chqno);
}

function validateSponserBtansfer() {
    var txtname = document.getElementById("txtname");
    var ddlbd = document.getElementById("ddlbd");
    var ddlcur = document.getElementById("ddlcur");
    var ddlsp = document.getElementById("ddlsp");
    var txtamt = document.getElementById("txtamt");
    var txtrem = document.getElementById("txtrem");
    var txtcn = document.getElementById("txtcn");
    var file1 = document.getElementById("fudoc");

    var ddlseason = document.getElementById("ddlseason");
   
    result = '';
    if (ddlseason.value == 0) {
        result += 'Season is Required.<br/>';

    } else if (ddlseason.value == 0) {
        result += 'Season is Required.<br/>';

    }

    if (ddlsp.value == 0) {
        result += 'Sponsor is Required.<br/>';
        jQuery('#ddlsp').css('border-color', 'red');
    } else if (ddlsp.value == 0) {
        result += 'Sponsor is Required.<br/>';
        jQuery('#ddlsp').css('border-color', 'red');
    }
    if (ddlbd.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlbd').css('border-color', 'red');
    } else if (ddlbd.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlbd').css('border-color', 'red');
    }
    if (ddlcur.value == 0) {
        result += 'Currency is Required.<br/>';
        jQuery('#ddlcur').css('border-color', 'red');
    } else if (ddlcur.value == 0) {
        result += 'Currency is Required.<br/>';
        jQuery('#ddlcur').css('border-color', 'red');
    }



    if (txtname.value.trim().length == 0) {
        result += 'Date is Required<br/>';
        jQuery('#txtname').css('border-color', 'red');
    } else if (txtname.value.trim().length == 0) {
        result += 'Date is Required.<br/>';
        jQuery('#txtname').css('border-color', 'red');
    }
    if (txtamt.value.trim().length == 0) {
        result += 'Amount is Required<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    } else if (txtamt.value.trim().length > 23) {
        result += 'Amount Maximum Length is 18.<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    }
    if (txtcn.value.trim().length == 0) {
        result += 'Cheque No Required<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    } else if (txtcn.value.trim().length > 100) {
        result += 'Cheque No Maximum Length is 100.<br/>';
        jQuery('#txtcn').css('border-color', 'red');
    }
    else {
        var d = alphaFewsymlatest(txtcn.value.trim());
        if (d != false) {
            result += d;
            jQuery('#txtcn').css('border-color', 'red');
        }
    }
    if (hdnEmployee.value != 'U') {
        if (file1.value.trim().length == 0) {
            result += 'Upload Document Required<br/>';
            jQuery('#fudoc').css('border-color', 'red');
        }
    }

    if (boardtransferamt == false) {
        result += "Transfer Amount should be less than Budget Balance Amount .<br/>";
    }
    //if (txtrem.value.trim().length == 0) {
    //    result += 'Remarks is Required<br/>';
    //    jQuery('#txtrem').css('border-color', 'red');
    //} else if (txtrem.value.trim().length > 200) {
    //    result += 'Remarks Maximum Length is 200.<br/>';
    //    jQuery('#txtrem').css('border-color', 'red');
    //}
    //else {
    //    var d = alphaFewsymlatest(txtrem.value.trim());
    //    if (d != false) {
    //        result += d;
    //        jQuery('#txtrem').css('border-color', 'red');
    //    }
    //}
    return result;
}
function ChangeValueb(e) {
    var txtamt = document.getElementById("txtamt");
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length - 1;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    txtamt.value = Comma(tot + Number(e.value.replace(/,/g, "")));
}
function callamountb() {
    var tRows = document.getElementById('tblchruch');
    var len = tRows.rows.length;
    tot = 0;
    for (var Index = 1, row = null; Index < len; Index++) {
        row = tRows.rows[Index].cells[10].childNodes[0].value;
        tot += Number(row.replace(/,/g, ""));
    }
    return Comma(tot);
}
function textsboxborder(e) {
    jQuery('#txtname').css('border-color', '');
}
function textsboxborder1(e) {
    jQuery('#ddlbd').css('border-color', '');
}
function textsboxborder2(e) {
    jQuery('#ddlcur').css('border-color', '');
}
function textsboxborder3(e) {
    jQuery('#ddlsp').css('border-color', '');
}
function textsboxborder4(e) {
    jQuery('#ddltd').css('border-color', '');
}
function textsboxborder5(e) {
    jQuery('#txtamt').css('border-color', '');
}
function textsboxborder6(e) {
    jQuery('#txtrem').css('border-color', '');
}
function textsboxborder7(e) {
    jQuery('#txtcn').css('border-color', '');
}
function textsboxborder8(e) {
    jQuery('#fudoc').css('border-color', '');
}