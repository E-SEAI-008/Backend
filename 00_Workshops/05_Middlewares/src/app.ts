import '#db';
import cors from 'cors';
import express from 'express';
import { postRoutes, userRoutes } from '#routes';
import {
  errorHandler,
  maintenanceMode,
  methodLogger,
  timeLogger,
} from '#middlewares';

const app = express();
const port = 3000;

app.use(cors());

// app.use(timeLogger);
// app.use(methodLogger);

// ROUTES
app.use('/users', userRoutes);
app.use('/posts', postRoutes);

app.use(errorHandler);

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
