import { claudeChat } from '#controllers';
import { Router } from 'express';

const claudeRouter = Router();

claudeRouter.post('/chat', claudeChat);

export default claudeRouter;
