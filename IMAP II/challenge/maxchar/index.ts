// --- Directions
// Given a string, return the character that is most
// commonly used in the string.
// --- Examples
// maxChar("abcccccccd") === "c"
// maxChar("apple 1231111") === "1"

export default function maxChar(str:string) :string{
     const charMap = {};
let max = 0;
let maxChar = '';

// create character map
for (let char of str) {
if (charMap[char]) {
 // increment the character's value if the character existed in the map
  charMap[char]++;
} else {
     // Otherwise, the value of the character will be set to 1
 charMap[char] = 1;
 }
 }

// find the most commonly used character
for (let char in charMap) {
    if (charMap[char] > max) {
max = charMap[char];
maxChar = char;
}
}
 return maxChar;
}