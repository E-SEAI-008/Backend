// let year: number = 2026;
// console.log(`The yaer is ${year}`);

// for (let i = 0; i < 3; i++) {
//   console.log(`Count: ${i}`);
// }

class Car {
  brand: string;
  model: string;
  year: number;

  constructor(brand: string, model: string, year: number) {
    this.brand = brand;
    this.model = model;
    this.year = year;
  }

  drive() {
    console.log('The car is driving');
  }

  stop() {
    console.log('The car has stopped');
  }
}

const myCar = new Car('Somecar', 'Somemodel', 2000);
