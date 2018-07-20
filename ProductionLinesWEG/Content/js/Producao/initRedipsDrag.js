/*jslint white: true, browser: true, undef: true, nomen: true, eqeqeq: true, plusplus: false, bitwise: true, regexp: true, strict: true, newcap: true, immed: true, maxerr: 14 */
/*global window: false, REDIPS: true */

/* enable strict mode */
"use strict";

// define redips_init variable
var redipsInit;


// redips initialization
redipsInit = function () {
    // reference to the REDIPS.drag library and message line
    var rd = REDIPS.drag,
        msg = document.getElementById('message');
    // how to display disabled elements
    rd.style.borderDisabled = 'solid';	// border style for disabled element will not be changed (default is dotted)
    rd.style.opacityDisabled = 60;		// disabled elements will have opacity effect
    // initialization
    rd.init();
    rd.event.switched = function (targetCell) {
        //msg.innerHTML = 'Switched';
    };
    rd.event.deleted = function (cloned) {
        // if cloned element is directly moved to the trash
        if (cloned) {
            // set id of original element (read from redips property)
            // var id_original = rd.obj.redips.id_original;

            //msg.innerHTML = 'Deleted (c)';
        }
        else {
            //msg.innerHTML = 'Deleted';
        }
    };
    rd.event.cloned = function (clonedElement) {

        //msg.innerHTML = 'Cloned';

        // append 'd' to the element text (Clone -> Cloned)

        //rd.obj.innerHTML += 'd';
    };
    rd.event.notCloned = function () {
        //msg.innerHTML = 'Not cloned';
    };

    rd.event.clicked = function (currentCell) {
        console.log(currentCell);
    }

    rd.event.moved = function () {
        console.log("Moved");
    }

    rd.event.notMoved = function () {
        console.log("Not Moved");
    };

    rd.event.droppedBefore = function (targetCell) {
        console.log(targetCell);
    }

    rd.event.dropped = function (targetCell) {
        reajustTable($(targetCell));
    }

    rd.clone.drop = false;
    rd.hover.colorTd = null;
    REDIPS.drag.dropMode = "single";
};

// toggles trash_ask parameter defined at the top
function toggleConfirm(chk) {
    if (chk.checked === true) {
        REDIPS.drag.trash.question = 'Are you sure you want to delete DIV element?';
    }
    else {
        REDIPS.drag.trash.question = null;
    }
}


// enables / disables dragging
function toggleDragging(chk) {
    REDIPS.drag.enableDrag(chk.checked);
}


// function sets drop_option parameter defined at the top
function setMode(radioButton) {
    REDIPS.drag.dropMode = radioButton.value;
}


// show prepared content for saving
function save(type) {
    // define table_content variable
    var table_content;
    // prepare table content of first table in JSON format or as plain query string (depends on value of "type" variable)
    table_content = REDIPS.drag.saveContent('table1', type);
    // if content doesn't exist
    if (!table_content) {
        alert('Table is empty!');
    }
    // display query string
    else if (type === 'json') {
        //window.open('/my/multiple-parameters-json.php?p=' + table_content, 'Mypop', 'width=350,height=260,scrollbars=yes');
        window.open('multiple-parameters-json.php?p=' + table_content, 'Mypop', 'width=360,height=260,scrollbars=yes');
    }
    else {
        //window.open('/my/multiple-parameters.php?' + table_content, 'Mypop', 'width=350,height=160,scrollbars=yes');
        window.open('multiple-parameters.php?' + table_content, 'Mypop', 'width=360,height=260,scrollbars=yes');
    }
}


// add onload event listener
if (window.addEventListener) {
    window.addEventListener('load', redipsInit, false);
}
else if (window.attachEvent) {
    window.attachEvent('onload', redipsInit);
}

function testeF() {
    console.log(REDIPS.drag.saveContent(tdropesteiras, "json"));
}