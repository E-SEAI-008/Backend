import { chatCompletion, generateImage } from '#controllers';
import { Router } from 'express';

const openAIRouter = Router();

openAIRouter.post('/chat/', chatCompletion);
openAIRouter.post('/images', generateImage);

export default openAIRouter;
