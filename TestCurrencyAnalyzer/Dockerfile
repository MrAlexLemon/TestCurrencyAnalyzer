FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/TestCurrencyAnalyzer.Domain/*.csproj ./src/TestCurrencyAnalyzer.Domain/
COPY src/TestCurrencyAnalyzer.Application/*.csproj ./src/TestCurrencyAnalyzer.Application/
COPY src/TestCurrencyAnalyzer.Infrastructure/*.csproj ./src/TestCurrencyAnalyzer.Infrastructure/
COPY src/TestCurrencyAnalyzer.Api/*.csproj ./src/TestCurrencyAnalyzer.Api/
RUN dotnet restore

COPY . .
RUN dotnet build
FROM build AS appraunittestrunner

FROM build AS publish
WORKDIR /app/src/TestCurrencyAnalyzer.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 55833
COPY --from=publish /app/src/TestCurrencyAnalyzer.Api/out ./
ENTRYPOINT dotnet TestCurrencyAnalyzer.Api.dll