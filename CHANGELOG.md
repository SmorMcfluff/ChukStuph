# Changelog:
## 1.0.0
- Initial release
## 1.0.1
- Changed the context menu text of `TextedAudioSO`
## 1.0.2
- Serialized deadzone field of `InputReactionValueSO`
## 1.0.3
- Made `InputReactionButtonSO.OnHeld` be invoked instantly, not having to wait for the repeat timer on the first tick
## 1.0.4
- Fixed a major bug where `OnHeld` would trigger every frame
## 2.0.0
- Simplified `InputReactionButtonSO`
    - `OnHeld` fires every frame while the button is held, much like `Input.GetKey`
    - Removed hold / repeat timing logic from button inputs
    - Removed `Performed event`
- Added a reusable `GroundCheck` in a new `ChukStuph.Gameplay` namespace
    - Can be toggled between 2D and 3D
    - Uses a box check
    - Should be placed on an empty GameObject to control the center position of the box
    - Should be used by calling `GroundCheck.IsGrounded()`
## 2.0.1
- Added `.meta` files I forgot to generate in 2.0.0
- Changed the version number to 2.0.1, which accidentally was marked as 1.0.5 in the last release
## 2.1.0
- Added a `LayerMask layerToCheck` parameter to `GroundCheck.IsGrounded`
    - Defaults to the assigned groundLayer, but can be used to 
    - Example: Use a single GroundCheck component to check for jumpable ground and separately for goomba stomping
## 2.1.1
- Added a semicolon LUL