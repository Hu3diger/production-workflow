var Xconectors = 11;
var countIdEm = 0;
var countIdEa = 0;
var countIdEe = 0;
var countIdEd = 0;

var instances2;
var idModal = "modal41";
var sobre = "";

var minX = 1;
var minY = 1;

var ListPieces = [];

$(document).ready(function () {
    $('.modal1').modal();
    $('.modal2').modal();

    $('.modal1').modal({
        onCloseStart: function(){
            $("#item").data("id", "");
        },
        onOpenStart: function () {
            getTickEsteira();
        }
    })
});

function getIds() {
    connector.server.getValuesPgm().done(function (reciveContentTable) {

        countIdEm = reciveContentTable.Array[0];
        countIdEa = reciveContentTable.Array[1];
        countIdEe = reciveContentTable.Array[2];
        countIdEd = reciveContentTable.Array[3];

        minX = reciveContentTable.Array[4];
        minY = reciveContentTable.Array[5];

        $("#tamanho_x").val(minX);
        $("#tamanho_y").val(minY);

        assignItemsTable(reciveContentTable.ArrayMapCells);
    
        $("#loading").hide();
    });
}

// para controlar a imagem dos conectores
var CellAfterDrop;

// adiciona as propriedades aos objetos (drag e drop)
function setDropDragItens() {

    $(".dragBase").draggable({ helper: 'clone' });

    // TESTES APENAS
    $(".dragClone").click(function () {
        let fn = "chumbarPecaEsteira(" + this.id + ")";
        let tn = "changeOnOff(" + this.id + ")";

        let onAll = "turnOnAllFront(" + $(this).attr("id") + ")";
        let offAll = "turnOffAllFront(" + $(this).attr("id") + ")";

        $("#item").data("id", this.id);
        $("#btnChumbar").attr("onclick", fn);
        $("#btnOnAll").attr("onclick", onAll);
        $("#btnOffAll").attr("onclick", offAll);
        $("#modalName").html("<b>"+ $(this).data().Name +"</b>");
        $("#modalDescription").html("<b>Descrição da esteira</b>: "+ $(this).data().Description);
        $("#modalLimit").html("<b>Limite de entrada</b>: "+ ($(this).data().InLimit == -1 ? "Limite infinito de " : $(this).data().InLimit) + " peças");
        $("#modalType").html("<b>Tipo da esteira</b>: "+ $(this).data().Type);

        setOnOff(this.id, $(this).data().Ligado);

        switch ($(this).data().TypeN) {
            case 1:
                sobre = "Processo Master</b>: " + $(this).data().Addtional;
                break;
            case 2:
                sobre = "";
                break;
            case 3:
                sobre = "Valor inicial</b>: " + $(this).data().Addtional + " (Para geração de etiquetas)";
                break;
            case 4:
                sobre = "Outras parada</b>: " + $(this).data().Addtional;
                break;
        }
 
        $("#modalAbout").html("<b>"+ sobre +"</b>");

        connector.server.getPieces($(this).attr("id")).done(function (json) {
            setPiecesModal(json);
        })
        
        $('.modal2').modal('close');
        
    });


    $(".dragClone").draggable({
        revert: 'invalid',
        start: function (event, ui) {
            var gotItem = $(ui.helper);

            CellAfterDrop = gotItem.parent();

            var objImg = gotItem.children().get(0);

            // verifica se � do tipo imgem
            if ($(objImg).is("img")) {

                let str = $(objImg).attr("src");

                // o caminho da imagem sem o nome da imagem atual
                let str2 = str.substring(0, str.lastIndexOf("/cn") + 3);

                // apenas o numero da imagem
                var nameImg = str.substring(
                    str.lastIndexOf("/cn") + 3, // remover 'cn' do nome d imagem
                    str.lastIndexOf(".png")
                );

                // verifica se a imagem contem o nome correto e o X, caso tiver, remove
                for (var i = 1; i <= Xconectors; i++) {

                    if (nameImg == (i + "x")) {

                        nameImg = nameImg.substring(0, nameImg.lastIndexOf("x"));
                        break;

                    }
                }

                // adicion o novo nome ao caminho da imagem
                str2 += nameImg + ".png";

                // seta o caminho a imagem
                $(objImg).attr("src", str2);

            }
        },
        stop: function (ev, ui) {
            $(this).attr('style', 'position: relative;');
        }
    });

    $(".tCell").droppable({
        // aceita os objeto que contenham as seguintes classes
        accept: '.dragBase, .dragClone',
        drop: function (ev, ui) {
            var droppedItem = $(ui.draggable);

            // verifica se contem nada na celula ou a celular contem o mesmo item que foi dropado
            if ($(this).children().length == 0 || droppedItem != $(this).children().get(0)) {

                if (droppedItem.hasClass("dragBase")) {

                    let tempData = droppedItem.data();

                    droppedItem = droppedItem.clone();
                    droppedItem.removeClass("dragBase").addClass("dragClone modal-trigger");
                    droppedItem.attr("data-target", idModal);

                    var idM = droppedItem.attr("id");

                    if (tempData.TypeN == 1) {
                        droppedItem.attr("id", idM + "c" + countIdEm++);
                    } else if (tempData.TypeN == 2) {
                        droppedItem.attr("id", idM + "c" + countIdEa++);
                    } else if (tempData.TypeN == 3) {
                        droppedItem.attr("id", idM + "c" + countIdEe++);
                    } else if (tempData.TypeN == 4) {
                        droppedItem.attr("id", idM + "c" + countIdEd++);
                    }

                    // for each com com os valores de tempData
                    $.each(tempData, function (i, item) {
                        if (i != "uiDraggable") {
                            droppedItem.data(i, item);
                        }
                    });

                    droppedItem.data("idM", idM);

                }

                $(this).html(droppedItem);

                // reajusta a tabela
                reajustTable();
            }


            if (CellAfterDrop) {
                analyzeCell(CellAfterDrop);
            }

            analyzeCell(droppedItem.parent());
        }
    });

    $('.dTrash').droppable({
        drop: function (event, ui) {
            var droppedItem = $(ui.draggable);

            if (!droppedItem.hasClass("dragBase")) {
                // remove o item do "sistema"
                ui.draggable.remove();
            }

            reajustTable();

            if (CellAfterDrop) {
                analyzeCell(CellAfterDrop);
            }
        }
    });
}

function modal2show(i) {

    var html = "";
    $("#modalTag").html("<b>Tag:</b> " + ListPieces[i].Tag);

    html += "\
    <tr>\
        <th>Id</th>\
        <th>Nome</th>\
        <th>Estado</th>\
        <th>Value</th>\
        <th>Data</th>\
        <th>Tempo Exc.</th>\
    </tr>\
    ";


    let list = ListPieces[i].ListAtributos;
    for (var j = 0; j < list.length; j++) {
        html += "\
            <tr>\
                <td>"+ list[j].IdP +"</td>\
                <td>"+ list[j].NameP +"</td>\
                <td>"+ list[j].Estado +"</td>\
                <td>"+ list[j].Value +"</td>\
                <td>"+ list[j].Data +"</td>\
                <td>"+ list[j].Time +"ms</td>\
            </tr>\
        ";
    }
    $("#tableAtPiece").html(html);
}

function changeOnOff(obj) {

    $(obj).data().Ligado = $("#onOffEsteira").prop('checked');

    if ($("#onOffEsteira").prop('checked')) {
        turnOnEsteira($(obj).attr("id"));
    } else {
        turnOffEsteira($(obj).attr("id"));
    }
}

function applyReajust() {
    minX = parseInt($("#tamanho_x").val());
    minY = parseInt($("#tamanho_y").val());

    reajustTable();
}

function chumbarPecaEsteira(obj) {

    chumbarPeca($(obj).attr("id"), $("#numberC").val());

}

// reajusta a tabela para que tenham uma linha e coluna a vazia no fim
function reajustTable() {

    let tBody = $("#tdropesteiras tbody");

    let bottom = tBody.get(0).lastElementChild;

    // verifica se a ultima linha contem algo
    var controlBottom = reajustTableVerifyLine($(bottom));

    // verifica se a ultima coluna contem algo
    var controlRight = reajustTableVerifyColumn(tBody, $(bottom).children().length - 1);

    // se conter algo na ultima coluna, ele gera uma nova coluna para todas as linhas
    if (controlRight) {

        for (var i = 0; i < tBody.children().length; i++) {
            if (i != 0) {
                $('<td class="tCell">').appendTo(tBody.children()[i]);
            } else {
                $('<td class="tCellColumn no-padding">').appendTo(tBody.children().get(0));
                $($(tBody.children().get(0)).children().get($(bottom).children().length)).html("" + ($(bottom).children().length));
            }
        }

        reajustTable();
    } else {
        if ($(bottom).children().length > minX + 1) {

            // verifica se contem algo na penultima linha para poder excluir a ultima
            if (!reajustTableVerifyColumn(tBody, $(bottom).children().length - 2)) {

                for (var i = 0; i < tBody.children().length; i++) {

                    var line = $(tBody.children().get(i));

                    $(line.children().get(line.children().length - 1)).remove();
                }

                reajustTable();
            }
        }
    }

    // se estiver na ultima linha, ele gera uma nova linha, sen�o 
    if (controlBottom) {

        let tr = $("<tr>").appendTo(tBody);

        for (var i = 0; i < $(bottom).children().length; i++) {
            if (i != 0) {
                $('<td class="tCell">').appendTo(tr);
            } else {
                $('<td class="tCellLines no-padding">').appendTo(tr);
                $(tr.children().get(0)).html("" + (tBody.children().length - 1));
            }
        }

        reajustTable();
    } else {
        if (tBody.children().length > minY + 1) {

            // verifica se contem algo na penultima linha para poder excluir a ultima
            if (!reajustTableVerifyLine($(tBody.children().get(tBody.children().length - 2)))) {
                $(tBody.get(0).lastElementChild).remove();
                reajustTable();
            }
        }
    }

    setDropDragItens();
}

// verifica se a linha inteira esta vazia
// false = vazia
function reajustTableVerifyLine(line) {

    if (line.parent().children().length < minY + 1) return true;

    for (var i = 0; i < line.children().length; i++) {

        // verifica se contem algo na celula
        if ($(line.children().get(i)).children().length != 0) {
            return true;
        }
    }

    return false;
}

// verifica se a coluna inteira esta vazia
// false = vazia
function reajustTableVerifyColumn(tBody, number) {

    if ($(tBody.get(0).lastElementChild).children().length < minX + 1) return true;

    for (var i = 0; i < tBody.children().length; i++) {

        var line = $(tBody.children().get(i));

        // verifica se contem algo na celula especificada
        if ($(line.children().get(number)).children().length != 0) {
            return true;
        }
    }

    return false;
}

// verifica a celula na frente e traz para ver se nao a conector para editar por perto
function analyzeCell(cell, reControl) {


    // faz modifica��es (se necessario) na imagem do conector
    // analiza a celula atual
    function analyzeCurrentCell(target, nextCell, directionClass) {

        // pega a div da celula
        let obj = $(target).children().get(0);

        // pega a div da celula
        let nObj = $(nextCell).children().get(0);

        if ($(obj).hasClass(directionClass)) {

            // verifica se estra preenchida
            if (obj) {

                // pega o objeto dentro da div
                let objImg = $(obj).children().get(0);

                // verifica se � do tipo imgem
                if ($(objImg).is("img")) {

                    // caminho da imagem toda
                    let str = $(objImg).attr("src");

                    // o caminho da imagem sem o nome da imagem atual
                    let str2 = str.substring(0, str.lastIndexOf("/cn") + 3);

                    // apenas o numero da imagem
                    var nameImg = str.substring(
                        str.lastIndexOf("/cn") + 3, // remover 'cn' do nome d imagem
                        str.lastIndexOf(".png")
                    );

                    // vriavel pra o novo nome da imagem
                    var newName = nameImg;

                    // verifica se a proxima celula contem algo e se conter, verifica se � outro conector
                    if (nObj && $(nObj).hasClass("conectorP") && $(nObj).hasClass("recive" + directionClass)) {

                        // verifica se a imagem contem o nome correto e o X, caso nao tiver, adiciona
                        for (var i = 1; i <= Xconectors; i++) {

                            if (("" + i) == nameImg) {

                                newName = nameImg + "x";
                                break;

                            }
                        }

                    } else {

                        // verifica se a imagem contem o nome correto e o X, caso tiver, remove
                        for (var i = 1; i <= Xconectors; i++) {

                            if (nameImg == (i + "x")) {

                                newName = nameImg.substring(0, nameImg.lastIndexOf("x"));
                                break;

                            }
                        }

                    }

                    // adicion o novo nome ao caminho da imagem
                    str2 += newName + ".png";

                    // seta o caminho a imagem
                    $($(obj).children().get(0)).attr("src", str2);
                }
            }
        }
    }









    // varialvel para o objeto dentro da celula
    let obj = $(cell.children().get(0));

    var Tr = cell.parent();

    var tBody = Tr.parent();

    let indexTr = tBody.children().index(Tr);

    var indexCell = Tr.children().index(cell);



    // celulas ao redor da principal
    let antCell = $(Tr.children().get(indexCell - 1));

    let nextCell = $(Tr.children().get(indexCell + 1));

    let upCell = $($(tBody.children().get(indexTr - 1)).children().get(indexCell));

    let downCell = $($(tBody.children().get(indexTr + 1)).children().get(indexCell));


    cell.removeClass("red darken-1");

    if (obj.hasClass("Cfront")) {
        // faz modifica��es (se necessario) na imagem do conector
        analyzeCurrentCell(cell, nextCell, "Cfront");

        // variavel de controle da recursividade
        if (!reControl) {
            // faz o processo com a proxima celular sem "habilitar" novamente a recursividade
            analyzeCell(nextCell, true);
        }

    } else if (obj.hasClass("Cup")) {

        analyzeCurrentCell(cell, upCell, "Cup");

        if (!reControl) {
            analyzeCell(upCell, true);
        }

    } else if (obj.hasClass("Cdown")) {

        analyzeCurrentCell(cell, downCell, "Cdown");

        if (!reControl) {
            analyzeCell(downCell, true);
        }

    }

    // verifica se existe a celula e se � possivel fazer a recursividade
    if (antCell && !reControl) {
        // faz a recursividade para verificar a celula vizinha
        analyzeCell(antCell, true);
    }

    if (upCell && !reControl) {
        analyzeCell(upCell, true);
    }

    if (nextCell && !reControl) {
        analyzeCell(nextCell, true);
    }

    if (downCell && !reControl) {
        analyzeCell(downCell, true);
    }

    // verifica se a celula existe
    if (antCell) {
        // analiza a celula e altera a imagem se necessario
        analyzeCurrentCell(antCell, cell, "Cfront");
        // verfica se o objeto da celula atual e tambem se a celula vizinha � uma esteira e se ela esta "enviando" na sua dire��o
        if (obj.hasClass("reciveCfront") && !$(antCell.children().get(0)).hasClass("esteiraP") && !$(antCell.children().get(0)).hasClass("Cfront")) {
            // marca a celula para representar um Erro
            cell.addClass("red darken-1");
        }
    }

    if (upCell) {
        analyzeCurrentCell(upCell, cell, "Cdown");

        if (obj.hasClass("reciveCdown") && !$(upCell.children().get(0)).hasClass("Cdown") && !$(upCell.children().get(0)).hasClass("reciveCup")) {
            cell.addClass("red darken-1");
        }
    }

    if (downCell) {
        analyzeCurrentCell(downCell, cell, "Cup");

        if (obj.hasClass("reciveCup") && !$(downCell.children().get(0)).hasClass("Cup") && !$(downCell.children().get(0)).hasClass("reciveCdown")) {
            cell.addClass("red darken-1");
        }
    }

    if (nextCell) {

        // verifica para qual dire��o o objeto da celula atual aponta e se tem algum conector ou esteira
        if (obj.hasClass("Cfront") && !$(nextCell.children().get(0)).hasClass("esteiraP") && (!$(nextCell.children().get(0)).hasClass("conectorP") || !$(nextCell.children().get(0)).hasClass("reciveCfront"))) {
            // marca a celula para representar um Erro
            cell.addClass("red darken-1");
        } else if (obj.hasClass("Cup") && !$(upCell.children().get(0)).hasClass("esteiraP") && (!$(upCell.children().get(0)).hasClass("conectorP") || !$(upCell.children().get(0)).hasClass("reciveCup"))) {
            cell.addClass("red darken-1");
        } else if (obj.hasClass("Cdown") && !$(downCell.children().get(0)).hasClass("esteiraP") && (!$(downCell.children().get(0)).hasClass("conectorP") || !$(downCell.children().get(0)).hasClass("reciveCdown"))) {
            cell.addClass("red darken-1");
        }
    }
}


//fun��o para gerar a lista da Planta com os drags
function generateListProducao(data, $e, type, colorClass) {

    //fun��o para gerar a lista interna das esteiras
    function InnerList(obj, $target) {
        $("<tr>").appendTo($target);
        var td = $("<td>").appendTo($target);

        //acrescenta dentro da ul->li as div's contendo as esteiras
        td.append(
            ' <div id="' + obj.Id + '" class="dragBase z-depth-3 esteiraP ' + colorClass + '"><div class="center">' + obj.Name + '</div></div>'
        );

        var jObj = $("#" + obj.Id);

        //verifica o tipo do item, e acrescenta valores espec�ficos para cada tipo
        if (type == 1) {
            jObj.data("Addtional", obj.NameProcessMaster);
            jObj.data("Type", "Esteira Modelo");
        } else if (type == 2) {
            jObj.data("Type", "Esteira de armazenamento");
        } else if (type == 3) {
            jObj.data("Addtional", obj.InitialValue);
            jObj.data("Type", "Esteira etiquetadora");
        } else if (type == 4) {
            jObj.data("Addtional", obj.tipoDesvio);
            jObj.data("Type", "Esteira de desvio");
        }

        //salva informa��es dentro da id do item
        jObj.data("Name", obj.Name);
        jObj.data("Description", obj.Description);
        jObj.data("InLimit", obj.InLimit);
        jObj.data("Ligado", obj.Ligado);
        jObj.data("TypeN", type);
    }

    //for para a cria��o de v�rias esteiras no collapsible
    for (var i = 0; i < data.length; i++) {
        InnerList(data[i], $e);
    }

    $(".collapsible").collapsible();
}









function saveContent() {
    // tabela "master"
    var tbl = $("#tdropesteiras");

    // verifica se encontrou
    if (tbl.length) {

        // inicia e declara as variaveis
        let tBody = tbl.find($("tbody"));
        let tr = tBody.children();
        var sendServ = {};
        var array = new Array(tr.length);
        var json = {};

        for (var i = 0; i < tr.length; i++) {

            let td = $(tr.get(i)).children();

            array[i] = new Array(td.length);

            for (var j = 0; j < td.length; j++) {

                let cell = $(td.get(j));

                let children = cell.children().get(0);

                // inicializa a variavel json com os valores pr�-definidos
                json = {
                    id: null,
                    dataObj: null,
                    classes: [],
                    children: $($(children).children().get(0)).prop('outerHTML')
                };

                // verifica se a div existe
                // se contem algo dentro da celula
                if (children) {
                    json.classes = $(children).attr("class").split(" ");

                    json.id = $(children).attr("id");

                    json.dataObj = $(children).data();

                    delete json.dataObj.uiDraggable;
                    delete json.dataObj.target;
                }

                array[i][j] = json;
            }
        }

        // atribui os valoes a variavel para enviar para o servidor
        sendServ.array = array;

        sendServ.countIdEm = countIdEm;
        sendServ.countIdEa = countIdEa;
        sendServ.countIdEe = countIdEe;
        sendServ.countIdEd = countIdEd;

        sendServ.minX = minX;
        sendServ.minY = minY;

        // metodo do servidor que faz o mapeamento
        console.log(sendServ);
        saveTableProduction(sendServ);
    } else {
        console.log("Object not found");
    }
}



function assignItemsTable(mapCells) {

    if (mapCells != null) {
        var auxX = minX;
        var auxY = minY;

        minX = mapCells[0].length - 1;
        minY = mapCells.length - 1;

        reajustTable();

        minX = auxX;
        minY = auxY;

        // tabela "master"
        var tbl = $("#tdropesteiras");

        // verifica se encontrou
        if (tbl.length) {

            // inicia e declara as variaveis
            let tBody = tbl.find($("tbody"));
            let tr = tBody.children();

            for (var i = 0; i < tr.length; i++) {

                let td = $(tr.get(i)).children();

                for (var j = 0; j < td.length; j++) {

                    let cell = $(td.get(j));
                    let item = mapCells[i][j];

                    if (item) {
                        cell.html("");
                        let div = $("<div>").appendTo(cell);

                        div.html(item.Html_Children);

                        div.attr("id", item.Id);

                        $.each(item.Classes, function (i, item) {
                            div.addClass(item);
                        });

                        $.each(item.DataObj, function (i, item) {
                            div.data(i, item);
                        });

                        if (item.Esteira) {
                            div.attr("data-target", idModal);
                            div.data("Ligado", item.Esteira.Ligado);
                        }
                    }
                }
            }
        } else {
            console.log("Object not found");
        }

        reajustTable();
    }

}

function clearContent() {
    // tabela "master"
    var tbl = $("#tdropesteiras");

    // verifica se encontrou
    if (tbl.length) {

        // inicia e declara as variaveis
        let tBody = tbl.find($("tbody"));
        let tr = tBody.children();

        for (var i = 1; i < tr.length; i++) {

            let td = $(tr.get(i)).children();

            for (var j = 1; j < td.length; j++) {

                let cell = $(td.get(j));

                $.each($(cell.children().get(0)).data(), function (i, item) {
                    delete $(cell.children().get(0)).data(i);
                });

                cell.html("");
            }
        }

        reajustTable();
    } else {
        console.log("Object not found");
    }
}

function closeModal40(){
    $('.modal2').modal('close');
}

function setOnOff(id, state) {
    $("#" + id).data("Ligado", state);
    if ($("#item").data().id == id) {
        $("#modalSwitch").html('\
            <label>\
                Desligado\
                <input id="onOffEsteira" type="checkbox" '+ ($("#" + id).data().Ligado ? "checked='checked'" : "") + ' onclick="changeOnOff(' + id + ')">\
                <span class="lever"></span>\
                Ligado\
            </label>\
            ');
    }
}

function setPiecesModal(json){
    ListPieces = json;
    let html = "";

    html += '\
                <tr>\
                    <th>Peças na fila de entrada</th>\
                    <th></th>\
                </tr>\
            ';

    for (var i = 0; i < json.length; i++) {
        html += '\
                    <tr>\
                        <td>'+ json[i].Tag + '</td>\
                        <td><a class="modal-trigger" href="#modal40" onclick="modal2show('+ i + ')">Detalhes</a></td>\
                    </tr>\
                ';
    }

    $("#tablePieces").html(html);
}