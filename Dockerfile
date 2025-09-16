# Assembling the project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copying project files and restoring dependencies
COPY TemplatesManager/*.csproj ./TemplatesManager/
RUN dotnet restore ./TemplatesManager/TemplatesManager.csproj

# Copy the entire code and publish it
COPY . ./
RUN dotnet publish -c Release -o out

# Lightweight image to run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

#For PuppeteerSharp/Chromium
RUN apt-get update && apt-get install -y \
    libglib2.0-0 \
    libnss3 \
    libatk1.0-0 \
    libatk-bridge2.0-0 \
    libcups2 \
    libdrm2 \
    libxkbcommon0 \
    libxcomposite1 \
    libxdamage1 \
    libxfixes3 \
    libxrandr2 \
    libgbm1 \
    libasound2 \
    libpangocairo-1.0-0 \
    libpango-1.0-0 \
    libcairo2 \
    libfontconfig1 \
    libgconf-2-4 \
    libgtk-3-0 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/out .

# Set ports and environment variables
ENV ASPNETCORE_URLS="http://+:5119;"

EXPOSE 5119

ENTRYPOINT ["dotnet", "TemplatesManager.dll"]
