var ctx = document.getElementById('myChart').getContext('2d');

var valores = [100, 100, 105, 102, 120, 103, 101];
var valores2 = [];
var graficos = [{
    label: "Esteira com processo b",
    backgroundColor: 'rgb(255, 99, 132)',
    borderColor: 'rgb(255, 99, 132)',
    data:valores,
    lineTension: 0,
    fill: false,
},
{
    label: "Esteira com processo c",
    backgroundColor: 'rgb(30, 99, 132)',
    borderColor: 'rgb(30, 99, 132)',
    lineTension: 0,
    data: [200, 205, 150, 120, 120, 130, 100],
    fill: false,
}];

for (let i = 0; i < 7; i++) {
    valores2.push(i*10 - i*5);
}

var x = {
    label: "Esteira com Processo a",
    backgroundColor: 'rgb(30, 255, 132)',
    borderColor: 'rgb(30, 255, 132)',
    data: valores2,
    lineTension: 0,
    fill: false,
};

graficos.push(x);
var itens = {
    type: 'line',
    data: {
        labels: ["1001", "1002", "1003", "1004", "1005", "1006", "1007"],
        datasets: graficos
    },


    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
    }
};

var chart = new Chart(ctx, itens);