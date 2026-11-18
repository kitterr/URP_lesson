# TA 跨对话交接卡 — 2026-11-18（当前有效）

> 仓库：`kitterr/URP_lesson`；唯一课程记录分支：`study/urp-ta-course-memory-2026`；main 未修改。
> 当前主线：C# 基础/OOP/Unity API 阅读补强；GPU-05-B Render Graph C# 实现继续暂停。

## 唯一下一课：CSharp-06 — Interface 与多态实践

从 interface 的声明与实现开始，比较 interface 和 abstract class；讲一个类实现多个接口；再用接口类型引用不同实现对象，说明多态在渲染功能组织中的用途。一次只讲一个阶段，用户回答/操作并验收后再继续。

## 最近学习状态（2026-11-18）

- **CSharp-04 Property**：用户独立写出 `SampleScale` 的 get/set 属性，基础语法正确；Clamp、构造函数通过属性赋值、this 独立编码未验收，用户明确跳过完整的简单 ZoomPass 练习。已讲解自动属性、private set、字段初始化、`transform.position` getter/setter 与 Unity 序列化字段区别。
- **CSharp-05 继承/base**：完成继承、protected/private、父类构造函数顺序、`: base(args)` 与 `base.Method()` 讲解；用户对 base 构造调用、父类方法调用及父类参数类型改变后的实参修正回答正确。
- **CSharp-05 virtual/abstract/override**：代码追踪 BAC、D、BC 三题正确。第四题部分正确；已澄清父类改 abstract method 后必须成为 abstract class，派生类仍用 override 实现，但不能调用不存在的 abstract 父类方法体。
- **Unity 实机证据**：本轮没有 Unity 编译、运行、截图或工程提交；不要将 CSharp-04/05 标记为 Unity 实践通过。
- CSharp-03 之前已讲解 out、方法重载、可选/命名参数、访问修饰符与封装；out 概念和方法重载/封装相关代码阅读验收通过。ref/in 独立编码仍未验收。

## 教学与验收约定

1. 先解释为什么需要，再拆类型、字段、参数、返回值、执行顺序和 Unity API 调用时机。
2. 不重复用户已理解的简单赋值/数值练习；优先真实 API 阅读、调用链追踪、独立实现与排错。
3. **不要预先显示选择题答案**；用户已指出此前选项直接暴露答案。优先开放式问题，若使用选择题必须保持选项中立且不提示答案。
4. 概念理解、静态代码审查、Unity 实机运行是不同证据等级，必须分别记录。
5. GPU-05-B 暂停；不要在 C# 基础门槛未通过前重新给完整 Renderer Feature 让用户复制。
6. 用户单独发送 `1` 表示继续。

## 仓库恢复文件

- `COURSE_MEMORY_2026.md`
- `TA_TEACHING_RULES.md`
- `TA_KNOWLEDGE_LEDGER.md`
- `CSHARP_FOUNDATION_BRIDGE_PLAN.md`
- `GPU_LOG_2026-11-18.md`
- `TA_HANDOFF_2026-11-18.md`

恢复时以本交接卡与最新日志为准；旧的 2026-11-17 handoff/log 指针属于历史记录。
