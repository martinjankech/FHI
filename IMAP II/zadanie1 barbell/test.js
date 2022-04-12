"use strict";
exports.__esModule = true;
var Barbell_1 = require("./src/Barbell");
var myBarbell = new Barbell_1.Barbell([{ key: '10', name: '10 kg', weight: 1000, count: 2, selected: true }]);
//demo test check jest matchers documentation
test('Loading 40 kg', function () {
    expect(myBarbell.calculate(4000)).toEqual(expect.arrayContaining([10, 10]));
});
