# 🐾 Pet-Feeder

> A smart, automated pet feeder: schedule meals, dispense precise portions and (soon) watch your pet eat from anywhere.

![Status](https://img.shields.io/badge/status-in%20development-yellow)
![License](https://img.shields.io/badge/license-MIT-blue)

<!-- TODO: add a photo or GIF of the finished device -->
<!-- ![Pet-Feeder demo](docs/images/demo.jpg) -->

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Hardware](#hardware)
4. [Wiring](#wiring)
5. [Software Architecture](#software-architecture)
6. [Getting Started](#getting-started)
7. [Configuration](#configuration)
8. [Usage](#usage)
9. [Project Structure](#project-structure)
10. [Roadmap](#roadmap)
11. [Troubleshooting](#troubleshooting)
12. [Contributing](#contributing)
13. [License](#license)

## Overview

Pet-Feeder is a DIY automatic feeder that dispenses food at scheduled times or on demand. It is designed to be cheap, repairable and easy to extend, using common electronics parts and open-source software.

**Goals**
- Reliable, repeatable portion control
- Simple scheduling without cloud lock-in
- Easy to build, modify and repair
- Safe for pets: no sharp parts, no jammed food, no overfeeding

## Features

**Current**
- [ ] Scheduled feeding at configurable times <!-- TODO: tick what works -->
- [ ] Manual feed on demand
- [ ] Adjustable portion size
- [ ] Logging of every feeding event

**Planned:** see the [Roadmap](#roadmap).

## Hardware

<!-- TODO: replace with your real parts -->

| Component | Model | Purpose |
|---|---|---|
| Microcontroller / SBC | Raspberry Pi / Arduino / ESP32 | Main controller |
| Actuator | Servo or stepper motor | Dispenses food |
| Power supply | 5 V, 2-3 A | Powers board and motor |
| Food container | Food-safe hopper | Stores kibble |
| Optional sensor | Load cell + HX711 | Weighs the portion |
| Optional sensor | Ultrasonic / IR | Detects food level |

Estimated cost: **<!-- TODO: add total in RON/EUR -->**

## Wiring

<!-- TODO: add a wiring diagram, for example docs/images/wiring.png -->

| Device pin | Board pin | Notes |
|---|---|---|
| Motor signal | GPIO `<!-- TODO -->` | PWM-capable pin |
| Motor VCC | 5 V (external) | Do not power from the board pin if it draws too much current |
| GND | GND | Common ground |

## Software Architecture

```
 Scheduler ──► Feeding controller ──► Motor driver ──► Food dispenser
     │                │
     │                └──► Event log (feeding history)
     └──► Config (times, portion size)
```

- **Scheduler**: triggers feeding at configured times.
- **Feeding controller**: runs the motor for the right duration or angle, or until the target weight is reached.
- **Logger**: stores each event with a timestamp.

## Getting Started

### Prerequisites

- <!-- TODO: Python 3.9+ / Arduino IDE / PlatformIO -->
- Git
- Assembled hardware (see [Hardware](#hardware))

### Installation

```bash
git clone https://github.com/ADobrovolschi/Pet-Feeder.git
cd Pet-Feeder
# TODO: install dependencies, for example:
# python3 -m venv .venv && source .venv/bin/activate
# pip install -r requirements.txt
```

## Configuration

Copy the example config and edit it. **Never commit real passwords or tokens.**

```bash
cp config.example.yaml config.yaml    # TODO: adapt to your format
```

| Setting | Example | Description |
|---|---|---|
| `feed_times` | `["08:00", "18:00"]` | Daily feeding schedule |
| `portion_seconds` | `2.5` | Motor run time per portion |
| `max_portions_per_day` | `4` | Safety limit against overfeeding |

## Usage

```bash
# TODO: replace with your real commands
python main.py            # start the feeder
python main.py --feed     # dispense one portion now
```

## Project Structure

<!-- TODO: adjust to your real layout -->

```
Pet-Feeder/
├── README.md
├── LICENSE
├── .gitignore
├── src/            # source code
├── config.example.yaml
├── docs/
│   └── images/     # photos, wiring diagram
└── tests/
```

## Roadmap

### Near term
- [ ] Safety limits: maximum portions per day and a minimum interval between feeds
- [ ] Persistent feeding history (CSV or SQLite)
- [ ] Jam detection (motor current or timeout) with a retry routine
- [ ] Low-food alert using a level sensor

### Camera and monitoring
- [ ] **Live camera view** of the bowl (Raspberry Pi Camera or ESP32-CAM) in a web page
- [ ] **Motion-triggered snapshots** or short clips when the pet approaches the bowl
- [ ] **Pet detection** with a lightweight model (for example TensorFlow Lite or YOLO) to log who ate and when
- [ ] **Photo log** attached to each feeding event
- [ ] Night view with an IR LED and a NoIR camera

### Smart feeding
- [ ] **Weight-based portions** with a load cell (HX711), for grams instead of seconds
- [ ] Track how much food was actually eaten
- [ ] Multi-pet support with RFID or camera recognition, with a separate schedule per pet
- [ ] Automatic portion adjustment based on eating history

### Connectivity and control
- [ ] Local web dashboard to schedule, feed and view history
- [ ] Mobile notifications (Telegram bot, ntfy or Pushover): "Rex was fed at 18:02"
- [ ] MQTT and Home Assistant integration
- [ ] Remote access through a VPN (WireGuard or Tailscale) instead of port forwarding
- [ ] Two-way audio or a speaker to call the pet

### Hardware improvements
- [ ] 3D-printed enclosure and a food-safe dispensing mechanism
- [ ] Battery or UPS backup for power outages
- [ ] Water level monitoring and a water dispenser module

## Troubleshooting

| Problem | Possible cause | Fix |
|---|---|---|
| Motor does not move | Wrong pin or no external power | Check wiring and common GND |
| Food jams | Kibble too large or hopper too narrow | Widen the outlet, add an agitator |
| Schedule is wrong | System time or timezone | Sync time (NTP) and set the timezone |
| Board resets when motor starts | Voltage drop | Use a separate power supply for the motor |

## Contributing

Issues and pull requests are welcome.

1. Fork the repository
2. Create a branch: `git checkout -b feature/camera-support`
3. Commit: `git commit -m "Add camera streaming"`
4. Push: `git push origin feature/camera-support`
5. Open a pull request

## Safety Notice

Do not rely on this device as the only source of food for your pet. Test it for several days while you are home, and keep a backup plan for longer absences.

## License

Distributed under the MIT License. See `LICENSE` for details.

## Author

**Alexandru Dobrovolschi** · [@ADobrovolschi](https://github.com/ADobrovolschi)
