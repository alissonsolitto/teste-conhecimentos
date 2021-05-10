FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Application/WebApplication/Web.App/Web.App.csproj", "src/Application/WebApplication/Web.App/"]
COPY ["src/Domain/Domain.Two/Domain.Two.csproj", "src/Domain/Domain.Two/"]
COPY ["src/Infrastructure/Useful/Useful.csproj", "src/Infrastructure/Useful/"]
RUN dotnet restore "src/Application/WebApplication/Web.App/Web.App.csproj"
COPY . .
WORKDIR "/src/src/Application/WebApplication/Web.App"
RUN dotnet build "Web.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.App.dll"]
