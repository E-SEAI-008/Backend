import type { RequestHandler } from 'express';

const timeLogger: RequestHandler = (req, res, next) => {
  console.log(new Date().toLocaleString());
  next();
};

export default timeLogger;
