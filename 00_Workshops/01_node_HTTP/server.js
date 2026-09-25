import http from 'node:http';
import chalk from 'chalk';
import { products } from './products.js';

const server = http.createServer((req, res) => {
  if (req.url === '/') {
    res.end('Welcome to our API');
    return;
  }

  if (req.url === '/hello') {
    res.end('Hello there!');
    return;
  }

  if (req.url === '/product') {
    const product = {
      id: 1,
      title: 'Backpack',
      price: 49.99,
      category: 'accessories',
    };

    res.setHeader('Content-Type', 'application/json');
    res.end(JSON.stringify(product));
    return;
  }

  if (req.url === '/products') {
    // send some products!
    // console.log(req.url);

    res.setHeader('Content-Type', 'application/json');
    res.end(JSON.stringify(products));
    return;
  }

  res.statusCode = 404;
  res.end('NOT FOUND');
});

const port = 3000;

server.listen(port, () =>
  console.log(
    chalk.hex('#5f38b9')(`Server is running on http://localhost:3000`),
  ),
);
