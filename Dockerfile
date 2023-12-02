FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["sales-up.csproj", "./"]
RUN dotnet restore "./sales-up.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "sales-up.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sales-up.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sales-up.dll"]
