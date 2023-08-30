FROM node:18-bullseye as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY Services/IdentityServer/ClientApp/package.json .
COPY Services/IdentityServer/ClientApp/package-lock.json .

RUN npm install -g npm@9.6.4
RUN npm install

COPY Services/IdentityServer/ClientApp .
RUN npm run build -- --c production

FROM nginx as runtime
COPY ["Services/IdentityServer/ClientApp/nginx.conf", "/etc/nginx/conf.d/default.conf"]
COPY --from=build /app/dist /usr/share/nginx/html
