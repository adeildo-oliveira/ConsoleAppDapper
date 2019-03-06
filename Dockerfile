FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ConsoleAppDapper/ConsoleAppDapper.csproj", "ConsoleAppDapper/"]
RUN dotnet restore "ConsoleAppDapper/ConsoleAppDapper.csproj"
COPY . .
WORKDIR "/src/ConsoleAppDapper"
RUN dotnet build "ConsoleAppDapper.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ConsoleAppDapper.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConsoleAppDapper.dll"]