// const sponsership_pie_chart_data = {
//   // labels: ["Red", "Orange", "Yellow", "Green", "Blue"],
//   datasets: [
//     {
//       borderWidth: 0.25,
//       weight: 0.8,

//       backgroundColor: [
//         "rgba(255, 0, 0, 0.785)",
//         "rgba(255, 165, 0, 0.785)",
//         "rgba(255, 255, 0, 0.785)",
//         "rgba(0, 128, 0, 0.785)",
//         "rgba(0, 0, 255, 0.785)",
//       ],
//       hoverBackgroundColor: [
//         "rgba(255, 0, 0, 0.85)",
//         "rgba(255, 165, 0, 0.85)",
//         "rgba(255, 255, 0, 0.85)",
//         "rgba(0, 128, 0, 0.85)",
//         "rgba(0, 0, 255, 0.85)",
//       ],
//     },
//   ],
// };

// var sponsorship_pie_canvas = document.getElementById("sponsorship_pie_chart");

// var sponsorPieChart = new Chart(
//   sponsorship_pie_canvas,
//   pieChartData(sponsership_pie_chart_data)
// );

// sponsorship_pie_canvas.onclick = function(evt) {
//   var activePoints = sponsorPieChart.getElementsAtEvent(evt);
//   if (activePoints[0]) {
//     var chartData = activePoints[0]['_chart'].config.data;
//     var idx = activePoints[0]['_index'];

//     // Get active label
//     var label = chartData.labels[idx];

//     // Get active value
//     var value = chartData.datasets[0].data[idx];

//     var url = "http://example.com/?label=" + label;
//     console.log(url);
//     alert(url);
//   }
// };

