import type { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  console.error('❌ Error:', err.stack);
  res.status(err.cause || 500).json({ message: err.message });
};

export default errorHandler;
