import express from 'express';

// initilaize the express app
const app = express();
const port = 3000;

app.get('/statusCode', (req, res) =>
  res.status(401).send('see the status code'),
);

app.all('/logger', (req, res) => {
  const { url, method } = req;

  res.send(`a ${method} was made to ${url}`);
});

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
