FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/UrlShortner.API/UrlShortner.API.csproj", "src/UrlShortner.API/"]
RUN dotnet restore "src/UrlShortner.API/UrlShortner.API.csproj"
COPY . .
WORKDIR "/src/src/UrlShortner.API"
RUN dotnet build "UrlShortner.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UrlShortner.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UrlShortner.API.dll"]