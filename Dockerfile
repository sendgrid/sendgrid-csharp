FROM microsoft/dotnet:1.0-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/*.csproj ./src/
WORKDIR /app/src
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY src/. ./src/
WORKDIR /app/src
RUN dotnet publish -c Release -o out

# test application
FROM build AS testrunner
WORKDIR /app/tests
COPY tests/. .
ENTRYPOINT ["dotnet", "test"]