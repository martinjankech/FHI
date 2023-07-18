"use strict";
// --- Directions
// Print out the n-th entry in the fibonacci series.
// The fibonacci series is an ordering of numbers where
// each number is the sum of the preceeding two.
// For example, the sequence
//  [0, 1, 1, 2, 3, 5, 8, 13, 21, 34]
// forms the first ten entries of the fibonacci series.
// Example:
//   fib(4) === 3
exports.__esModule = true;
function fib(n) {
    var num1 = 0;
    var num2 = 1;
    var i = 0;
    var sum;
    console.log(num1);
    for (i = 0; i < n - 1; i++) {
        console.log(num2);
        sum = num1 + num2;
        num1 = num2;
        num2 = sum;
    }
    return num2;
}
exports["default"] = fib;
console.log(fib(39));
