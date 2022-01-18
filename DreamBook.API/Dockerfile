#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DreamBook.API/DreamBook.API.csproj", "DreamBook.API/"]
COPY ["DreamBook.Persistence/DreamBook.Persistence.csproj", "DreamBook.Persistence/"]
COPY ["DreamBook.Domain/DreamBook.Domain.csproj", "DreamBook.Domain/"]
COPY ["DreamBook.Application/DreamBook.Application.csproj", "DreamBook.Application/"]
RUN dotnet restore "DreamBook.API/DreamBook.API.csproj"
COPY . .
WORKDIR "/src/DreamBook.API"
RUN dotnet build "DreamBook.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DreamBook.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DreamBook.API.dll"]