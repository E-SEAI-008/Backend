import type { RequestHandler } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import { User } from '#models';

// POST /auth/register
export const register: RequestHandler = async (req, res) => {
  const { firstName, lastName, email, password } = req.body;
  /*
    Check if user exists (email) [X]
        - If user exists, return an Error [X]
        - If user does not exists:
            - Secure the  password using bcrypt [X]
            - Store the user in DB [X]
            - Sign a token [X]
            - Return the token [X]
*/

  //    1) Is the email already taken?
  const userExists = await User.exists({ email });
  if (userExists) {
    throw new Error('Registration failed', { cause: { status: 400 } });
  }

  //   2) has the pw, then create the User
  const hash = await bcrypt.hash(password, 10);
  const user = await User.create({
    firstName,
    lastName,
    email,
    password: hash,
  });

  //   3) sign a token, the payload holds the user id + roles
  const token = jwt.sign(
    { jti: user._id, roles: user.roles },
    process.env.ACCESS_JWT_SECRET!,
    {
      expiresIn: '1h',
      issuer: process.env.JWT_ISSUER,
    },
  );

  res.status(201).json({ messsage: 'Registered successfully', token: token });
};

// POST /auth/login
export const login: RequestHandler = async (req, res) => {
  const { email, password } = req.body;

  const user = await User.findOne({ email }).select('+password');
  if (!user) throw new Error('Invalid credentials', { cause: { status: 401 } });

  const isMatch = await bcrypt.compare(password, user.password);
  if (!isMatch)
    throw new Error('Invalid credentials', { cause: { status: 401 } });

  const token = jwt.sign(
    { jti: user._id, roles: user.roles },
    process.env.ACCESS_JWT_SECRET!,
    {
      expiresIn: '1h',
      issuer: process.env.JWT_ISSUER,
    },
  );

  res.json({ message: 'Logged in successfully', token: token });
};

// GET /auth/me (protected)
export const me: RequestHandler = async (req, res) => {
  const user = await User.findById(req.user?.id);
  if (!user) throw new Error('User not found', { cause: { status: 404 } });
  res.json(user);
};
