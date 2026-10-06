import express from 'express';

// initilaize the express app
const app = express();
const port = 3000;

// USERS
// app.get('/something/:id', (req, res) => {
//   const { id } = req.params;
//   console.log(id);
// });

// USERS
app.get('/users', (req, res) => res.json({ message: 'GET all users' }));
app.post('/users', (req, res) =>
  res.status(201).json({ message: 'CREATE a user' }),
);
app.get('/users/:id', (req, res) =>
  res.json({ message: 'GET a user', id: req.params.id }),
);
app.put('/users/:id', (req, res) =>
  res.json({ message: 'UPDATE a user', id: req.params.id }),
);
app.delete('/users/:id', (req, res) =>
  res.json({ message: 'DELETE a user', id: req.params.id }),
);

// POSTS
app.get('/posts', (req, res) => res.json({ message: 'GET all posts' }));
app.post('/posts', (req, res) =>
  res.status(201).json({ message: 'CREATE a post' }),
);
app.get('/posts/:id', (req, res) =>
  res.json({ message: 'GET a post', id: req.params.id }),
);
app.put('/posts/:id', (req, res) =>
  res.json({ message: 'UPDATE a post', id: req.params.id }),
);
app.delete('/posts/:id', (req, res) =>
  res.json({ message: 'DELETE a post', id: req.params.id }),
);

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
