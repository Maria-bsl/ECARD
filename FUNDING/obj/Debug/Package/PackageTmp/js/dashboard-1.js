$(document).ready(function () {
    var believerChart = document.getElementById('believerChart');
    var contributionChart = document.getElementById('contributionChart');
    var userLogs = document.getElementById('userLogs');

    // Exception Handling
    if (believerChart) {
        believerChart.getContext('2d');
        var myChart = new Chart(believerChart, {
            type: 'bar',
            data: {
                labels: af.split(','),
                datasets: [{
                    label: 'Male',
                    data: ym.split(','),
                    backgroundColor: '#3869AE',
                    borderColor: 'rgba(1, 12, 23, 0.35)',
                    borderWidth: 0,
                    hoverBackgroundColor: '#3869C0',

                },
                {
                    label: 'Female',
                    data: yf.split(','),
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
                    text: Nb,
                    fontStyle: "bold",
                    fontSize: 18,
                },
                scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: BB,
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
                            labelString: Age,
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
            type: 'pie',
            data: {

                //labels: ['Ahadi','jengo','Bell Tower','Fungu ka lumbi','General'],
                datasets: [{
                    label: 'Contributions in TZS',
                    // data: [12500000, 4950000, 5250000, 7250000, 1665500],
                    borderColor: 'rgba(32, 179, 66, 0.85)',
                    fill: true,
                    hoverOffset: 4,
                    backgroundColor: ['#E5E326', '#032B8B', '#00BDE8', '#EFAE22', '#936E6E', '#2D5D69',
                        '#2D5D69', '#2D5D69', '#87A878', '#F18805', '#A22522', '#DBF9B8',
                        '#C7CCB9', '#2F2235', '#FE5F55', '#6E2594', '#091E05', '#004F2D',
                    ],
                }]
            },
            options: {
                legend: {
                    display: true,
                    position: 'bottom',
                },
                responsive: true,
                maintainAspectRatio: false

            }
        });

        jQuery('#todayContributions').click(function (e) {
            e.preventDefault();
            updateContributionsDay();
        });
        jQuery('#weekContributions').click(function (e) {
            e.preventDefault();
            updateContributionsWeek();
        });
        jQuery('#monthContributions').click(function (e) {
            e.preventDefault();
            updateContributionsMonth();
        });
        jQuery('#yearContributions').click(function (e) {
            e.preventDefault();
            updateContributionsYear();
        });

        var durationSettings = jQuery('#todayContributions').next();
        updateContributionsDay();
        function updateContributionsDay() {

            jQuery(durationSettings).find('span:first').removeClass('d-none').next().removeClass('d-none').next().removeClass('d-none');
            myChart.data.labels = dtt1.split(',');
            myChart.data.datasets[0].data = dtt2.split(',');
            // myChart.options.scales.xAxes[0].scaleLabel.labelString = "Contribution Type (in a day)";
            myChart.update();
        }

        function updateContributionsWeek() {
            myChart.data.labels = dtt1.split(',');
            myChart.data.datasets[0].data = dtt2.split(',');
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().removeClass('d-none');
            // myChart.options.scales.xAxes[0].scaleLabel.labelString = "Contribution Type (in a week)";
            myChart.update();
        }

        function updateContributionsMonth() {
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().removeClass('d-none');
            myChart.data.labels = dtt1.split(',');
            myChart.data.datasets[0].data = dtt2.split(',');
            //console.log(dtt2.split(',').map(function (x) { Number(x) }));
            // myChart.options.scales.xAxes[0].scaleLabel.labelString = "Contribution Type (in a month)";
            myChart.update();
        }

        function updateContributionsYear() {
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().addClass('d-none');
            myChart.data.labels = dtt1.split(',');
            myChart.data.datasets[0].data = dtt2.split(',')
            // myChart.options.scales.xAxes[0].scaleLabel.labelString = "Contribution Type (in a year)";
            myChart.update();
        }
    }

    // Exception Handling
    if (userLogs) {
        userLogs.getContext('2d');
        var myChart = new Chart(userLogs, {
            type: 'line',
            data: {
                //labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
                datasets: [{
                    label: 'Institutions',
                    //data: [125, 49, 52, 72, 149, 52, 172, 12],
                    borderColor: '#E8E737',
                    fill: true,
                    lineTension: 0.2,
                    pointHoverRadius: 10,
                    backgroundColor: 'rgba(246, 246, 246, 0.4)',
                }, {
                    label: 'Bank Staff',
                    //data: [25, 149, 52, 172, 19, 2, 72, 102],
                    borderColor: '#3869AE',
                    fill: true,
                    lineTension: 0.2,
                    pointHoverRadius: 10,
                    backgroundColor: 'rgba(246, 246, 246, 0.4)',
                }, {
                    label: 'Believers',
                    //data: [12, 23, 44, 64, 85, 94, 64],
                    borderColor: 'green',
                    fill: true,
                    lineTension: 0.2,
                    pointHoverRadius: 10,
                    backgroundColor: 'rgba(246, 246, 246, 0.4)',
                }],
            },
            options: {
                title: {
                    // ! Disable title {Shilloh}
                    display: false,
                    fontStyle: "bold",
                    fontSize: 18,
                },
                scales: {
                    yAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Users',
                            fontSize: 14,
                            fontStyle: "bold",
                        },
                        ticks: {
                            min: 0,
                            beginAtZero: true,
                            // stepSize: 1,
                            callback: function (value, index, values) {
                                if (Math.floor(value) === value) {
                                    return value;
                                }
                            }
                        },
                    }],
                    xAxes: [{
                        scaleLabel: {
                            display: true,
                            labelString: 'Time (Days)',
                            fontSize: 14,
                            fontStyle: "bold",
                        },
                        maxBarThickness: 60,
                    }]
                },
                legend: {
                    display: true,
                },
                responsive: true,
                maintainAspectRatio: false

            }

        });

        jQuery('#todayLogs').click(function (e) {
            e.preventDefault();
            updateLogDay();
        });

        jQuery('#weekLogs').click(function (e) {
            e.preventDefault();
            updateLogWeek();
        });

        jQuery('#monthLogs').click(function (e) {
            e.preventDefault();
            updateLogMonth();
        });

        jQuery('#yearLogs').click(function (e) {
            e.preventDefault();
            updateLogYear();
        });

        var durationSettings = jQuery('#todayLogs').next();
        updateLogDay();
        function updateLogDay() {
            jQuery(durationSettings).find('span:first').removeClass('d-none').next().removeClass('d-none').next().removeClass('d-none');
            myChart.data.labels = ['12am - 5am', '6am - 11am', '12pm - 5pm', '6pm - 12pm',];
            myChart.data.datasets[0].data = [i1, i2, i3, i4];
            myChart.data.datasets[1].data = [s1, s2, s3, s4];
            myChart.data.datasets[2].data = [b1, b2, b3, b4];
            myChart.options.scales.xAxes[0].scaleLabel.labelString = TH;
            myChart.update();
        }

        function updateLogWeek() {
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().removeClass('d-none');
            myChart.data.labels = [w7, w6, w5, w4, w3, w2, w1];
            myChart.data.datasets[0].data = [d7, d6, d5, d4, d3, d2, d1, 12];
            myChart.data.datasets[1].data = [s7, s6, s5, s4, s3, s2, s1, s1];
            myChart.data.datasets[2].data = [b7, b6, b5, b4, b3, b2, b1];
            myChart.options.scales.xAxes[0].scaleLabel.labelString = TD;
            myChart.update();
        }

        function updateLogMonth() {
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().removeClass('d-none');
            myChart.data.labels = ['1 - 5', '6 - 10', '11 - 15', '16-20', '21-25', '26-30'];
            myChart.data.datasets[0].data = [d1, d2, d3, d4, d5, d6, 172];
            myChart.data.datasets[1].data = [s1, s2, s3, s4, s5, s6, 72];
            myChart.data.datasets[2].data = [b1, b2, b3, b4, b5, b6];
            myChart.options.scales.xAxes[0].scaleLabel.labelString = dat;
            myChart.update();
        }

        function updateLogYear() {
            jQuery(durationSettings).find('span:first').addClass('d-none').next().addClass('d-none').next().addClass('d-none');
            myChart.data.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            myChart.data.datasets[0].data = [d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, 172, 12];
            myChart.data.datasets[1].data = [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, 72, 102];
            myChart.data.datasets[2].data = [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12];
            myChart.options.scales.xAxes[0].scaleLabel.labelString = TM;
            myChart.update();
        }
    }
});

