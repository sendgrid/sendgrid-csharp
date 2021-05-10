FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/Inbound/*.csproj ./src/Inbound/
COPY tests/Inbound.Tests/*.csproj ./tests/Inbound.Tests/
RUN dotnet restore

# copy everything else and build app
COPY src/Inbound/. ./src/Inbound/
WORKDIR /app/src/Inbound
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/src/Inbound/out ./

RUN echo "ASPNETCORE_URLS=http://0.0.0.0:\$PORT\nDOTNET_RUNNING_IN_CONTAINER=true" > /app/SetupHerokuEnv.sh && chmod +x /app/SetupHerokuEnv.sh

CMD /bin/bash -c "source /app/SetupHerokuEnv.sh && dotnet Inbound.dll"
