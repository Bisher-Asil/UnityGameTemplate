# Camera Setup Documentation

## Overview
The game uses 2D orthographic cameras configured for optimal 2D gameplay. Both GameScene and StoreScene have automatic camera setup.

## Camera Configuration

### Standard Settings
- **Projection**: Orthographic
- **Size**: 5 (10 units vertical visible)
- **Position**: (0, 0, -10)
- **Near Clip**: 0.3
- **Far Clip**: 1000
- **Background**: Black (0, 0, 0, 1)
- **Clear Flags**: Solid Color

### Scene-Specific Setup

#### GameScene Camera
- Auto-configured by `GameSceneSetup.cs`
- Follows standard settings above
- Includes AudioListener for sound playback
- Positioned to view gameplay area (-10 to 10 on X, -6 to 6 on Y)

#### StoreScene Camera
- Auto-configured by `StoreSceneSetup.cs`
- Same settings as GameScene
- Centered on store UI elements

## Coordinate System

### World Space
```
         (0, 5)
           ↑
           │
(-10, 0) ←─┼─→ (10, 0)
           │
           ↓
         (0, -5)

Camera is at (0, 0, -10)
Looking at positive Z direction
```

### Visible Area
- **Width**: 20 units (-10 to +10 on X-axis)
- **Height**: 10 units (-5 to +5 on Y-axis)
- **Aspect Ratio**: 2:1 (assuming standard 16:9 display)

## Scripts

### CameraManager.cs
```csharp
public class CameraManager : MonoBehaviour
{
    public void SetOrthographicSize(float size);
    public float GetOrthographicSize();
}
```

### GameSceneSetup.cs
```csharp
// Runs on scene load
// Automatically configures main camera for GameScene
// Creates camera if none exists
```

### StoreSceneSetup.cs
```csharp
// Runs on scene load
// Automatically configures main camera for StoreScene
// Creates camera if none exists
```

## Positioning Guidelines

### Enemy Squares
- Spawn in area: X(-8 to 8), Y(-4 to 4)
- Size: 0.5 to 1.0 units
- Visible in main play area

### Mouse Cursor
- Tracks mouse position in world space
- Scales from 0.5 to 1.5 units
- Center of screen is approximately (0, 0)

### UI Elements
- Store Scene centers on (0, 0)
- HUD elements positioned at screen edges
- Use screen-space overlay for UI canvas

## Screen Coordinates to World Coordinates

```csharp
// Convert screen position to world position
Vector3 mouseScreenPos = Input.mousePosition;
Vector3 worldPos = camera.ScreenToWorldPoint(mouseScreenPos);

// Example: Mouse at center screen (960, 540)
// → World position approximately (0, 0, 0)
```

## Performance Considerations

- Single camera per scene
- Orthographic rendering is more efficient than perspective
- No complex culling needed for 2D gameplay
- AudioListener on camera for spatial audio

## Future Camera Features (Optional)

- Camera shake on hits/explosions
- Zoom effects on round transitions
- Pan animations between scenes
- Parallax background if implemented

## Troubleshooting

### Camera Not Visible
- Ensure GameObject has `Camera` component
- Check position: should be at (0, 0, -10)
- Verify clear flags are set to "Solid Color"
- Check background color is not black on black

### Objects Appearing Outside Screen
- Verify object world position is within visible area
- For enemies: should be in X(-10 to 10), Y(-5 to 5)
- Check object scale (might be too large)

### AudioListener Issues
- Only one AudioListener allowed per scene
- Camera should have AudioListener component
- Error: "There can only be one AudioListener in a scene"
- Solution: Ensure only camera has AudioListener

## Scene Setup Checklist

GameScene:
- [ ] Main Camera at (0, 0, -10)
- [ ] Camera set to Orthographic, Size 5
- [ ] AudioListener component added
- [ ] GameSceneSetup script on a GameObject
- [ ] EnemySpawner configured

StoreScene:
- [ ] Main Camera at (0, 0, -10)
- [ ] Camera set to Orthographic, Size 5
- [ ] AudioListener component added
- [ ] StoreSceneSetup script on a GameObject
- [ ] StoreUIManager configured
