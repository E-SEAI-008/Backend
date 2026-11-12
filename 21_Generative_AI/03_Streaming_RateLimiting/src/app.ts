import { errorHandler } from '#middlewares';
import { claudeRouter, openAIRouter } from '#routes';
import express from 'express';

const app = express();
const port = 3000;

app.use(express.json());

app.use('/claude', claudeRouter);
app.use('/openai', openAIRouter);

app.use(errorHandler);

app.listen(port, () =>
  console.log(`listening on port: http://localhost:${port}`),
);
