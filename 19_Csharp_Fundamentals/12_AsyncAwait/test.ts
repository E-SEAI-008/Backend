export {};

async function fetchData(url: string): Promise<object> {
  const response = await fetch(url);
  return response.json();
}

const result = await fetchData('https://fakestoreapi.com/products/10');
console.log(result);
