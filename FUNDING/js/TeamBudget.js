


function resetbudget() {
    txtSID = document.getElementById('txtSID');
    txtSID = '';
    jQuery("#ddlsponser").val(0).change();
    jQuery("#ddlboard").val(0).change();
    jQuery("#ddlteam").val(0).change();
    jQuery("#ddlseason").val(0).change();
    jQuery("#txtsponseramt").val('');
    //jQuery("#txtSeason").val('');
    jQuery("#txtseasondate").val('');
    jQuery('#txtmonthperseason').val('');
    jQuery("#ddlteam").val(0).change();

    jQuery('#ddlboard').prop("disabled", false);
    jQuery('#ddlsponser').prop("disabled", false);
    jQuery('#txtsponseramt').prop("disabled", false);
    jQuery('#ddlseason').prop("disabled", false);
    jQuery('#txtseasondate').prop("disabled", false);
    jQuery('#txtmonthperseason').prop("disabled", false);
    jQuery('#ddlteam').prop("disabled", false);

    
}


function validatebudgetadd() {

    var ddlboard = document.getElementById("ddlboard");
    var ddlsponser = document.getElementById("ddlsponser");
    var txtdates = document.getElementById("txtseasondate");
    var sponseramt = document.getElementById("txtsponseramt");
    var ddltd = document.getElementById("ddlteam");
    //var season = document.getElementById("txtSeason");
    var season = document.getElementById("ddlseason");
    var txtmonthperseason = document.getElementById("txtmonthperseason");
    result = '';

    if (ddlboard.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlboard').css('border-color', 'red');
    } else if (ddlboard.value == 0) {
        result += 'Board is Required.<br/>';
        jQuery('#ddlboard').css('border-color', 'red');
    }
    else { jQuery('#ddlboard').css('border-color', '');}
    if (ddlsponser.value == 0) {
        result += 'Sponser is Required.<br/>';
        jQuery('#ddlsponser').css('border-color', 'red');
    } else if (ddlsponser.value == 0) {
        result += 'Sponser is Required.<br/>';
        jQuery('#ddlsponser').css('border-color', 'red');
    }
    else {
        jQuery('#ddlsponser').css('border-color', '')
    }

    if (sponseramt.value.trim().length == 0) {
        result += 'Sponser Amount is Required<br/>';
        jQuery('#txtsponseramt').css('border-color', 'red');
    } else if (sponseramt.value.trim().length > 23) {
        result += 'Amount Maximum Length is 18.<br/>';
        jQuery('#txtsponseramt').css('border-color', 'red');
    }
    else
    {
        jQuery('#txtsponseramt').css('border-color', '');
    }

    if (season.value  == 0) {
        result += 'Season is Required<br/>';
        jQuery('#ddlseason').css('border-color', 'red');
        
    }
    else {
        jQuery('#ddlseason').css('border-color', '');
    }

    if (txtdates.value.trim().length == 0) {
        result += 'Season Start Month is Required<br/>';
        jQuery('#txtseasondate').css('border-color', 'red');
    } else if (txtdates.value.trim().length == 0) {
        result += 'Season Start Month is Required<br/>';
        jQuery('#txtseasondate').css('border-color', 'red');
    }
    else {
        jQuery('#txtseasondate').css('border-color', '');
    }

    if (txtmonthperseason.value.trim().length == 0) {
        result += 'Months Per Season is Required<br/>';
        jQuery('#txtmonthperseason').css('border-color', 'red');
    }
    else {
        jQuery('#txtmonthperseason').css('border-color', '');
    }


    if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddlteam').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddlteam').css('border-color', 'red');
    }
    else {
        jQuery('#ddlteam').css('border-color', '');
    }
    return result;
}

function validatebudget() {
    
    var txtdates = document.getElementById("txtseasondate1");
    var sponseramt = document.getElementById("txtsponseramt1");
    var ddltd = document.getElementById("ddlteam");
    var season = document.getElementById("ddlseason");
    var txtmonthperseason = document.getElementById("txtmonthperseason1");
    var monthlength = jQuery('#txtmonthperseason1').val();

    result = '';
   
    if (sponseramt.value.trim().length == 0) {
        result += 'Sponser Amount is Required<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    } else if (sponseramt.value.trim().length > 23) {
        result += 'Amount Maximum Length is 18.<br/>';
        jQuery('#txtamt').css('border-color', 'red');
    }
    else { jQuery('#txtsponseramt1').css('border-color', ''); }

    

    if (txtdates.value.trim().length == 0) {
        result += 'Season Start Month is Required<br/>';
        jQuery('#txtseasondate1').css('border-color', 'red');
    } else if (txtdates.value.trim().length == 0) {
        result += 'Season Start Month is Required<br/>';
        jQuery('#txtseasondate1').css('border-color', 'red');
    }
    else {
        jQuery('#txtseasondate1').css('border-color', '');
    }
    if (txtmonthperseason.value.trim().length == 0) {
        result += 'Months Per Season is Required<br/>';
        jQuery('#txtmonthperseason1').css('border-color', 'red');
    }
    else {
        jQuery('#txtmonthperseason1').css('border-color', '');
    }


    if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    } else if (ddltd.value == 0) {
        result += 'Team is Required.<br/>';
        jQuery('#ddltd').css('border-color', 'red');
    }
    else {
        jQuery('#ddltd').css('border-color', '');
    }

    if (monthlength == 0) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason').css('border-color', 'red');
    }
    else if (monthlength > 12) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason').css('border-color', 'red');
    }
    return result;
}

function getbudgetdataret() {
    jQuery("#loader").css("display", "block");
    var txtdt = document.getElementById("txtseasondate1").value;
    var sponser = ddlsponser1.options[ddlsponser1.selectedIndex].value;
    var board = ddlboard1.options[ddlboard1.selectedIndex].value;
    hdnEmployee = document.getElementById('hdnEmployee');
    var sponamt = jQuery("#txtsponseramt1").val().replace(/,/g, "");
    var seasonmonth = jQuery("#txtmonthperseason1").val();// document.getElementById('txtmonthperseason');
    var tseason = ddlseason.options[ddlseason.selectedIndex].text;
    var txtcomt = document.getElementById('txtcomt');
    var tseasonval = ddlseason.options[ddlseason.selectedIndex].value;
    var strcoun = "";
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var dltext = $('#ddlteam option:selected')
        .toArray().map(item => item.text).join();
    var dlvalue = $('#ddlteam option:selected')
        .toArray().map(item => item.value).join();
    if (dlvalue == 0 && dlvalue != '') {
        dlvalue = '', dltext = '';
        for (var i = 0; i < tararray[0].length; i++) {
            dltext += tararray[0][i].Target_Type + ',';
            dlvalue += tararray[0][i].SNO + ',';
        }
        dltext = dltext.substr(0, dltext.length - 1);
        dlvalue = dlvalue.substr(0, dlvalue.length - 1);
    }

    var check = true;
    if (hdnEmployee.value == "C") {
        txtSID.value = '0';
        jk = false;
        var data = {
            date: txtdt,
            board: board,
            sponser: sponser,
            currcode: "TZS",
            sponseramt: sponamt,
            season: tseason,
            smonth: seasonmonth,
            target: dltext,
            targetval: dlvalue,
            seasonval: tseasonval,
            sno: 0,
            cmt : null ,
            dummy: check,
        }
        return data;
    }
    if (hdnEmployee.value == "U") {
        jk = false;
        var data = {
            date: txtdt,
            board: board,
            sponser: sponser,
            currcode: "TZS",
            sponseramt: sponamt,
            season: tseason,
            smonth: seasonmonth,
            target: dltext,
            targetval: dlvalue,
            seasonval: tseasonval,
            sno: txtSID.value,
            cmt: txtcomt.value.trim(),
            dummy: check,
        }
        return data;
    }
}

function getbudgetdata() {
    jQuery("#loader").css("display", "block");
    var txtdt = document.getElementById("txtseasondate").value;
    var sponser = ddlsponser.options[ddlsponser.selectedIndex].value;
    var board = ddlboard.options[ddlboard.selectedIndex].value;
    //var ddlteam = document.getElementById('ddlteam');
    hdnEmployee = document.getElementById('hdnEmployee');
    // var sponseramt = document.getElementById('txtamttxtoversponseramt');
    var sponamt = jQuery("#txtsponseramt").val().replace(/,/g, "");
    var seasonmonth = jQuery("#txtmonthperseason").val();// document.getElementById('txtmonthperseason');
    var tseason = ddlseason.options[ddlseason.selectedIndex].text;
    var tseasonval = ddlseason.options[ddlseason.selectedIndex].value;


    var strcoun = "";
    if (txtSID.value == '') {
        txtSID.value = '0';
    }
    var dltext = $('#ddlteam option:selected')
        .toArray().map(item => item.text).join();
    var dlvalue = $('#ddlteam option:selected')
        .toArray().map(item => item.value).join();
    if (dlvalue == 0 && dlvalue != '') {
        dlvalue = '', dltext = '';
        for (var i = 0; i < tararray[0].length; i++) {
            dltext += tararray[0][i].Target_Type + ',';
            dlvalue += tararray[0][i].SNO + ',';
        }
        dltext = dltext.substr(0, dltext.length - 1);
        dlvalue = dlvalue.substr(0, dlvalue.length - 1);
    }
    
    var check = true;
    if (hdnEmployee.value == "C")
    {
        txtSID.value = '0';
        jk = false;
        var data = {
            date: txtdt,
            board: board,
            sponser: sponser,
            currcode: "TZS",
            sponseramt: sponamt,
            season : tseason,
            smonth: seasonmonth,
            target: dltext,
            targetval: dlvalue,
            seasonval: tseasonval,
            sno:0,
            dummy: check,
        }
        return data;
    }
    if (hdnEmployee.value == "U") {
        jk = false;
        var data = {
            date: txtdt,
            board: board,
            sponser: sponser,
            currcode: "TZS",
            sponseramt: sponamt,
            season: tseason,
            smonth: seasonmonth,
            target: dltext,
            targetval: dlvalue,
            seasonval: tseasonval,
            sno: txtSID.value,
            dummy: check,
        }
        return data;
    }
}

function monthsValidate1(e) {
    result = '';
    var monthlength = jQuery('#txtmonthperseason').val();
    if (monthlength == 0) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason').val('');
        notifyMessage(result, 'danger');
    }
    else if (monthlength > 12) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason').val('');
        notifyMessage(result, 'danger');
    }
    return true    
} 


function monthsValidateret(e) {
    result = '';
    var monthlength = jQuery('#txtmonthperseason1').val();
    if (monthlength == 0) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason1').val('');
        notifyMessage(result, 'danger');
    }
    else if (monthlength > 12) {
        result += 'Please enter Months Between 1 and 12<br/>';
        jQuery('#txtmonthperseason1').val('');
        notifyMessage(result, 'danger');
    }
    return true
} 


