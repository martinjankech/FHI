"use strict";
// --- Directions
// Write a function that accepts a string.  The function should
// capitalize the first letter of each word in the string then
// return the capitalized string.
// --- Examples
//   capitalize('a short sentence') --> 'A Short Sentence'
//   capitalize('a lazy fox') --> 'A Lazy Fox'
//   capitalize('look, it is working!') --> 'Look, It Is Working!'
exports.__esModule = true;
function capitalize(str) {
    return str.replace(/\w\S*/g, function skusam(txt) {
        return txt.charAt(0).toLocaleUpperCase() + txt.substr(1).toLocaleLowerCase();
    });
}
exports["default"] = capitalize;
