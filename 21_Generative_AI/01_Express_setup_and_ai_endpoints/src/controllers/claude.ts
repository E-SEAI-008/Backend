import type { RequestHandler } from 'express';
import Anthropic from '@anthropic-ai/sdk';

const client = new Anthropic({ apiKey: process.env.ANTHROPIC_API_KEY });

export const claudeChat: RequestHandler = async (req, res) => {
  const { prompt } = req.body as { prompt: string };

  const message = await client.messages.create({
    model: 'claude-sonnet-5',
    max_tokens: 1024,
    system: 'You are a helpful assistant',
    messages: [{ role: 'user', content: prompt }],
  });

  console.log(message);

  const text = message.content
    .filter((block) => block.type === 'text')
    .map((block) => block.text)
    .join('');

  res.json({ result: text });
};
