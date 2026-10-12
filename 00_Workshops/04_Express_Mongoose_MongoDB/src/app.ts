import express from 'express';
import { postRoutes, userRoutes } from '#routes';
import '#db';
import { User } from '#models';

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

// const user = new User({
//   firstName: 'John',
//   lastName: 'Doe',
//   email: 'something@mail.com',
// });

// const newUser = await user.save();
// console.log('User created:', newUser);
