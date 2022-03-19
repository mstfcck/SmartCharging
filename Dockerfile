# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["SmartCharging.Api.csproj", "SmartCharging.Api/"]
# RUN dotnet restore "SmartCharging.Api.csproj"
# COPY . .
# WORKDIR "/src/SmartCharging.Api"
# RUN dotnet build "SmartCharging.Api.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "SmartCharging.Api.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "SmartCharging.Api.dll"]


# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY *.csproj ./SmartCharging.Api/
RUN dotnet restore -r linux-musl-arm64

# copy everything else and build app
COPY . ./SmartCharging.Api/
WORKDIR /source/SmartCharging.Api
RUN dotnet publish -c release -o /app -r linux-musl-arm64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-arm64v8
WORKDIR /app
COPY --from=build /app ./

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
# ENV \
#     DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
#     LC_ALL=en_US.UTF-8 \
#     LANG=en_US.UTF-8
# RUN apk add --no-cache icu-libs

ENTRYPOINT ["./SmartCharging.Api"]