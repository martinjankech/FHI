// --- Directions
// Print out the n-th entry in the fibonacci series.
// The fibonacci series is an ordering of numbers where
// each number is the sum of the preceeding two.
// For example, the sequence
//  [0, 1, 1, 2, 3, 5, 8, 13, 21, 34]
// forms the first ten entries of the fibonacci series.
// Example:
//   fib(4) === 3

export default function fib(n) 

{
 let num1=0;
 let num2=1;
 let i=0;
 let sum;
 console.log(num1)
  for(i=0 ; i<n-1;i++)
  {console.log(num2);
   sum=num1+num2
  num1=num2;
  num2=sum;
  }
return num2;
}

console.log(fib(39));


