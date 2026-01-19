# Chuk Stuph

A Unity package containing reusable, modular code for use in game jams

## Features
- **2D Animation System** - Play sprite-based animations via `AnimatedSpriteData` ScriptableObjects. Works with both `SpriteRenderer` and UI `Image` components.
- **Timed Text for Audio** - Display subtitles, lyrics, or other timed text with `TextedAudio` assets. Automatically syncs with `AudioSource` playback
- **Input System** - Create reusable input reactions using `InputReactionSO`. Supports button, float and Vector2 inputs using the `Unity InputSettings` package, without having to write your own subscribe/unsubscribe code
- **Settings System** - Manage game settings with `SettingValueSO<T>` ScriptableObjects. Supports floats, ints, bools and strings. Can optionally save to PlayerPrefs.
- And more...

## Installation
1. Open the Unity Package Manager (Window -> Package Manager)
2. Click the plus symbol, select **"Install package from git URL..."**
3. Type in `https://github.com/SmorMcfluff/ChukStuph.git`
4. And boom goes the dynamite