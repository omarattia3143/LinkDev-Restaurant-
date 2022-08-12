FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LinkDev.EgyptianRecipes.csproj", "./"]
RUN dotnet restore "LinkDev.EgyptianRecipes.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "LinkDev.EgyptianRecipes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinkDev.EgyptianRecipes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinkDev.EgyptianRecipes.dll"]
