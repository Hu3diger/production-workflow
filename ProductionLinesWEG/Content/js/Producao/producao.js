$(function () {
    setDropDragItens();
});

function setDropDragItens() {

    $(".dragBase").draggable({ helper: 'clone' });
    $(".dragClone").draggable({
        revert: 'invalid',
        stop: function (ev, ui) {
            $(this).attr('style', 'position: relative;');
        }
    });

    $(".tCell").droppable({
        accept: '.dragBase, .dragClone',
        drop: function (ev, ui) {
            var droppedItem = $(ui.draggable);

            if (droppedItem.hasClass("dragBase")) {
                droppedItem = $(ui.draggable).clone();
            }

            droppedItem.removeClass("dragBase").addClass("dragClone");

            droppedItem.draggable({ revert: 'invalid' });

            $(this).html(droppedItem);

            reajustTable($(this));

            setDropDragItens();
        }
    });

    $('.tTrash').droppable({
        drop: function (event, ui) {
            var droppedItem = $(ui.draggable);

            if (!droppedItem.hasClass("dragBase")) {
                ui.draggable.remove();
            }
        }
    });
}

function reajustTable($currentCell) {

    // verifica se a celula esta na ultima linha
    let controlBottom = $currentCell.parent()[0] == $("#tdropesteiras tbody")[0].lastElementChild;

    // verifica se a celula esta na ultima coluna
    let controlRight = $currentCell[0] == $currentCell.parent()[0].lastElementChild;

    // se estiver na ultima coluna, ele gera uma nova coluna para todas as linhas
    if (controlRight) {
        let tBody = $currentCell.parent().parent();

        for (var i = 0; i < tBody.children().length; i++) {
            $('<td class="tCell">').appendTo(tBody.children()[i]);
        }
    }

    // se estiver na ultima linha, ele gera uma nova linha
    if (controlBottom) {

        let tr = $("<tr>").appendTo($currentCell.parent().parent());

        for (var i = 0; i < $currentCell.parent().children().length; i++) {
            $('<td class="tCell">').appendTo(tr);
        }
    }
}

