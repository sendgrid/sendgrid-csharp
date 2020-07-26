FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /App

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Src/EventWebhook/*.csproj ./Src/EventWebhook/
COPY Tests/EventWebhook.Tests/*.csproj ./Tests/EventWebhook.Tests/
RUN dotnet restore

# copy everything else and build app
COPY Src/EventWebhook/. ./Src/EventWebhook/
WORKDIR /App/Src/EventWebhook
RUN dotnet publish -c Release -o Out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /App
COPY --from=build /App/Src/EventWebhook/Out ./

RUN echo "ASPNETCORE_URLS=http://0.0.0.0:\$PORT\nDOTNET_RUNNING_IN_CONTAINER=true" > /App/SetupHerokuEnv.sh && chmod +x /App/SetupHerokuEnv.sh

CMD /bin/bash -c "source /App/SetupHerokuEnv.sh && dotnet EventWebhook.dll"