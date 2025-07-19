# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity space shooter game prototype. The project is a 2D arcade-style space shooter with enemies, power-ups, and player progression. The game includes animations, audio, post-processing effects, and is built with Unity 6000.0.39f1 and Universal Render Pipeline (URP).

## Key Commands

**Building and Running:**
- Open Unity Editor and load the project
- Build settings accessible via `File > Build Settings`
- Run game by pressing Play button in Unity Editor or build to platform
- No specific build scripts or custom commands required

**Testing:**
- No automated test framework configured in this project
- Testing is done manually through Unity Editor play mode

## Architecture Overview

**Core Game Components (Assets/Scripts/):**
- `Player.cs` - Player ship controller with movement, shooting, power-ups, and damage system
- `Enemy.cs` - Enemy behavior and movement patterns
- `SpawnManager.cs` - Handles spawning of enemies and power-ups using coroutines
- `GameManager.cs` - Game state management and scene transitions
- `UIManager.cs` - UI updates for score, lives, and game over states
- `PowerUp.cs` - Power-up collection and effects
- `Laser.cs` - Projectile behavior
- `Asteroid.cs` - Asteroid enemy behavior

**Unity Project Structure:**
- `Assets/Scenes/` - Main game scene and menu
- `Assets/Prefabs/` - Organized prefabs for enemies, power-ups, and projectiles
- `Assets/Sprites/` - Game artwork including explosion animations
- `Assets/Audio/` - Sound effects and background music
- `Assets/Animations/` - Animation controllers and clips
- `Assets/Materials/Shaders/` - Custom Galaxy shader

**Key Unity Packages:**
- Universal Render Pipeline (URP) for rendering
- Input System for player controls
- Post-processing for visual effects
- Timeline for cutscenes/animations

**Game Architecture Patterns:**
- Component-based architecture following Unity patterns
- GameObject.Find() used for manager references (could be improved with dependency injection)
- Coroutines for timed events (spawning, power-up durations)
- SerializeField for inspector configuration
- Object pooling could be implemented for better performance

**Audio System:**
- AudioSource components on game objects
- Separate audio clips for laser shots, explosions, power-ups, and background music

**Input Handling:**
- Arrow keys/WASD for movement
- Space bar for shooting
- Left Shift for speed boost
- R key to restart when game over
- Escape to quit

## Development Notes

- Unity Editor is the primary development environment
- No external build tools or package managers beyond Unity's built-in systems
- Project uses Unity's Package Manager for dependencies
- Version control via Git (files like .DS_Store should be in .gitignore)
- No automated testing framework - relies on manual testing in play mode