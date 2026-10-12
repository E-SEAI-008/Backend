const greet = (name) => console.log(`Hello, ${name}`);

function runTwice(fn) {
  fn('Hey');
  fn('Hello again');
}

runTwice(greet);
