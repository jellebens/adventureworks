FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
ARG VERSION="dev"
ENV VERSION=$VERSION
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS publish
WORKDIR /src

COPY . .
RUN dotnet restore DocumentProcessor/DocumentProcessor.csproj
RUN dotnet publish DocumentProcessor/DocumentProcessor.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentProcessor.dll"]