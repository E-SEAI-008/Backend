// JS primitives => value
let a = 5;
let b = a;
b++;
console.log(a);

// JS Objects are shared => ref
let obj1 = { Count: 5 };
let obj2 = obj1;
obj2.Count++;
console.log(obj1.Count);
