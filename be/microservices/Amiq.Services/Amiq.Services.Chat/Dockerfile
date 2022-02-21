FROM node:14.15.1

COPY ["package.json", "./"]
COPY ["package-lock.json", "./"]

COPY . .

RUN npm install -g nodemon
RUN npm install

EXPOSE 4000

ENTRYPOINT npm run server