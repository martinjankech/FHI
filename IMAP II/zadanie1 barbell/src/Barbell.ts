//Martin_Jankech_HI3_2020

export type Plate = {name:string, key:string, weight:number, count:number, selected:boolean };

export let myPlates:  Plate[] = [
   {key:'0.5', name:'0.5 kg',weight:50, count: 2, selected: false},
   {key:'1', name:'1 kg',weight:100, count: 2, selected: false},
   {key:'1.5', name:'1.5 kg',  weight:150, count: 2, selected: false},
   {key:'2', name:'2 kg', weight:200, count: 2, selected: false},
   {key:'2.5', name:'2.5 kg',weight:250, count: 2, selected: false},
   {key:'5', name:'5 kg',weight:500, count: 2, selected: false},
   {key:'10', name:'10 kg',weight:1000, count: 2, selected: false},
   {key:'15', name:'15 kg',weight:1500, count: 2, selected: false},
   {key:'20', name:'20 kg',weight:2000, count: 2, selected: false},
   {key:'25', name:'25 kg',weight:2500, count: 2, selected: false}
  ];
    // implement the right solution
     export class Barbell {
    targetWeight: number = 0;
    barWeight: number = 2000;
    availablePlates = new Map<string,Plate>();
    nalozene_kotuce:number[]
    nalozenekotuce_vpravo:number[]=[]
    nalozenekotuce_vlavo:number[]=[]
    missing:number=0
    
    public setWeight (barweight:number)
    {this.barWeight=barweight}
    
    
    constructor(plates:Plate[]) {
      
      plates.forEach((plate)=> {
         this.availablePlates.set(plate.key,plate);
      this.availablePlates
      })
      this.nalozene_kotuce=[]
    
   
    }
    reset(){this.availablePlates.forEach((value)=>{value.count=2;value.selected=true})}
    calculate(targetWeight: number):number[] {
 this.nalozenekotuce_vlavo.splice(0,this.nalozenekotuce_vlavo.length)
 this.nalozenekotuce_vpravo.splice(0,this.nalozenekotuce_vpravo.length)
       let loadedWeight=this.barWeight
        let nalozeneKotuce:number[]=[]
        targetWeight=targetWeight-this.barWeight
      
  
  this.availablePlates.forEach((value)=>
  
  {
    if((targetWeight)>=(2*value.weight)&&(value.count>0)&&(value.selected===true))
    {
    loadedWeight = loadedWeight + (value.count*value.weight)
    targetWeight=targetWeight-(value.count*value.weight)
  this.nalozenekotuce_vlavo.push(value.weight/100)
  this.nalozenekotuce_vpravo.push(value.weight/100)
value.count=0
}
 })
  nalozeneKotuce=this.nalozenekotuce_vlavo.concat(this.nalozenekotuce_vpravo) 
if(targetWeight>0)
{this.missing=targetWeight/100
}
   return nalozeneKotuce
  
      }
  
  }
  