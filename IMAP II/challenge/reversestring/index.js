"use strict";
// --- Directions
// Given a string, return a new string with the reversed
// order of characters
// --- Examples
//   reverse('apple') === 'elppa'
//   reverse('hello') === 'olleh'
//   reverse('Greetings!') === '!sgniteerG'
exports.__esModule = true;
function reverse(str) {
    return str.split('').reverse().join('');
}
exports["default"] = reverse;
