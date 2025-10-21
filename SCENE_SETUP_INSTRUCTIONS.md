# Scene Setup Instructions

## ✅ GameScene - COMPLETE

The GameScene has been fully set up with:

### Camera
- **Main Camera** GameObject with:
  - Camera component (Orthographic, Size: 5, Background: Black)
  - AudioListener component
  - Position: (0, 0, -10)

### Game Objects Created:
1. **GameSceneBootstrap** - Contains GameSceneSetup component
2. **GameManagers** - Contains:
   - CurrencyManager
   - UpgradeManager
   - DataManager
   - SceneTransitionManager
3. **GameController** - Contains:
   - GameController
   - EnemySpawner
4. **PlayerSystems** - Contains:
   - MouseCursor
   - AutoShooter
   - PointDropper
5. **AudioVisualSystems** - Contains:
   - VFXManager
   - GameAudioManager
   - ScreenShake

---

## ⚠️ StoreScene - NEEDS MANUAL SETUP

Please open **StoreScene** in Unity Editor and add the following:

### 1. Create Main Camera
- GameObject → Camera
- Name: "Main Camera"
- Tag: "MainCamera"
- Add Component: Audio Listener
- Transform Position: (0, 0, -10)
- Camera Settings:
  - Projection: Orthographic
  - Size: 5
  - Clear Flags: Solid Color
  - Background: Black (#000000)

### 2. Create StoreSceneBootstrap
- Create Empty GameObject
- Name: "StoreSceneBootstrap"
- Add Component: **StoreSceneSetup** script

### 3. Create StoreManagers
- Create Empty GameObject
- Name: "StoreManagers"
- Add Components:
  - CurrencyManager
  - UpgradeManager
  - DataManager
  - SceneTransitionManager

### 4. Create StoreUIManager
- Create Empty GameObject (or UI Canvas)
- Name: "StoreUI"
- Add Component: **StoreUIManager** script
- Note: You'll need to create UI Canvas with upgrade display elements

### 5. Create AudioVisualSystems
- Create Empty GameObject
- Name: "AudioVisualSystems"
- Add Component: **GameAudioManager** script

---

## Quick Setup in Unity (Copy-Paste Friendly)

Open StoreScene, then in the menu:
1. GameObject → Camera (rename to "Main Camera")
2. GameObject → Create Empty (rename to "StoreSceneBootstrap", add StoreSceneSetup component)
3. GameObject → Create Empty (rename to "StoreManagers", add CurrencyManager, UpgradeManager, DataManager, SceneTransitionManager)
4. GameObject → Create Empty (rename to "StoreUI", add StoreUIManager component)
5. GameObject → Create Empty (rename to "AudioVisualSystems", add GameAudioManager)
6. Save scene (Ctrl+S)

---

## Next Steps After Scene Setup

Once both scenes are properly set up:
1. Create UI prefabs for:
   - Game HUD (points, round, stats, "Go to Store" button)
   - Store UI (upgrade displays, "Return to Game" button)
2. Create Enemy Square prefab with sprite and collider
3. Assign references in inspectors:
   - EnemySpawner needs enemy prefab reference
   - MouseCursor needs sprite renderer and collider references
   - StoreUIManager needs UI element references
4. Test scene transitions
5. Test full game loop

---

## Important Notes

- Both scenes must be added to Build Settings (File → Build Settings → Add Open Scenes)
- Managers use singleton patterns - they persist between scene loads via DontDestroyOnLoad
- The GameAudioManager (renamed from AudioManager to avoid conflicts) needs audio clips assigned
- VFXManager can work with procedural effects or prefab references
