# Hollow Knight

2D action platformer built with Unity.

## Features

- Dual state machine (Movement + Action) character controller
- Multiple attack types (horizontal, up, down)
- Skills: Shockwave, Up Roar, Dive, Heal
- Dash / Black Dash with cooldown
- Enemy AI with decision-based state machine
- Stagger, hurt, and death systems
- Camera shake (Cinemachine Impulse)
- Screen fade transitions (DOTween)

## Controls

| Key | Action |
|-----|--------|
| WASD | Move |
| K | Jump |
| L | Dash |
| J | Attack |
| I | Skill |
| U | Heal |

## Project Structure

```
Assets/Script/
├── Core/           Shared State & StateMachine base
├── Player/         Player character
│   ├── Core/       Player state & FSM
│   ├── Components/ Movement, Animation, Combat, Stats, Audio
│   └── States/
│       ├── Movement/  Idle, Move, Jump, Fall, Dash
│       └── Action/    Attack, Heal, Shockwave, UpRoar, Dive, Hurt, Dead
├── Enemy/          Enemy AI
│   ├── States/     Intro, Decision, MoveLoop, JumpLoop, BackJumpLoop, Stagger, Dead
│   └── Components/ Movement, Animation, Stats, Audio
├── Item/           Projectiles, VFX, Door
├── Manager/        GameEnd, CameraShake, ScreenFader
└── ScriptableObject/ Events
```
