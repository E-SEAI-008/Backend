import type { RequestHandler } from 'express';
import Anthropic from '@anthropic-ai/sdk';

const client = new Anthropic();

export const checkLimits: RequestHandler = async (req, res) => {
  const { data: message, response } = await client.messages
    .create({
      model: 'claude-sonnet-5-5',
      max_tokens: 64,
      messages: [{ role: 'user', content: 'Hello' }],
    })
    .withResponse();

  const text = message.content
    .filter((block) => block.type === 'text')
    .map((block) => block.text)
    .join('');

  res.json({
    reply: text,
    limits: {
      requestRemaining: response.headers.get(
        'anthropic-ratelimit-requests-remaining',
      ),
      inputTokensRemaining: response.headers.get(
        'anthropic-ratelimit-input-tokens-remaining',
      ),
      outputTokensRemaining: response.headers.get(
        'anthropic-ratelimit-output-tokens-remaining',
      ),
    },
  });
};
