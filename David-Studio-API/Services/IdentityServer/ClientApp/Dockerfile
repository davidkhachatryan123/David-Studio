FROM node:18-bullseye as build
WORKDIR /app

ARG host="localhost"
ENV HOST=$host

RUN npm install -g @angular/cli

COPY Services/IdentityServer/ClientApp/package.json .
COPY Services/IdentityServer/ClientApp/package-lock.json .

RUN npm install -g npm@9.6.4
RUN npm install

COPY Services/IdentityServer/ClientApp .

RUN apt-get update
RUN apt-get install -y gettext
RUN envsubst < src/environments/environment.prod.ts.tpl > src/environments/environment.prod.ts

RUN npm run build -- --c production

FROM nginx as runtime
COPY ["Services/IdentityServer/ClientApp/nginx.conf", "/etc/nginx/conf.d/default.conf"]
COPY --from=build /app/dist /usr/share/nginx/html
