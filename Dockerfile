# Defines an image for BUILD enviroment
FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS BUILD

# Defines the work directory
WORKDIR /build

# Copy Everything
COPY . .

# Restore and Build application
RUN dotnet restore
RUN dotnet publish -r alpine-x64 -p:PublishTrimmed=true -p:PublishReadyToRun=true -p:PublishSingleFile=true --self-contained true -c Release -o /app

# Defines an image for RUNTIME enviroment
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS RUNTIME

# Add needed packages
RUN apk add --no-cache icu-libs

# Defines the work directory
WORKDIR /app

# Defines globalization invariant to FALSE
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Defines ASP .NET Core App Port
ENV ASPNETCORE_URLS=http://*:8080

COPY --from=BUILD /app .

ENTRYPOINT ["./Healthcheck.API"]