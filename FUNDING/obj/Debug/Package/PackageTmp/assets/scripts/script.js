// $(document).ready(function () {

var believerChart = document.getElementById('believerChart');
var contributionChart = document.getElementById('contributionChart');

// Exception Handling
if (believerChart) {
    believerChart.getContext('2d');
    var myChart = new Chart(believerChart, {
        type: 'bar',
        data: {
            labels: ['Children', 'Youth', 'Adult', 'Elders', ],
            datasets: [{
                    label: 'Male',
                    data: [10, 23, 15, 22, ],
                    backgroundColor: '#3869AE',
                    borderColor: 'rgba(1, 12, 23, 0.35)',
                    borderWidth: 0,
                    hoverBackgroundColor: '#3869C0',

                },
                {
                    label: 'Female',
                    data: [13, 15, 17, 6, ],
                    backgroundColor: '#E8E737',
                    borderWidth: 0,
                    borderColor: 'rgba(1, 12, 23, 0.35)',
                    hoverBackgroundColor: '#E8F600',

                }
            ],
        },
        options: {
            title: {
                display: true,
                text: 'Number of Believers',
                fontStyle: "bold",
                fontSize: 18,
            },
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Believers',
                        fontSize: 14,
                        fontStyle: "bold",
                    },
                    ticks: {
                        beginAtZero: true,

                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Age group',
                        fontSize: 14,
                        fontStyle: "bold",
                    },
                    // maxBarThickness: 50,
                    categoryPercentage: 0.7,
                    barPercentage: 1,
                }]
            },
            responsive: true,
            maintainAspectRatio: false

        }
    });
}

// Exception Handling
if (contributionChart) {
    contributionChart.getContext('2d');
    var myChart = new Chart(contributionChart, {
        type: 'line',
        data: {
            labels: ['Contribution 1', 'Contribution 2', 'Contribution 3', 'Contribution 4', ],
            datasets: [{
                label: 'Contributions in TZS',
                data: [12500000, 4950000, 5250000, 7250000, ],
                borderColor: 'rgba(32, 179, 66, 0.85)',
                fill: true,
                lineTension: 0.2,
                pointHoverRadius: 10,
                backgroundColor: 'rgba(246, 246, 246, 0.4)',
            }]
        },
        options: {
            title: {
                display: true,
                text: 'Contributions',
                fontStyle: "bold",
                fontSize: 18,
            },
            scales: {
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Amount',
                        fontSize: 14,
                        fontStyle: "bold",
                    },
                    ticks: {
                        beginAtZero: true,

                        // Include a dollar sign in the ticks
                        callback: function (value) {
                            // return 'TZS ' + value;
                            return 'TZS ' + new Intl.NumberFormat().format(value);
                        }
                    }
                }],
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: 'Contribution Type',
                        fontSize: 14,
                        fontStyle: "bold",
                    },
                    maxBarThickness: 60,
                }]
            },
            legend: {
                display: false,
            },
            responsive: true,
            maintainAspectRatio: false

        }
    });
}


console.time();

//traversing modal [new family member] textfields and get the values of each field
var familyMemberForm = $('#f_member-form-row').children();
var nf_fullname = familyMemberForm.find('[name=fullname]');
var nf_relation = familyMemberForm.find('[name=relationship]');
var nf_maritalStatus = familyMemberForm.find('[name=marital-status]');
var nf_gender = familyMemberForm.find('[name=gender]');
var nf_dob = familyMemberForm.find('[name=dob]');
var nf_mobile = familyMemberForm.find('[name=mobile]');
var nf_telephone = familyMemberForm.find('[name=telephone]');
var nf_address = familyMemberForm.find('[name=address]');
var nf_email = familyMemberForm.find('[name=email]');

// get modal [new family member] save button
var nf_saveBtn = $('#btn-family-member');

//Targer family members table
var familyTable = $('#familyMembersTable');

// for sno [tr>td:first]
function getRowCount(param) {
    getRowCount.counter = rowCount;
    getRowCount.counter++;
    return getRowCount.counter + '.';
}

// create new table row
function getNewElement(param) {
    return new_tr = '<tr>\n' +
        '\t<td>' + param + '.</td>\n' +
        '\t<td class="t_fullname">\n' +
        '\t\t<span class="i_f-fullname"></span>\n' +
        '\t\t<input class="d-none" name="t_fullname" type="text">\n' +
        '\t</td>\n' +
        '\t<td class="t_relation">\n' +
        '\t\t<span class="i_f-relation"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_relation">\n' +
        '\t</td>\n' +
        '\t<td class="t_dob">\n' +
        '\t\t<span class="i_f-dob"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_dob">\n' +
        '\t</td>\n' +
        '\t<td class="t_gender">\n' +
        '\t\t<span class="i_f-gender"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_gender">\n' +
        '\t</td>\n' +
        '\t<td class="t_marital-status">\n' +
        '\t\t<span class="i_f-marital-status"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_marital-status">\n' +
        '\t</td>\n' +
        '\t<td class="t_mobile t_telephone t_email">\n' +
        '\t\t<span class="i_f-mobile"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_mobile">\n' +
        '\t\t<input type="text" class="d-none" name="t_telephone">\n' +
        '\t\t<input type="text" class="d-none" name="t_email">\n' +
        '\t</td>\n' +
        '\t<td class="t_address">\n' +
        '\t\t<span class="i_f-address"></span>\n' +
        '\t\t<input type="text" class="d-none" name="t_address">\n' +
        '\t</td>\n' +
        '\t<td>\n' +
        '\t\t<div class="btn-group" role="group" aria-label="Button group">\n' +
        '\t\t\t<span class="btn btn-outline-secondary btn-sm" data-toggle="modal" data-target="#familyMemberEditModal">\n' +
        '\t\t\t\t<i class="fas fa-pen"></i>\n' +
        '\t\t\t</span>\n' +
        '\t\t\t<span class="btn btn-outline-dark btn-sm">\n' +
        '\t\t\t\t<i class="fas fa-trash" aria-hidden="true"></i>\n' +
        '\t\t\t</span>\n' +
        '\t\t</div>\n' +
        '\t</td>\n' +
        '</tr>';
}


// append before closing tbody
// familyTable.find('tbody').append(new_tr);


// click saveBtn for new family member
$(nf_saveBtn).click(function (e) {
    e.preventDefault();

    var rowCount = familyTable.find('tbody').children().length;
    familyTable.find('tbody').append(getNewElement(++rowCount));



    // provide full name to the table
    familyTable.find('tbody tr:last .i_f-fullname').text(nf_fullname.val());
    familyTable.find('tbody tr:last [name=t_fullname]').val(nf_fullname.val());

    // provide relationship to the table
    if (nf_relation.find('option:selected').val() !== "") {
        familyTable.find('tbody tr:last .i_f-relation').text(nf_relation.find('option:selected').text());
        familyTable.find('tbody tr:last [name=t_relation]').val(nf_relation.find('option:selected').text());
    }

    // provide dob to the table
    familyTable.find('tbody tr:last .i_f-dob').text(nf_dob.val());
    familyTable.find('tbody tr:last [name=t_dob]').val(nf_dob.val());

    // provide gender to the table
    if (nf_gender.find('option:selected').val() !== "") {
        familyTable.find('tbody tr:last .i_f-gender').text(nf_gender.find('option:selected').text());
        familyTable.find('tbody tr:last [name=t_gender]').val(nf_gender.find('option:selected').text());
    }

    // provide marital status to the table
    if (nf_maritalStatus.find('option:selected').val() !== "") {
        familyTable.find('tbody tr:last .i_f-marital-status').text(nf_maritalStatus.find('option:selected').text());
        familyTable.find('tbody tr:last [name=t_marital-status]').val(nf_maritalStatus.find('option:selected').text());
    }

    // provide mobile to the table
    familyTable.find('tbody tr:last .i_f-mobile').text(nf_mobile.val());
    familyTable.find('tbody tr:last [name=t_mobile]').val(nf_mobile.val());
    familyTable.find('tbody tr:last [name=t_telephone]').val(nf_telephone.val());
    familyTable.find('tbody tr:last [name=t_email]').val(nf_email.val());

    // provide dob to the table
    familyTable.find('tbody tr:last .i_f-address').text(nf_address.val());
    familyTable.find('tbody tr:last [name=t_address]').val(nf_address.val());


});

console.timeEnd();





// });