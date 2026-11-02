import { Router } from 'express';
import {
  getAllPosts,
  getPostById,
  createPost,
  updatePost,
  deletePost,
} from '#controllers';
import { authenticate, authorize } from '#middlewares';

const postRoutes = Router();

postRoutes.get('/', getAllPosts);
postRoutes.post('/', authenticate, createPost);
postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', authenticate, authorize, updatePost);
postRoutes.delete('/:id', authenticate, authorize, deletePost);

export default postRoutes;
