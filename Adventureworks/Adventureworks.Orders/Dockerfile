FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS publish
WORKDIR /src

COPY . .
RUN dotnet restore Adventureworks.Orders/Adventureworks.Orders.csproj
RUN dotnet publish Adventureworks.Orders/Adventureworks.Orders.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adventureworks.Orders.dll"]