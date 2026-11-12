import type { RequestHandler } from 'express';
import Anthropic from '@anthropic-ai/sdk';
import { z } from 'zod';

const client = new Anthropic();

const PromptBody = z.object({
  prompt: z.string().min(1),
});

export const streamCompletion: RequestHandler = async (req, res) => {
  const { prompt } = PromptBody.parse(req.body);

  res.setHeader('Content-Type', 'text/event-stream');
  res.setHeader('Cache-Control', 'no-cache');

  const stream = client.messages.stream({
    model: 'claude-haiku-5-5',
    max_tokens: 1024,
    messages: [{ role: 'user', content: prompt }],
  });

  // send every text chunk to the client
  stream.on('text', (text) => {
    res.write(`data: ${JSON.stringify({ text })}\n\n`);
  });

  // wait until Claude finished
  await stream.finalMessage();

  res.end();
};
