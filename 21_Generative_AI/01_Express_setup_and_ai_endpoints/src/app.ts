import { claudeRouter, openAIRouter } from '#routes';
import express from 'express';

const app = express();
const port = 3000;

app.use(express.json());
app.get('/', (req, res) => res.json({ message: 'Hello!' }));

// ROUTES
app.use('/openai', openAIRouter);
app.use('/claude', claudeRouter);

app.listen(port, () =>
  console.log(`Server is running on: http://localhost:${port}`),
);
