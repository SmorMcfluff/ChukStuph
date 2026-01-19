# Changelog:
## 1.0.0
- Initial release
## 1.0.1
- Changed the Context Menu text of TextedAudioSO
## 1.0.2
- Serialized deadzone field of InputReactionValueSO
## 1.0.3
- Made InputReactionButtonSO.OnHeld be invoked instantly, not having to wait for the repeat timer on the first tick
## 1.0.4
- Fixed a major bug in the last version where OnHeld would trigger every frame