import '#db';
import cors from 'cors';
import express from 'express';
import { authRoutes, postRoutes } from '#routes';
import { errorHandler } from '#middlewares';

const app = express();
const port = 3000;

app.use(cors());
app.use(express.json());

// ROUTES
app.use('/auth', authRoutes);
app.use('/posts', postRoutes);

app.use(errorHandler);

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
