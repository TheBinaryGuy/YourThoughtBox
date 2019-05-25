FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectX.App/ProjectX.App.csproj", "ProjectX.App/"]
RUN dotnet restore "ProjectX.App/ProjectX.App.csproj"
COPY . .
WORKDIR "/src/ProjectX.App"
RUN dotnet build "ProjectX.App.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectX.App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectX.App.dll"]