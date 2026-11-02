import type { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  // 1) in development only, print the full error stack to the console (red text)
  if (process.env.NODE_ENV !== 'production') {
    console.error(`\x1b[31m${err.stack}\x1b[0m`);
  }

  // 2) get the status we attached when throwing (cause.status), or fall back to 500
  const status = (err.cause as { status?: number })?.status ?? 500;

  // 3) send a clean JSON response with the error message
  res.status(status).json({ message: err.message || 'Internal server error' });
};

export default errorHandler;
