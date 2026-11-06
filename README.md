# MauiBlazorViews Template

A mobile-friendly app framework template combining **.NET MAUI** and **Blazor** to build cross-platform applications with web-based UI components.

## Overview

MauiBlazorViews provides a modern template for developing mobile applications that leverage Blazor's powerful web UI capabilities within the MAUI framework. This approach enables developers to:

- Build once, deploy to multiple platforms (iOS, Android, Windows, macOS)
- Use familiar Blazor components and C# for UI logic
- Maintain a responsive, mobile-first design
- Share code between web and mobile implementations

## Features

- ✨ MAUI + Blazor integration
- 📱 Cross-platform support (iOS, Android, Windows, macOS)
- 🎯 Mobile-first responsive design
- 🔄 Shared C# codebase
- ⚡ Modern .NET 10 stack

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later
- Visual Studio 2026 or Visual Studio Code with appropriate extensions
- Platform-specific requirements:
  - **iOS**: Xcode (on macOS)
  - **Android**: Android SDK and emulator/device
  - **Windows**: Windows 10/11
  - **macOS**: macOS development tools

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/nhave/MauiBlazorViewsTemplate.git
   cd MauiBlazorViewsTemplate
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run on your target platform:
   ```bash
   # Windows
   dotnet build -f net10.0-windows

   # macOS
   dotnet build -f net10.0-maccatalyst

   # iOS
   dotnet build -f net10.0-ios

   # Android
   dotnet build -f net10.0-android
   ```

## Project Structure

```
MauiBlazorViewsTemplate/
├── Components/          # Blazor components
├── Pages/              # Blazor pages
├── Services/           # Application services
├── Platforms/          # Platform-specific code
├── wwwroot/            # Static assets
└── MauiProgram.cs      # MAUI configuration
```

## Usage

### Creating a New Component

Create Blazor components in the `Components/` directory and use them within your MAUI app:

```razor
@component
<div class="container">
    <h1>Welcome to MauiBlazorViews</h1>
    <p>@Message</p>
</div>

@code {
    [Parameter]
    public string Message { get; set; } = "Hello, World!";
}
```

### Customization

Modify the `MauiProgram.cs` to configure:
- Theme and styling
- Services and dependency injection
- Platform-specific settings
- Navigation and routing

## Documentation

For more information:
- [MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui)
- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor)
- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues and questions, please open an [issue](https://github.com/nhave/MauiBlazorViewsTemplate/issues) on GitHub.

---

**Built with ❤️ using .NET MAUI and Blazor**