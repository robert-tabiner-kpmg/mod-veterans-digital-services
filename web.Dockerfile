FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY Forms.Web/Forms.Web.csproj Forms.Web/
COPY Forms.Core/Forms.Core.csproj Forms.Core/
COPY Forms.Infrastructure/Forms.Infrastructure.csproj Forms.Infrastructure/
COPY Common/Common.csproj Common/
COPY Graph/Graph.csproj Graph/

RUN dotnet restore Forms.Web/Forms.Web.csproj -s https://api.bintray.com/nuget/gov-uk-notify/nuget -s https://api.nuget.org/v3/index.json

COPY . .
WORKDIR /src/Forms.Web

RUN dotnet publish Forms.Web.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:8080
ENV PORT=8080
EXPOSE 8080/tcp

ENTRYPOINT ["dotnet", "Forms.Web.dll"] 
