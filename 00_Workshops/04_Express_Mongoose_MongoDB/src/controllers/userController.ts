import { User } from '#models';
import type { RequestHandler } from 'express';

export const getAllUsers: RequestHandler = async (req, res) => {
  const users = await User.find();
  res.json(users);
};

export const getUserById: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const user = await User.findById(id);

  if (!user) {
    res.status(404).json({ message: 'User not found' });
    return;
  }

  res.json(user);
};

export const createUser: RequestHandler = async (req, res) => {
  const { firstName, lastName, email } = req.body;

  if (!firstName || !lastName || !email) {
    res.status(400).json({ message: 'Provide the fiels' });
    return;
  }

  const user = await User.create({ firstName, lastName, email });
  res.status(201).json(user);
};

export const updateUser: RequestHandler = async (req, res) => {
  const { id } = req.params;

  const user = await User.findByIdAndUpdate(id, req.body, {
    new: true,
    runValidators: true,
  });

  if (!user) {
    res.status(404).json({ message: 'User not found' });
  }

  res.json(user);
};

export const deleteUser: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const user = await User.findByIdAndDelete(id);

  if (!user) {
    res.status(404).json({ message: 'User not found' });
  }

  res.json({ message: 'User deleted successfully' });
};
