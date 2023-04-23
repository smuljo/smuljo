FROM node:18-alpine as build-deps
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . ./
ENV NODE_ENV=production
ENV CI true

RUN npm run build

FROM nginx:1.23.3-alpine
COPY --from=build-deps /usr/src/app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["/bin/sh" , "-c" , "exec nginx -g 'daemon off;'"]