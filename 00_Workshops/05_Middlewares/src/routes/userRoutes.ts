import {
  createUser,
  deleteUser,
  getAllUsers,
  getUserById,
  updateUser,
} from '#controllers';
import { maintenanceMode, methodLogger, timeLogger } from '#middlewares';
import { Router } from 'express';

const userRoutes = Router();
// userRoutes.use(maintenanceMode);
// userRoutes.use(methodLogger);

userRoutes.get('/', methodLogger, getAllUsers);
userRoutes.post('/', methodLogger, timeLogger, createUser);
userRoutes.post('/', createUser);

userRoutes.get('/:id', timeLogger, getUserById);
userRoutes.put('/:id', updateUser);
userRoutes.delete('/:id', deleteUser);

export default userRoutes;
