import { chat, describeImage, summarize, support } from '#controllers';
import { Router } from 'express';
import multer from 'multer';

const claudeRouter = Router();

const upload = multer({
  storage: multer.memoryStorage(),
  limits: { fileSize: 5 * 1024 * 1024 },
});

claudeRouter.post('/chat', chat);
claudeRouter.post('/support', support);
claudeRouter.post('/summarize', summarize);
claudeRouter.post('/image', upload.single('image'), describeImage);

export default claudeRouter;
