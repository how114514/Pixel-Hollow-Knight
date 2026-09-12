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
