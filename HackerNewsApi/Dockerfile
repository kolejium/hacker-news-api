#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
WORKDIR /src
COPY ["./HackerNewsApi.csproj", "HackerNewsApi/"]
RUN dotnet restore "HackerNewsApi/HackerNewsApi.csproj"

WORKDIR "/src/HackerNewsApi"
COPY . .

RUN dotnet build "HackerNewsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNewsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNewsApi.dll"]