"use strict";
// --- Directions
// Given a string, return the character that is most
// commonly used in the string.
// --- Examples
// maxChar("abcccccccd") === "c"
// maxChar("apple 1231111") === "1"
exports.__esModule = true;
function maxChar(str) {
    var charMap = {};
    var max = 0;
    var maxChar = '';
    // create character map
    for (var _i = 0, str_1 = str; _i < str_1.length; _i++) {
        var char = str_1[_i];
        if (charMap[char]) {
            // increment the character's value if the character existed in the map
            charMap[char]++;
        }
        else {
            // Otherwise, the value of the character will be set to 1
            charMap[char] = 1;
        }
    }
    // find the most commonly used character
    for (var char in charMap) {
        if (charMap[char] > max) {
            max = charMap[char];
            maxChar = char;
        }
    }
    return maxChar;
}
exports["default"] = maxChar;
