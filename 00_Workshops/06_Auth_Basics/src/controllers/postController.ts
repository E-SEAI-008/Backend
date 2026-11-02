import { Post } from '#models';
import type { RequestHandler } from 'express';

export const getAllPosts: RequestHandler = async (req, res) => {
  const posts = await Post.find().populate('author', 'firstName lastName');
  res.json(posts);
};

export const getPostById: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const post = await Post.findById(id).populate('author', 'firstName lastName');

  if (!post) {
    throw new Error('Post not found', { cause: { status: 404 } });
  }

  res.json(post);
};

// !!!
export const createPost: RequestHandler = async (req, res) => {
  const { title, body } = req.body;

  const post = await Post.create({
    title,
    body,
    author: req.user?.id,
  });

  res.json({ message: 'Post created successfully', post: post });
};

export const updatePost: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const { title, body } = req.body;

  const post = await Post.findByIdAndUpdate(
    id,
    { title, body },
    { returnDocument: 'after' },
  );

  if (!post) throw new Error('post not found', { cause: { status: 404 } });

  res.json({
    message: `Updated post successfully with id: ${id}`,
    updates: post,
  });
};

export const deletePost: RequestHandler = async (req, res) => {
  const { id } = req.params;

  await Post.findByIdAndDelete(id);
  res.json({ message: `Post with ID: ${id} was deleted` });
};
