FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Notes.Domain/Notes.Domain.csproj", "Notes.Domain/"]
COPY ["Notes.Application/Notes.Application.csproj", "Notes.Application/"]
COPY ["Notes.Infrastructure/Notes.Infrastructure.csproj", "Notes.Infrastructure/"]
COPY ["Notes.Api/Notes.Api.csproj", "Notes.Api/"]
RUN dotnet restore "Notes.Api/Notes.Api.csproj"
COPY . .
WORKDIR "/src/Notes.Api"
RUN dotnet publish "Notes.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Notes.Api.dll"]
