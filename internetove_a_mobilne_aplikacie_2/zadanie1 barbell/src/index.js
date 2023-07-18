"use strict";
exports.__esModule = true;
//Martin_Jankech HI3_2020
var Barbell_1 = require("./Barbell");
var inquirer = require("inquirer");
var chalk_1 = require("chalk");
// feel free and modify inquirer screens to better support your solution
function displayPlateList() {
    console.log("My plates: ");
    myBarbell.availablePlates.forEach(function (plate) {
        if (plate.selected == true)
            console.log(chalk_1["default"].greenBright(plate.name + "  " + (plate.name.length <= 5 ? '\t\t' : '\t') + "[" + plate.count + "] " + (plate.selected ? plate.selected : '')));
    });
}
function displayBarWeight() {
    console.log(chalk_1["default"].greenBright("-----||=========*** Bar = " + myBarbell.barWeight / 100 + " kg ***==========||-----"));
}
function displayLoadedBarbell() {
    // display loaded barbell  e.g. [1,2,5,25]||=========*** Bar = 20 kg ***==========||[25,5,2,1]
    console.log(chalk_1["default"].yellowBright("Loaded Barbell"));
    console.log(chalk_1["default"].blueBright("[" + myBarbell.nalozenekotuce_vlavo.reverse() + "]||========***  Bar=" + myBarbell.barWeight / 100 + "kg  ***========||[" + myBarbell.nalozenekotuce_vpravo + "]"));
    if (myBarbell.missing !== 0)
        console.log(chalk_1["default"].redBright(myBarbell.missing + "kg cannot be loaded because of missing plates"));
}
var Commands;
(function (Commands) {
    Commands["Bar"] = "Bar";
    Commands["Plates"] = "Plates";
    Commands["Calculate"] = "Calculate";
    Commands["Quit"] = "Quit";
    Commands["Reset"] = "Reset plates and barbell";
})(Commands || (Commands = {}));
;
function promptUser() {
    console.clear();
    displayPlateList();
    displayBarWeight();
    displayLoadedBarbell();
    inquirer.prompt({
        type: "list",
        name: "command",
        message: "Choose option",
        choices: Object.values(Commands)
    })
        .then(function (answers) {
        switch (answers["command"]) {
            case Commands.Bar:
                promptBar();
                break;
            case Commands.Plates:
                promptPlates();
                break;
            case Commands.Calculate:
                promptTargetWeight();
                break;
            case Commands.Reset:
                reset();
                break;
        }
    });
}
function reset() {
    myBarbell.availablePlates.forEach(function (value) {
        console.clear;
        value.count = 2;
        value.selected = false;
    });
    myBarbell.nalozenekotuce_vlavo.splice(0, myBarbell.nalozenekotuce_vlavo.length);
    myBarbell.nalozenekotuce_vpravo.splice(0, myBarbell.nalozenekotuce_vpravo.length);
    myBarbell.missing = 0;
    promptUser();
}
function promptBar() {
    console.clear();
    inquirer.prompt({ type: "input", name: "bar", message: "Enter bar weight [kg]:" })
        .then(function (answers) {
        if (answers["bar"] !== "") {
            myBarbell.barWeight = parseInt(answers["bar"]) * 100;
        }
        promptUser();
    });
}
function promptTargetWeight() {
    console.clear();
    inquirer.prompt({ type: "input", name: "barbell", message: "Enter target barbell weight [kg]:" })
        .then(function (answers) {
        if (answers["barbell"] !== "") {
            //calculate  loaded plates so that it creates the closest weight to targetWeight
            myBarbell.nalozene_kotuce = myBarbell.calculate(parseInt(answers["barbell"]) * 100);
            myBarbell.targetWeight = (answers["barbell"]) * 100;
        }
        promptUser();
    });
}
function promptPlates() {
    console.clear();
    var nrPartofString;
    inquirer.prompt({ type: "checkbox", name: "plates", message: "Select plates:", choices: Barbell_1.myPlates })
        .then(function (answers) {
        myBarbell.availablePlates.forEach(function (plate) { return plate.selected = false; });
        if (answers["plates"] !== "") {
            answers.plates.forEach(function (value) {
                nrPartofString = (value.split(' ', 1)[0]);
                var selectedPlate = myBarbell.availablePlates.get(nrPartofString);
                if (selectedPlate)
                    selectedPlate.selected = true;
            });
        }
        promptUser();
    });
}
var myBarbell = new Barbell_1.Barbell(Barbell_1.myPlates.reverse());
promptUser();
