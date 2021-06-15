FROM mcr.microsoft.com/dotnet/sdk:3.1

COPY prism/prism/nginx/cert.crt /usr/local/share/ca-certificates/cert.crt

RUN apt-get update \
    && apt-get install -y make apt-transport-https \
    && update-ca-certificates

COPY . .

RUN make install
