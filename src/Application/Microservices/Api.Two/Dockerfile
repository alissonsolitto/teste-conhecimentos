FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Application/Microservices/Api.Two/Api.Two.csproj", "src/Application/Microservices/Api.Two/"]
COPY ["src/Domain/Domain.Two/Domain.Two.csproj", "src/Domain/Domain.Two/"]
COPY ["src/Infrastructure/Useful/Useful.csproj", "src/Infrastructure/Useful/"]
RUN dotnet restore "src/Application/Microservices/Api.Two/Api.Two.csproj"
COPY . .
WORKDIR "/src/src/Application/Microservices/Api.Two"
RUN dotnet build "Api.Two.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Two.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Two.dll"]