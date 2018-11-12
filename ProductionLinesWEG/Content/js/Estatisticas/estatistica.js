var graphic1 = document.getElementById("bar").getContext("2d");
var graphic2 = document.getElementById("bar2").getContext("2d");
var graphic3 = document.getElementById("bar3").getContext("2d");
var graphic4 = document.getElementById("bar4").getContext("2d");

function setDataStatistic(data){
  var maxPieces = 0;
  var totalSuccess = 0;
  var totalFail = 0;

  var g1 = {
    type: "bar",
    data: {
      labels: [],
      datasets: [
        {
          label: "Maior Tempo",
          backgroundColor: "rgb(2, 136, 209)",
          borderColor: "rgb(2, 136, 209)",
          data: [],
          lineTension: 0,
          fill: false
        },
        {
          label: "Menor tempo",
          backgroundColor: "rgb(0, 200, 83)",
          borderColor: "rgb(0, 200, 83)",
          data: [],
          lineTension: 0,
          fill: false
        },
        {
          label: "Media de tempo",
          backgroundColor: "rgb(213, 0, 0)",
          borderColor: "rgb(213, 0, 0)",
          data: [],
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

  var g2 = {
    type: "line",
    data: {
      labels: [],
      datasets: []
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

  var g3 = {
    type: "bar",
    data: {
      labels: [],
      datasets: [
        {
          label: "Produzidas",
          backgroundColor: "rgb(2, 136, 209)",
          borderColor: "rgb(2, 136, 209)",
          data: [],
          lineTension: 0,
          fill: false
        },
        {
          label: "Perfeitas",
          backgroundColor: "rgb(0, 200, 83)",
          borderColor: "rgb(0, 200, 83)",
          data: [],
          lineTension: 0,
          fill: false
        },
        {
          label: "Falhadas",
          backgroundColor: "rgb(213, 0, 0)",
          borderColor: "rgb(213, 0, 0)",
          data: [],
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

  var g4 = {
    type: "doughnut",
    data: {
      labels: ["Peças Boas", "Peças Ruins"],
      datasets: [
        {
          labels: ["Peças boas"],
          backgroundColor: ["rgb(0, 175, 11)", "rgb(175, 0, 0)"],
          borderColor: ["rgb(0, 175, 11)", "rgb(175, 0, 0)"],
          data: [],
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

  for(var x in data){
    let item = data[x];

    // ===== Graphics 1 load =====
    g1.data.labels.push(item.Name);
    var maior = null;
    var menor = null;
    var media = 0;

    for(var y in item.listTime){
      let itemTime = item.listTime[y];

      if(itemTime > maior || maior == null){
        maior = itemTime;
      }
      if(itemTime < menor || menor == null){
        menor = itemTime;
      }

      media += itemTime;
    }

    if(item.listTime.length > 0){
      media = media / item.listTime.length;
    }

    if(maior == null){
      maior = 0;
    }

    if(menor == null){
      menor = 0;
    }

    g1.data.datasets[0].data[data.indexOf(item)] = maior;
    g1.data.datasets[1].data[data.indexOf(item)] = menor;
    g1.data.datasets[2].data[data.indexOf(item)] = media;

    // ===== Graphics 2 Load =====
    if(item.listTime.length > maxPieces){
      maxPieces = item.listTime.length;
    }
    
    var color = "rgb(" +
    Math.floor(Math.random() * 256) + "," +
    Math.floor(Math.random() * 256) + "," +
    Math.floor(Math.random() * 256) + ")";

    var dt = {
      label: item.Name,
      backgroundColor: color,
      borderColor: color,
      data: item.listTime,
      lineTension: 0,
      fill: false
    }

    g2.data.datasets.push(dt);

    // ===== Graphics 3 Load =====
    g3.data.labels.push(item.Name);
    g3.data.datasets[0].data[data.indexOf(item)] = item.Success + item.Fail;
    g3.data.datasets[1].data[data.indexOf(item)] = item.Success;
    g3.data.datasets[2].data[data.indexOf(item)] = item.Fail;

    // ===== Graphics 4 Load =====
    totalSuccess += item.Success;
    totalFail += item.Fail;
  }

  for (let i = 1; i < maxPieces + 1; i++) {
    g2.data.labels.push(i);
  }

  if(totalFail > 0 || totalSuccess > 0){
    g4.data.datasets[0].data[0] = totalSuccess;
    g4.data.datasets[0].data[1] = totalFail;
  }

  new Chart(graphic1, g1);
  new Chart(graphic2, g2);
  new Chart(graphic3, g3);
  new Chart(graphic4, g4);
}

