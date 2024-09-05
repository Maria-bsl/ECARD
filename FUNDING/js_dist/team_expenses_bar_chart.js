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

var expenses_bar_chart_data = {};
teamExpenses_bar_canvas = document.getElementById("expenses_bar_chart");

var expensesdBarChart = new Chart(
  teamExpenses_bar_canvas,
  barChartData(expenses_bar_chart_data)
);

teamExpenses_bar_canvas.onclick = function (evt) {
  var activePoints = expensesdBarChart.getElementsAtEvent(evt);
  if (activePoints[0]) {
    var chartData = activePoints[0]["_chart"].config.data;
    var idx = activePoints[0]["_index"];

    // Get active label
    var label = chartData.labels[idx];

    // Get active value
    var value = chartData.datasets[0].data[idx];

    // var url = "http://example.com/?label=" + label;
    // console.log(url);
    // alert(url);

    $('#teamExpenses_modal').modal('show');
  }
};
