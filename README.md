<h1 align="center">
<a href="https://github.com/jmoubry/ClickToPop/releases/latest/download/ClickToPop.dll">
    <img align="left" alt="Icon" height="90" src="Icon.png?v=2">
    <img align="right" alt="Download" height="75" src="https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/BloonsTD6%20Mod%20Helper/Resources/DownloadBtn.png">
</a>
Click To Pop
</h1>


Click bloons to pop them.

## What it does

- Left-click near a bloon during gameplay to instantly pop the closest one.
- Uses a click radius so you do not need pixel-perfect aim.

## Compatibility

- BTD6 version: `52.0`
- Mod version: `1.0.0`
- Built with BTD6 Mod Helper / MelonLoader conventions.

## How to use

1. Start a game in BTD6 with the mod loaded.
2. Left-click near any bloon.
3. The nearest bloon within range is popped.

## Current behavior details

- Click detection uses screen-space distance from your mouse to each bloon.
- Current click radius: `140` pixels.
- If no bloon is close enough, nothing is popped.

## Development notes

- Main logic: `ClickToPop.cs`
- Metadata: `ModHelperData.cs`
- Icon asset: `Icon.png`

[![Requires BTD6 Mod Helper](https://raw.githubusercontent.com/gurrenm3/BTD-Mod-Helper/master/banner.png)](https://github.com/gurrenm3/BTD-Mod-Helper#readme)
