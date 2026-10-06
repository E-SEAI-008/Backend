import express from 'express';
import { postRoutes, userRoutes } from '#routes';

const app = express();
const port = 3000;

// makes req.body readable => without this, req.body is undefined on POST/PUT
app.use(express.json());

// ROUTES
app.use('/users', userRoutes);
app.use('/posts', postRoutes);

app.listen(port, () =>
  console.log(`Server is running on http://localhost:${port}`),
);
