"use strict";
//Martin_Jankech_HI3_2020
exports.__esModule = true;
exports.Barbell = exports.myPlates = void 0;
exports.myPlates = [
    { key: '0.5', name: '0.5 kg', weight: 50, count: 2, selected: false },
    { key: '1', name: '1 kg', weight: 100, count: 2, selected: false },
    { key: '1.5', name: '1.5 kg', weight: 150, count: 2, selected: false },
    { key: '2', name: '2 kg', weight: 200, count: 2, selected: false },
    { key: '2.5', name: '2.5 kg', weight: 250, count: 2, selected: false },
    { key: '5', name: '5 kg', weight: 500, count: 2, selected: false },
    { key: '10', name: '10 kg', weight: 1000, count: 2, selected: false },
    { key: '15', name: '15 kg', weight: 1500, count: 2, selected: false },
    { key: '20', name: '20 kg', weight: 2000, count: 2, selected: false },
    { key: '25', name: '25 kg', weight: 2500, count: 2, selected: false }
];
// implement the right solution
var Barbell = /** @class */ (function () {
    function Barbell(plates) {
        var _this = this;
        this.targetWeight = 0;
        this.barWeight = 2000;
        this.availablePlates = new Map();
        this.nalozenekotuce_vpravo = [];
        this.nalozenekotuce_vlavo = [];
        this.missing = 0;
        plates.forEach(function (plate) {
            _this.availablePlates.set(plate.key, plate);
            _this.availablePlates;
        });
        this.nalozene_kotuce = [];
    }
    Barbell.prototype.setWeight = function (barweight) { this.barWeight = barweight; };
    Barbell.prototype.reset = function () { this.availablePlates.forEach(function (value) { value.count = 2; value.selected = true; }); };
    Barbell.prototype.calculate = function (targetWeight) {
        var _this = this;
        this.nalozenekotuce_vlavo.splice(0, this.nalozenekotuce_vlavo.length);
        this.nalozenekotuce_vpravo.splice(0, this.nalozenekotuce_vpravo.length);
        var loadedWeight = this.barWeight;
        var nalozeneKotuce = [];
        targetWeight = targetWeight - this.barWeight;
        this.availablePlates.forEach(function (value) {
            if ((targetWeight) >= (2 * value.weight) && (value.count > 0) && (value.selected === true)) {
                loadedWeight = loadedWeight + (value.count * value.weight);
                targetWeight = targetWeight - (value.count * value.weight);
                _this.nalozenekotuce_vlavo.push(value.weight / 100);
                _this.nalozenekotuce_vpravo.push(value.weight / 100);
                value.count = 0;
            }
        });
        nalozeneKotuce = this.nalozenekotuce_vlavo.concat(this.nalozenekotuce_vpravo);
        if (targetWeight > 0) {
            this.missing = targetWeight / 100;
        }
        return nalozeneKotuce;
    };
    return Barbell;
}());
exports.Barbell = Barbell;
