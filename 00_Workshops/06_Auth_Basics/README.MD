# Express Blog API — Relationships, Authentication & Authorization

> !!!!!EKSIK VAR POST CONTROLLER'DA!!!

We continue working with our existing **Express + TypeScript + Mongoose** project. No new project setup is needed.

**Goal:** Everyone can read posts. Logged-in users can create posts. Only the post's author or an admin can edit and delete them.

## 1. Relationships — User & Post

<details>
<summary>1️⃣ Update the User Schema</summary>

Before creating our posts, we add a role to users. Our authentication code uses **`roles`** (an array), so the schema uses the same name. Every new user starts with the `user` role.

**`src/models/User.ts`** — keep the existing fields and add/change these fields:

```ts
password: {
  type: String,
  required: true,
  select: false,
},
roles: {
  type: [String],
  default: ['user'],
},
```

We will need `password` when we implement registration and login. `select: false` prevents the password hash from being included in normal user queries. Remove the old singular `role` field if it is still there.

**Note:** Existing users from earlier workshops do not have passwords. Create new users through `/auth/register` when we get to authentication.

</details>

<details>
<summary>2️⃣ Create the Post Schema & Relationship</summary>

One user can write many posts. Each post belongs to one user.

**`src/models/Post.ts`**

```ts
import { Schema, model } from 'mongoose';

const postSchema = new Schema({
  title: { type: String, required: true },
  body: { type: String, required: true },
  author: {
    type: Schema.Types.ObjectId,
    ref: 'User',
    required: true,
  },
});

export default model('Post', postSchema);
```

**`src/models/index.ts`**

```ts
export { default as User } from './User.ts';
export { default as Post } from './Post.ts';
```

- `Schema.Types.ObjectId` stores the referenced user's MongoDB ID.
- `ref: 'User'` tells Mongoose which model that ID belongs to.
- The relationship is stored in the **Post**, not in both models.

</details>

<details>
<summary>3️⃣ Populate Authors in the Existing Post Controller</summary>

Our current `postController.ts` returns placeholder messages. For now, **replace only the two GET functions** and keep `createPost`, `updatePost`, and `deletePost` as they were. We will implement those later, after authentication.

**`src/controllers/postController.ts`**

```ts
import type { RequestHandler } from 'express';
import { Post } from '#models';

export const getAllPosts: RequestHandler = async (req, res) => {
  const posts = await Post.find().populate('author', 'firstName lastName');
  res.json(posts);
};

export const getPostById: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const post = await Post.findById(id).populate('author', 'firstName lastName');

  if (!post) {
    res.status(404).json({ message: 'Post not found' });
    return;
  }

  res.json(post);
};

export const createPost: RequestHandler = (req, res) => {
  res.json({ message: 'Post created successfully', post: req.body });
};

export const updatePost: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Update post with id ${id}`, updates: req.body });
};

export const deletePost: RequestHandler = (req, res) => {
  const { id } = req.params;
  res.json({ message: `Delete post with id ${id}` });
};
```

**What does `populate()` do?** The post stores only the author's ObjectId. When we query posts, `populate('author', 'firstName lastName')` replaces the ID in the **response** with selected fields from the referenced User document. It does not change the stored post.

**Test:** In MongoDB Atlas, manually insert one document into the `posts` collection. Set `title` and `body` to strings and `author` to an existing user's ID using the **ObjectId** type (not a plain string). Then try:

```http
GET http://localhost:3000/posts
GET http://localhost:3000/posts/<post-id>
```

Compare the stored `author` ID with the populated result.

</details>

## 2. Authentication — Concepts

<details>
<summary>1️⃣ Authentication & Authorization</summary>

## 🔐 Authentication vs. Authorization

These two terms are often confused – but they belong together:

### 🪪 Authentication

**Who are you?**

Here, the system checks

- **if the user really is who they claim to be.**
- This happens, for example, through:
  - username + password
  - fingerprint or other login methods
  - token (e.g., JWT)

**Example:**

- When we log in with email and password
- that's _Authentication_.

---

### 🔓 Authorization

= **What are you allowed to do?**

After the user is authenticated,

- the system checks **which rights or roles** they have.
- So: _“Is this person allowed to access /admin?”_

**Example:**

- A normal user may only see their profile
- An admin may also delete or create new users

---

### 🧠 In short:

> **Authentication** = Check identity  
> **Authorization** = Check permissions

Or simply:

> First we ask: _Who are you?_ → then: _What are you allowed to do?_

</details>

<details>
<summary>2️⃣ Session vs. Token Based Auth</summary>

## 🔑 Token-based vs. Session-based Authentication

### 🧭 Session-based Authentication

With classic **session-based authentication**

- a **session is created on the server** after login
- The server stores information about the logged-in user there
- (e.g., ID, roles, status).

The browser receives a **session cookie**,

- which is automatically sent with every request.
- The server then checks, using the session ID, whether the user is valid.

**Advantages:**

- Easy to implement
- Works well with classic server-rendered apps

**Disadvantages:**

- Server must store every session → more memory usage
- CSRF risk, since cookies are sent automatically
- A session is only valid as long as the browser or tab is open
- (e.g., on many banking websites
- after closing the tab you are automatically logged out)

---

### 🪪 Token-based Authentication (e.g., JWT)

Here, after login, the server creates a **token**

- (usually a JWT = JSON Web Token)
- This token contains signed info about the user
- (e.g., `userId`, `roles`, etc.)

The client stores the token

- (e.g., in a cookie or local storage)
- and sends it with every request in the **header**:

```http
Authorization: Bearer <access-token>
```

The server only checks the signature – it does not need to store a session.

**Advantages:**

- No server-side session needed
- Ideal for APIs and microservices

**Disadvantages:**

- If the token is leaked, it remains valid until it expires

</details>

## 3. Authentication — Implementation

<details>
<summary>3️⃣ Install Packages & Set Up .env</summary>

Install bcrypt for password hashing and jsonwebtoken for generating and verifying JWTs.

```bash
npm install bcrypt jsonwebtoken
npm install -D @types/bcrypt @types/jsonwebtoken
```

Keep the existing `MONGO_DB` setting in **`.env`** and add:

```dotenv
ACCESS_JWT_SECRET=<your-long-random-secret>
JWT_ISSUER=express-blog-api
```

You can generate a secret with:

```bash
node -e "console.log(require('node:crypto').randomBytes(32).toString('hex'))"
```

- `ACCESS_JWT_SECRET` signs and verifies access tokens.
- `JWT_ISSUER` identifies the token issuer.
- `expiresIn: '1h'` in our controller makes tokens valid for one hour.
- Do not commit `.env` to Git.

Also ensure **`app.ts`** has `app.use(express.json());` before the routes.

</details>

<details>
<summary>4️⃣ Auth Controller — Register, Login & Me</summary>

**`src/controllers/authController.ts`** — the controller we will build in class:

```ts
// src/controllers/authController.ts
import type { RequestHandler } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import { User } from '#models';

// POST /auth/register
export const register: RequestHandler = async (req, res) => {
  /*
    Check if user exists (email) [X]
        - If user exists, return an Error [X]
        - If user does not exists:
            - Secure the users password using bcrypt [X]
            - Store the user in DB [X]
            - Sign a token [X]
            - Return the token [X]
  */

  const { firstName, lastName, email, password } = req.body;

  // 1) is the email already taken?
  const userExists = await User.exists({ email });
  if (userExists) {
    throw new Error('Registration failed', { cause: { status: 400 } });
  }

  // 2) hash the password, then create the user
  const hash = await bcrypt.hash(password, 10);
  const user = await User.create({
    firstName,
    lastName,
    email,
    password: hash,
  });

  // 3) sign a token — the payload holds the user id (jti) + roles
  const token = jwt.sign(
    { jti: user._id, roles: user.roles },
    process.env.ACCESS_JWT_SECRET!,
    { expiresIn: '1h', issuer: process.env.JWT_ISSUER },
  );

  // 4) respond WITHOUT the password hash
  const { password: _pw, ...safeUser } = user.toObject();
  res
    .status(201)
    .json({ message: 'Registered successfully', user: safeUser, token });
};

// POST /auth/login
export const login: RequestHandler = async (req, res) => {
  const { email, password } = req.body;

  // we need the password here → ask for it explicitly (it's select:false in the schema)
  const user = await User.findOne({ email }).select('+password');
  if (!user) throw new Error('Invalid credentials', { cause: { status: 401 } });

  const isMatch = await bcrypt.compare(password, user.password);
  if (!isMatch)
    throw new Error('Invalid credentials', { cause: { status: 401 } });

  // same as register: sign a token and return it
  const token = jwt.sign(
    { jti: user._id, roles: user.roles },
    process.env.ACCESS_JWT_SECRET!,
    { expiresIn: '1h', issuer: process.env.JWT_ISSUER },
  );

  const { password: _pw, ...safeUser } = user.toObject();
  res
    .status(200)
    .json({ message: 'Logged in successfully', user: safeUser, token });
};

// GET /auth/me  (protected)
export const me: RequestHandler = async (req, res) => {
  const user = await User.findById(req.user?.id); // password excluded by schema
  if (!user) throw new Error('User not found', { cause: { status: 404 } });
  res.json({ user });
};
```

**`src/controllers/index.ts`** — add:

```ts
export * from './authController.ts';
```

**Note:** Because your controller uses `{ cause: { status: ... } }`, the existing error handler needs to read `err.cause.status` (see next section).

</details>

<details>
<summary>5️⃣ Authenticate Middleware</summary>

The middleware reads `Authorization: Bearer <token>`, verifies the JWT, and attaches the user ID and roles to `req.user` for later handlers.

**`src/middlewares/authenticate.ts`**

```ts
// src/middlewares/authenticate.ts
import type { RequestHandler } from 'express';
import jwt from 'jsonwebtoken';

const secret = process.env.ACCESS_JWT_SECRET;
if (!secret) {
  console.log('missing access token secret');
  process.exit(1);
}

const authenticate: RequestHandler = (req, res, next) => {
  // the header looks like:  "Authorization: Bearer eyJhbGciOi..."
  const authHeader = req.headers.authorization;
  if (!authHeader?.startsWith('Bearer ')) {
    return next(new Error('Not authenticated', { cause: { status: 401 } }));
  }

  const token = authHeader.split(' ')[1]; // the part after "Bearer "

  try {
    const decoded = jwt.verify(token, secret) as jwt.JwtPayload;
    if (!decoded.jti) {
      return next(new Error('Invalid token', { cause: { status: 401 } }));
    }

    // attach the user for the next middlewares / controllers
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
```

**TypeScript:** Add the user property that the middleware attaches to the request. This is needed for `req.user` in our existing controller/middleware code.

**`src/types/express.d.ts`**

```ts
import 'express-serve-static-core';

declare module 'express-serve-static-core' {
  interface Request {
    user?: { id: string; roles: string[] };
  }
}
```

**`src/middlewares/index.ts`** — export the middleware:

```ts
export { default as authenticate } from './authenticate.ts';
```

</details>

<details>
<summary>6️⃣ Connect Auth Routes & Test</summary>

**`src/routes/authRoutes.ts`**

```ts
import { Router } from 'express';
import { register, login, me } from '#controllers';
import { authenticate } from '#middlewares';

const authRoutes = Router();

authRoutes.post('/register', register);
authRoutes.post('/login', login);
authRoutes.get('/me', authenticate, me);

export default authRoutes;
```

**`src/routes/index.ts`** — add:

```ts
export { default as authRoutes } from './authRoutes.ts';
```

**`src/app.ts`** — import and mount `authRoutes`:

```ts
import { authRoutes, postRoutes } from '#routes';

// ... after express.json()
app.use('/auth', authRoutes);
app.use('/posts', postRoutes);
```

The previous `/users` CRUD routes should **not** remain publicly writable: they bypass registration and can directly update account fields (including roles). For this workshop, remove `app.use('/users', userRoutes);`.

**`src/middlewares/errorHandler.ts`** — adapt the existing handler for our controller's error shape:

```ts
import type { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  console.error('❌ Error:', err.stack);
  res.status(err.cause?.status || 500).json({ message: err.message });
};

export default errorHandler;
```

**Test 1 — Register**

```http
POST http://localhost:3000/auth/register
Content-Type: application/json
```

```json
{
  "firstName": "Alice",
  "lastName": "Writer",
  "email": "alice@example.com",
  "password": "alice12345"
}
```

Expected: **201**, user without password hash, plus a JWT.

**Test 2 — Login**

```http
POST http://localhost:3000/auth/login
Content-Type: application/json
```

```json
{
  "email": "alice@example.com",
  "password": "alice12345"
}
```

Expected: **200** with a token. Try a wrong password → **401**.

**Test 3 — Me**

```http
GET http://localhost:3000/auth/me
Authorization: Bearer <your-token>
```

No token → **401**; valid token → **200**, current user's data.

</details>

## 4. Authorization — Who Can Modify a Post?

<details>
<summary>7️⃣ Authorize Middleware</summary>

Rules:

- Anyone can read posts.
- An authenticated user can create posts.
- Only the author or a user with the `admin` role can update/delete a post.

**`src/middlewares/authorize.ts`**

```ts
// src/middlewares/authorize.ts
import type { RequestHandler } from 'express';
import { Post } from '#models';

const authorize: RequestHandler = async (req, res, next) => {
  const { id } = req.params;
  const post = await Post.findById(id);

  if (!post) {
    return next(new Error('Post not found', { cause: { status: 404 } }));
  }

  // admins may edit/delete anything  ← roles come from req.user (the token)
  if (req.user?.roles?.includes('admin')) return next();

  // otherwise: only the owner may continue
  if (post.author.toString() !== req.user?.id) {
    return next(
      new Error('Forbidden: You cannot modify this post', {
        cause: { status: 403 },
      }),
    );
  }

  next();
};

export default authorize;
```

**`src/middlewares/index.ts`** — add:

```ts
export { default as authorize } from './authorize.ts';
```

</details>

<details>
<summary>8️⃣ Finish Post CRUD & Protect Routes</summary>

Now replace only the placeholder **write** handlers in `src/controllers/postController.ts`. Keep the two GET handlers with `populate()` from the beginning of the workshop.

```ts
export const createPost: RequestHandler = async (req, res) => {
  const { title, body } = req.body;
  const post = await Post.create({ title, body, author: req.user?.id });
  res.status(201).json(post);
};

export const updatePost: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const { title, body } = req.body;

  const post = await Post.findByIdAndUpdate(
    id,
    { title, body },
    { new: true, runValidators: true },
  );

  if (!post) {
    res.status(404).json({ message: 'Post not found' });
    return;
  }

  res.json(post);
};

export const deletePost: RequestHandler = async (req, res) => {
  const { id } = req.params;
  const post = await Post.findByIdAndDelete(id);

  if (!post) {
    res.status(404).json({ message: 'Post not found' });
    return;
  }

  res.json({ message: 'Post deleted successfully' });
};
```

**Why don't we take `author` from `req.body`?** The authenticated user's ID comes from the verified token (`req.user.id`). Otherwise, a client could claim another user authored the post.

**`src/routes/postRoutes.ts`** — use the same controller imports you already have; replace the route definitions with:

```ts
import { authenticate, authorize } from '#middlewares';

postRoutes.get('/', getAllPosts);
postRoutes.get('/:id', getPostById);
postRoutes.post('/', authenticate, createPost);
postRoutes.put('/:id', authenticate, authorize, updatePost);
postRoutes.delete('/:id', authenticate, authorize, deletePost);
```

The order matters: `authenticate` checks identity → `authorize` checks whether the user owns the post or is an admin → controller performs the change.

</details>

<details>
<summary>9️⃣ Test Authorization (Alice, Bob & Admin)</summary>

1. Register **Alice** and **Bob**, and save each token.
2. Use Alice's token to create a post:

```http
POST http://localhost:3000/posts
Authorization: Bearer <alice-token>
Content-Type: application/json
```

```json
{
  "title": "Hello Express",
  "body": "My first post"
}
```

3. Copy the new post's `_id` and run these tests:

| Request                              | Expected                                |
| ------------------------------------ | --------------------------------------- |
| `GET /posts` without token           | **200** — public, with populated author |
| `POST /posts` without token          | **401** — authentication required       |
| `PUT /posts/:id` with Bob's token    | **403** — Bob is not the author         |
| `PUT /posts/:id` with Alice's token  | **200** — Alice owns the post           |
| `DELETE /posts/:id` with Bob's token | **403** — Bob is not the author         |

For the admin test, change **Bob's `roles` in MongoDB Atlas** to `['user', 'admin']`, log in again, and use his **new token** to delete Alice's post. Expected: **200**.

**Why log in again?** The roles are copied into the JWT when it is signed. An old token does not automatically receive role changes made in the database.

</details>

## 5. Additional Information (No Coding Required)

<details>
<summary>🔟 Cookies — Why Use Them?</summary>

In our lesson, the API client sends the JWT manually using:

```http
Authorization: Bearer <access-token>
```

A browser can instead send a cookie automatically. An **HttpOnly** cookie cannot be read by JavaScript, which can reduce token theft through some XSS attacks. Cookies also require careful configuration (`Secure`, `SameSite`) and protection against CSRF.

**Important:** Cookies are a transport/storage mechanism, not an alternative authentication algorithm. Session-based and token-based systems can both use cookies.

</details>

<details>
<summary>1️⃣1️⃣ Refresh Tokens — Why Do We Need Them?</summary>

Our access token expires after **1 hour**. After that, the user must log in again in this workshop.

In a larger application, a **refresh token** can request a new short-lived access token without asking for the password every time. Refresh tokens need secure storage, expiration, and a way to revoke them.

**Today:** Only access tokens; no refresh token implementation.

</details>

<details>
<summary>1️⃣2️⃣ Zod, Schemas & DTOs — Later Improvements</summary>

Today we focus on the authentication flow. In a larger project we could:

- Validate incoming register, login, and post request bodies with **Zod**.
- Define DTOs based on those schemas.
- Control exactly which data is accepted and returned by our API.

**Today:** No Zod and no separate DTO or helper setup.

</details>
