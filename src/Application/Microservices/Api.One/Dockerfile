FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Application/Microservices/Api.One/Api.One.csproj", "src/Application/Microservices/Api.One/"]
COPY ["src/Infrastructure/Useful/Useful.csproj", "src/Infrastructure/Useful/"]
COPY ["src/Domain/Domain.One/Domain.One.csproj", "src/Domain/Domain.One/"]
RUN dotnet restore "src/Application/Microservices/Api.One/Api.One.csproj"
COPY . .
WORKDIR "/src/src/Application/Microservices/Api.One"
RUN dotnet build "Api.One.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.One.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.One.dll"]