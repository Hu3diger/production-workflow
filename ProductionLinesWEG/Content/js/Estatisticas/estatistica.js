var ctx = document.getElementById("myChart").getContext("2d");
var ctx1 = document.getElementById("myChart1").getContext("2d");
var ctx3 = document.getElementById("myChart3").getContext("2d");
var ctx4 = document.getElementById("myChart4").getContext("2d");
var barras = document.getElementById("bar").getContext("2d");

var valores = [100, 100, 105, 102, 120, 103, 101];
var valores2 = [];
var graficos = [
  {
    label: "Esteira b",
    backgroundColor: "rgb(255, 99, 132)",
    borderColor: "rgb(255, 99, 132)",
    data: valores,
    lineTension: 0,
    fill: false
  },
  {
    label: "Esteira c",
    backgroundColor: "rgb(30, 99, 132)",
    borderColor: "rgb(30, 99, 132)",
    lineTension: 0,
    data: [200, 205, 150, 120, 120, 130, 100],
    fill: false
  }
];

for (let i = 0; i < 7; i++) {
  valores2.push(i * 10 - i * 5);
}

var x = {
  label: "Esteira a",
  backgroundColor: "rgb(30, 255, 132)",
  borderColor: "rgb(30, 255, 132)",
  data: valores2,
  lineTension: 0,
  fill: false
};

graficos.push(x);
var itens = {
  type: "line",
  data: {
    labels: ["1001", "1002", "1003", "1004", "1005", "1006", "1007"],
    datasets: graficos
  },

  options: {
    tooltips: {
      mode: "index",
      intersect: false
    },
    title: {
        display: true,
        text: 'Alguma coisa'
    }
  }
};

var bagulho = {
  type: "bar",
  data: {
    labels: ["Esteira A", "Esteira B", "Esteira C", "Esteira D", "Esteira E", "Esteira F", "Esteira G"],
    datasets: [
      {
        label: "Produzidas",
        backgroundColor: "rgb(2, 136, 209)",
        borderColor: "rgb(2, 136, 209)",
        data: [10, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      },
      {
        label: "Perfeitas",
        backgroundColor: "rgb(0, 200, 83)",
        borderColor: "rgb(0, 200, 83)",
        data: [10, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      },
      {
        label: "Falhadas",
        backgroundColor: "rgb(213, 0, 0)",
        borderColor: "rgb(213, 0, 0)",
        data: [10, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      }
    ]
  },

  options: {
    tooltips: {
      mode: "index",
      intersect: false
    },
    title: {
        display: true,
        text: 'Total de peças produzidas, falhadas e perfeitas'
    }
  }
};

var chart = new Chart(ctx, itens);
var chart1 = new Chart(ctx1, itens);
var chart1 = new Chart(ctx3, itens);
var chart1 = new Chart(ctx4, itens);

var chartBarras = new Chart(barras, bagulho);
setTimeout(function(){
    bagulho.data.datasets[0].data = [15, 55, 470, 58, 100, 45, 12];
    chartBarras = new Chart(barras, bagulho);
}, 3000);
