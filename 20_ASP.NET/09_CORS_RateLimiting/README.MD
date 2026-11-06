### CORS

**allowed Origin**

```bash
curl -i -X OPTIONS http://localhost:3000/transactions \
  -H "Origin: http://localhost:5173" \
  -H "Access-Control-Request-Method: POST" \
  -H "Access-Control-Request-Headers: authorization,content-type"
```

```bash
curl -i http://localhost:3000/transactions \
  -H "Origin: http://localhost:5173"
```

```bash
curl -i http://localhost:3000/transactions \
  -H "Origin: http://localhost:5173" \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

**not allowed Origin**

```bash
curl -i -X OPTIONS http://localhost:3000/transactions \
  -H "Origin: http://example.com" \
  -H "Access-Control-Request-Method: POST" \
  -H "Access-Control-Request-Headers: authorization,content-type"
```
