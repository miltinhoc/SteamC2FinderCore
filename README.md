# SteamC2Finder

## Overview
**SteamC2Finder** is a tool designed to identify Steam profiles that include Command and Control (C2) server information within their profile names. It scans profiles for patterns that indicate potential C2 servers and exports the findings in MISP format.

The tool generates two different MISP JSON files:
- One for IP addresses (`ip-dst` type).
- One for URLs (`url` type).

## Building and Running

It's possible to create builds for Windows, Linux and macOS.

### Step 1: Download and Install .NET Core 8.0 SDK

Download and install the .NET Core 8.0 SDK from the official website:
[.NET Core 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Follow the installation instructions for your operating system.

### Step 2: Restore Dependencies
```bash
dotnet restore
```

### Step 3: Build the Project
```bash
dotnet build
```

### Step 4: Run the Project
```bash
dotnet run --project SteamC2FinderCore/SteamC2FinderCore.csproj
```

Make sure to pass the arguments needed.

### Step 5: Publish the Project (Optional)
```bash
dotnet publish --configuration Release --runtime linux-x64
```

If you want to build for other platform, check the official list of available options from microsoft:
[.NET RID Catalog](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog)

## TODO
- [ ] **C2 History Tracking**: Implement a mechanism to track and store previously found C2 addresses to avoid creating duplicates after multiple usages.
