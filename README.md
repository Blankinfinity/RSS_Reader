# RSS Reader

A console-based RSS feed reader application built in C# that downloads, stores, and displays RSS feeds. This application supports multiple feed types and provides both online and offline reading capabilities.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Architecture](#architecture)
- [Dependencies](#dependencies)
- [Contributing](#contributing)
- [License](#license)

## Overview

RSS Reader is a .NET Framework 4.5.2 console application designed to fetch RSS feeds from configured URLs, parse the XML content, and store the data locally in JSON format for offline access. The application currently supports two predefined feed categories: Books and Food from BuzzFeed.

## Features

- **RSS Feed Download**: Fetches RSS feeds from configured URLs
- **XML Parsing**: Parses RSS XML format and extracts feed items
- **JSON Storage**: Stores downloaded feeds locally in JSON format for offline access
- **Interactive Console Menu**: User-friendly console interface with menu-driven navigation
- **Multiple Feed Support**: Supports multiple RSS feed categories (Books, Food)
- **Offline Reading**: Read previously downloaded feeds without internet connection
- **Error Handling**: Graceful error handling for network failures and file operations
- **Asynchronous Operations**: Non-blocking operations for better performance

## Project Structure

```
RSS_Reader/
├── README.md
├── RSS_Reader.sln                 # Visual Studio Solution File
├── RSS_Reader/                    # Main Application Project
│   ├── RSS_Reader.csproj         # Project file
│   ├── Program.cs                # Entry point and menu system
│   ├── RssService.cs            # RSS downloading and parsing logic
│   ├── FeedFileService.cs       # File operations and JSON handling
│   ├── App.config               # Configuration file with RSS URLs
│   ├── packages.config          # NuGet package references
│   ├── books.json              # Cached books feed data
│   ├── food.json               # Cached food feed data
│   └── Properties/
│       └── AssemblyInfo.cs
└── RSS_Model/                   # Data Models Library
    ├── RSS_Model.csproj        # Model project file
    ├── ItemModel.cs            # RSS item data structure
    ├── IFeedInfo.cs           # Feed information interface
    ├── FeedInfo.cs            # Feed information implementation
    └── Properties/
        └── AssemblyInfo.cs
```

## Prerequisites

- .NET Framework 4.5.2 or higher
- Visual Studio 2015 or higher (recommended)
- Internet connection for downloading RSS feeds

## Installation

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd RSS_Reader
   ```

2. **Open the solution:**
   - Open `RSS_Reader.sln` in Visual Studio
   - Or build from command line using MSBuild

3. **Restore NuGet packages:**
   ```bash
   nuget restore RSS_Reader.sln
   ```

4. **Build the solution:**
   ```bash
   msbuild RSS_Reader.sln /p:Configuration=Release
   ```

## Configuration

The application uses `App.config` to store RSS feed URLs. To modify or add new feeds:

```xml
<appSettings>
  <add key="food" value="https://www.buzzfeed.com/food.xml"/>
  <add key="books" value="https://www.buzzfeed.com/books.xml"/>
  <!-- Add more feeds as needed -->
</appSettings>
```

**Note:** To add new feed types, you'll also need to modify the code to handle the new categories.

## Usage

1. **Run the application:**
   ```bash
   cd RSS_Reader\bin\Release
   RSS_Reader.exe
   ```

2. **Menu Options:**
   - **Option 1**: Download RSS feeds - Fetches latest feeds from configured URLs
   - **Option 2**: Read RSS Books feed - Display cached books feed
   - **Option 3**: Read RSS Food feed - Display cached food feed
   - **Option 4**: Exit - Close the application

3. **First Run:**
   - Select option 1 to download feeds initially
   - This creates local JSON cache files (books.json, food.json)

4. **Subsequent Runs:**
   - Use options 2-3 to read cached feeds offline
   - Use option 1 to refresh feeds with latest data

## Architecture

### Core Components

1. **Program.cs**: Entry point with interactive console menu
2. **RssService.cs**: Handles RSS feed downloading and XML parsing
3. **FeedFileService.cs**: Manages JSON file operations and cached data
4. **RSS_Model**: Separate library containing data models and contracts

### Key Classes

#### ItemModel
Represents a single RSS feed item with properties:
- `Title`: Feed item title
- `Description`: Feed item description/content
- `Link`: URL link to the full article
- `PubDate`: Publication date and time

#### IFeedInfo / FeedInfo
Interface and implementation for managing lists of RSS items:
- `FeedList`: Collection of `ItemModel` objects

### Data Flow

1. User selects "Download RSS feeds"
2. `RssService` fetches XML from configured URLs
3. XML is parsed into `ItemModel` objects
4. Data is serialized to JSON and saved locally
5. User can read cached data via `FeedFileService`

## Dependencies

### NuGet Packages
- **Newtonsoft.Json 12.0.3**: JSON serialization and deserialization

### .NET Framework References
- System.Configuration: Reading app settings
- System.Net.Http: HTTP client for web requests
- System.Xml: XML parsing and manipulation
- System.Threading.Tasks: Asynchronous programming

## Error Handling

The application includes error handling for:
- Network connectivity issues
- Invalid RSS feed URLs
- Malformed XML content
- File system errors
- JSON parsing errors

## Future Enhancements

- Support for additional RSS feed sources
- RSS feed validation and format checking
- Feed update scheduling and automation
- Export functionality for different formats
- GUI interface option
- Feed categorization and filtering
- Search functionality within cached feeds

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.