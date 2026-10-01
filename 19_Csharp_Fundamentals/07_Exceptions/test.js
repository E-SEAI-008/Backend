function divide(a, b) {
  if (b === 0) {
    throw new Error('You tried to divide by zero');
  }

  return a / b;
}

try {
  const result = divide(10, 5);
  console.log(`Result: ${result}`);
} catch (error) {
  console.error(`Error: ${error.message}`);
  // show something in UI maybe alert
}

console.log('app continues..');
