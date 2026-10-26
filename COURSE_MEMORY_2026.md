# Unity 6 URP Rendering TA 学习计划与跨对话交接（2026）
> 维护日期：2026-10-24；性质：课程记录与训练协议，不是程序源代码；当前进度：**D01 未验收**。

## 0. 学习者背景与方向
- 学习者主要来自 3D 场景地编、美术与 Houdini/VEX 背景，C#/Python 了解基础语法，HLSL、图形学、数学和 GPU 渲染需要从基础建立，不默认已会。
- 2026-12-31 前：真正理解实时图形学、Shader/HLSL、Unity 6 URP 架构与 Render Graph，能独立做、解释、调试、测试自定义渲染功能。目标“8/10”是课程内部实战标尺，并非行业高级专家认定，也不要求重写完整 SRP。
- 2027 下一阶段：独立开发实际项目的渲染效果、插件、场景工具；其后研究手机 URP / GPU 优化。
- Gaussian Splatting 学习**暂停**；原参考文件 `UnityGaussianSplatting_TA_48Lesson_StudyPlan_v4_Unity6.md` 单独保留，不作为当前主线。
- 公司旧项目 Unity 2022.3.62f1 + URP 不默认兼容当前 Unity 6 教学代码。
- 学习工程： https://github.com/kitterr/URP_lesson ，源码学习记录放在独立分支 `study/urp-ta-course-memory-2026`；不改动 main。

## 1. 时间和课程推送
- 北京时间 (Asia/Shanghai) 每天上午 **10:00** 推送当天课程，与每周五 TA 技术新闻时间相同。
- 周一到周五每天 **3 小时**；**周六日两天总计约 5 小时，默认各 2.5 小时**，每周约 20 小时；不是周末每天 5 小时。
- 当前计划区间：2026-10-24 至 2026-12-31；首次计划推送 2026-10-25。
- 每天课程包含：今日目标与上下游关系、分时安排、实际讲解、最小代码/API、Unity 实验、可观察证据、验收和排错建议。每日推送只是安排，**不能在无学员反馈时自动标记课程完成**。
- 40 节技术课 D01–D40、8 次阶段验收 T01–T08。节数不是必须每天前进一课；复习和排障可以跨天。学习证据优先于赶截止日期。

## 2. 跨对话固定教学协议（必须遵循）
**为什么需要 → 与紧邻概念对应的图/流程/曲线 → 最小必要公式 → C#/ShaderLab/HLSL/API → Unity 实验验证**。
1. 先讲上游输入、本步骤的作用、不做会怎样、输出交给谁；从场景、贴图、Camera、Houdini 等已有经验引入。
2. 函数配曲线与参数滑块；向量、空间和矩阵配几何/坐标图；渲染技术配数据流、Frame Debugger 截图和画面对照。图必须与公式、代码参数一一对应，不用无关插画。
3. 新出现的符号、函数、数据类型和坐标空间需逐一解释；不要堆积数值代入或跨越推导。将可见变化与公式变量建立联系。
4. 新 API 说明：用处、来历、输入输出、参数类型、调用位置、生命周期、限制、当前 Unity/URP 版本以及如何验证；短表达式不人为拆行。
5. 学员说没懂，应回到前置概念换角度/图解释；不要凭空引入更多符号。保留独立判断，明确错误、代价与兼容性风险。
6. 每课要留下截图、代码/commit、Profiler/Frame Debugger/Render Graph Viewer、可复现问题记录中的至少一项。先预测、再动手、后对比；验收不通过不打勾。
7. 小规模修改只给变化的脚本和覆盖路径/补丁，不反复打完整工程包。
8. 新对话先读取本文件和进度日志；确认目前版本与未解决问题，不能只凭对话记忆推断完成度。

## 3. 课程总表（待按验收动态调整）
| 周次 | 日期 | 课程 | 关键知识、当周产出 |
| --- | --- | --- | --- |
| 1 | 10/25–10/30 | D01–D04 | Unity/URP 环境锁定、CPU→GPU→像素的管线、Draw Call、Frame Debugger、Render Graph Viewer；**架构流程图** |
| 2 | 10/31–11/06 | D05–D08、T01 | 向量和坐标空间、相机投影与矩阵、ZTest/ZWrite/Stencil、Linear/Gamma/HDR/Blend；**遮挡与混合排错** |
| 3 | 11/07–11/13 | D09–D12、T02 | ShaderLab、Unlit HLSL、顶点/片元、法线/光照、渲染状态；**Shader 独立编写及故障排查** |
| 4 | 11/14–11/20 | D13–D16、T03 | URP Asset、Renderer Data、Universal Renderer、Camera 与 Pass、RendererFeature 生命周期、注入时机；**URP 架构图** |
| 5 | 11/21–11/27 | D17–D20、T04 | Render Graph、Pass/资源声明、RecordRenderGraph、AddRasterRenderPass、PassData、TextureHandle、ContextContainer、UniversalResourceData、全屏处理；**最小自定义 Pass** |
| 6 | 11/28–12/04 | D21–D24、T05 | Feature+Pass、Camera 过滤、RendererList、Layer Mask、Override Material、Inspector 和生命周期；**可配置对象筛选 Feature** |
| 7 | 12/05–12/11 | D25–D28、T06 | 深度/法线输入、对象 Mask、Edge Detection、多 Pass、依赖与 Pass Culling；**描边原型** |
| 8 | 12/12–12/18 | D29–D32、T07 | Camera Stack、多 Camera、Volume、Compute Shader 入门、SRP Batcher、Instancing 和资源成本；**多场景测试** |
| 9 | 12/19–12/25 | D33–D36、T08 | Frame Debugger、Render Graph Viewer、RenderDoc/Profiler、GPU/CPU 时间、纹理带宽与 Pass 优化、故障诊断；**性能报告与盲测排错** |
| 10 | 12/26–12/31 | D37–D40 | 独立毕业作品：对象筛选→Mask Pass→边缘检测→合成，验证遮挡、多相机和分辨率；**源代码、架构图、README、性能测试、限制说明** |

### 全部技术课明细
- D01 Unity 6/URP 环境与精确版本锁定；D02 一帧画面的 CPU/GPU/Camera/Pass 流程；D03 Draw Call/Mesh/Shader/顶点到像素；D04 Frame Debugger/Render Graph Viewer/Profiler。
- D05 向量与 Object/World/View/Clip/Screen 空间；D06 矩阵和投影；D07 深度/模板缓冲；D08 颜色空间与 Alpha 混合；T01 图形基础验收。
- D09 ShaderLab 结构；D10 Vertex/Fragment HLSL；D11 法线和基础光照、URP Lit；D12 Cull/ZTest/ZWrite/Blend；T02 Shader 编写与排错。
- D13 URP 核心架构；D14 Camera/Culling/Stack；D15 ScriptableRendererFeature 与 ScriptableRenderPass；D16 Pass 注入时机；T03 URP 架构复述。
- D17 Render Graph 资源与依赖；D18 RecordRenderGraph / PassData / RasterRenderPass；D19 TextureHandle / 帧数据；D20 全屏 Shader 合成；T04 独立 Render Graph Pass。
- D21 完整 Renderer Feature；D22 注入点及 Camera 过滤；D23 RendererList、Layer、OverrideMaterial；D24 参数和工程结构；T05 独立 Feature。
- D25 深度与法线纹理；D26 对象 Mask；D27 描边检测与合成；D28 Render Graph 生命周期和 Pass Culling；T06 多 Pass 描边验收。
- D29 多 Camera；D30 Volume 自定义参数；D31 Compute Shader 入门；D32 Batcher/Instancing/带宽/分辨率成本；T07 工程集成测试。
- D33 渲染调试器深入；D34 CPU/GPU Profiling 与 RenderDoc；D35 资源与 Pass 优化；D36 黑屏/注入/资源错误诊断；T08 无提示盲测排错。
- D37 毕业项目架构设计；D38 独立对象 Mask Pass；D39 轮廓与合成；D40 兼容性、性能、README 和交付。
- **优先级**：理解渲染流水线、数学/Shader、URP 架构、Render Graph、自主排错 > 赶课；当时间不足，可压缩 Compute Shader 与 Camera/Volume 进阶。不把示例代码能运行当成真正会了。

## 4. 版本核对规则
- D01 必须从实际 `ProjectSettings/ProjectVersion.txt`、`Packages/manifest.json` 和 Package Manager 验证并**锁定真实 Unity Editor、URP Package、Graphics API**；仓库与学习者本地可能不一致。
- 仅以锁定版本的官方 API 为准，主线使用 Unity 6 URP Render Graph；关闭 Compatibility Mode (Render Graph Disabled)。旧版 `Execute()`/传统 `Blit()` 示例只能用来比较，不作为现代课程代码。
- 如需对照 URP 源码，记录相应 Package 版本/commit 和具体文件路径，避免误把新版 main 当作实际安装的源码。

## 5. 能力验收：课程内部 L8
毕业项目计 100 分：架构和时序 20、Render Graph 依赖与生命周期 25、Shader/视觉正确性 15、Camera/对象筛选与集成 10、调试和性能对照 15、代码/文档/工程权衡 15。目标 >=80，且不允许有严重的资源/时序错误。此分数只证明这套课程定义的独立实战能力，**不等价于业界 8 年经验或可以重写完整 SRP**。

## 6. 当前状态（课程重置于 2026-10-26）
- 用户要求将今天（2026-10-26）重新定义为 **Day 1 / D01**，并从明天（2026-10-27）开始 Day 2 / D02。旧对话中曾发过 D01 与 D02 的内容，但用户尚未提供验收反馈，因此**所有课程仍未验收通过**。
- **当前课：Day 1 / D01，已发送课程内容，待学员实际操作与验收**。Day 1 聚焦环境核对、URP/渲染管线入门、测试场景和环境检测脚本。明天课程应从 Day 2 / D02 开始，主题为一帧画面的产生、CPU/GPU、渲染流水线与 Camera/Culling。
- 计划推送：自动化任务“每日 URP TA 课程”已更新为北京时间上午10:00，首次按重置进度的推送时间为 2026-10-27，持续至 2026-12-31。周一至周五每天3小时；周末两天合计约5小时（默认每天2.5小时）。自动任务与 GitHub 文件**不会自动同步**；课程进度需在明确的 GitHub 写入操作后更新。
- 版本锁定：**待 Day 1 确认**。仓库配置曾显示 Unity 6000.6.7f1 与 URP 17.6.0，但本地 Editor/URP/Graphics API 尚未由用户验证。
- 毕业项目：尚未启动。
- 已知学习阻塞：暂无学员提交的环境检测结果或实验截图。
- 下一步：完成 Day 1 环境核对和测试场景；用户反馈实际版本、Console 输出和实验结果后再验收。

## 7. 每日学习日志模板（每次真实完成后追加）
```
### YYYY-MM-DD — Dxx/Txx — 状态：未开始 / 进行中 / 待验收 / 通过
- 当天课程时长：
- 关键原理（用自己的话）：
- Unity Editor / URP / Graphics API：
- 代码路径与 Git commit：
- 预测现象 → 实际观察：
- 图片 / Frame Debugger / Render Graph Viewer / Profiler 证据：
- BUG、定位与修复：
- 未理解的问题：
- 老师验收反馈及结论：
- 下次学习起点：
```

## 8. 新对话开课口令
> 请读取 `kitterr/URP_lesson` 的 `study/urp-ta-course-memory-2026` 分支中本文件和最新学习日志，先恢复版本、进度和未解决问题。继续 Dxx/Txx 课程。按“为什么→对应图/曲线→最小必要公式→C#/HLSL/API→Unity 验证”教学，不默认图形学和 HLSL 基础。周一到周五 3 小时；周末两天合计 5 小时（默认每天 2.5 小时）；每课要可验收，未验证不能勾选。课程结束后经明确操作更新此分支的学习记录，不影响 main。
