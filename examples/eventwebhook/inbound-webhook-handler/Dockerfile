FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /App

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Src/Inbound/*.csproj ./Src/Inbound/
COPY Tests/Inbound.Tests/*.csproj ./Tests/Inbound.Tests/
RUN dotnet restore

# copy everything else and build app
COPY Src/Inbound/. ./Src/Inbound/
WORKDIR /App/Src/Inbound
RUN dotnet publish -c Release -o Out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /App
COPY --from=build /App/Src/Inbound/Out ./

RUN echo "ASPNETCORE_URLS=http://0.0.0.0:\$PORT\nDOTNET_RUNNING_IN_CONTAINER=true" > /App/SetupHerokuEnv.sh && chmod +x /App/SetupHerokuEnv.sh

CMD /bin/bash -c "source /App/SetupHerokuEnv.sh && dotnet Inbound.dll"
