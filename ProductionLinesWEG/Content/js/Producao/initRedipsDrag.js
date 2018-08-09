/*jslint white: true, browser: true, undef: true, nomen: true, eqeqeq: true, plusplus: false, bitwise: true, regexp: true, strict: true, newcap: true, immed: true, maxerr: 14 */
/*global window: false, REDIPS: true */

/* enable strict mode */
"use strict";

// define redips_init variable
var redipsInit;


// redips initialization
redipsInit = function () {
    // reference to the REDIPS.drag library and message line
    var rd = REDIPS.drag;
    // how to display disabled elements
    rd.style.borderDisabled = 'solid';	// border style for disabled element will not be changed (default is dotted)
    rd.style.opacityDisabled = 60;		// disabled elements will have opacity effect
    // initialization
    rd.init();

    rd.clone.drop = false;
    rd.hover.colorTd = null;
    REDIPS.drag.dropMode = "single";
};


// add onload event listener
if (window.addEventListener) {
    window.addEventListener('load', redipsInit, false);
}
else if (window.attachEvent) {
    window.attachEvent('onload', redipsInit);
}