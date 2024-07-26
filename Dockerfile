FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY DataAccess/*.csproj ./DataAccess/
COPY Domain/*.csproj ./Domain/
COPY Seeder/*.csproj ./Seeder/
COPY Services/*.csproj ./Services/
COPY UnitTesting/*.csproj ./UnitTesting/
COPY BetsApi/*.csproj ./BetsApi/

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5155
EXPOSE 7291

ENTRYPOINT ["dotnet", "BetsApi.dll"]
