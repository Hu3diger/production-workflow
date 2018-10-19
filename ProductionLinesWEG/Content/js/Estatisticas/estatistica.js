var ctx = document.getElementById('myChart').getContext('2d');
var listinha = [0, 10, 5, 2, 20, 30, 10];
var listona = [{
    label: "Primeiro",
    backgroundColor: 'rgb(255, 99, 132)',
    borderColor: 'rgb(255, 99, 132)',
    data:listinha,
    fill: false,
},
{
    label: "Segundo",
    backgroundColor: 'rgb(30, 99, 132)',
    borderColor: 'rgb(30, 99, 132)',
    data: [0, 5, 50, 12, 120, 30, 10],
    fill: false,
}];

var kappa = [];

for (let i = 0; i < 7; i++) {
    kappa.push(i*10);
}

var x = {
    label: "Terceiro",
    backgroundColor: 'rgb(30, 255, 132)',
    borderColor: 'rgb(30, 255, 132)',
    data: kappa,
    fill: false,
};

listona.push(x);

var itens = {
    type: 'line',
    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: listona
    },


    options: {
        tooltips: {
            mode: 'index',
            intersect: false,
        },
    }
};

var chart = new Chart(ctx, itens);