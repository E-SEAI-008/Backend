import { checkLimits, claudeChat, streamCompletion } from '#controllers';
import { Router } from 'express';
import rateLimit from 'express-rate-limit';

const claudeRouter = Router();

claudeRouter.use(
  rateLimit({
    windowMs: 60_000,
    limit: 2,
  }),
);

claudeRouter.post('/chat', claudeChat);
claudeRouter.post('/stream', streamCompletion);
claudeRouter.get('/limits', checkLimits);

export default claudeRouter;
