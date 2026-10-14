<details>
<summary>1️⃣ Intro</summary>

### ⚙️ Introduction to Express Middlewares

In the past few days, we've learned
how to create routes, controllers, and models with Express,
connect our app to MongoDB, and perform CRUD operations.

- Today we're going a bit deeper
- and looking at how Express processes requests internally
- and how we can customize this process ourselves.

### 🧩 What is a Middleware?

A middleware is simply put, a function
that executes between Request and Response.

and can:

- Read requests
- Check conditions (e.g., authentication)
- Handle errors
- or simply call further functions

and every middleware automatically receives three parameters:

**req**, **res**, **next**

- **req**: the client's request
- **res**: the response sent to the client
- **next()**: calls the next middleware or route

🧠 In short

Middlewares are like intermediate stations between client and server:
Every request runs through a chain of middlewares
before it reaches the actual route.

```ts
(req, res, next) => {
  // Middleware logic
  next();
};
```

</details>

<details>
<summary>2️⃣ Types of Middlewares</summary>

---

There are three main types of middlewares

- Built-in Middlewares
- Third-Party Middlewares
- Custom Middlewares

### 1️⃣ Built-in Middlewares

```ts
app.use(express.json());
express.static(); // → serves static files (e.g., images, CSS)
express.urlencoded(); // → processes form data
```

💡 express.json() automatically reads the Request Body and
converts JSON data into a JavaScript object.

### 2️⃣ Third-Party Middlewares

are additional packages that we need to install
to make certain tasks easier

```ts
import morgan from 'morgan';
import cors from 'cors';

app.use(cors()); // allows Cross-Origin requests
app.use(morgan('dev')); // logs every request in the console
```

```ts
// cors enables frontend-backend requests on different domains
app.use(
  cors({
    origin: 'http://localhost:5173', // only allow requests from this domain
    methods: ['GET', 'POST', 'PUT', 'DELETE'],
    credentials: true, // if cookies or auth data are being sent
  }),
);
```

💡 Why useful?

- morgan helps with debugging

### Custom Middlewares

Sometimes we want to check or log something ourselves
before a request continues — then we write our own middleware.

</details>

<details>
<summary>3️⃣ Application-Level Middlewares timelogger & maintenance</summary>

### Application-Level Middlewares

Now we're writing our first custom middlewares directly in the app.
These are called Application-Level Middlewares,
because they execute at the entire application level –
meaning for every request, regardless of which route is called.

#### Example 1️⃣ – ⏱️ timeLogger

```ts
//src/app.ts then move it to =>
//src/middlewares/timeLogger.ts
import type { RequestHandler } from 'express';

// 🧩 1. Define middleware
const timeLogger: RequestHandler = (req, res, next) => {
  console.log(new Date().toLocaleString());
  next(); // very important!
};

// export default timeLogger (when in middleware folder)
app.use(timeLogger);
```

```ts
//src/middlewares/index.ts
export { default as timeLogger } from './timeLogger.ts';
```

This middleware logs the current time with every request.
Afterwards we call next() so Express continues with the next middleware.

```ts
//src/middlewares/methodLogger.ts
import type { RequestHandler } from 'express';

const methodLogger: RequestHandler = (req, res, next) => {
  console.log(`${req.method} ${req.url}`);
  next();
};

export default methodLogger;
```

🧠 In short:

- Express processes middlewares in the order they are registered
- **app.use()** and **app.get()** are part of this chain
- Therefore global middlewares (e.g., express.json(), Logger, CORS)
- should always be at the top

💡 Important

- Application-Level Middlewares are registered with **app.use()**.
- They apply globally to all routes.
- If a middleware doesn't call next(),
- the request gets stuck and Express sends no response.

</details>

<details>
<summary>4️⃣ Router-Level Middleware</summary>

### Router-Level Middlewares

Application-Level Middlewares affect all routes.
Sometimes we want to activate a middleware only for certain routes —
e.g., to temporarily block or protect individual areas.

```ts
//src/middlewares/maintenanceMode.ts
import type { RequestHandler } from 'express';

const maintenanceMode: RequestHandler = (req, res, next) => {
  const isUnderMaintenance = true;

  if (isUnderMaintenance) {
    return res.status(503).json({
      message: 'The site is under maintenance. Please try again later.',
    });
  }

  next();
};

export default maintenanceMode;
```

then in app.ts

```ts
app.use('/posts', maintenanceMode, postRoutes);
```

💡 What happens here?

- 1️⃣ The client calls /posts
- 2️⃣ First the middleware runs (Maintenance-Check)
- 3️⃣ If isUnderMaintenance === true,
- → the request is immediately ended (return res.status(503)...)
- 4️⃣ If not,
- → it continues with next() to the actual route

</details>

<details>
<summary>5️⃣ Combining Middlewares</summary>

In Express we can execute multiple middlewares in sequence.
This creates a middleware chain.

```ts
app.use('/users', timeLogger, methodLogger, userRoutes);
```

🧠 In short

- Multiple middlewares are executed from left to right
- Each middleware must call next() if it doesn't want to end the request
- As soon as a middleware sends a response,
- the chain doesn't continue further

</details>

<details>
<summary>5️⃣ Route-Level Middleware in Router </summary>

🧠 Small Analogy

- Imagine you're on a news website
- on the homepage you see all article headlines,
- but as soon as you click on an article,
- suddenly a window appears:

"🔒 This article is only available to subscribers."

let's look at how to implement this with Express

- How does the server check who can read and who can't?

👉 For this we use Route-Level Middlewares —
and we'll see right away how to implement something like a paywall with them.

```ts
// src/middlewares/paywallMiddleware.ts
import type { RequestHandler } from 'express';

export const paywallMiddleware: RequestHandler = (req, res, next) => {
  const { userRole } = req.body; // role from body

  if (userRole === 'subscriber') {
    return next(); // allowed to continue
  }

  return res.status(402).json({
    message: 'This article is available to subscribers only.',
  });
};

export default paywallMiddleware;
```

This gives us maximum control:
We decide exactly where and when a middleware applies

🧠 In short

- Application-Level: app.use(...) → applies to all routes of the app
- Router-Level (global in router): articleRoutes.use(...) → applies to all routes of this router
- Route-Level (per endpoint): articleRoutes.get('/:id', middleware, handler) → applies only here

</details>

<details>
<summary>6️⃣ Error Handler Middleware</summary>

### ❗ Error-Handling Middleware

- In Express we can write our own error handling middlewares.
- They have four parameters:
- (err, req, res, next) → Error-Middlewares.

```ts
//src/middlewares/errorHandler.ts
import type { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (err, req, res, next) => {
  console.error('❌ Error:', err.stack); // logs the error in the console
  res.status(err.cause || 500).json({ message: err.message });
};

export default errorHandler;
```

💡 What happens here?

- If an error occurs somewhere in the code (e.g., **throw new Error('Error!'))**,
- this middleware catches it.
- Express recognizes it automatically
- because it has four parameters (err, req, res, next).
- We log the error in the console and send a response to the client.

```ts
//src/controllers/userController.ts
export const getAllUsers: RequestHandler = (req, res) => {
  // Simulated error:
  throw new Error('Error getting list of users', { cause: 418 });

  // Won't be executed anymore if an error was thrown above
  res.json({ message: 'List of users' });
};

export const getAllUsers: RequestHandler = async (req, res) => {
  const users = await User.find();
  if (!users.length) throw new Error('No users found', { cause: 404 });
  res.json(users);
};
```

### before Express v.5

```ts
export const getAllUsers: RequestHandler = async (req, res, next) => {
  try {
    const users = await User.find();

    if (!users.length) {
      // here a "custom" error is created
      throw new Error('No users found', { cause: 404 });
    }

    res.json(users);
  } catch (err) {
    // Manually pass error to the global error handler
    next(err);
  }
};
```

In Express 5 we don't need next(err) anymore.
If we use throw new Error(),
the error is automatically passed to the Error-Middleware.

Even in async functions we don't need to write try/catch anymore.
Express 5 catches it automatically.

but it's still allowed

🧠 In short

- Error-Middlewares are always at the end of the middleware chain
- They catch all unhandled errors
- They help handle server errors gracefully,
- instead of the app crashing 💥

```ts
//src/app.ts
app.use(errorHandler);
// before app.listen
```

This is the "last line of defense" –
All errors end up here, no matter where they occur in the code.

```ts
//src/middlewares/maintenanceMode.ts
export const maintenanceMode: RequestHandler = (req, res, next) => {
  const maintenance = true;
  if (maintenance) {
    throw new Error('Server is under maintenance', { cause: 503 });
  }
  next();
};
```

```ts
//src/controllers/articleController.ts
export const getAllArticles: RequestHandler = (req, res) => {
  throw new Error('Error getting list of users', { cause: 418 });
  res.json({
    message: 'List of all articles',
  });
};
```

</details>

<details>
<summary>7️⃣ Outro</summary>

- Middlewares are functions that sit between Request and Response
- They can modify, block, or forward requests
- And: The order in app.ts is crucial 🧩

and Error Handler Middleware
catches all errors – regardless
of whether they come from a route, middleware, or controller.

</details>
