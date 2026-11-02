import type { RequestHandler } from 'express';
import jwt from 'jsonwebtoken';

const secret = process.env.ACCESS_JWT_SECRET;
if (!secret) {
  console.log('missing access token secret');
  process.exit(1);
}

const authenticate: RequestHandler = (req, res, next) => {
  const authHeader = req.headers.authorization;
  if (!authHeader?.startsWith('Bearer ')) {
    return next(new Error('Not authenticated', { cause: { status: 401 } }));
  }

  const token = authHeader.split(' ')[1];
  if (!token)
    return next(new Error('Invalid token', { cause: { status: 401 } }));

  try {
    const decoded = jwt.verify(token, secret) as jwt.JwtPayload;
    if (!decoded.jti) {
      return next(new Error('Invalid token', { cause: { status: 401 } }));
    }

    req.user = { id: decoded.jti, roles: decoded.roles ?? [] };
    next();
  } catch (error) {
    if (error instanceof jwt.TokenExpiredError) {
      return next(
        new Error('Expired access token', { cause: { status: 401 } }),
      );
    }
    return next(new Error('Invalid token', { cause: { status: 401 } }));
  }
};

export default authenticate;
