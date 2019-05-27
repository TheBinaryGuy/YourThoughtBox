FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ThoughtBox.App/ThoughtBox.App.csproj", "ThoughtBox.App/"]
RUN dotnet restore "ThoughtBox.App/ThoughtBox.App.csproj"
COPY . .
WORKDIR "/src/ThoughtBox.App"
RUN dotnet build "ThoughtBox.App.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ThoughtBox.App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ThoughtBox.App.dll"]
