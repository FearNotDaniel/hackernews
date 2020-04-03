FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY hackernews/hackernews.csproj hackernews/
COPY HackerNewsScraper/HackerNewsScraper.csproj HackerNewsScraper/
RUN dotnet restore hackernews/hackernews.csproj
COPY . .
WORKDIR /src/hackernews
RUN dotnet build hackernews.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish hackernews.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "hackernews.dll", "--posts 50"]
