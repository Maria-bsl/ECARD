/* Static properties */
Chart.defaults.global.defaultFontColor = "rgb(33, 37, 41)";

var backgroundColor = [
  "rgba(0, 0, 255, 0.785)",
  "rgba(255, 165, 0, 0.785)",
  "rgba(0, 128, 0, 0.785)",
  "rgba(255, 255, 0, 0.785)",
  "rgba(255, 0, 0, 0.785)",
];
var hoverBackgroundColor = [
  "rgba(0, 0, 255, 0.85)",
  "rgba(255, 165, 0, 0.85)",
  "rgba(0, 128, 0, 0.85)",
  "rgba(255, 255, 0, 0.85)",
  "rgba(255, 0, 0, 0.85)",
];

/* ################################################################################ */
/* ################################################################################ */

function pieChartData(data) {
  var config_pieChart = {
    type: "pie",
    data: data,
    options: {
      responsive: true,
      maintainAspectRatio: false,
      legend: {
        display: true,
        labels: {
          usePointStyle: true,
        },
      },

      layout: {
        padding: {
          // top: 5,
        },
      },

      tooltips: {
        callbacks: {
          label: function (tooltipItem, data) {
            var label = data.labels[tooltipItem.index] || "";

            if (label) {
              label += ": TZS ";
            }

            var tooTipData =
              data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];

            label += numeral(tooTipData).format("0,0");
            return label;
          },
        },
      },

      plugins: {
        legend: {
          position: "top",
        },
      },
    },
    plugins: [
      {
        beforeInit: function (chart, options) {
          chart.legend.afterFit = function () {
            this.height = this.height + 15;
          };
        },
      },
    ],
  };

  return config_pieChart;
}

function stackedBarChartData(data) {
  var config_stackedBarChart = {
    type: "bar",
    data: data,
    options: {
      maintainAspectRatio: false,
      responsive: true,

      legend: {
        display: true,
        labels: {
          usePointStyle: true,
        },
      },

      tooltips: {
        callbacks: {
          label: function (tooltipItem, data) {
            var label = data.datasets[tooltipItem.datasetIndex].label || "";

            if (label) {
              label += ": TZS ";
            }

            label += numeral(tooltipItem.yLabel).format("0,0");
            return label;
          },
        },
      },

      scales: {
        xAxes: [
          {
            stacked: true,
            scaleLabel: {
              display: true,
              labelString: "Teams",
              fontFamily: "sans-serif",
              fontSize: 13,
              fontStyle: "600",
            },
            barThickness: "flex",
            maxBarThickness: 87.5,
            categoryPercentage: 1,
            barPercentage: 0.5,
            gridLines: {
              offsetGridLines: true,
            },
          },
        ],
        yAxes: [
          {
            stacked: true,
            scaleLabel: {
              display: true,
              labelString: "Amount",
              fontFamily: "sans-serif",
              fontSize: 13,
              fontStyle: "600",
            },
            ticks: {
              beginAtZero: true,

              userCallback: function (label) {
                /*
                 when the floored value is the same as the value we have a whole number
                 */
                if (Math.floor(label) === label) {
                  // return label;
                  return "TZS " + numeral(label).format("0,0");
                }
              },
            },
          },
        ],
      },
    },
    plugins: [
      {
        beforeInit: function (chart, options) {
          chart.legend.afterFit = function () {
            this.height = this.height + 15;
          };
        },
      },
    ],
  };

  return config_stackedBarChart;
}

function barChartData(data) {
  var config_barChart = {
    type: "bar",
    data: data,
    options: {
      maintainAspectRatio: false,
      responsive: true,

      legend: {
        display: false,
      },

      hover: {
        onHover: function (e) {
          var point = this.getElementAtEvent(e);
          if (point.length) e.target.style.cursor = "pointer";
          else e.target.style.cursor = "default";
        },
      },

      tooltips: {
        callbacks: {
          label: function (tooltipItem, data) {
            var label = data.labels[tooltipItem.index] || "";

            if (label) {
              label += ": TZS ";
            }

            var tooTipData =
              data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];

            label += numeral(tooTipData).format("0,0");

            return label;
          },
        },
      },

      scales: {
        xAxes: [
          {
            scaleLabel: {
              display: true,
              labelString: "Teams",
              fontFamily: "sans-serif",
              fontSize: 13,
              fontStyle: "600",
            },
            barThickness: "flex",
            maxBarThickness: 87.5,
            categoryPercentage: 1,
            barPercentage: 0.5,
            gridLines: {
              offsetGridLines: true,
            },
          },
        ],
        yAxes: [
          {
            scaleLabel: {
              display: true,
              labelString: "Amount",
              fontFamily: "sans-serif",
              fontSize: 13,
              fontStyle: "600",
            },
            ticks: {
              beginAtZero: true,

              userCallback: function (label) {
                /*
                 when the floored value is the same as the value we have a whole number
                 */
                if (Math.floor(label) === label) {
                  // return label;
                  return "TZS " + numeral(label).format("0,0");
                }
              },
            },
          },
        ],
      },
    },
  };

  return config_barChart;
}

/**
 * This function transforms text to uppercase.
 *
 * @param {*} text text to be transformed to uppercase
 * @returns Transformed text into uppercase
 */
function textToUppecase(text) {
  return text.toUpperCase();
}

/**
 * This function intializes the chart instance based on the chart type.
 *
 * @param {#} chart_canvas_id text of id property of chart canvas
 * @param {*} chart_data the configuration dataset of the chart
 * @param {*} type type of chart [e.g. pie, bar, or stakedbar]
 * @returns Initialized chart instance.
 */
function chartInit(chart_canvas_id, chart_data, type) {
  if (textToUppecase(type) === "PIE") {
    return new Chart(
      document.getElementById(chart_canvas_id),
      pieChartData(chart_data)
    );
  } else if (textToUppecase(type) === "BAR") {
    return new Chart(
      document.getElementById(chart_canvas_id),
      barChartData(chart_data)
    );
  } else if (textToUppecase(type) === "STACKEDBAR") {
    return new Chart(
      document.getElementById(chart_canvas_id),
      stackedBarChartData(chart_data)
    );
  }
}

function stackedChart_multiDatasets(labels, sponsor, sponsorshipValue) {
  var datasetValue = [];
  var i_count = sponsor.length;

  for (var j = 0; j < i_count; j++) {
    datasetValue[j] = {
      backgroundColor: backgroundColor[j],
      hoverBackgroundColor: hoverBackgroundColor[j],
      label: sponsor[j],
      data: sponsorshipValue[j],
      borderWidth: 0.875,
      borderColor: "#9b9b9b",
    };
  }
  chartUpdate_mutli_dataset(
    disbursement_stacked_bar_chart_data,
    labels,
    datasetValue,
    disbursementStackedBarChart
  );
}

function barChart_multiDatasets(labels, sponsorshipValue) {
  datasetValue = [
    {
      backgroundColor: backgroundColor,
      hoverBackgroundColor: hoverBackgroundColor,
      data: sponsorshipValue,
      borderWidth: 0.875,
      borderColor: "#9b9b9b",
    },
  ];

  chartUpdate_mutli_dataset(
    teamExpenses_bar_chart_data,
    labels,
    datasetValue,
    expensesdBarChart
  );
}

/**
 * This function updates / initialize the chart with single dataset.
 *
 * @param {array} labels array of chart labels.
 * @param {array} data array of chart data.
 * @param {*} Chart the instance of the chart.
 */
function chartUpdate_single_dataset(chart_data, labels, data, Chart) {
  chart_data.labels = labels;
  chart_data.datasets[0].data = data;
  Chart.update();
}

/**
 * This function updates / initialize the chart with multiple dataset.
 *
 * @param {array} labels array of chart labels.
 * @param {array} datasets array of chart datasets.
 * @param {*} Chart the instance of the chart.
 */
function chartUpdate_mutli_dataset(chart_data, labels, datasets, Chart) {
  chart_data.labels = labels;
  chart_data.datasets = datasets;
  Chart.update();
}

/* ################################################################################ */
/* ################################################################################ */

var balance_pie_chart_data = {
  datasets: [
    {
      borderWidth: 0.25,
      weight: 0.8,
      backgroundColor: backgroundColor,
      hoverBackgroundColor: hoverBackgroundColor,
    },
  ],
};

var team_expenses_pie_chart_data = {
  datasets: [
    {
      borderWidth: 0.25,
      weight: 0.8,
      backgroundColor: backgroundColor,
      hoverBackgroundColor: hoverBackgroundColor,
    },
  ],
};

var sponsership_pie_chart_data = {
  datasets: [
    {
      borderWidth: 0.25,
      weight: 0.8,
      backgroundColor: backgroundColor,
      hoverBackgroundColor: hoverBackgroundColor,
    },
  ],
};

/* ############################################################################## */

var balancePieChart = chartInit(
  "balance_pie_chart",
  balance_pie_chart_data,
  "pie"
);

var teamExpensesPieChart = chartInit(
  "team_expenses_pie_chart",
  team_expenses_pie_chart_data,
  "pie"
);

var sponsorPieChart = chartInit(
  "sponsorship_pie_chart",
  sponsership_pie_chart_data,
  "pie"
);

/* ################################################################################ */
/* ################################################################################ */

var disbursement_stacked_bar_chart_data = {};

var disbursementStackedBarChart = chartInit(
  "disbursement_stacked_bar_chart",
  disbursement_stacked_bar_chart_data,
  "stackedbar"
);

/* ################################################################################ */
/* ################################################################################ */

var teamExpenses_bar_chart_data = {
  datasets: [
    {
      borderWidth: 0.25,
      weight: 0.8,
      backgroundColor: backgroundColor,
      hoverBackgroundColor: hoverBackgroundColor,
    },
  ],
};

var expensesdBarChart = chartInit(
  "expenses_bar_chart",
  teamExpenses_bar_chart_data,
  "bar"
);

function chartModalProcessing(
  teamIndividualExpenses,
  teamIndividualExpensesLabels
) {
  var teamExpenses_bar_canvas = document.getElementById("expenses_bar_chart");

  teamExpenses_bar_canvas.onclick = function (evt) {
    var activePoints = expensesdBarChart.getElementsAtEvent(evt);
    if (activePoints[0]) {
      var chartData = activePoints[0]["_chart"].config.data;
      var idx = activePoints[0]["_index"];

      // Team Expenses Chart
      chartUpdate_single_dataset(
        team_expenses_pie_chart_data,
        teamIndividualExpensesLabels[idx],
        teamIndividualExpenses[idx],
        teamExpensesPieChart
      );

      // Get active label
      var label = chartData.labels[idx];
      $("#teamName").text(label);

      var teamNames = $("#teamSelect option");

      $.each(teamNames, function (indexInArray, teamName) {
        if ($(teamName).text() === label) {
          $(teamName).attr("selected", "selected");
        }
      });

      $("#teamExpenses_modal").modal("show");
    }
  };
}
