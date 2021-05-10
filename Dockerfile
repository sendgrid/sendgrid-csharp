FROM mcr.microsoft.com/dotnet/sdk:3.1

RUN apt-get update \
    && apt-get install -y curl make apt-transport-https

COPY prism/prism/nginx/cert.crt /usr/local/share/ca-certificates/cert.crt
RUN update-ca-certificates

COPY . .

RUN make install
