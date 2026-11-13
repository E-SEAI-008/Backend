import Anthropic from '@anthropic-ai/sdk';
import type { RequestHandler } from 'express';
import { z } from 'zod';

const client = new Anthropic();

const SummaryBody = z.object({
  text: z.string().min(1).max(50000),
  effort: z.enum(['low', 'high']).default('low'),
});

export const summarize: RequestHandler = async (req, res) => {
  const { text, effort } = SummaryBody.parse(req.body);

  const message = await client.messages.create({
    model: 'claude-sonnet-5-5',
    max_tokens: 10000,
    output_config: { effort },
    system: `Summarize the provided text in three concise bullet points. Use only information found in the text. If something is missing, do not invent it.  
    `,
    messages: [{ role: 'user', content: text }],
  });

  const reply = message.content
    .filter((block) => block.type === 'text')
    .map((block) => block.text)
    .join('');

  res.json({ reply, usage: message.usage });
};
