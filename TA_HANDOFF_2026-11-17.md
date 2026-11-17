# TA 跨对话交接卡 — 2026-11-17（当前有效）

> 仓库：`kitterr/URP_lesson`；唯一课程记录分支：`study/urp-ta-course-memory-2026`；main 不修改。
> **当前主线已转为 C# 基础与 OOP 补强**。以 [`CSHARP_FOUNDATION_BRIDGE_PLAN.md`](CSHARP_FOUNDATION_BRIDGE_PLAN.md) 为计划依据。旧 GPU_LOG/TA_HANDOFF 的“下一节 F3 / D8”均属过期历史。

## 唯一下一课：CSharp-01

**类、类型、字段、局部变量、方法、构造函数和对象创建顺序。**

用户在 GPU-05-B 直言当前无法看懂 Render Graph C# 示例，尤其是 API 参数类型、构造函数、对象、继承及 override。不要再立即展开 Render Graph 实现，先通过小案例建立 C# 阅读和独立编程能力。

固定讲法：先展示简短可完整读懂的 `ZoomPass` 类→逐项拆关键字与声明（类型/变量/参数/返回值）→解释 `new`、构造函数与方法调用时机→少量独立编码并验收。**`Create()` 是重写的方法，不是构造函数；构造函数不能 override。**

## 课时调整

- 工作日 3 小时/天；周末 2 天合计约 5 小时；每周约 20 小时，**时间预算不变**。
- 插入 8–12 次集中 C# 课程（弹性），分类型/方法→OOP→Unity API 阅读→URP 复归四阶段。
- 原 D01–D40 / 2026-12-31 日历目标改为参考，课程验收优先；未完成项目可能顺延至 2027，不硬赶进度。
- 用户单独输入 `1` 表示继续当前教学模块；禁止把阅读参考代码等同于实践通过。

## 最新进度边界

- GPU-04-F2 Fresnel+Alpha Blend：实践通过。
- GPU-04-F3 交界发光：用户截图和 Debug 正确反馈，**实践通过**。
- GPU-04-F4 CopyDepth / Depth Prepass / Depth Priming：Frame Debugger 对照截图，**实践通过**。
- GPU-05-A URP 架构/Render Graph：**已系统教学**，不当作独立编程验收。
- GPU-05-B Renderer Feature/Render Graph ZoomPass：**示例已给但用户无法消化，暂停，未运行/未验收**。
- GPU-04-D8 性能分析：继续主动延后，保留之前其他 GPU-04 分节已通过/待确认边界。

## 实践与教学协议

1. 先讲为什么、代码在整体流程里的位置，再逐条讲 C# 新语法、新 API 的类型/来源/参数/返回值/调用时机，然后让用户独立实践。
2. 用 IDE 查找类定义与官方文档作为训练，不背 API，不堆新术语。
3. 不以简单算数题消耗时间；验收侧重阅读陌生代码、解释调用流程、独立实现和故障定位。
4. 对数学/Shader 仍要提供真实可见的公式曲线/示意图，不可只贴绘图代码。
5. 没有用户操作反馈，不把 CSharp-01 或 GPU-05-B 标记完成。

## 新对话恢复口令

“请读取 `kitterr/URP_lesson` 的 `study/urp-ta-course-memory-2026` 分支中的 `COURSE_MEMORY_2026.md`、`TA_TEACHING_RULES.md`、`TA_KNOWLEDGE_LEDGER.md`、`CSHARP_FOUNDATION_BRIDGE_PLAN.md`、`GPU_LOG_2026-11-17.md`、`TA_HANDOFF_2026-11-17.md`。恢复当前 C# 补强主线，按原定每周约 20 小时、8–12 次弹性课时开始 CSharp-01；GPU-05-B 暂停，别直接丢完整 Renderer Feature。”
