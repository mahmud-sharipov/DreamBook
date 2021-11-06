#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DreamBook.WebApplication/DreamBook.WebApplication.csproj", "DreamBook.WebApplication/"]
COPY ["DreamBook.Persistence/DreamBook.Persistence.csproj", "DreamBook.Persistence/"]
COPY ["DreamBook.Domain/DreamBook.Domain.csproj", "DreamBook.Domain/"]
COPY ["DreamBook.Application/DreamBook.Application.csproj", "DreamBook.Application/"]
COPY ["DreamBook.AppLanguageResources/DreamBook.AppLanguageResources.csproj", "DreamBook.AppLanguageResources/"]
COPY ["DreamBook.Services/DreamBook.Services.csproj", "DreamBook.Services/"]
COPY ["DreamBook.IoC/DreamBook.IoC.csproj", "DreamBook.IoC/"]
COPY ["DreamBook.WebApplication/DreamBook.WebApplication.csproj", "DreamBook.WebApplication/"]
RUN dotnet restore "DreamBook.WebApplication/DreamBook.WebApplication.csproj"
COPY . .
WORKDIR "/src/DreamBook.WebApplication"
RUN dotnet build "DreamBook.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DreamBook.WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DreamBook.WebApplication.dll"]