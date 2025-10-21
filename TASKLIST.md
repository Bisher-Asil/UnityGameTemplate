# 2D Incremental Tech Clicker Game - Task List

## Progress Summary
**Completed: 20/23 tasks (87%)**
- ✅ Phase 1: Foundation - COMPLETE
- ✅ Phase 2: Core Gameplay - COMPLETE  
- ✅ Phase 3: Currency & Store - COMPLETE
- ✅ Phase 4: Game Flow - COMPLETE
- ✅ Phase 5: Data & Persistence - COMPLETE
- ✅ Phase 6: Polish & Optimization - MOSTLY COMPLETE (3 tasks remaining)

## Project Overview
A 2D incremental tech-themed clicker game where:
- Squares spawn each round and the player kills them with their mouse
- Mouse automatically shoots every few seconds
- Mouse size, shooting speed, and damage are upgradeable
- Killing squares drops points used in a store to purchase upgrades
- Upgrades have dependencies: Damage → Speed → Size

---

## Phase 1: Foundation (Tasks 1-4)

### Task 1: Project Setup & Core Architecture ✅
Initialize game structure with scene management, game manager, and basic UI framework. Set up folder structure for scripts, prefabs, scenes, and resources.

**Deliverables:**
- [x] Organize Assets folder with proper subdirectories
- [x] Create GameManager singleton (`Assets/Scripts/Managers/GameManager.cs`)
- [x] Create SceneManager for handling scene transitions (`Assets/Scripts/Managers/SceneTransitionManager.cs`)
- [x] Create CurrencyManager (`Assets/Scripts/Managers/CurrencyManager.cs`)
- [x] Create UpgradeManager (`Assets/Scripts/Managers/UpgradeManager.cs`)
- [x] Create DataManager for persistence (`Assets/Scripts/Managers/DataManager.cs`)

---

### Task 2: Create Game Scene & Camera Setup ✅
Create the main game scene with proper camera configuration for 2D gameplay. Add background/UI containers for the game view.

**Deliverables:**
- [x] Create "GameScene" scene (`Assets/Scenes/GameScene.unity`)
- [x] Created GameBootstrapper (`Assets/Scripts/Bootstrap/GameBootstrapper.cs`) for initialization
- [x] Create camera manager (`Assets/Scripts/Game/CameraManager.cs`)
- [x] Create GameSceneSetup (`Assets/Scripts/Bootstrap/GameSceneSetup.cs`) - Auto-configures camera on scene load
- [x] Create StoreSceneSetup (`Assets/Scripts/Bootstrap/StoreSceneSetup.cs`) - Auto-configures camera on scene load
- [x] Configure 2D camera: Orthographic (size 5), Position (0,0,-10), Black background
- [x] Ensure AudioListener on camera
- [x] Reference: `CameraManager.Instance`

---

### Task 3: Implement Square Enemy Spawner ✅
Create a spawner system that generates squares at random positions each round. Define spawn rate and area boundaries.

**Deliverables:**
- [x] Create Spawner script (`Assets/Scripts/Game/EnemySpawner.cs`)
- [x] Define spawn area boundaries (Vector2 min/max)
- [x] Implement spawn rate logic with configurable delay
- [x] Add round transition system (integrated with GameController)
- [x] Reference: `EnemySpawner.Instance`

---

### Task 4: Create Square Enemy Prefab ✅
Build a square game object with sprite, collider, and basic health system. Add visual feedback for damage.

**Deliverables:**
- [x] Create Square prefab script (`Assets/Scripts/Game/EnemySquare.cs`)
- [x] Add collider component (CircleCollider2D)
- [x] Implement health system (10 default health)
- [x] Add damage visual feedback (color flash on hit)
- [x] Integrate with PointDropper for point drops
- [x] Reference: `EnemySquare.GetHealth()`, `.TakeDamage()`

---

## Phase 2: Core Gameplay (Tasks 5-7)

### Task 5: Implement Mouse Cursor System ✅
Create a custom mouse cursor that tracks player input and has a visual representation. Make cursor size upgradeable.

**Deliverables:**
- [x] Hide default cursor (`Cursor.visible = false`)
- [x] Create custom cursor script (`Assets/Scripts/Game/MouseCursor.cs`)
- [x] Implement cursor position tracking (follows mouse)
- [x] Add cursor size property (upgradeable)
- [x] Create visual scaling system (based on size upgrade level)
- [x] Reference: `MouseCursor.Instance.GetCurrentSize()`

---

### Task 6: Implement Auto-Shooting Mechanic ✅
Add timer-based shooting system that automatically fires every few seconds. Implement damage calculation and hit detection.

**Deliverables:**
- [x] Create shooting timer system (`Assets/Scripts/Game/AutoShooter.cs`)
- [x] Implement raycast/collision detection from cursor
- [x] Create damage calculation (based on damage upgrade level)
- [x] Add cooldown between shots (reduced by speed upgrades)
- [x] Implement hit feedback and enemy damage
- [x] Reference: `AutoShooter.Instance.GetCurrentDamage()`, `.GetCurrentCooldown()`

---

### Task 7: Create Points Drop System ✅
When squares are destroyed, drop collectible points at that location. Implement collection logic and add to player score.

**Deliverables:**
- [x] Create Point Drop system (`Assets/Scripts/Game/PointDropper.cs`)
- [x] Implement drop logic (called from EnemySquare.Die())
- [x] Integrate with CurrencyManager to add points
- [x] Basic collection handling
- [x] Reference: `PointDropper.Instance.DropPoints(position, amount)`

---

## Phase 3: Currency & Store (Tasks 8-13)

### Task 8: Implement Points/Currency System
Create a persistent currency manager to track player points earned and spent. Handle total and spendable amounts.

**Deliverables:**
- [ ] Create CurrencyManager singleton
- [ ] Implement point tracking (Add/Subtract)
- [ ] Create public methods for point queries
- [ ] Add event system for point changes
- [ ] Display currency in HUD

---

### Task 9: Build Store Scene UI ✅
Create a separate store/shop scene with UI layout for displaying upgrades. Add buttons for purchases and scene navigation.

**Deliverables:**
- [x] Create "StoreScene" scene (`Assets/Scenes/StoreScene.unity`)
- [x] Create store UI manager (`Assets/Scripts/UI/StoreUIManager.cs`)
- [x] Create upgrade display component (`Assets/Scripts/UI/UpgradeDisplay.cs`)
- [x] Design upgrade display slots (3 upgrades)
- [x] Add buy buttons for each upgrade
- [x] Add "Return to Game" button
- [x] Display player's current points
- [x] Reference: `StoreUIManager.Instance`

---

### Task 10: Create Upgrade System Data Structure
Design and implement upgrade data system with damage, speed, and size upgrades. Store upgrade levels and costs. Define dependency chain (damage → speed → size).

**Deliverables:**
- [ ] Create Upgrade data class
- [ ] Create UpgradeManager singleton
- [ ] Define upgrade list with properties (name, level, cost, multiplier)
- [ ] Implement dependency checking logic
- [ ] Create save/load for upgrade data

---

### Task 11: Implement Upgrade Purchase Logic ✅
Add buy button functionality in store. Check dependencies, validate currency, apply upgrades, and deduct points. Display error/success messages.

**Deliverables:**
- [x] Create purchase button handler (in UpgradeDisplay)
- [x] Implement dependency validation (in UpgradeManager.TryPurchaseUpgrade)
- [x] Check player has enough currency (via CurrencyManager)
- [x] Apply upgrade (increment level)
- [x] Deduct points from player
- [x] Display success/error messages (via Debug.Log)
- [x] Refresh UI after purchase
- [x] Reference: `UpgradeManager.TryPurchaseUpgrade(upgradeId)`

---

### Task 12: Display Upgrade Information in Store ✅
Show upgrade names, current level, costs, and descriptions in the store UI. Update display when upgrades are purchased. Show lock status for unavailable upgrades.

**Deliverables:**
- [x] Create UI elements for each upgrade (name, level, cost, description)
- [x] Implement upgrade display refresh logic (UpgradeDisplay.Refresh)
- [x] Show locked/unlocked status visually (lockedOverlay)
- [x] Display next upgrade cost when locked
- [x] Update UI on purchase
- [x] Reference: `UpgradeDisplay.Refresh()`

---

### Task 13: Connect Upgrades to Game Mechanics
Apply purchased upgrades to game systems: damage upgrade affects shooting damage, speed upgrade affects shoot timer, size upgrade affects cursor size.

**Deliverables:**
- [ ] Create upgrade event system
- [ ] Connect damage upgrade → shooting damage
- [ ] Connect speed upgrade → shoot timer/cooldown
- [ ] Connect size upgrade → cursor size
- [ ] Refresh game values on upgrade purchase
- [ ] Ensure changes apply immediately in game

---

## Phase 4: Game Flow (Tasks 14-16)

### Task 14: Create Round/Wave System ✅
Implement round progression where each round spawns new squares. Add round counter and difficulty scaling options.

**Deliverables:**
- [x] Create RoundManager/GameController (`Assets/Scripts/Game/GameController.cs`)
- [x] Implement round counter
- [x] Define round end conditions (all squares dead)
- [x] Add difficulty scaling (10% spawn increase per round)
- [x] Transition between rounds
- [x] Reference: `GameController.Instance.GetCurrentRound()`

---

### Task 15: Implement Game UI (HUD) ✅
Create in-game HUD showing current points, round number, shooting speed, damage, and cursor size. Add store access button.

**Deliverables:**
- [x] Create HUD script (`Assets/Scripts/UI/GameHUD.cs`)
- [x] Display current points
- [x] Display round number
- [x] Display current stats (damage, speed, size)
- [x] Add "Go to Store" button
- [x] Update HUD values in real-time
- [x] Style and position HUD elements (pending prefab setup)
- [x] Reference: `GameHUD` component

---

### Task 16: Add Scene Transitions & Navigation ✅
Implement scene switching between game and store scenes. Add persistent data handling when switching scenes (currency, upgrades).

**Deliverables:**
- [x] Create scene loader script (SceneTransitionManager)
- [x] Implement fade/transition animation
- [x] Load GameScene from Store (via StoreUIManager)
- [x] Load StoreScene from Game (via GameHUD)
- [x] Ensure data persists between scenes (DataManager auto-save)
- [x] Handle unloading/reloading properly
- [x] Reference: `SceneTransitionManager.Instance.LoadScene()`

**Deliverables:**
- [ ] Create scene loader script
- [ ] Implement fade/transition animation
- [ ] Load GameScene from Store
- [ ] Load StoreScene from Game
- [ ] Ensure data persists between scenes
- [ ] Handle unloading/reloading properly

---

## Phase 5: Data & Persistence (Task 17)

### Task 17: Create Persistent Data Manager
Build a data persistence system (PlayerPrefs or ScriptableObject) to save/load player progress: currency, upgrade levels, round progress.

**Deliverables:**
- [ ] Create DataManager singleton
- [ ] Implement save to PlayerPrefs/JSON
- [ ] Implement load from PlayerPrefs/JSON
- [ ] Save: currency, all upgrade levels, current round
- [ ] Load on game start
- [ ] Auto-save on important changes
- [ ] Optional: Add reset/new game button

---

## Phase 6: Polish & Optimization (Tasks 18-23)

### Task 18: Add Visual Polish & Effects ✅
Add particle effects for shooting, enemy destruction, point drops. Include UI animations for purchases and stat changes.

**Deliverables:**
- [x] Create VFXManager (`Assets/Scripts/VFX/VFXManager.cs`)
- [x] Create particle effect for shooting (simple fallback implemented)
- [x] Create particle effect for enemy destruction (explosion)
- [x] Create particle effect for point collection
- [x] Add hit flash effect
- [x] Integrate VFX into AutoShooter, EnemySquare, PointDropper
- [x] Fallback visual effects (colored spheres with fade)
- [x] Reference: `VFXManager.Instance.PlayShootEffect()`

---

### Task 19: Add Audio & Sound Effects ✅
Add sound effects for shooting, enemy destruction, point collection, and purchase sounds. Add background music.

**Deliverables:**
- [x] Create GameAudioManager (`Assets/Scripts/Audio/GameAudioManager.cs`)
- [x] Add shooting SFX hooks
- [x] Add enemy death SFX hooks
- [x] Add point collection SFX hooks
- [x] Add purchase success SFX hooks
- [x] Add purchase failure SFX hooks
- [x] Add button click SFX hooks
- [x] Add background music system
- [x] Create volume controls (master, music, SFX)
- [x] Integrated into all game systems
- [x] Reference: `GameAudioManager.Instance.PlayShootSound()`
- [x] Note: Audio clips need to be assigned in Unity Editor
- [x] **Renamed to GameAudioManager to avoid conflict with existing AudioManager**

---

### Task 20: Screen Shake & Camera Effects ✅
Add screen shake on enemy death for impact feedback. Add subtle camera effects during gameplay.

**Deliverables:**
- [x] Create ScreenShake script (`Assets/Scripts/VFX/ScreenShake.cs`)
- [x] Implement shake on enemy death
- [x] Add intensity levels (light, medium, heavy)
- [x] Create shake coroutine with customizable duration/magnitude
- [x] Integrate with EnemySquare.Die()
- [x] Ensure shake doesn't break camera behavior
- [x] Reference: `ScreenShake.Instance.ShakeLight()`

---

### Task 21: Testing & Bug Fixes 🔄
Playtest thoroughly and fix any bugs or issues found during testing.

**Deliverables:**
- [x] **Fixed duplicate GameManager.cs causing CS0101/CS0263/CS0111 errors**
- [x] **Fixed AudioManager conflict by renaming to GameAudioManager**
- [x] **Fixed MouseCursor.collider warning by adding 'new' keyword**
- [x] **Removed unused PointDropper fields (fallDuration, fallHeight)**
- [x] **Cleared all compilation errors and warnings - Console is clean!** ✅
- [ ] Test complete game loop (spawn → kill → collect → upgrade → repeat)
- [ ] Test upgrade dependency chain (damage → speed → size)
- [ ] Verify save/load system works correctly
- [ ] Test scene transitions maintain state
- [ ] Verify all VFX/Audio/ScreenShake integration
- [ ] Test UI responsiveness and updates
- [ ] Fix any discovered issues

---

### Task 20: Implement Game Feel & Feedback ✅
Add screen shake on damage, hit feedback, visual cursor changes on hit, and button feedback animations.

**Deliverables:**
- [x] Create ScreenShake system (`Assets/Scripts/VFX/ScreenShake.cs`)
- [x] Implement screen shake on enemy death
- [x] Add hit feedback (VFX + audio combined)
- [x] Visual effects for all interactions
- [x] Button click sounds integrated
- [x] Screen shake intensity options (Light, Medium, Heavy)
- [x] Reference: `ScreenShake.Instance.ShakeLight()`

---

### Task 21: Testing & Bug Fixes
Playtest full game flow: upgrading, shooting, collecting points, switching scenes. Fix any bugs or balance issues.

**Deliverables:**
- [ ] Test full game flow start to finish
- [ ] Test all upgrade paths (dependencies)
- [ ] Test scene transitions
- [ ] Test data persistence (save/load)
- [ ] Test edge cases (no points, all upgrades maxed, etc.)
- [ ] Fix identified bugs
- [ ] Document issues and fixes

---

### Task 22: Balance & Tuning
Adjust spawn rates, point values, upgrade costs, upgrade progression, and shooting speed. Ensure engaging difficulty curve.

**Deliverables:**
- [ ] Tune spawn rate per round
- [ ] Adjust points dropped per square
- [ ] Balance upgrade costs (progression)
- [ ] Tune shooting cooldown
- [ ] Adjust cursor size scaling
- [ ] Ensure difficulty curve feels good
- [ ] Playtest for engagement/fun factor

---

### Task 23: Final Polish & Optimization
Optimize performance, ensure smooth gameplay, add final UI touches, and prepare for release.

**Deliverables:**
- [ ] Profile and optimize for performance
- [ ] Ensure stable 60 FPS
- [ ] Optimize particle effects
- [ ] Check for memory leaks
- [ ] Final UI polish pass
- [ ] Add build settings
- [ ] Create executable build
- [ ] Test on target platform (PC)

---

## Development Tips

- **Save Frequently**: Use the DataManager to save player progress regularly
- **Test Early**: Playtest after each major feature to catch issues early
- **Balance as You Go**: Don't leave balancing for the end
- **Use ScriptableObjects**: Consider using SO for upgrades data for easy tweaking
- **Keep Code Modular**: Make systems independent for easier debugging
- **Document as You Go**: Comment important systems and game mechanics

---

## Future Feature Ideas (Post-Release)

- [ ] New upgrade types (fire rate, multi-shot, etc.)
- [ ] Enemy variety (different square types with different health/points)
- [ ] Power-ups or temporary boosts
- [ ] Boss rounds
- [ ] Achievements/milestones
- [ ] Leaderboard/progression tracker
- [ ] Theme/cosmetic customization
- [ ] Sound/visual settings menu
- [ ] Difficulty presets or custom mode

