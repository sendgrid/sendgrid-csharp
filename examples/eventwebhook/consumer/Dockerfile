FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/EventWebhook/*.csproj ./src/EventWebhook/
COPY tests/EventWebhook.Tests/*.csproj ./tests/EventWebhook.Tests/
RUN dotnet restore

# copy everything else and build app
COPY src/EventWebhook/. ./src/EventWebhook/
WORKDIR /app/src/EventWebhook
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/src/EventWebhook/out ./

RUN echo "ASPNETCORE_URLS=http://0.0.0.0:\$PORT\nDOTNET_RUNNING_IN_CONTAINER=true" > /app/SetupHerokuEnv.sh && chmod +x /app/SetupHerokuEnv.sh

CMD /bin/bash -c "source /app/SetupHerokuEnv.sh && dotnet EventWebhook.dll"