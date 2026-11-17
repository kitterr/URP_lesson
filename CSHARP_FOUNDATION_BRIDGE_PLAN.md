# Unity 6 URP TA：C# 基础补强与 GPU-05 课程重排（2026-11-17）

> 状态：**当前生效的插入式教学计划**。适用于仓库 `study/urp-ta-course-memory-2026` 分支。不修改 main、不删除原 D01–D40 长期路线。
> 原因：学员在 GPU-05-B `TA_RenderGraphZoomFeature` 案例中明确反馈：难以判断 API 参数类型、对象与类、构造函数、继承/override 和 C# 基础语法，当前直接编写复杂 Renderer Feature 会沦为复制代码。
> **原则**：先提高 C# 阅读和独立编程能力，再恢复 URP Render Graph 实践；现有 Shader/Depth/Fresnel 成果全部保留。

## 1. 为什么调整

- Shader/HLSL、Depth Buffer、Fresnel、深度交界效果已有多项概念与 Unity 实际验证；这部分基础不需要重头再学。
- C# **类型系统、对象、构造函数、方法、继承、多态、Unity API 参数、泛型/委托/Lambda** 尚未形成稳定理解。不能因为用户曾使用过 C#/Python 就认定熟练。
- GPU-05-A（URP/Render Graph 架构）已讲解但未独立验收；GPU-05-B（最小 Renderer Feature）已提供参考 C# 代码，**未见用户成功编译运行/理解 API，暂停实现，不能标记通过**。
- 先在小而完整的案例中建立语言能力，再回到原 ZoomPass，减少盲目复制、无法排错的风险。

## 2. 学习时间与原计划变更

| 项目 | 原计划 | 本次调整 |
|---|---|---|
| 工作日 | 每天约 3 小时 | **不变：每天约 3 小时**，优先用于 C# 理解与少量独立编码 |
| 周末 | 周六日合计约 5 小时（默认每天 2.5 小时） | **不变**，用于复习、简短 Unity 实践、整理问题 |
| 每周预算 | 约 20 小时 | **不变**，不额外增加负担 |
| 当期主线 | Unity 6 URP / Render Graph | **暂时转为 C# 语法 + OOP + Unity API 阅读训练** |
| 预计插入课时 | 无 | **先规划 8–12 次集中学习**，每次约 2–3 小时，按实际验收动态延长或压缩；是初步预算不是承诺 |
| 2026-12-31 | 原 D01–D40 + T01–T08 目标窗口 | **原截止目标改为参考里程碑**；不强制追赶或跳过 C# 门槛。Renderer Feature、Render Graph、毕业作品按掌握情况顺延，必要时进入 2027 |
| 2027 长期目标 | 开发真实项目效果、工具，之后专项移动端优化 | **方向不变**；先完成 C# 门槛和 URP 基础，再启动独立项目 |

- 预计 8–12 次课属于课程重排，不是把这 8–12 次塞进原日历、要求加班补课。
- 只有用户实际答题、代码审查或运行证明通过，才推进模块；时间到但未通过则继续。
- 保留原 D01–D40、T01–T08 和 MSR 等全部历史规划与已通过证据，不追溯撤销成果。

## 3. 四阶段课程与建议课次

| 阶段 | 预计节次 | 主要内容 | 通关证据 |
|---|---|---|---|
| CSharp-01～03：类型、变量、方法 | 2–3 次 | 类型与标识符；字段/局部变量/参数/返回值；class、struct 的基础区别；方法签名、作用域、访问修饰符；`static` 的基本用途 | 给陌生 C# 片段能指出类型、变量、方法、参数、返回值；独立写一个简单工具类 |
| CSharp-04～06：对象与 OOP | 2–3 次 | `new`、对象引用、构造函数、字段初始化、`this`、属性、继承、`base`、`virtual/override`、接口概念；**区分构造函数与 Unity 回调 `Create()`** | 独立编写父类/子类与构造函数；解释何时调用 override 方法；能分析引用关系 |
| CSharp-07～09：Unity API 阅读 | 2–3 次 | MonoBehaviour 生命周期、组件获取、API 文档和 IDE 查找定义；泛型 `<T>`、`ref/out`、委托/Lambda、`using` / IDisposable；只讲当前案例需要的深度 | 给未见过的 API 能从声明和文档辨认输入输出、所属类/命名空间、调用时机；改写小型 Unity 示例并运行 |
| CSharp-10～12：回归 URP | 2–3 次 | 从简化 C# Pass 模型恢复 `ScriptableRendererFeature`、`ScriptableRenderPass`、`Create`、`AddRenderPasses`、`RecordRenderGraph`、`PassData`、`TextureHandle`、`SetRenderFunc`；独立完成最小 Pass | 能讲清各个类/对象、构造函数/override/API 参数及记录与执行阶段；**由用户自己编写、Unity 编译运行并用 Render Graph Viewer 验证** |

节次是**弹性模块编号**，不是必须一节一天，也不是完整内容必须恰好 12 节。第一阶段开始点固定为 **CSharp-01：类、类型、字段、方法、构造函数**，先查明真实薄弱处再决定节奏。

## 4. 每次课程的固定教学流程

1. **用一个真实代码片段建立问题**：优先使用用户刚才看不懂的 `ZoomPass` 缩减版，不一次性贴几十个陌生 API。
2. **拆声明和执行**：识别关键字、类型、变量名、参数及返回类型；明确区分字段、局部变量、方法、构造函数、实例；说明对象何时创建、何时调用方法、何时释放。
3. **解释 API**：先指出声明位于哪个类/命名空间；说明参数类型、返回类型、调用者、调用阶段、具体作用与限制，鼓励 IDE 跳转定义/查阅文档，不强背 API。
4. **少量独立编码**：完整概念教学后，让学员独立写小案例、解释一次调用流程或在 Unity 中验证；根据实际反馈纠错。
5. **分项验收后再推进**：不使用大量简单运算填空、低价值反复测试。每节更新证据类型与唯一下一步。

**特殊要求**：不能把 `public override void Create()` 解释为构造函数。`ZoomPass pass = new ZoomPass()` 的 `new ZoomPass()` 会调用构造函数；`Create()` 是框架生命周期中被调用的重写方法。构造函数不能被 `override`。

## 5. 重新进入 GPU-05-B 的门槛

必须能独立解释这一声明的每个要素：

```csharp
public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
```

并能分别回答：
- `RecordRenderGraph` 是方法，不是构造函数；`override` 适用的前提是什么？
- `RenderGraph`、`ContextContainer` 是类型，`renderGraph`、`frameData` 是参数变量。
- `new ZoomPass()` 是对象创建，`Create()` 和 `AddRenderPasses()` 是不同的生命周期方法。
- `PassData` 是什么自定义类型，泛型 `<PassData>`、`out var`、Lambda、`using` 是什么；至少能查文档解释当前示例为何这么用。
- 录制资源读写声明与执行 GPU 绘制的关系；能在 Unity 中完成一份最小实现并使用 Render Graph Viewer 验证。

不需要在此阶段完全精通 C# 所有高级特性，但需要具备独立阅读、修改和排错能力。

## 6. 当前学习进度与下一步

- GPU-04-F3：交界发光原理 + 最终视觉 + Debug 用户确认正确，**实践通过**。证据是用户截图及明确文字反馈，未单独逐模式提交完整录屏。
- GPU-04-F4：Depth Prepass / Copy Depth / Depth Priming 的独立变量对照与 Frame Debugger 截图，**实践通过**。控制 Renderer Features 和 Depth Priming 后明确看到 After Opaques 的 Opaque→CopyDepth、Force Prepass 的 DepthOnly→Opaque。
- GPU-05-A：URP Asset / Renderer Data / Renderer Feature / ScriptableRenderPass / Render Graph 的架构**已系统讲解**，不能直接当作独立实践验收。
- GPU-05-B：`TA_RenderGraphZoomFeature` 完整参考示例已提供，**理解/独立编写/Unity 运行均未验收，暂停**。
- **下次唯一开课：CSharp-01**。先用简短的 `ZoomPass` 类做“类/类型/变量/字段/方法/构造函数”的全流程解释和小实践；之后再逐步讲 OOP 与 Unity API。
- GPU-04-D8 Profiler/复杂性能测量继续按此前决定延后到真实项目。

## 7. 历史和跨对话恢复

本计划是自 2026-11-17 起对 `COURSE_MEMORY_2026.md` 第 3 节原课表的**前置插入与动态延期**；旧课表作为历史方向与课程骨架保存，不当作未来必须按原日期逐日执行的进度命令。

最新日志：[`GPU_LOG_2026-11-17.md`](GPU_LOG_2026-11-17.md)。
最新交接：[`TA_HANDOFF_2026-11-17.md`](TA_HANDOFF_2026-11-17.md)。
