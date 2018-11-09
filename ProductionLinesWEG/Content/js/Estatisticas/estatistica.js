function setDataStatistic(data){
  
}



var barras = document.getElementById("bar").getContext("2d");
var barras2 = document.getElementById("bar2").getContext("2d");
var barras3 = document.getElementById("bar3").getContext("2d");
var barras4 = document.getElementById("bar4").getContext("2d");

var bagulho = {
  type: "bar",
  data: {
    labels: ["Esteira A", "Esteira B", "Esteira C", "Esteira D", "Esteira E", "Esteira F", "Esteira G"],
    datasets: [
      {
        label: "Maior Tempo",
        backgroundColor: "rgb(2, 136, 209)",
        borderColor: "rgb(2, 136, 209)",
        data: [10, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      },
      {
        label: "Menor tempo",
        backgroundColor: "rgb(0, 200, 83)",
        borderColor: "rgb(0, 200, 83)",
        data: [10, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      },
      {
        label: "Media de tempo",
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
        text: 'Media de tempo por esteira'
    }
  }
};

var bagulho2 = {
  type: "line",
  data: {
    labels: ["Esteira A", "Esteira B", "Esteira C", "Esteira D", "Esteira E", "Esteira F", "Esteira G"],
    datasets: [
      {
        label: "PEÇA X",
        backgroundColor: "rgb(87, 136, 4)",
        borderColor: "rgb(87, 136, 4)",
        data: [90, 50, 80, 90, 100, 450, 120],
        lineTension: 0,
        fill: false
      },
      {
        label: "PEÇA Y",
        backgroundColor: "rgb(65, 0, 45)",
        borderColor: "rgb(65, 0, 45)",
        data: [150, 53, 10, 100, 150, 150, 620],
        lineTension: 0,
        fill: false
      },
      {
        label: "PEÇA Z",
        backgroundColor: "rgb(98, 46, 6)",
        borderColor: "rgb(98, 46, 6)",
        data: [65, 51, 98, 45, 160, 650, 170],
        lineTension: 0,
        fill: false
      },
      {
        label: "PEÇA C",
        backgroundColor: "rgb(2, 6, 99)",
        borderColor: "rgb(2, 6, 99)",
        data: [5, 65, 80, 46, 60, 450, 190],
        lineTension: 0,
        fill: false
      },
    ]
  },

  options: {
    tooltips: {
      mode: "index",
      intersect: false
    },
    title: {
        display: true,
        text: 'Tempo por peça'
    }
  }
};

var bagulho3 = {
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

var bagulho4 = {
  type: "doughnut",
  data: {
    labels: ["Peças Boas", "Peças Ruins"],
    datasets: [
      {
        labels: ["Peças boas"],
        backgroundColor: ["rgb(0, 175, 11)", "rgb(175, 0, 0)"],
        borderColor: ["rgb(0, 175, 11)", "rgb(175, 0, 0)"],
        data: [10, 50],
        fill: false
      },
    ]
  },

  options: {
    tooltips: {
      mode: "index",
      intersect: false
    },
    title: {
        display: true,
        text: 'Qualidade de produção de todas as esteiras'
    }
  }
};
var chartBarras = new Chart(barras, bagulho);
var chartBarras2 = new Chart(barras2, bagulho2);
var chartBarras3 = new Chart(barras3, bagulho3);
var chartBarras4 = new Chart(barras4, bagulho4);
