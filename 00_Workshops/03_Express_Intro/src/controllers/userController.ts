import type { RequestHandler } from 'express';

export const getAllUsers: RequestHandler = (req, res) => {
  res.json({ messsage: 'List of users' });
};

export const getUserById: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `GET user with id: ${id}` });
};

export const createUser: RequestHandler = (req, res) => {
  res.json({ message: 'User created successfully', user: req.body });
};

export const updateUser: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `UPDATE user with id: ${id}` });
};

export const deleteUser: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `DELETE user with id: ${id}` });
};
