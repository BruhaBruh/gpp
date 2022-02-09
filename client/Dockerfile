FROM node:16-alpine as builder

WORKDIR /app

EXPOSE 3000

COPY . .

RUN yarn
RUN yarn build

CMD yarn preview --host 0.0.0.0 --port 3000