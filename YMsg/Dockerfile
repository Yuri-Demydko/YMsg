FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["YMsg/YMsg.csproj", "YMsg/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "YMsg/YMsg.csproj"
COPY . .
WORKDIR "/src/YMsg"
RUN dotnet build "YMsg.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YMsg.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "YMsg.dll"]
