"use strict";
exports.__esModule = true;
var index_1 = require("./index");
test('Capitalize is a function', function () {
    expect(typeof index_1["default"]).toEqual('function');
});
test('capitalizes the first letter of every word in a sentence', function () {
    expect(index_1["default"]('hi there, how is it going?')).toEqual('Hi There, How Is It Going?');
});
test('capitalizes the first letter', function () {
    expect(index_1["default"]('i love breakfast at bill miller bbq')).toEqual('I Love Breakfast At Bill Miller Bbq');
});
