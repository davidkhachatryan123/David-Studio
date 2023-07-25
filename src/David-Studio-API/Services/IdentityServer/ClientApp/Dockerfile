FROM node:18-bullseye as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY package.json .
COPY package-lock.json .

RUN npm install -g npm@9.6.4
RUN npm install

COPY . .
RUN npm run build -- --c production

FROM nginx as runtime
COPY ["nginx.conf", "/etc/nginx/conf.d/default.conf"]
COPY --from=build /app/dist /usr/share/nginx/html
