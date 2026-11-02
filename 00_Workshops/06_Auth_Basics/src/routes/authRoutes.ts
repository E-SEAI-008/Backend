import { login, me, register } from '#controllers';
import { authenticate } from '#middlewares';
import { Router } from 'express';

const authRoutes = Router();

authRoutes.post('/register', register);
authRoutes.post('/login', login);
authRoutes.get('/me', authenticate, me);

export default authRoutes;
