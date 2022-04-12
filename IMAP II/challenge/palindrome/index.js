"use strict";
// --- Directions
// Given a string, return true if the string is a palindrome
// or false if it is not.  Palindromes are strings that
// form the same word if it is reversed. *Do* include spaces
// and punctuation in determining if the string is a palindrome.
// --- Examples:
//   palindrome("abba") === true
//   palindrome("abcdefg") === false
exports.__esModule = true;
function palindrome(str) {
    return str.split('').reverse().join('') === str; // <-- solution here
}
exports["default"] = palindrome;
/*module.exports = palindrome;*/
