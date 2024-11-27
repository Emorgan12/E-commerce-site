# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 5001

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["api.csproj", "./"]
RUN dotnet restore "./api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY products.db ./
USER root
RUN chmod 666 /app/products.db
ENTRYPOINT ["dotnet", "api.dll"]