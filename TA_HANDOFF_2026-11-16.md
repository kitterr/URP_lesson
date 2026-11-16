# TA 跨对话交接卡 — 2026-11-16

> 仓库：`kitterr/URP_lesson`；**独立课程分支** `study/urp-ta-course-memory-2026`；不修改 main。
> 主线：Unity 6 + URP Rendering TA；上次核对的历史 Editor 版本 Unity 6000.6.7f1 / URP 17.6.0，本次未重新核验。
> 最新详细日志：[`GPU_LOG_2026-11-16.md`](GPU_LOG_2026-11-16.md)。

## 唯一下一课

**GPU-04-F3：Scene Depth 交界发光的 Unity 实际运行和 Debug 验收**。

用户已理解两条 Eye Depth 数据通路、`intersection = 1-saturate(depthDiff/_IntersectionWidth)` 与交界亮带的关系，已获得带中文 API 注释的 `Custom/TA_FresnelDepth` 完整参考 Shader 和四个调试模式；**尚未确认在 Unity 中编译/运行/实际 Debug 效果，不能标记 F3 实践通过。**

下次先检查 URP Depth Texture、透明 Sphere 与不透明 Cube 的交界实验，再切换 `RawDepth` / `EyeDepth` / `Intersection` 观察图像，用宽度参数验证交界权重；如有异常按屏幕 UV → Raw Depth → Linear Eye Depth → Depth Diff → Blend 的顺序排查。之后再系统研究 URP 深度纹理生成时机/方式与 Reversed-Z 限制。

## 最近状态

- **GPU-04-F2：完整通过**。Fresnel + Alpha Blend 护盾概念正确，并收到用户明确的 Unity 运行效果符合预期反馈。
- **GPU-04-F1：概念通过**；用户自写 Shader 曾把 Object Position 误当作 Normal 输入，已指出应读取 `normalOS:NORMAL`；F1 独立运行未单独确认。
- **GPU-04-E1～E7：核心概念通过**（Cull/ZTest、ZWrite、透明排序、Blend、clip、Stencil、X-Ray）；对应实验未逐项确认，不升级为完整运行验收。
- **GPU-04-D8：性能工具与 Vertex/Pixel SH 性能对照主动延后**，非当前门槛；没有 GPU 时间测量证据。
- **GPU-04-D7 及此前已通过条目保持有效**，见旧日志和知识台账；C/D 若干细分待确认不擅自补验收。

## 教学关键反馈

1. **先讲渲染问题与整个数据流，再讲 API，最后给 Shader**。尤其是新 API 必须说清为何需要、输入/输出、参数、空间和执行阶段，不能先整段贴 Shader 再解释。
2. 对图形相关公式要提供**实际可见的**曲线、场景示意图及参数滑块；单贴未运行的 Python 绘图代码无效。每张图要关联公式和 Shader 参数。
3. `depthDiff` 是**同一屏幕像素下沿相机观察方向的 Eye Depth 差**，非三维最短距离。亮带由权重人为控制颜色，不是计算 Mesh 真实相交线。
4. 单独发送 `1` = “继续”；不重复简单算术、公式代入；验收优先概念解释、Shader、画面和真实排错。
5. 课程以准确证据为准：用户明确运行反馈可以用于运行验收，未验证的 Debug/性能不打勾。

## 新对话口令

“请读取 `kitterr/URP_lesson` 的 `study/urp-ta-course-memory-2026` 分支 `COURSE_MEMORY_2026.md`、`TA_TEACHING_RULES.md`、`TA_KNOWLEDGE_LEDGER.md`、`GPU_LOG_2026-11-16.md`、`TA_HANDOFF_2026-11-16.md`，恢复分项验收边界。继续 GPU-04-F3 Unity 交界发光 Debug 实践，不重复已懂的理论和简单数学题；先概念数据流再 API/Shader，图表必须可见。”
