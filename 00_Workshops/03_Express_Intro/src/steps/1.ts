import express from 'express';

// initilaize the express app
const app = express();
const port = 3000;

// ROUTES
app.get('/', (req, res) => res.send('Hello'));
app.get('/something', (req, res) => res.send('Something'));
app.post('/idontknow', (req, res) => res.send('congrats!'));
app.put('/update', (req, res) => res.send('put request'));
app.delete('/delete', (req, res) => res.send('delete req'));

app.use((req, res) => res.status(404).json({ message: 'Not Found' }));

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
