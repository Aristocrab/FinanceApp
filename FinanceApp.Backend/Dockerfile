FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./FinanceApp.Domain/FinanceApp.Domain.csproj", "src/FinanceApp.Domain/"]
COPY ["./FinanceApp.Application/FinanceApp.Application.csproj", "src/FinanceApp.Application/"]
COPY ["./FinanceApp.WebApi/FinanceApp.WebApi.csproj", "src/FinanceApp.WebApi/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/FinanceApp.WebApi/FinanceApp.WebApi.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/FinanceApp.WebApi/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "FinanceApp.WebApi.dll" ]