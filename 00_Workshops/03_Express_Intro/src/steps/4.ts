import { products } from '#data';
import express from 'express';

const app = express();
const port = 3000;

app.get('/products', (req, res) => {
  res.json(products);
});

app.get('/products/:id', (req, res) => {
  const { id } = req.params;

  const product = products.find((p) => p.id === Number(id));

  if (!product) {
    return res.status(404).json({ message: 'product not found' });
  }

  res.json(product);
});

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
