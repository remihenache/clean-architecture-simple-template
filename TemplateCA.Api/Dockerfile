FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SMH.Api/SMH.Api.csproj", "SMH.Api/"]
RUN dotnet restore "SMH.Api/SMH.Api.csproj"
COPY . .
WORKDIR "/src/SMH.Api"
RUN dotnet build "SMH.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMH.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMH.Api.dll"]
