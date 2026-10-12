import mongoose from 'mongoose';

const MONGO_DB = process.env.MONGO_DB || '';

try {
  const con = await mongoose.connect(MONGO_DB);
  console.log('✅ Connected to MongoDB');
  console.log(`📂 Using database: ${con.connection.name}`);
} catch (err) {
  console.error('❌ MongoDB connection error:', err);
  process.exit(1);
}
