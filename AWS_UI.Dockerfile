FROM node:16 AS base
WORKDIR /src
COPY ./InventoryInCSharpUI/package*.json ./
COPY ./InventoryInCSharpUI .
EXPOSE 8080


FROM base AS build
RUN npm install

FROM build AS publish
RUN npm run build

#FROM base AS run
#COPY --from=publish ./src/build/static/ .

#ENTRYPOINT ["node", "./build/static/js/main.min.js"]
FROM nginx:1.19-alpine AS server
COPY --from=publish ./src/build /usr/share/nginx/html
COPY ./InventoryInCSharpUI/nginx.conf /etc/nginx/conf.d/default.conf
# Expose port
EXPOSE 80

# Copy .env file and shell script to container
WORKDIR /usr/share/nginx/html
COPY ./InventoryInCSharpUI/env.sh .
COPY ./InventoryInCSharpUI/.env .env

# Add bash
RUN apk add --no-cache bash

# Make our shell script executable
RUN chmod +x env.sh

# Start nginx
CMD ["nginx", "-g", "daemon off;"]