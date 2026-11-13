import Anthropic from '@anthropic-ai/sdk';
import type { ErrorRequestHandler } from 'express';
import { ZodError } from 'zod';

export const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  if (err instanceof ZodError) {
    res.status(400).json({ error: err.issues });
    return;
  }

  if (err instanceof Anthropic.AnthropicError) {
    res
      .status(503)
      .json({ error: 'AI service could not complete this request' });
    return;
  }

  console.error(err);
  res.status(500).json({ error: 'Something went wrong' });
};

export default errorHandler;
