import express from 'express';
import { errorHandler } from '#middlewares';
import { claudeRouter } from '#routes';

const app = express();
const port = 3000;

app.use(express.json());

// ROUTES
app.use('/claude', claudeRouter);

app.use(errorHandler);

app.listen(port, () => console.log(`running at http://localhost:${port}`));
