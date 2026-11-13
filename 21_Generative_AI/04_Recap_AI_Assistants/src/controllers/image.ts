import Anthropic from '@anthropic-ai/sdk';
import type { RequestHandler } from 'express';
import type {} from 'multer';

const client = new Anthropic();

export const describeImage: RequestHandler = async (req, res) => {
  const file = req.file;

  if (!file) {
    res.status(400).json({ error: 'Please upload an image' });
    return;
  }

  const allowedTypes = ['image/png', 'image/jpeg', 'image/webp'];

  if (!allowedTypes.includes(file.mimetype)) {
    res.status(400).json({ error: 'Unsupported image format.' });
    return;
  }

  const message = await client.messages.create({
    model: 'claude-haiku-5-5',
    max_tokens: 1024,
    messages: [
      {
        role: 'user',
        content: [
          {
            type: 'image',
            source: {
              type: 'base64',
              media_type: file.mimetype as
                | 'image/png'
                | 'image/jpeg'
                | 'image/webp',
              data: file.buffer.toString('base64'),
            },
          },
          {
            type: 'text',
            text: `
                Analyze this educational diagram and create a short study note in Markdown.

                Follow this structure:

                # Diagram Title

                ## What Does This Diagram Show?
                Explain the main idea in 2-3 sentences.

                ## Key Takeaways
                List 3-5 important points using bullet points.

                ## Conclusion
                Summarize the main lesson in 1-2 sentences.

                Rules:
                - Use simple, beginner-friendly English.
                - Return valid Markdown only.
                - Keep the explanation short and easy to read aloud.
                - Only use information visible in the image.
                - If something is unclear or unreadable, say so.
                - Do not invent missing information.
                - Return raw Markdown only
                - Do not wrap the response in Markdown code fences
                - Do not escape newline characters   
             `,
          },
        ],
      },
    ],
  });

  const description = message.content.find((block) => block.type === 'text');

  const markdown = description?.text ?? '';

  res.type('text/markdown').send(markdown);
};
