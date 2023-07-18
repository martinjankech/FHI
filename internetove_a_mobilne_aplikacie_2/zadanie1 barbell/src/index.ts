//Martin_Jankech HI3_2020
import { Barbell, myPlates, Plate } from './Barbell';
import * as inquirer from 'inquirer';
import chalk from 'chalk';


// feel free and modify inquirer screens to better support your solution
function displayPlateList(): void {
   console.log(`My plates: `);
   myBarbell.availablePlates.forEach(plate=>{
    if(plate.selected==true)
      console.log(chalk.greenBright(`${plate.name}  ${plate.name.length<=5 ? '\t\t':'\t'}[${plate.count}] ${plate.selected ? plate.selected : ''}`));
   });
}

function displayBarWeight() {
  console.log(chalk.greenBright(`-----||=========*** Bar = ${myBarbell.barWeight/100} kg ***==========||-----`));
}

function displayLoadedBarbell():void {
  // display loaded barbell  e.g. [1,2,5,25]||=========*** Bar = 20 kg ***==========||[25,5,2,1]
console.log(chalk.yellowBright("Loaded Barbell"))


console.log(chalk.blueBright(`[${myBarbell.nalozenekotuce_vlavo.reverse()}]||========***  Bar=${myBarbell.barWeight/100}kg  ***========||[${myBarbell.nalozenekotuce_vpravo}]`))
if(myBarbell.missing!==0)
console.log(chalk.redBright(`${myBarbell.missing}kg cannot be loaded because of missing plates`))



}


enum Commands { Bar = "Bar", Plates = "Plates", Calculate = "Calculate", Quit = "Quit" ,Reset="Reset plates and barbell" };

function promptUser(): void { 
  console.clear();
  displayPlateList();
  displayBarWeight();
  displayLoadedBarbell()
  inquirer.prompt({
     type: "list",
     name: "command",
     message: "Choose option",
     choices: Object.values(Commands) })
      .then(answers => {
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
            reset()
            break;

        } 
      })
}
function reset():void
{ myBarbell.availablePlates.forEach((value)=>
{console.clear
value.count=2
value.selected=false

}

  )
  myBarbell.nalozenekotuce_vlavo.splice(0,myBarbell.nalozenekotuce_vlavo.length)
  myBarbell.nalozenekotuce_vpravo.splice(0,myBarbell.nalozenekotuce_vpravo.length)
  myBarbell.missing=0
  promptUser();
  
  
}

function promptBar(): void {
  console.clear();
  inquirer.prompt({ type: "input", name: "bar", message: "Enter bar weight [kg]:"})
      .then(answers => {if (answers["bar"] !== "") {
          myBarbell.barWeight = parseInt(answers["bar"])*100;
      }
      promptUser();
  })
}

function promptTargetWeight(): void {
  console.clear();
  inquirer.prompt({ type: "input", name: "barbell", message: "Enter target barbell weight [kg]:"})
      .then(answers => {if (answers["barbell"] !== "") {
        //calculate  loaded plates so that it creates the closest weight to targetWeight
        myBarbell.nalozene_kotuce= myBarbell.calculate( parseInt(answers["barbell"])*100);
         myBarbell.targetWeight=(answers["barbell"])*100;
       
       }
      promptUser();
  })
}

function promptPlates(): void {
  console.clear();
  let nrPartofString:string;
  inquirer.prompt({ type: "checkbox", name: "plates", message: "Select plates:", choices: myPlates })
      .then(answers => { 
          myBarbell.availablePlates.forEach(plate=>plate.selected=false);
          if (answers["plates"] !== "") {
            answers.plates.forEach((value:string) => {
              nrPartofString =(value.split(' ',1)[0]);
              const selectedPlate = myBarbell.availablePlates.get(nrPartofString);
              if(selectedPlate)
                selectedPlate.selected = true;
            });
      }
      promptUser();
  })
}
let myBarbell = new Barbell(myPlates.reverse());

promptUser();

