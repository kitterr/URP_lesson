# TA 跨对话交接卡 — 2026-11-10

> 仓库：kitterr/URP_lesson
> 分支：study/urp-ta-course-memory-2026（只更新此独立学习分支，不修改 main）
> 当前环境：Unity 6 + URP 主线；Editor/URP Package 的精确小版本尚未在 2026-11-10 重新核实。

## 1. 当前主线位置

- CPU Mini Software Renderer 原理复习已于 2026-11-05 暂时收尾，不再作为 GPU/URP 主线的前置门槛；完整 CPU Renderer 仍是未来可选综合练习。
- GPU-01：Vertex/Fragment 数据流、Attributes/Varyings、Houdini string 属性不能直接作为普通 GPU 顶点输入、UV 可视化和相关实验已完成主要教学；详见 GPU_LOG_2026-11-08.md。
- GPU-02：ZTest/ZWrite 与红蓝 Quad A/B/C 实践、Frame Debugger 绘制顺序核对已验收通过。
- GPU-03：Depth Prepass / Depth Priming 的 Disabled/Auto 对照实践通过；Early-Z、Overdraw、Reversed-Z 和三者关系已系统讲解。
- GPU-03 合成 Overdraw/Early-Z 性能测试已由用户主动跳过/延后。原因：测试高度依赖具体场景，合成 Quad 测试消耗时间且未提供足够有价值的结论；未来在真实项目场景遇到瓶颈时再结合工具分析。
- 第三轮 GPU-01～03 诊断测验为 5/8，但含多项未教学知识，不作为正式成绩。详细逐题记录见 GPU_LOG_2026-11-10.md。

## 2. 唯一下一步

进入 **GPU-04：ShaderLab 与 HLSL 架构**。

开课先用一张知识地图说明：
- ShaderLab 的 Shader / Properties / SubShader / Pass / Tags 结构；
- HLSL 程序块、Attributes / Varyings / Vertex / Fragment 的位置；
- URP Pass Tags 与实际 Renderer 选择 Pass 的关系；
- Cull、ZTest、ZWrite、Blend 等渲染状态；
- Material 属性、常量缓冲区和 GPU 输入数据的来源区别。

随后系统讲解当前模块，再决定是否需要一个集中实践。不要在理论框架尚未讲完时插入零散操作。

## 3. 后续需要补课的知识（按相关课程自然安排）

- GPU Instancing Buffer / 每实例数据，与 SRP Batcher 的区别。
- HLSL 插值修饰符：noperspective、nointerpolation、centroid、sample。
- 透明物体排序、Alpha Blend 与 ZWrite On/Off 的实际影响。
- DepthOnly 与 Forward Pass 的 Alpha Clipping、顶点动画/位移一致性。
- SV_Depth、clip/discard 对深度测试和 Early-Z 的影响。
- GPU Frame Time、VSync、分辨率和真实性能测量。

这些主题均不能在未完整教学前作为正式测验内容。用户在诊断题中答对某个未教学主题，也不等于已完成教学。

## 4. 最新教学协议（必须遵循）

用户时间有限，明确要求教学高效、不拖沓。严格按以下方式执行：

1. **先整体讲解，再实践**：每个完整主题先给目标和知识地图，再系统讲清机制、数据流、相关 API 和边界。
2. 不把完整主题拆成大量碎片化微步骤；不要让用户先动手、再根据截图不断追加原本可以预先规划的实验。
3. 实践只在有明确学习收益时安排；如安排，一次说明目标、配置、变量、预期结果和验收标准。
4. 不为完成课程制造场景依赖、低判别力的测试。性能分析优先留到真实项目问题出现时。
5. 不为占满 3 小时预算而拉长课程；避免重复已掌握内容、冗长铺垫和无必要选择题。
6. 正式测验只覆盖已系统教学的内容；未教学内容只能作为明确标注、不计分的诊断/预习题。
7. 保持技术判断独立、直接；说明限制和不确定性，不夸大截图或调试器能证明的内容。
8. 每次课程结束更新知识台账，区分未引入、已提及、已系统教学、理解已确认、实践已验收、主动跳过/延后。

完整规则见 TA_TEACHING_RULES.md；掌握状态见 TA_KNOWLEDGE_LEDGER.md；本次进度见 GPU_LOG_2026-11-10.md。

## 5. 下次开课不要做的事

- 不重做五层 Quad 的 Overdraw 实验。
- 不要求补做 GPU-03 的合成性能测试。
- 不重复 GPU-02 已验收的基础 ZTest/ZWrite A/B/C 实验。
- 不把第三轮诊断题的未教学失分记为用户缺陷。
- 不默认 CBUFFER、Instancing Buffer、noperspective、SV_Depth、Alpha Clipping 跨 Pass 一致性、VSync 性能分析已经教过。

## 6. 恢复口令

“请读取 URP_lesson 的 study/urp-ta-course-memory-2026 分支中 COURSE_MEMORY_2026.md、TA_TEACHING_RULES.md、TA_KNOWLEDGE_LEDGER.md、最新 GPU_LOG 和 TA_HANDOFF。按 2026-11-10 的高效教学约定继续 GPU-04：先给完整知识地图并系统讲解，再决定是否集中实践。不要重复 GPU-01～03 已通过内容，不要用未教学知识出正式测验。”
