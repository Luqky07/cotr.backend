#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
EXPOSE 80
EXPOSE 443
EXPOSE 587
WORKDIR /app

RUN apt-get update && \
    apt-get install -y openjdk-17-jdk

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["cotr.backend.csproj", "."]
RUN dotnet restore "./cotr.backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "cotr.backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cotr.backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .

WORKDIR /app
RUN mkdir -p /exercises/java_default/bin/code
RUN mkdir -p /exercises/java_default/bin/test
RUN mkdir -p /exercises/java_default/lib
RUN mkdir -p /exercises/java_default/src/code
RUN mkdir -p /exercises/java_default/src/test

COPY java_test_files/hamcrest-core-1.3.jar /exercises/java_default/lib/
COPY java_test_files/junit-4.13.2.jar /exercises/java_default/lib/

ENTRYPOINT ["dotnet", "cotr.backend.dll"]