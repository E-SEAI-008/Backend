import type { RequestHandler } from 'express';

export const getAllPosts: RequestHandler = (req, res) => {
  res.json({ message: 'List of posts' });
};

export const getPostById: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Get post with id ${id}` });
};

export const createPost: RequestHandler = (req, res) => {
  res.json({ message: 'Post created successfully', post: req.body });
};

export const updatePost: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Update post with id ${id}`, updates: req.body });
};

export const deletePost: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Delete post with id ${id}` });
};
