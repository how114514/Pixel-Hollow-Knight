# Unity 项目功能测试报告

**测试日期**：2026-09-12（复测更新：2026-09-12 22:10）
**Unity 版本**：6000.3.18f1 (5ebeb53e4c07) — Windows 11 Pro 10.0.26200
**测试方式**：Unity Editor 编译检查 + 资源/场景/Animator 静态完整性扫描 + 批处理模式 Play Mode 自动化（多次尝试，未能进入）
**测试范围**：只读。未 Build、未修改任何 C# / 动画 / Animator / Prefab / 场景 / 资源、未执行任何 Git 操作（本次仅新增本报告文件）

---

## 0. 复测更新摘要（22:10）

上一轮报告中发现的问题已由你修复，本轮**重新扫描当前工程**确认如下：

| 上一轮问题 | 当前状态 | 验证方式 |
|---|---|---|
| Player Animator Controller `healed` 状态丢失剪辑 | ✅ **已修复**：`healed` → `Assets/Animation/Player/Skill/healed.anim` | 控制器 YAML 扫描 |
| Heal 只有手柄绑定 | ✅ **已修复**：新增 `<Keyboard>/u` | `.inputactions` 解析 |
| 4 条 CS0108 警告 | ✅ **已修复**：`Player.cs:20/44`、`Enemy.cs:36/51` 均已加 `new` | 当前源码重新编译，**0 warning** |
| 全项目丢失资源引用 | ✅ **已清零**（仅剩 Unity 内置资源伪 GUID，属正常） | 全项目 GUID 扫描 |
| `New State` 空占位状态 | ➖ 按你的要求不处理 | — |
| 运行时功能（19 项） | ⚠️ **仍未验证** | 见第 5 节 |

---

## 1. 总体结果

| 项 | 数量 |
|---|---|
| 通过（静态+编译可验证项） | 13 |
| 失败（未解决的缺陷） | **0** |
| 无法验证（需要 Play Mode 的运行时项） | 19 |

**总体状态：PASS WITH ISSUES**

「PASS」指的是：编译零错误零警告、场景/预制体/Animator/音频/图层/Build Settings 的引用完整性全部通过、上一轮发现的所有资源缺陷均已修复。
「ISSUES」指的是：**移动、战斗、技能、受伤、死亡、UI 同步、Camera、GameEnd 等 19 项运行时行为本次仍未能自动验证**（不是发现异常，而是没能测到）。

---

## 2. 编译与 Console

### 2.1 编译结果（当前工程，最新一次编译）

```
Errors：0
Warnings：0
```

- Assembly-CSharp / Assembly-CSharp-Editor / 全部 Package 程序集均编译成功。
- 上一轮的 4 条 `CS0108`（`Player.animation`、`Player.audio`、`Enemy.animation`、`Enemy.audio`）已随 `new` 关键字补全而消失。

### 2.2 Console / 资源检查

| 检查项 | 结果 |
|---|---|
| Error 数量 | **0** |
| Warning 数量 | **0** |
| Missing Script | **未发现**（全项目 `.unity/.prefab/.asset/.controller` 的 `m_Script` GUID 全部可解析） |
| 资源加载错误（丢失 GUID 引用） | **0**（仅剩 Unity 内置资源伪 GUID `0000...e000...`，属正常现象） |
| 序列化错误 | 未发现 |
| Animator / Animation 错误 | **0**（上一轮的 `healed` 已修复） |

---

## 3. 功能测试

| 功能 | 状态 | 说明 |
|---|---|---|
| 场景启动 | ⚠️ | 静态验证通过：BattleScene 在 Build Settings（index 0），场景内含 Player / Enemy / Canvas / Camera / 三个 Manager / Door / StartGameManager，引用无丢失。运行时启动未验证 |
| 移动 | ⚠️ | 无法自动验证 |
| 跳跃 | ⚠️ | 无法自动验证 |
| Dash | ⚠️ | 无法自动验证 |
| 普通攻击 | ⚠️ | 无法自动验证 |
| 上劈 | ⚠️ | 无法自动验证 |
| 下劈 | ⚠️ | 无法自动验证（历史数据曾失败，见 6.2） |
| Heal | ⚠️ | 无法自动验证（`healed` 剪辑与按键绑定已修复，行为待实机确认） |
| Dive | ⚠️ | 无法自动验证 |
| Shockwave | ⚠️ | 无法自动验证 |
| Roar / UpRoar | ⚠️ | 无法自动验证 |
| 受伤 | ⚠️ | 无法自动验证 |
| 死亡 | ⚠️ | 无法自动验证 |
| Enemy | ⚠️ | 无法自动验证（Enemy 默认 inactive，由 StartGameManager/StartTrigger 触发，该结构静态正常） |
| HP UI | ⚠️ | 静态通过：HPUI.stats / animator 均已赋值，HP.controller 含 hp0~hp9 且剪辑完整 |
| Energy UI | ⚠️ | 静态通过：EnergyUI.stats / animator 均已赋值，Energy.controller 含 energy0~energy9 且剪辑完整 |
| Camera Shake | ⚠️ | 静态通过：CameraShakeManager + CinemachineImpulseSource + CinemachineBrain 均存在 |
| Screen Fader | ⚠️ | 静态通过：ScreenFader.fadeImage 已赋值 |
| Audio | ⚠️ | 静态通过：`Assets/Sound` 31 个音频资源；PlayerAudio 17 槽 + EnemyAudio 11 槽全部有值且 GUID 可解析 |
| Game End | ⚠️ | 静态通过：GameEndManager 的 door / confiner / endBounds 已赋值；BattleScene 在 Build Settings 中，`RestartScene()` 的 buildIndex 有效 |

---

## 4. 发现的问题

### Critical

无。

### Major

**4.1 批处理模式无法进入 Play Mode（阻塞了全部运行时测试）**

这是**环境/工具链限制，不是项目缺陷**：

- 现象：`Unity.exe -batchmode -projectPath <项目> -executeMethod <测试入口>` 时，Editor 启动、编译、加载场景全部正常，但在 `EditorApplication.EnterPlaymode()` 之后**主线程卡死**（CPU 持续满载，日志停在 `TrimDiskCacheJob: Current cache size 0mb`，`EditorApplication.update` 不再回调，心跳日志一条都打不出来）。
- 已排除的因素（逐项实测，现象完全一致）：
  - 关闭 Search 索引（`UserSettings/Search.settings` → `indexOnEditorStartup = false`）
  - `-nographics`、`-noUpm`、复制 / 不复制 `Library`、清空 `Library/Search`
  - 关闭 Bash 沙箱执行
  - **关闭另一个正在运行的 Unity Editor 实例**（即你 22:00 后的这次关闭）→ **仍然卡死**
  - 改用非 `-executeMethod` 路径（`[InitializeOnLoadMethod]` 自启动 + 等待 Editor 完全就绪后再 `EnterPlaymode()`）→ **仍然卡死**
- **对照实验**：同一个 `EnterPlaymode()` 探针在一个**几乎空白的最小项目**（只有 ProjectSettings/Packages，无脚本无场景）上执行，**同样卡死**。
- 结论：本机 Unity 6000.3.18f1 的 **batchmode 下无法进入 Play Mode**，与本项目的资源、脚本、场景均无关。（同一套自动化脚本在 2026-09-12 15:08 曾正常跑完，说明是环境状态变化所致。）

### Minor

**4.2 两个无剪辑的占位状态（按你的要求不处理）**
- `Door Animator Controller.controller` → `New State`（无 Motion）
- `dashCharge.controller` → `New State`（无 Motion）
- 代码中没有任何 `Play("New State")` 调用，**不影响运行**，仅登记。

### Animation / Resource

**当前无问题。** 全部控制器状态与剪辑对照结果：

| 控制器 | 状态数 | 缺失剪辑 |
|---|---|---|
| `Assets/Animation/Player/Player Animator Controller.controller` | 22 | 无（`healed` 已修复） |
| `Assets/Animation/Enemy/Enemy Animator Controller.controller` | 21 | 无 |
| `Assets/Animation/UI/HP/HP.controller` | 10 (hp0~hp9) | 无 |
| `Assets/Animation/UI/Energy/Energy.controller` | 10 (energy0~energy9) | 无 |
| `Assets/Animation/VFX/ClashVFX.controller` / `BattleCryVFX.controller` | 各 1 | 无 |
| `Assets/Animation/Item/*`（Door / EnemyShockwave / EnemySpike / PlayerGroundwave / PlayerShockwave / PlayerSonicwave / RoarVFX） | — | 无（Door 另有空占位，见 4.2） |

代码里所有 `animation.Play("xxx")` 请求的状态名**在对应控制器中全部存在**：
`idle / move / jump / fall / dash / blackDash / doubleJump / attack1 / attack2 / attackUp / attackDown / hurt / dying / dead / stunned / heal / healed / upRoar / castShockwave / diveWindup / diving / diveRecover`（Player）
`idle / move / jump / fall / slashWindup / slash / upperSlashWindup / upperSlash / diveWindup / diving / diveRecover / chargeWindup / charge / chargeRecover / castShockwave / stagger / dying / dead / roar`（Enemy）

> 对照：恢复过程 15:44 的缺口清单（`RecoveryWork/anim_gap.json`）当时还缺 35 个动画，**现在已全部补齐**。

---

## 5. 无法自动验证

```text
⚠️ 无法自动验证 — 场景启动运行时表现（Player/Enemy 是否真的出现、是否卡死）
⚠️ 无法自动验证 — 移动 / 跳跃 / 下落 / Dash 的实际状态切换
⚠️ 无法自动验证 — 普通攻击 / 上劈 / 下劈 的动画触发、HitBox 与对敌伤害
⚠️ 无法自动验证 — Skill：Shockwave / UpRoar / Dive / Heal 的触发、能量消耗与特效生成
⚠️ 无法自动验证 — 受伤（HP 扣减 / Hurt 状态 / Hit Flash / 无敌帧）
⚠️ 无法自动验证 — 死亡流程与死亡后行为
⚠️ 无法自动验证 — Enemy 生成、移动、攻击、受击、死亡
⚠️ 无法自动验证 — HP UI / Energy UI 与 Player 数据的实时同步
⚠️ 无法自动验证 — Camera Shake 实际抖动、Screen Fader 淡入淡出
⚠️ 无法自动验证 — 音效是否真的出声、攻击/受击 VFX 是否真的生成
⚠️ 无法自动验证 — GameEndManager → ScreenFader → 场景重载 的完整链路
```

**复现命令（本机必卡）：**

```bash
"D:/Unity/6000.3.18f1/Editor/Unity.exe" -batchmode \
  -projectPath <任意项目，含最小空项目> \
  -executeMethod PHKProbe.Run -logFile probe.log
# 日志最后一行固定停在：TrimDiskCacheJob: Current cache size 0mb
# 之后 EnterPlaymode() 永不返回
```

**随时可执行的自动化测试**（放在项目外的沙箱副本，**没有写进你的工程**）：
`D:\_PHK_TestRun2\Assets\Editor\PHKTest.cs`
—— 虚拟 Gamepad 驱动真实 Input System 事件，覆盖 60+ 断言：场景启动 / 组件接线 / 空引用 / Animator 状态覆盖 / 移动左右 / 跳跃 / 下落 / 冲刺 / 普通攻击 / 上劈 / 空中下劈 / Shockwave / UpRoar / Dive / Heal / 受伤 / 无敌帧 / 死亡 / 场景重载 / Enemy 激活与受击 / CameraShake / ScreenFader，并全程捕获 Console 的 Error / Warning / Exception。

要跑完 19 项运行时验证，只剩**一条路**：在 Unity Editor 里**手动按 Play**（批处理模式这条路在本机已被证明走不通）。

---

## 6. 其他证据

### 6.1 静态完整性扫描结果

| 检查项 | 结果 |
|---|---|
| 场景文件 | `Assets/Scenes/BattleScene.unity` 存在且唯一，已在 Build Settings（enabled, index 0） |
| 场景对象 | Player、Enemy、Canvas(HP/Energy/FadeImage)、Camera(Main Camera + Virtual Camera + CinemachineBrain)、CameraShakeManager、GameEndManager、StartGameManager(StartTrigger)、Door、BGM、Background、Grid/Ground |
| Missing Script 槽位 | 0 |
| 丢失资源引用 | 0 |
| 物理图层 | `Ground / PlayerBody / PlayerAttack / EnemyBody / EnemyAttack / PlayerSkill / EnemySkill` 全部存在 |
| 关键组件引用 | PlayerAbility(shockwave/sonicwave/groundwave + 三个挂点)、PlayerCombat(clashVFX)、Enemy(player/roarVFX/shockwave/groundSpike)、ScreenFader(fadeImage)、GameEndManager(door/confiner/endBounds)、HPUI(stats/animator)、EnergyUI(stats/animator) **全部已赋值** |
| 音频资源 | `Assets/Sound` 31 个音频文件，PlayerAudio 17 槽 + EnemyAudio 11 槽全部有值且可解析 |
| 输入系统 | GamePlay 动作集：Move = leftStick/WASD；Jump = buttonSouth/K；Dash = buttonEast/L；Attack = buttonWest/J；**Heal = dpad-down/U（已补齐键盘）**；Skill = buttonNorth/I；ReStart = start/R |

### 6.2 历史运行记录（**不是本次测试结果，仅供参考**）

来自项目内 `RecoveryWork/` 的恢复期记录，时间 **2026-09-12 15:08–15:32**（此后 20:14、21:50 又改过代码、场景与动画，结论已不保证适用）：

- 15:08 Editor 批处理 Play Mode 自动化：30+ 断言全部 PASS，**2 项 FAIL**：
  1. `attackType == Down while airborne` —— 空中按下劈得到 `Horizontal`；
  2. `HP restored by heal` —— 治疗未回血（`6 -> 5`）。（此项与当时缺失的 `healed.anim` 高度相关，现已修复，建议实机复测）
- 15:29 用当时构建的玩家包（`RecoveryWork/TestBuild/Recovered.exe`）复测，失败项相同。
- 组件接线、状态机初始状态（idle / none）、HP=9/9、Energy=9、isGround、三个 Manager 单例、Enemy 激活后接线、伤害与死亡状态、CameraShake、ScreenFader 等在**当时**均通过。

---

## 7. 结论

**当前工程在「静态 + 编译」层面已经完全通过，未发现任何未解决的缺陷。**

- ✅ 编译 0 Error / 0 Warning；全项目 Missing Script = 0、丢失资源引用 = 0。
- ✅ 上一轮的 3 个问题（`healed` 缺剪辑、Heal 无键盘绑定、4 条 CS0108 警告）**均已修复并复测通过**。
- ✅ 恢复期缺失的 35 个动画已全部补齐；所有代码请求的动画状态在控制器中均存在。
- ⚠️ 运行时行为（19 项）**仍未验证**，原因是本机 Unity batchmode 无法进入 Play Mode（已用最小空项目对照证明与工程无关）。

**建议人工检查的清单（只需按一次 Play）：**

1. 确认 Play Mode 能正常进入、Player 与 Enemy 正常出现、Console 无红字。
2. **治疗**：受伤后按住 Heal（手柄 Dpad-↓ 或键盘 **U**），确认 `heal`→`healed` 动画播完后能正常退出治疗状态、HP +1、消耗 3 点能量（重点验证历史失败项）。
3. **空中下劈**：先跳起、推下摇杆（或按 S/↓）再按攻击，确认判定为 `Down` 而不是 `Horizontal`（重点验证历史失败项）。
4. Enemy 流程：走到 StartTrigger 触发敌人 → 确认敌人能动、能攻击、能被砍、死亡后走 `EnemyDeadState`。
5. 走完一次「死亡 → ScreenFader 淡出 → 场景重载」链路，确认 `GameEndManager` 正常。

---

## 附：本次测试的环境清理说明

为做运行时自动化测试，在项目**外部**建了三个沙箱副本（**`D:\EngineeringDocuments\Pixel-Hollow-Knight` 内除本报告外未改动任何文件**）：

- `D:\_PHK_TestRun`（含 Library 完整副本 + 测试脚本）
- `D:\_PHK_TestRun2`（干净副本 + 已同步到最新工程 + 测试脚本 `Assets/Editor/PHKTest.cs`，可直接复用）
- `D:\_PHK_Mini`（最小对照项目）

不需要时可直接删除这三个目录。
