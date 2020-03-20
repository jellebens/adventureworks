#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS publish
WORKDIR /src

COPY . .
RUN dotnet restore Adventureworks.Web/Adventureworks.Web.csproj
RUN dotnet publish Adventureworks.Web/Adventureworks.Web.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adventureworks.Web.dll"]