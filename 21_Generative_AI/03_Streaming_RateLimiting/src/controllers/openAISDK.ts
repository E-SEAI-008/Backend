import type { RequestHandler } from 'express';
import OpenAI from 'openai';

const client = new OpenAI({ apiKey: process.env.OPENAI_API_KEY });

export const chatCompletion: RequestHandler = async (req, res) => {
  const { prompt } = req.body as { prompt: string };

  const responses = await client.responses.create({
    model: 'gpt-4o',
    instructions: 'You are a helpful assistant',
    input: prompt,
  });

  res.json({ result: responses.output_text });
};

export const generateImage: RequestHandler = async (req, res) => {
  const { prompt } = req.body as { prompt: string };

  if (!prompt) {
    res.status(400).json({ message: 'Prompt is required' });
    return;
  }

  const response = await client.images.generate({
    model: 'chatgpt-image-latest',
    prompt,
    n: 1,
    size: '1024x1024',
  });

  console.log(response);
  const imageUrl = response.data?.[0]?.b64_json;
  // convert b64_json to img

  res.json({ url: imageUrl });
};
