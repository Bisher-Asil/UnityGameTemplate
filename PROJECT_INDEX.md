# 2D Incremental Clicker Game - Project Index

## 📋 Documentation Files

### Planning & Progress
- **TASKLIST.md** - Main task list with deliverables (65% complete)
- **PROGRESS_SUMMARY.md** - Session 1 completion summary
- **DEVELOPMENT_REFERENCE.md** - Complete API and architecture reference
- **PROJECT_INDEX.md** - This file

## 🎮 Game Architecture

### Core Systems
```
GameManager (Singleton)
    ├── CurrencyManager (Points tracking)
    ├── UpgradeManager (Upgrade progression)
    ├── DataManager (Save/Load)
    ├── SceneTransitionManager (Scene loading)
    └── GameController (Round management)
```

### Gameplay Systems
```
GameController
    ├── EnemySpawner
    │   └── EnemySquare (enemies)
    │       └── PointDropper (point collection)
    ├── AutoShooter
    │   ├── MouseCursor
    │   └── CurrencyManager
    └── GameHUD
```

### UI Systems
```
GameScene
    └── GameHUD
        ├── Points Display
        ├── Round Display
        ├── Stats Display
        └── Store Button

StoreScene
    └── StoreUIManager
        ├── Points Display
        ├── UpgradeDisplay[0] (Damage)
        ├── UpgradeDisplay[1] (Speed)
        ├── UpgradeDisplay[2] (Size)
        └── Return Button
```

## 📁 File Structure

```
Assets/
├── Scripts/
│   ├── Managers/
│   │   ├── GameManager.cs
│   │   ├── CurrencyManager.cs
│   │   ├── UpgradeManager.cs
│   │   ├── SceneTransitionManager.cs
│   │   └── DataManager.cs
│   ├── Game/
│   │   ├── GameController.cs
│   │   ├── EnemySpawner.cs
│   │   ├── EnemySquare.cs
│   │   ├── MouseCursor.cs
│   │   ├── AutoShooter.cs
│   │   └── PointDropper.cs
│   ├── UI/
│   │   ├── GameHUD.cs
│   │   ├── StoreUIManager.cs
│   │   └── UpgradeDisplay.cs
│   └── Bootstrap/
│       └── GameBootstrapper.cs
├── Scenes/
│   ├── GameScene.unity
│   └── StoreScene.unity
├── Prefabs/ (TO BE CREATED)
│   ├── EnemySquare.prefab
│   ├── MouseCursor.prefab
│   ├── GameHUD.prefab
│   └── StoreUI.prefab
└── Sprites/ (TO BE CREATED)
    ├── Square.png
    └── Cursor.png
```

## 🎯 Quick Reference

### Accessing Core Systems
```csharp
GameManager.Instance              // Main game controller
CurrencyManager.Instance          // Points management
UpgradeManager.Instance           // Upgrade progression
DataManager.Instance              // Save/load
SceneTransitionManager.Instance   // Scene loading

GameController.Instance           // Round management
EnemySpawner.Instance            // Enemy spawning
MouseCursor.Instance             // Cursor system
AutoShooter.Instance             // Shooting system
PointDropper.Instance            // Point drops
```

### Key Methods

**Add Points**
```csharp
CurrencyManager.Instance.AddPoints(100);
```

**Purchase Upgrade**
```csharp
bool success = UpgradeManager.Instance.TryPurchaseUpgrade("damage");
```

**Get Stats**
```csharp
float damage = AutoShooter.Instance.GetCurrentDamage();
float cooldown = AutoShooter.Instance.GetCurrentCooldown();
float size = MouseCursor.Instance.GetCurrentSize();
int round = GameController.Instance.GetCurrentRound();
long points = CurrencyManager.Instance.GetCurrentPoints();
```

**Save/Load**
```csharp
DataManager.Instance.SaveGame();
DataManager.Instance.LoadGame();
```

## 🔄 Game Flow

### Startup
1. GameBootstrapper runs Awake()
2. All managers initialize (Singleton pattern)
3. DataManager.LoadGame() restores saved progress
4. GameBootstrapper verifies all managers exist

### Gameplay Loop
1. GameController.StartRound() begins round
2. EnemySpawner.SpawnRound() creates squares
3. Each frame:
   - AutoShooter checks cooldown timer
   - When timer expires, Shoot() fires
   - Raycast detects EnemySquares
   - Squares take damage and update visuals
   - On death, PointDropper adds points
   - EnemySpawner checks if round is complete
4. Round ends when all squares are destroyed
5. GameController increases difficulty and starts new round

### Purchase Upgrade
1. Player clicks Buy button on UpgradeDisplay
2. UpgradeDisplay.OnBuyClicked() → UpgradeManager.TryPurchaseUpgrade()
3. UpgradeManager checks:
   - Does player have enough currency?
   - Is dependency met?
4. If valid:
   - Deduct points: CurrencyManager.TrySpendPoints()
   - Increment upgrade level
   - Fire: OnUpgradePurchased event
5. Event listeners update their systems:
   - AutoShooter.UpdateShootStats() if damage/speed
   - MouseCursor.UpdateCursorSize() if size
6. UI updates display

### Store Visit
1. Player clicks "Go to Store" button in HUD
2. SceneTransitionManager.LoadScene("StoreScene")
3. Fade out → Load scene → Fade in
4. StoreUIManager initializes displays
5. Player can purchase upgrades
6. Click "Return to Game" → Save and load back to GameScene

### Game End (Not Yet Implemented)
- Player can play indefinitely
- Round difficulty increases each round
- Could add win/lose conditions in future

## 📊 Upgrade System Details

### Progression Path
```
START
  ↓
Purchase Damage (Lv 1) - 50 points
  ↓
Purchase Speed (Lv 1) - 100 points (Damage must be Lv 1+)
  ↓
Purchase Size (Lv 1) - 150 points (Speed must be Lv 1+)
  ↓
Purchase any upgrade multiple times (escalating costs)
```

### Cost Formula
```
Cost = BaseCost × (CostMultiplier ^ CurrentLevel)
Example for Damage at Level 2: 50 × (1.5 ^ 2) = 112.5
```

### Stat Effects
- **Damage Lv 1**: Base 5 → 7.5 damage per shot
- **Speed Lv 1**: Base 1.0s → 0.9s cooldown
- **Size Lv 1**: Base 0.5 → 0.7 cursor size

## 🧪 Testing Checklist

### Functional Tests
- [ ] Game starts without errors
- [ ] All managers initialize correctly
- [ ] Can shoot and hit squares
- [ ] Squares take damage visually
- [ ] Points collect properly
- [ ] Round transitions work
- [ ] New round spawns more enemies
- [ ] Can navigate to store
- [ ] Can purchase upgrades
- [ ] Upgrade dependencies enforce
- [ ] Purchased upgrades affect stats
- [ ] Can return to game
- [ ] Save/load preserves progress

### Balance Tests
- [ ] Damage progression feels rewarding
- [ ] Speed upgrades noticeably faster
- [ ] Size upgrades improve hit feel
- [ ] Costs balance progression
- [ ] Difficulty curve is engaging

## 🚀 Future Enhancements

### Immediate (Phase 6 - Polish)
- Particle effects (shooting, impacts, points)
- Sound effects and music
- Screen shake and juice
- UI animations

### Medium Term
- Enemy variety (different types)
- Power-ups (temporary boosts)
- New upgrade types
- Boss battles

### Long Term
- Prestige/reset system
- Achievements
- Leaderboards
- Mobile optimization
- Different themes/skins

## 📝 Notes

- All scripts compile with 0 errors
- Event-driven architecture allows easy additions
- Singleton managers persist across scenes
- PlayerPrefs used for simple save system (could use JSON for complex saves)
- Spawn rate increases 10% per round for difficulty scaling

## 🎓 Learning Resources

### Key Patterns Used
1. **Singleton Pattern** - For persistent managers
2. **Event Pattern** - For loose coupling between systems
3. **Prefab Pattern** - For reusable components (squares, UI)
4. **State Pattern** - For round/game states

### Related Reading
- Unity Singleton Best Practices
- Event-Driven Architecture
- UI Optimization in Unity
- Save System Design

---

**Project Status**: ✅ 65% COMPLETE (15/23 tasks)
**Last Updated**: October 22, 2025
**Ready For**: Asset creation and scene configuration
