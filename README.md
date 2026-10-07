# 🐶 Pet-Feeder

> A smart pet feeder controlled over the internet: an ASP.NET Core Web API talks to the feeder device through **Azure IoT Hub**, so you can trigger feeding remotely and, in future versions, watch your pet eat on camera.

![Status](https://img.shields.io/badge/status-in%20development-yellow)
![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-512BD4)
![Cloud](https://img.shields.io/badge/cloud-Azure%20IoT%20Hub-0078D4)
![License](https://img.shields.io/badge/license-MIT-blue)

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Repository Structure](#repository-structure)
4. [Tech Stack](#tech-stack)
5. [Getting Started](#getting-started)
6. [Configuration](#configuration)
7. [API Reference](#api-reference)
8. [Device Side](#device-side)
9. [Roadmap](#roadmap)
10. [Security Notes](#security-notes)
11. [Contributing](#contributing)
12. [License](#license)

## Overview

Pet-Feeder lets you control a dog feeder from anywhere. The backend (`DogFeederAPI2`) exposes a REST API. When you call it, the API uses an IoT Hub service to send a command to the registered feeder device, which then dispenses food.

**Goals**
- Remote, reliable feeding over the internet
- Secure device communication through Azure IoT Hub (no open ports at home)
- A clean API that any client (web, mobile, bot) can use
- Easy to extend with a camera, sensors and scheduling

## Architecture

```
┌────────────┐   HTTPS   ┌──────────────────┐  Azure SDK  ┌───────────────┐  MQTT/AMQP  ┌──────────────┐
│  Client    │ ────────► │  DogFeederAPI2   │ ──────────► │ Azure IoT Hub │ ──────────► │ Feeder device│
│ (web/app)  │           │ (ASP.NET Core)   │             │               │             │ (motor/servo)│
└────────────┘           └──────────────────┘             └───────────────┘             └──────────────┘
```

1. A client sends a request to the API.
2. `DeviceController` handles it and calls `IoTHubService`.
3. `IoTHubService` sends the command to the device through Azure IoT Hub.
4. The device runs the motor and can report its status back.

## Repository Structure

```
Pet-Feeder/
├── README.md
├── .gitignore
└── DogFeederAPI2/
    ├── Controllers/
    │   └── DeviceController.cs     # API endpoints for the device
    ├── Services/
    │   └── IoTHubService.cs        # Azure IoT Hub communication
    ├── Properties/                 # launch settings
    ├── Program.cs                  # app startup and dependency injection
    ├── appsettings.json            # configuration (no secrets!)
    ├── DogFeederAPI2.csproj
    └── DogFeederAPI2.sln
```

## Tech Stack

| Area | Technology |
|---|---|
| Backend | C#, ASP.NET Core Web API |
| Cloud messaging | Azure IoT Hub |
| IDE | Visual Studio / VS Code / Rider |
| Device | <!-- TODO: Raspberry Pi / ESP32 / Arduino + motor or servo --> |
| Version control | Git, GitHub |

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (the version in `DogFeederAPI2.csproj`)
- An Azure account with an **IoT Hub** and a registered device
- Git

### Installation

```bash
git clone https://github.com/ADobrovolschi/Pet-Feeder.git
cd Pet-Feeder/DogFeederAPI2
dotnet restore
```

### Run

```bash
dotnet run
```

The API starts on the URL printed in the console (see `Properties/launchSettings.json`). If Swagger is enabled, open `/swagger` in the browser.

## Configuration

The IoT Hub connection string is a secret. **Never commit it.** Use .NET user secrets for local development:

```bash
cd DogFeederAPI2
dotnet user-secrets init
dotnet user-secrets set "IoTHub:ConnectionString" "<your-service-connection-string>"
dotnet user-secrets set "IoTHub:DeviceId" "<your-device-id>"
```

| Setting | Description |
|---|---|
| `IoTHub:ConnectionString` | Service connection string from Azure IoT Hub (Shared access policy: service) |
| `IoTHub:DeviceId` | The ID of the registered feeder device |

<!-- TODO: make sure these key names match the ones read in IoTHubService.cs / Program.cs -->

In production, use environment variables or Azure Key Vault.

## API Reference

<!-- TODO: replace with the real routes from DeviceController.cs -->

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/device/feed` | Send a "feed now" command to the device |
| `GET` | `/api/device/status` | Get device status |

Example:

```bash
curl -X POST https://localhost:<port>/api/device/feed
```

## Device Side

<!-- TODO: describe what runs on the physical feeder -->

- Board: <!-- Raspberry Pi / ESP32 / Arduino -->
- Actuator: <!-- servo / stepper / DC motor + driver -->
- Connects to Azure IoT Hub with its **device** connection string
- Listens for cloud-to-device messages or direct methods and runs the motor

## Roadmap

### Camera and monitoring
- [ ] Live camera stream of the bowl (Raspberry Pi Camera or ESP32-CAM)
- [ ] Motion-triggered snapshots when the pet approaches
- [ ] Pet detection with a lightweight model (TensorFlow Lite / YOLO) to log who ate and when
- [ ] Photo attached to every feeding event
- [ ] Night vision with IR LED and NoIR camera

### Smart feeding
- [ ] Scheduled feeding (daily timetable stored in the cloud)
- [ ] Portion control by weight with a load cell (HX711)
- [ ] Safety limits: maximum portions per day, minimum interval between feeds
- [ ] Feeding history in a database (Azure SQL or Cosmos DB)
- [ ] Multi-pet support (RFID or camera recognition)

### Backend and cloud
- [ ] Device telemetry: food level, motor state, online/offline
- [ ] Replace sample `WeatherForecast` code with real models
- [ ] Authentication (JWT) and per-user devices
- [ ] Swagger/OpenAPI documentation
- [ ] Unit tests and GitHub Actions CI
- [ ] Deployment to Azure App Service or Docker

### Clients and notifications
- [ ] Web dashboard (Vue/React) and a mobile app
- [ ] Notifications via Telegram, ntfy or push: "Rex was fed at 18:02"
- [ ] Home Assistant / MQTT integration
- [ ] Low-food and jam alerts

## Security Notes

- Keep connection strings, keys and tokens out of Git. Use user secrets, environment variables or Key Vault.
- If a key was ever committed, **rotate it** in Azure. Deleting the file is not enough, because it stays in Git history.
- Add authentication to the API before exposing it publicly, otherwise anyone can trigger the feeder.
- Do not rely on this device as the only food source for your pet. Test it while you are home.

## Contributing

1. Fork the repository
2. Create a branch: `git checkout -b feature/camera-support`
3. Commit: `git commit -m "Add camera streaming"`
4. Push and open a pull request

## License

Distributed under the MIT License. See `LICENSE`.

## Author

**Alexandru Dobrovolschi** · [@ADobrovolschi](https://github.com/ADobrovolschi)
