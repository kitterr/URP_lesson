# TA 跨对话交接卡 — 2026-11-15

> 仓库：kitterr/URP_lesson；**只用独立分支** `study/urp-ta-course-memory-2026`；main 不改。
> 主线：Unity 6 URP Rendering TA。历史版本为 Unity 6000.6.7f1 / URP 17.6.0，本轮未重新验证小版本。
> 最新详细日志：`GPU_LOG_2026-11-15.md`。

## 唯一下一课

**GPU-04-D8：GPU Profiling（尚未开始实测）**。从 CPU Time vs GPU Time、VSync、Frame Debugger vs Profiler 的用途讲起；结合已完成的 Vertex SH / Pixel SH 变体，设计可复现、能解释波动的性能 A/B 测试。优先解释为什么某种优化可能快/慢，不预判真实性能结果。场景不具判别力时可将测试留待真实项目，不强制低价值合成基准测试。

## 最近已正式验收

- GPU-04-A：ShaderLab/HLSL Unlit Shader 实现与运行。
- GPU-04-B：_BaseColor / UnityPerMaterial / SRP Batcher Compatible 与故意破坏后的 Not Compatible。
- GPU-04-D2：非均匀缩放法线逆转置。
- GPU-04-D3：Lambert + saturate，Unity 视觉正确。
- GPU-04-D4：直接光 + 固定环境光的主要公式/视觉通过；具体 SRP Batcher 复核待补。
- GPU-04-D7：**正式通过**。材质 Inspector Toggle 可切换 Vertex/Pixel SH；Frame Debugger Vertex SH 见 `TA_SH_VERTEX`（vs/fs、Local），Pixel SH 展开后用户确认没有该 Keyword；两者绘制处于 `SRP Batch`，但未测 GPU 时间。

## 保留待确认，禁止假装验收

- GPU-04-C1：Texture2D/Sampler/UV 运行与 Batch 部分项目；C2 Clamp/Batch；C3 Filter/Mipmap 详细视觉对照。C4 Aniso 1x/16x 实验通过。
- GPU-04-D1：World Normal Debug 运行过，部分代码修正及旋转运行验证未单独确认。
- GPU-04-D5：`SampleSH(normalWS)` 运行正确、9 基函数及 L0/L1/L2 理解、SH HLSL 源码已教学；Gradient 上蓝下橙的 Unity 实测与源码分项实验未正式验收。
- GPU-04-D6：Vertex/Pixel SH 在材质面板切换成功；低/高面数画质对照和性能实测未完成。
- GPU-04-D8：尚无 GPU Profiler 测量数据。Frame Debugger 的 `SRP Batch` 不是性能收益证据。

## 教学风格（最新优先）

1. 学员单独发送 `1` 表示“继续”，无需问含义。
2. 不再布置已掌握的乘法、平方、坐标分量等简单题；重点放在 Shader/源码/渲染现象/排错与可靠性能实测。
3. 对新数学符号逐个解释，尽量配对应图形；先概念直觉、再关键 API 和实际实现，避免堆概念。
4. 核心主题：**先知识地图和完整系统讲解，再安排有价值的集中实践**。拒绝拖沓和频繁的碎片化微测验。
5. 继续区分“已讲解”“理解已确认”“实践已验收”“待验证/主动延后”；不为赶课补打勾。
6. 使用 Unity 6 + URP 教学主线，区别于公司 Unity 2022.3.62f1 项目；短代码尽量维持一行表达。

## 新对话口令

“读取 kitterr/URP_lesson 的 `study/urp-ta-course-memory-2026` 分支中 COURSE_MEMORY_2026.md、TA_TEACHING_RULES.md、TA_KNOWLEDGE_LEDGER.md、GPU_LOG_2026-11-15.md 和 TA_HANDOFF_2026-11-15.md。恢复已通过与待补项，直接继续 GPU-04-D8 GPU Profiling；不要重复简单数值题，不要把 Shader Variant 调试等同于性能测量。”
