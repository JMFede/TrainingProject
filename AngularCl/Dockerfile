# base image
FROM node:lts as builder

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY ./package.json /app/package.json
RUN npm install --no-package-lock
RUN npm install -g @angular/cli@9.1.7

# add app
COPY . /app

RUN ls
# generate build
RUN ng build --configuration production --output-path=dist

RUN ls -r dist

FROM nginx:1.25.3-alpine

# copy artifact build from the 'build environment'
COPY --from=builder /app/dist /usr/share/nginx/html
# copy default.conf in nginx
COPY --from=builder /app/nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/default.conf /etc/nginx/conf.d/default.conf

# expose port 80
EXPOSE 80

# run nginx
CMD ["nginx", "-g", "daemon off;"]
