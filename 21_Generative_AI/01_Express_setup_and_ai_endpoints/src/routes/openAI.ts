import { chatCompletion, generateImage, responseApi } from '#controllers';
import { Router } from 'express';

const openAIRouter = Router();

openAIRouter.post('/chat', chatCompletion);
openAIRouter.post('/chat/responses', responseApi);
openAIRouter.post('/images', generateImage);

export default openAIRouter;
