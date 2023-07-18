import { Barbell, myPlates } from './Barbell';
let myBarbell = new Barbell([{key:'10', name:'10 kg',weight:1000, count: 2, selected: true}]);
let myBarbell1 = new Barbell
([{ key: '25', name: '25 kg', weight: 2500, count: 2, selected: true },
{ key: '20', name: '20 kg', weight: 2000, count: 2, selected: true },
{ key: '15', name: '15 kg', weight: 1500, count: 2, selected: true },
{ key: '10', name: '10 kg', weight: 1000, count: 2, selected: true },
{ key: '5', name: '5 kg', weight: 500, count: 2, selected: true },
{ key: '2.5', name: '2.5 kg', weight: 250, count: 2, selected: true },
 { key: '2', name: '2 kg', weight: 200, count: 2, selected: true },
{ key: '1.5', name: '1.5 kg', weight: 150, count: 2, selected: true },
 { key: '1', name: '1 kg', weight: 100, count: 2, selected: true },
{ key: '0.5', name: '0.5 kg', weight: 50, count: 2, selected: true },
])
let myBarbell2 = new Barbell
([//{ key: '25', name: '25 kg', weight: 2500, count: 2, selected: true },
{ key: '20', name: '20 kg', weight: 2000, count: 2, selected: true },
//{ key: '15', name: '15 kg', weight: 1500, count: 2, selected: true },
{ key: '10', name: '10 kg', weight: 1000, count: 2, selected: true },
{ key: '5', name: '5 kg', weight: 500, count: 2, selected: true },
{ key: '2.5', name: '2.5 kg', weight: 250, count: 2, selected: true },
 { key: '2', name: '2 kg', weight: 200, count: 2, selected: true },
//{ key: '1.5', name: '1.5 kg', weight: 150, count: 2, selected: true },
 //{ key: '1', name: '1 kg', weight: 100, count: 2, selected: true },
{ key: '0.5', name: '0.5 kg', weight: 50, count: 2, selected: true }
])
//demo test check jest matchers documentation
test('Loading 40 kg only 10kg are available', ()=> {
    expect(myBarbell.calculate(4000)).toEqual(expect.arrayContaining([10,10]));
});
 test('Loading 80kg all plates available ', ()=> {
       myBarbell1.reset()
    expect(myBarbell1.calculate(12000)).toEqual([25,20,5,25,20,5])
    });
   test('Loading 180kg all plates available ', ()=> {myBarbell1.reset()
    expect(myBarbell1.calculate(18000)).toEqual([25,20,15,10,5,2.5,2,0.5,25,20,15,10,5,2.5,2,0.5,])
    });
   
    test('Loading 185kg all plates available ', ()=> {
       myBarbell1.reset()
    expect(myBarbell1.calculate(18500)).toEqual([25,20,15,10,5,2.5,2,1.5,1,0.5,25,20,15,10,5,2.5,2,1.5,1,0.5])
    });

    test('Loading 190kg all plates available and 5kg is missing ', ()=> {
        myBarbell1.reset()
    expect(myBarbell1.calculate(19000)).toEqual([25,20,15,10,5,2.5,2,1.5,1,0.5,25,20,15,10,5,2.5,2,1.5,1,0.5])
    expect(myBarbell1.missing).toEqual(5)
    });

    test('Loading 250 all plates available and 65kg is missing ', ()=> {
        myBarbell1.reset()
    expect(myBarbell1.calculate(25000)).toEqual([25,20,15,10,5,2.5,2,1.5,1,0.5,25,20,15,10,5,2.5,2,1.5,1,0.5])
    expect(myBarbell1.missing).toEqual(65)
    });

   
     test('changing bar weight to 50kg and loading 120kg ', ()=> {
        myBarbell1.reset()
        myBarbell1.setWeight(5000)
    expect(myBarbell1.calculate(12000)).toEqual([25,10,25,10])
    });

    test('changing bar weight to 40kg and loading 100kg ', ()=> {
myBarbell1.reset()
        myBarbell1.setWeight(4000)
    expect(myBarbell1.calculate(10000)).toEqual([25,5,25,5])
    });

   
     test('changing bar weight to 60kg and loading 200kg ', ()=> {
myBarbell1.reset()
        myBarbell1.setWeight(6000)
    expect(myBarbell1.calculate(20000)).toEqual([25,20,15,10,25,20,15,10])
    });

     test('changing selected plates a loading 100kg ', ()=> {

        
    expect(myBarbell2.calculate(10000)).toEqual([20,10,5,2.5,2,0.5,20,10,5,2.5,2,0.5])
    });

