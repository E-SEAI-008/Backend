function firstIdentity(value: string): string {
  return value;
}

// console.log(firstIdentity('hello'));
// console.log(firstIdentity(10));

function secondIdentity<T>(value: T): T {
  return value;
}

// console.log(secondIdentity('hello'));
// console.log(secondIdentity(10));
// console.log(secondIdentity(true));
