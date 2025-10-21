# 2D Incremental Clicker Game - Development Reference

## Scripts Created Summary

### Manager Scripts (Assets/Scripts/Managers/)

#### GameManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Core game initialization and state management
- **Key Methods**: 
  - `OnGameStart()` - Called when game begins
  - `OnGameEnd()` - Called when game ends
- **References**: `GameManager.Instance`

#### CurrencyManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Tracks and manages player points
- **Key Methods**:
  - `AddPoints(long amount)` - Add points to player
  - `TrySpendPoints(long amount)` - Attempt to spend points (returns bool)
  - `GetCurrentPoints()` - Get current point total
  - `SetPoints(long amount)` - Set points directly
- **Events**: `OnPointsChanged`, `OnPointsAdded`, `OnPointsSpent`
- **References**: `CurrencyManager.Instance`

#### UpgradeManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Manages upgrade system with dependencies
- **Key Methods**:
  - `TryPurchaseUpgrade(string upgradeId)` - Purchase upgrade with validation
  - `GetUpgrade(string upgradeId)` - Get upgrade data
  - `GetUpgradeLevel(string upgradeId)` - Get current upgrade level
  - `GetAllUpgrades()` - Get all upgrades dictionary
- **Upgrades**:
  - `damage` - No dependency, base cost 50
  - `speed` - Depends on damage level 1, base cost 100
  - `size` - Depends on speed level 1, base cost 150
- **Events**: `OnUpgradePurchased`, `OnUpgradesChanged`
- **References**: `UpgradeManager.Instance`

#### SceneTransitionManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Handles scene loading with fade transitions
- **Key Methods**:
  - `LoadScene(string sceneName)` - Load scene with fade animation
  - `GetCurrentSceneName()` - Get active scene name
- **References**: `SceneTransitionManager.Instance`

#### DataManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Save/load game data using PlayerPrefs
- **Key Methods**:
  - `SaveGame()` - Save all progress
  - `LoadGame()` - Load saved progress
  - `ResetGame()` - Clear all save data
- **Saves**: Points, upgrade levels
- **References**: `DataManager.Instance`

### Game Scripts (Assets/Scripts/Game/)

#### GameController.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Main game flow and round management
- **Key Methods**:
  - `StartRound()` - Begin new round
  - `EndRound()` - End current round
  - `GetCurrentRound()` - Get round number
  - `IsRoundActive()` - Check if round is active
- **Events**: `OnRoundStarted`, `OnRoundEnded`
- **Difficulty**: 10% spawn increase per round
- **References**: `GameController.Instance`

#### EnemySpawner.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Spawns enemy squares each round
- **Key Methods**:
  - `SpawnRound(int squareCount)` - Spawn squares for round
  - `GetActiveSquareCount()` - Count remaining enemies
  - `GetActiveSquares()` - Get list of active enemies
- **Config**:
  - `spawnAreaMin/Max` - Spawn boundaries
  - `spawnDelay` - Time between spawns
- **References**: `EnemySpawner.Instance`

#### EnemySquare.cs
- **Type**: MonoBehaviour (Prefab component)
- **Purpose**: Enemy square that players shoot
- **Key Methods**:
  - `TakeDamage(float damage)` - Damage the square
  - `GetHealth()` - Current health
  - `GetMaxHealth()` - Maximum health
  - `GetHealthPercent()` - Health percentage
- **Features**:
  - Flashes red on damage hit
  - Drops points on death
  - Broadcasts death event
- **References**: Via `EnemySpawner.GetActiveSquares()`

#### MouseCursor.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Custom mouse cursor system
- **Key Methods**:
  - `GetCurrentSize()` - Get cursor size
  - `UpdateCursorSize()` - Refresh size from upgrades
  - `ShowCursor()` / `HideCursor()` - Toggle visibility
- **Features**:
  - Hides default cursor
  - Follows mouse position
  - Scales with size upgrades
- **References**: `MouseCursor.Instance`

#### AutoShooter.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Automatic shooting system
- **Key Methods**:
  - `GetCurrentDamage()` - Get damage value
  - `GetCurrentCooldown()` - Get shoot cooldown
  - `UpdateShootStats()` - Recalculate from upgrades
- **Features**:
  - Auto-shoots on timer
  - Raycasts from cursor position
  - Damage scales with upgrades
  - Cooldown reduces with speed upgrades
- **Events**: `OnShot`
- **References**: `AutoShooter.Instance`

#### PointDropper.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Manages point drops from defeated enemies
- **Key Methods**:
  - `DropPoints(Vector3 position, long amount)` - Drop points at location
- **References**: `PointDropper.Instance`

### UI Scripts (Assets/Scripts/UI/)

#### GameHUD.cs
- **Type**: MonoBehaviour
- **Purpose**: In-game HUD display
- **Features**:
  - Shows current points
  - Shows round number
  - Shows damage stat
  - Shows fire rate stat
  - Shows cursor size stat
  - "Go to Store" button
- **Real-time Updates**: All stats update continuously

#### StoreUIManager.cs
- **Type**: Singleton MonoBehaviour
- **Purpose**: Manage store scene UI
- **Key Methods**:
  - `RefreshUpgradeDisplays()` - Update all upgrade displays
- **Features**:
  - Displays player points
  - Creates upgrade display slots
  - Save/load on scene transitions
  - Return to game button

#### UpgradeDisplay.cs
- **Type**: MonoBehaviour (Prefab component)
- **Purpose**: Display single upgrade in store
- **Key Methods**:
  - `Initialize(Upgrade upgrade)` - Setup display
  - `Refresh()` - Update display state
- **Features**:
  - Shows upgrade name and description
  - Shows current level
  - Shows cost
  - Shows locked/unlocked status
  - Buy button with validation

#### CameraManager.cs
- **Type**: MonoBehaviour
- **Purpose**: Centralized camera configuration and management
- **Key Methods**:
  - `SetOrthographicSize(float size)` - Change camera zoom
  - `GetOrthographicSize()` - Get current zoom level
- **Features**:
  - Automatic setup on Start()
  - 2D orthographic configuration
  - Black background

### Bootstrap Scripts (Assets/Scripts/Bootstrap/)

#### GameBootstrapper.cs
- **Type**: MonoBehaviour
- **Purpose**: Initialize game on startup
- **Features**:
  - Verifies all manager singletons exist
  - Loads saved game data

#### GameSceneSetup.cs
- **Type**: MonoBehaviour
- **Purpose**: Configures GameScene camera on load
- **Features**:
  - Auto-creates camera if missing
  - Sets orthographic (size 5)
  - Positions at (0, 0, -10)
  - Adds AudioListener

#### StoreSceneSetup.cs
- **Type**: MonoBehaviour
- **Purpose**: Configures StoreScene camera on load
- **Features**:
  - Auto-creates camera if missing
  - Sets orthographic (size 5)
  - Positions at (0, 0, -10)
  - Adds AudioListener

## Upgrade System

### Dependency Chain
```
Damage (No Dependency)
  ↓ (Required for Speed)
Speed (Requires Damage Lv 1)
  ↓ (Required for Size)
Size (Requires Speed Lv 1)
```

### Upgrade Data Structure
```csharp
public class Upgrade
{
    public string id;                           // "damage", "speed", "size"
    public string displayName;                  // "Weapon Damage", "Fire Rate", "Cursor Size"
    public string description;                  // User-friendly description
    public int currentLevel;                    // Current upgrade level
    public long baseCost;                       // Base cost at level 0
    public float costMultiplier;                // Cost multiplier per level
    public string dependsOnUpgradeId;          // null or upgrade id
    public int dependencyRequiredLevel;         // Required dependency level
}
```

## Scenes

### GameScene (Assets/Scenes/GameScene.unity)
- Main gameplay loop
- Player shoots squares, collects points
- Access store button in HUD

### StoreScene (Assets/Scenes/StoreScene.unity)
- Purchase upgrades with earned points
- View upgrade information
- Return to game button

## Data Persistence

### PlayerPrefs Keys
- `player_points` - Total points (stored as string)
- `upgrade_level_{upgradeId}` - Level for each upgrade

### Save Flow
1. DataManager.SaveGame() called
2. Points saved to PlayerPrefs
3. All upgrade levels saved
4. PlayerPrefs.Save() called

### Load Flow
1. On game start, DataManager.LoadGame() called
2. Points restored to CurrencyManager
3. Upgrade levels restored to UpgradeManager
4. GameBootstrapper triggers load

## Event System Overview

### CurrencyManager Events
- `OnPointsChanged(long newPoints)` - Points total changed
- `OnPointsAdded(long amount)` - Points added
- `OnPointsSpent(long amount)` - Points spent

### UpgradeManager Events
- `OnUpgradePurchased(string upgradeId)` - Upgrade purchased
- `OnUpgradesChanged` - Any upgrade changed

### GameController Events
- `OnRoundStarted(int roundNumber)` - Round began
- `OnRoundEnded(int roundNumber)` - Round ended

### AutoShooter Events
- `OnShot` - Shot fired

## Key Integration Points

### Upgrade → Game Mechanic Flow
1. Player purchases upgrade in store
2. UpgradeManager.TryPurchaseUpgrade() succeeds
3. OnUpgradePurchased event fires
4. Relevant system (AutoShooter, MouseCursor) updates
5. Example: "damage" upgrade → AutoShooter.UpdateShootStats() → Damage recalculated

### Point Collection Flow
1. EnemySquare health reaches 0
2. EnemySquare.Die() called
3. PointDropper.DropPoints() invoked
4. CurrencyManager.AddPoints() adds points
5. OnPointsChanged event fires
6. GameHUD updates display

### Round Progression Flow
1. GameController.StartRound() called
2. EnemySpawner.SpawnRound() begins spawning
3. Player destroys squares
4. EnemySpawner detects all squares destroyed
5. GameController.EndRound() called
6. Difficulty increases (10% more spawns)
7. Next round starts after delay

## Testing Checklist

- [ ] Managers initialize correctly on startup
- [ ] Currency adds/subtracts properly
- [ ] Upgrade dependencies enforce correctly
- [ ] Damage upgrade increases shooting damage
- [ ] Speed upgrade decreases cooldown
- [ ] Size upgrade increases cursor size
- [ ] Points drop on square death
- [ ] Rounds transition smoothly
- [ ] Scene transitions work
- [ ] Save/load preserves data
- [ ] HUD updates in real-time
- [ ] Store displays locked upgrades correctly

## Next Steps

1. **Create 3D Models/Sprites** - Square and cursor visuals
2. **Set up UI Prefabs** - HUD and Store layouts
3. **Configure Scenes** - Add GameObjects to both scenes
4. **Add Audio** - Sound effects and music
5. **Polish & Effects** - Particles, animations
6. **Balance** - Adjust costs and progression
7. **Test** - Full playthrough and bug fixes
