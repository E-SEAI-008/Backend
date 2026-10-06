import { Router } from 'express';
import {
  getAllPosts,
  getPostById,
  createPost,
  updatePost,
  deletePost,
} from '#controllers';

const postRoutes = Router();

postRoutes.get('/', getAllPosts);
postRoutes.post('/', createPost);
postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', updatePost);
postRoutes.delete('/:id', deletePost);

export default postRoutes;
