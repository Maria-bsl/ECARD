jQuery(document).ready(function () {
    /* var dropDownLinks = jQuery('.app-link__dropdown');
    var arrayDropDownLinks = dropDownLinks.toArray();

    arrayDropDownLinks.forEach(function (dropDownLink) {
        jQuery(dropDownLink).click(function () {
            compareLinks(dropDownLink, arrayDropDownLinks);
        });
    });

    var notificationBell = jQuery('#notifyID');
    var notificationBox = document.getElementById('notificationBox');
    jQuery(notificationBell).click(function (e) {
        e.preventDefault();
        jQuery(this).toggleClass('active');
    });


    jQuery(document).mouseup(function (e) {
        if (jQuery(notificationBell).hasClass('active')) {
            if ((e.target != notificationBox) &&
                (e.target.parentNode != notificationBox) &&
                (e.target.parentNode.parentNode != notificationBox) &&
                (e.target.parentNode.parentNode.parentNode != notificationBox)) {
                jQuery(notificationBell).removeClass('active');
            }
        }
    }); */


    jQuery('#contTarget').dropdown();
    jQuery('#my-select').dropdown();
    jQuery('.family_member').dropdown();
    jQuery('#mySelect').dropdown({
        // maxSelections: 1000
    });

    jQuery('.ui.dropdown').dropdown({
        clearable: true,
    });


    var inputNumeral = document.querySelector('.input-numeral');

    if (inputNumeral) {
        var cleaveNumeral = new Cleave(inputNumeral, {
            numeral: true,
            numeralThousandsGroupStyle: 'thousand'
        });
    }


    var h_button = $('header button:last-of-type')[1];
    var h_list = $('ul.account-info');


    $(window).mouseup(function (e) {

        if (!$(h_button).is(e.target) && $(h_button).has(e.target).length === 0) {
            if (!$(h_list).is(e.target) && $(h_list).has(e.target).length === 0) {
                // Outside click
                $('header ul').removeClass('active');
            } else {
                // Inside click
                // DO nothing {Real do nothing!!!}
            }
            // Outside click
        } else {
            // Inside click
            $('header ul').toggleClass('active');
        }

    });

    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################
    var addContributionBtn = jQuery('#addBtn');

    jQuery(addContributionBtn).click(function (e) {
        e.preventDefault();
        jQuery(this).next().fadeOut();
        var contrForm = jQuery('#contrForm');
        var clone = jQuery(contrForm).find('.row').clone();
        jQuery('#cLists').html(clone);

    });




    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################
    // #############################################################################################################




    var dateField = jQuery('#dateField');

    function fullYearField() {
        var todaysDate = new Date();
        var dd = todaysDate.getDate();
        var mm = todaysDate.getMonth();
        var yyyy = todaysDate.getFullYear();
        var monthArray = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

        var dateTh = '';
        switch (dd) {
            case 1:
            case 21:
            case 31:
                dateTh = 'st';
                break;
            case 2:
            case 22:
                dateTh = 'nd';
                break;
            case 3:
            case 23:
                dateTh = 'rd';
                break;
            default:
                dateTh = 'th';
                break;
        }

        return '<span>' + dd + '</span><sup>' + dateTh + '&nbsp;</sup><span>' + monthArray[mm] + '&nbsp;</span>' + yyyy;
    }
    jQuery(dateField).append(fullYearField());


    // 3#################################################################################################################
    // 3#################################################################################################################
    // 3#################################################################################################################

    // DataTable
    jQuery('#memberContributions').DataTable();
    jQuery('#example').DataTable();
    // jQuery('#example').DataTable({
    //     "columnDefs": [{
    //         "orderable": false,
    //         "targets": -1
    //     }],
    // });
    jQuery('#memberTable').DataTable({
        // "pageLength": 5,
        // lengthMenu: [[5, 10, 20, -1], [5, 10, 20, 'All']],
        "columnDefs": [{
            "orderable": false,
            "targets": -1
        }],
    });
    jQuery('#familyMemberTable').DataTable({
        "columnDefs": [{
            "orderable": false,
            "targets": -1
        }],
    });
    jQuery('#tblchruch').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": false,
        "bInfo": false,
        "bAutoWidth": false
    });
    jQuery('#beliverContributionsTable').DataTable({
        "columnDefs": [{
            "orderable": false,
            "targets": -1
        }],
    });
    jQuery('#pendingContributionTable').DataTable();
    jQuery('#paidContributionTable').DataTable();

    // Datepicker

    function TodaysDate() {
        var todaysDate = new Date();
        var dd = todaysDate.getDate();

        var mm = todaysDate.getMonth() + 1;
        var yyyy = todaysDate.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }

        if (mm < 10) {
            mm = '0' + mm;
        }

        return todaysDate = dd + '/' + mm + '/' + yyyy;
    }

    var today = TodaysDate();

    if (jQuery('#dob')) {
        jQuery('#dob').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'dd/mm/yyyy',
            iconsLibrary: 'fontawesome',
            weekStartDay: 1,
        });
    }
    if (jQuery('#txtDate1')) {
        jQuery('#txtDate1').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'mm/dd/yyyy',
            iconsLibrary: 'fontawesome',
            weekStartDay: 1,
            maxDate: today,
        });
    }
    if (jQuery('#memberDate')) {
        jQuery('#memberDate').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'mm/dd/yyyy',
            iconsLibrary: 'fontawesome',
            weekStartDay: 1,
            maxDate: today,
        });
    }
    if (jQuery('#effDate')) {
        jQuery('#effDate').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'dd/mm/yyyy',
            iconsLibrary: 'fontawesome',
            weekStartDay: 1,
            value: today,
            minDate: today,
        });
    }

    if (jQuery('#expDate')) {
        jQuery('#expDate').datepicker({
            uiLibrary: 'bootstrap4',
            format: 'dd/mm/yyyy',
            iconsLibrary: 'fontawesome',
            weekStartDay: 1,
            minDate: today,
        });
    }

});

function compareLinks(link, array) {
    for (let i = 0; i < array.length; i++) {

        if (link == array[i]) {
            jQuery(link.nextElementSibling).slideToggle();
            jQuery(link).toggleClass('active');
            // console.log(jQuery(link).text());
        } else {
            jQuery(array[i].nextElementSibling).slideUp();
            jQuery(array[i]).removeClass('active');
            // console.log('Inactive: ' + array[i]);
        }
    }
}