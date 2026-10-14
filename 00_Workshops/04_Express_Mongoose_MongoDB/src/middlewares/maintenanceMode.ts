import type { RequestHandler } from 'express';

const maintenanceMode: RequestHandler = (req, res, next) => {
  const isUnderMaintenance = true;

  if (isUnderMaintenance) {
    throw new Error('Server is under maintenance', { cause: 503 });
  }

  next();
};

export default maintenanceMode;
