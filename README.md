 # EFT Helper

Ease your Escape From Tarkov experience with EFTHelper - A comprehensive desktop companion application for Escape From Tarkov players.

## 📥 Download

**[Download the latest version](https://github.com/ZeQuyk/EFTHelper/releases/latest)** by clicking on the EFTHelperSetup.exe file

---

## 📖 About

EFT Helper is a Windows desktop application designed to enhance your Escape From Tarkov gameplay experience. Built with WPF and .NET 7, it provides quick access to essential game information including item details, map information, quest data, and more - all accessible from your system tray.

## ✨ Features

- **Item Information**: Search and view detailed information about Tarkov items including:
  - Item prices and flea market data
  - Trader buy/sell prices
  - Item statistics and descriptions
  - Transaction information
  
- **Quest/Task Tracker**: Access quest information including:
  - Quest objectives and requirements
  - Quest rewards
  - Trader reputation changes
  
- **Map Information**: View details for all Tarkov maps:
  - Customs, Woods, Factory, Shoreline
  - Lighthouse, Interchange, Reserve
  - The Lab, Streets of Tarkov, Ground Zero
  
- **System Tray Integration**: Run the application in the background with quick access from the system tray

- **Theme Support**: Switch between light and dark themes to match your preference

- **Auto-Update**: Automatically receive updates when new versions are released

- **Hotkey Support**: Use global hotkeys to quickly access information while in-game

## 🚀 Installation

1. Download the latest `EFTHelperSetup.exe` from the [releases page](https://github.com/ZeQuyk/EFTHelper/releases/latest)
2. Run the installer
3. Launch EFT Helper from your Start Menu or Desktop
4. The application will appear in your system tray

## 💻 Usage

- **Launch**: Start the application - it will run in your system tray
- **Access**: Click the system tray icon to open the main window
- **Search Items**: Use the search functionality to find item information
- **Browse by Category**: Filter items by type (Ammo, Armor, Keys, Meds, etc.)
- **View Maps**: Select a location to view map-specific information
- **Check Tasks**: Browse quests and their requirements
- **Settings**: Configure theme, hotkeys, and other preferences

## 🛠️ Development

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or later
- Visual Studio 2022 (recommended) or any IDE supporting .NET 7 WPF projects
- Windows 10 or later

### Building from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/ZeQuyk/EFTHelper.git
   cd EFTHelper
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run --project src/EFTHelper/EFTHelper.csproj
   ```

### Project Structure

```
EFTHelper/
├── src/
│   └── EFTHelper/
│       ├── Controls/        # Custom WPF controls
│       ├── Converters/      # XAML value converters
│       ├── Enums/           # Enumerations
│       ├── Extensions/      # Extension methods
│       ├── Helpers/         # Helper classes
│       ├── Models/          # Data models
│       ├── Services/        # Business logic services
│       ├── ViewModels/      # MVVM view models
│       └── Views/           # WPF views
├── EFTHelper.sln           # Visual Studio solution
└── README.md
```

## 🔧 Technology Stack

- **Framework**: .NET 7.0 (Windows Desktop)
- **UI**: WPF (Windows Presentation Foundation)
- **Architecture**: MVVM (Model-View-ViewModel) using Caliburn.Micro
- **Data Source**: [Tarkov.dev GraphQL API](https://api.tarkov.dev/)
- **Auto-Update**: Clowd.Squirrel
- **Theme**: MahApps.Metro

## 📦 Dependencies

- [MahApps.Metro](https://github.com/MahApps/MahApps.Metro) - Modern UI framework for WPF
- [Caliburn.Micro](https://caliburnmicro.com/) - MVVM framework
- [Clowd.Squirrel](https://github.com/clowd/Clowd.Squirrel) - Auto-update framework
- [GlobalMouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook) - Global hotkey support
- [Hardcodet.NotifyIcon.Wpf](https://github.com/hardcodet/wpf-notifyicon) - System tray icon support
- [Tarkov.dev API](https://github.com/the-hideout/tarkov-api) - Game data API
- [BusyIndicator](https://github.com/Peoky/BusyIndicator) - Loading indicators
- [AppData-Lurker](https://github.com/C1rdec/AppData-Lurker) - AppData monitoring

## 🤝 Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Copyright (c) 2021 Vincent Cadieux

## 🙏 Acknowledgments

- [Tarkov.dev](https://tarkov.dev/) for providing the comprehensive API
- [Battlestate Games](https://www.battlestategames.com/) for Escape From Tarkov
- All contributors and users of EFT Helper

## 📞 Support

If you encounter any issues or have questions:
- Open an [issue](https://github.com/ZeQuyk/EFTHelper/issues) on GitHub
- Check existing issues for solutions

---

**Note**: This is a community tool and is not affiliated with or endorsed by Battlestate Games.
