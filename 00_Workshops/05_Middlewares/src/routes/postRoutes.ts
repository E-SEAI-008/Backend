import { Router } from 'express';
import {
  getAllPosts,
  getPostById,
  createPost,
  updatePost,
  deletePost,
} from '#controllers';
import { maintenanceMode, timeLogger } from '#middlewares';

const postRoutes = Router();
// postRoutes.use(timeLogger);
// postRoutes.use(maintenanceMode);

postRoutes.get('/', getAllPosts);
postRoutes.post('/', createPost);
postRoutes.get('/:id', getPostById);
postRoutes.put('/:id', updatePost);
postRoutes.delete('/:id', deletePost);

export default postRoutes;

// 123456789012345678901234
