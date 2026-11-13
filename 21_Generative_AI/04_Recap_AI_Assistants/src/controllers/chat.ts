import Anthropic from '@anthropic-ai/sdk';
import type { RequestHandler } from 'express';
import { z } from 'zod';

const client = new Anthropic();

const PromptBody = z.object({
  prompt: z.string().min(1).max(2000),
});

export const chat: RequestHandler = async (req, res) => {
  const { prompt } = PromptBody.parse(req.body);

  const message = await client.messages.create({
    model: 'claude-haiku-5-5',
    max_tokens: 1024,
    messages: [{ role: 'user', content: prompt }],
  });

  const reply = message.content
    .filter((block) => block.type === 'text')
    .map((block) => block.text)
    .join('');

  res.json({ reply });
  // res.json({ reply, usage: message.usage });
};
