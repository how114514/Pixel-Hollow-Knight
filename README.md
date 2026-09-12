# Hollow Knight

2D action platformer built with Unity.

## Features

- Dual state machine (Movement + Action) character controller
- Multiple attack types (horizontal, up, down)
- # Hollow Knight

  基于 Unity 开发的 2D 动作平台游戏原型。

  ## 项目简介

  本项目主要用于练习 Unity 2D 动作游戏开发，重点实现角色状态机、战斗系统、技能系统以及敌人 AI 等功能。

  ## 主要功能

  * **双状态机角色控制**

    * Movement State Machine：移动、跳跃、下落、冲刺等
    * Action State Machine：攻击、受伤、治疗、技能、死亡等

  * **战斗系统**

    * 普通攻击
    * 上劈 / 下劈
    * 受伤与死亡
    * 敌人硬直（Stagger）
    * 玩家生命值与资源管理

  * **技能系统**

    * 冲击波
    * 上吼
    * 俯冲
    * 治疗
    * 黑冲刺及冷却机制

  * **敌人 AI**

    * 基于状态机的敌人行为
    * 移动、跳跃、攻击前决策等行为
    * 受击硬直与死亡

  * **其他系统**

    * Cinemachine 镜头与镜头震动
    * DOTween 场景过渡
    * UI 生命值与资源显示
    * 音效与动画事件

  ## 操作

  | 按键 | 功能 |
  | ---- | ---- |
  | WASD | 移动 |
  | K    | 跳跃 |
  | L    | 冲刺 |
  | J    | 攻击 |
  | I    | 技能 |
  | U    | 治疗 |

  ## 项目结构

  ```text
  Assets/Script/
  ├── Core/        # 通用状态机基础类
  ├── Player/      # 玩家控制、战斗、状态机等
  ├── Enemy/       # 敌人 AI 与相关状态
  ├── Item/        # 技能、攻击效果等
  └── Manager/     # 游戏管理、镜头、场景过渡等
  ```

  ## 技术栈

  * Unity
  * C#
  * Unity Input System
  * Cinemachine
  * DOTween
  * Animator / Animation
  * 2D Physics
  * State Machine

  ```
  
  这个版本更适合你现在拿来当**作品集 GitHub 项目介绍**：重点放在“我实现了什么”和“用了什么技术”，不会把目录写得过于复杂，也不会有明显的过度包装。
  
  如果你后面准备拿它投 Unity 实习，我还建议把 README 再加一个很短的 **“开发重点”**，专门突出你做这个项目时解决的架构问题。
  ```

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
```

# Hollow Knight

基于 Unity 开发的 2D 动作平台游戏原型。

## 项目简介

本项目主要用于练习 Unity 2D 动作游戏开发，重点实现角色状态机、战斗系统、技能系统以及敌人 AI 等功能。

## 主要功能

* **双状态机角色控制**

  * Movement State Machine：移动、跳跃、下落、冲刺等
  * Action State Machine：攻击、受伤、治疗、技能、死亡等

* **战斗系统**

  * 普通攻击
  * 上劈 / 下劈
  * 受伤与死亡
  * 敌人硬直（Stagger）
  * 玩家生命值与资源管理

* **技能系统**

  * 冲击波
  * 上吼
  * 俯冲
  * 治疗
  * 黑冲刺及冷却机制

* **敌人 AI**

  * 基于状态机的敌人行为
  * 移动、跳跃、攻击前决策等行为
  * 受击硬直与死亡

* **其他系统**

  * Cinemachine 镜头与镜头震动
  * DOTween 场景过渡
  * UI 生命值与资源显示
  * 音效与动画事件

## 操作

| 按键 | 功能 |
| ---- | ---- |
| WASD | 移动 |
| K    | 跳跃 |
| L    | 冲刺 |
| J    | 攻击 |
| I    | 技能 |
| U    | 治疗 |

## 项目结构

```text
Assets/Script/
├── Core/        # 通用状态机基础类
├── Player/      # 玩家控制、战斗、状态机等
├── Enemy/       # 敌人 AI 与相关状态
├── Item/        # 技能、攻击效果等
└── Manager/     # 游戏管理、镜头、场景过渡等
```

## 技术栈

* Unity
* C#
* Unity Input System
* Cinemachine
* DOTween
* Animator / Animation
* 2D Physics
* State Machine
