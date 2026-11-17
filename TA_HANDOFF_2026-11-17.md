# TA 跨对话交接卡 — 2026-11-17（当前有效）

> 仓库：`kitterr/URP_lesson`；唯一课程记录分支：`study/urp-ta-course-memory-2026`；main 不修改。
> **当前主线已转为 C# 基础与 OOP 补强**。以 [`CSHARP_FOUNDATION_BRIDGE_PLAN.md`](CSHARP_FOUNDATION_BRIDGE_PLAN.md) 为计划依据。旧 GPU_LOG/TA_HANDOFF 的“下一节 F3 / D8”均属过期历史。

## 晚间更新后的唯一下一课：CSharp-03

**ref / out / in、方法签名及陌生 Unity API 阅读。**

用户在 GPU-05-B 直言当前无法看懂 Render Graph C# 示例，尤其是 API 参数类型、构造函数、对象、继承及 override。不要再立即展开 Render Graph 实现，先通过小案例建立 C# 阅读和独立编程能力。

已在晚间使用简短 ZoomPass 类完成 CSharp-01 的基础代码检查；用户自主将无参 ResetScale 写对。CSharp-02 的值/引用类型与参数默认按值传递，经用户解释 Replace(a) 的结果，概念通过。下一节不重复这些基础题，转向 ref/out/in、方法签名与 API 读法。**`Create()` 是重写的方法，不是构造函数；构造函数不能 override。**

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
5. CSharp-01 仅**简单代码静态验收通过**，不是 Unity 实机运行通过；CSharp-02 **核心概念理解确认**，独立 ref/out/in 和 Unity 运行未验收；GPU-05-B 未通过。

## 新对话恢复口令

“请读取 `kitterr/URP_lesson` 的 `study/urp-ta-course-memory-2026` 分支中的 `COURSE_MEMORY_2026.md`、`TA_TEACHING_RULES.md`、`TA_KNOWLEDGE_LEDGER.md`、`CSHARP_FOUNDATION_BRIDGE_PLAN.md`、`GPU_LOG_2026-11-17.md`、`TA_HANDOFF_2026-11-17.md`。恢复当前 C# 补强主线，按每周约 20 小时预算继续 CSharp-03：ref/out/in、方法签名、陌生 API 读法；CSharp-01 简单代码静态通过，CSharp-02 概念通过；剩余 C# 学习初步预算约 20–35 小时、约 2–3 周（每周约 12–16 小时分配 C#，需实际验收）；GPU-05-B 暂停，别直接丢完整 Renderer Feature。”


## 2026-11-17 晚间时间估计与证据边界

- 约 20:34–21:34 的**对话时间窗口**内推进 CSharp-01 静态代码与 CSharp-02 概念；这不是计时器记录的纯学习时间。不要据此认定用户 C# 已全面提速。
- CSharp-01：ResetScale 初版参数设计不符要求，后来自行修正无参方法；Start 最终只对 b 调用 Reset，不是两者，Unity 实机运行没有确认。
- CSharp-02：能够独立解释普通引用类型参数 `pass = new ZoomPass()` 不影响外部 `a`；纠正堆/栈简化，ref/out/in 尚待系统实践。
- **剩余 C# 桥接估算：约 20–35 有效小时，日历约 2–3 周（若每周投入 C# 12–16 小时）**。目标是恢复 Unity TA/URP API 自主开发能力，不是掌握 C# 的一切；到 CSharp-03 后依据独立代码的错误率与实践时间复核估算。
