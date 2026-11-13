import Anthropic from '@anthropic-ai/sdk';
import type { RequestHandler } from 'express';
import { z } from 'zod';

const client = new Anthropic();

const PromptBody = z.object({
  prompt: z.string().min(1).max(2000),
});

export const support: RequestHandler = async (req, res) => {
  const { prompt } = PromptBody.parse(req.body);

  const message = await client.messages.create({
    model: 'claude-haiku-5-5',
    max_tokens: 1024,
    system: `
        You are a friendly customer support assistant for a shop that sells very fluffy pillows.
        Answer in two sentences at most.

        Store return policy:
        - Customers can return unused pillows within 14 days of delivery.
        - Used pillows cannot be returned.
        - Refunds are issued to the original payment method.

        Only answer policy questions using the information provided above.
        If the information is missing, say that you don't know.
`,
    messages: [{ role: 'user', content: prompt }],
  });

  const reply = message.content
    .filter((block) => block.type === 'text')
    .map((block) => block.text)
    .join('');

  res.json({ reply, usage: message.usage });
};
