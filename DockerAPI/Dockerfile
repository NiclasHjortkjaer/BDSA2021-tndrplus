# Base sdk from microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build-env
WORKDIR /app
#copy CSPROJ and restore any dependecies
COPY *.csproj ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
#port
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet, DockerAPI.dll" ]