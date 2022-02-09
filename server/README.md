```
proxy_set_header Host $http_host;
proxy_set_header X-Real-IP $remote_addr;
proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
proxy_set_header X-Frame-Options SAMEORIGIN;

location /api {
    proxy_pass http://localhost:5000/api;
}

location /graphql {
    proxy_set_header Upgrade $http_upgrade;
    proxy_set_header Connection "upgrade";

    proxy_pass http://localhost:5000/graphql;
}

location / {
    proxy_pass http://localhost:4200;
}
```
