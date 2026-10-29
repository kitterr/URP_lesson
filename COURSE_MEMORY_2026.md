# Unity 6 URP Rendering TA 学习计划与跨对话交接（2026）
> 维护日期：2026-10-27；性质：课程记录与训练协议，不是程序源代码；当前进度：**D01 已验收通过；MSR-01 的 CPU Framebuffer、四象限绘制、像素坐标换算与安全写入子步骤已通过；MSR-01 整体仍进行中，三角形绘制尚未开始**。

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
9. **讲解深度要求（用户于 2026-10-26 明确提出）**：避免只用几行概括复杂概念。每个核心知识点要说明“为什么需要、具体处理什么数据、输入/输出是什么、内部步骤如何衔接、容易混淆的概念有何区别、常见错误会导致什么现象”。从直觉例子开始，分层展开，术语首次出现时给出中英文名称与解释；对光栅化、坐标空间、矩阵、插值、GPU 资源等关键内容，不可只列流程名词。
10. **C# 代码教学要求**：每次提供 C# 示例，都要同时讲解相关语法（如 using、class、变量与类型、var、函数、参数、if/return、字符串插值等）、执行顺序、Unity API 的输入/输出和调用时机。不能只让学员复制代码。
11. **辅助资料要求**：在适合的课程中提供经过核对的官方文档、文章与视频链接，并说明资料适用目的、推荐章节或时间点、观看前需要理解什么、看完后要验证什么。资料是主讲解的补充，不应只丢一串链接，也不应用外部资料替代必要的详细讲解。
12. **阶段式推进**：用户要求一次只讲一个阶段。每阶段说明清晰目标与验收标准；学员提交结果后只验收当前阶段，未通过时针对缺口继续解释，不提前进入下一阶段。

## 3. 课程总表（待按验收动态调整）
| 周次 | 日期 | 课程 | 关键知识、当周产出 |
| --- | --- | --- | --- |
| 1 | 10/25–10/30 | D01–D04、MSR-01 | Unity/URP 环境锁定、CPU→GPU→像素的管线、Draw Call；MSR-01 建立 CPU Framebuffer 与屏幕空间三角形；**架构流程图 + 第一张软件绘制图像** |
| 2 | 10/31–11/06 | D05–D08、T01、MSR-02/03 | 向量/坐标空间、矩阵与投影、深度/颜色；MSR-02 Object→World，MSR-03 Software Camera 与投影；**遮挡与混合排错 + 变换可视化** |
| 3 | 11/07–11/13 | D09–D12、T02、MSR-04/05 | ShaderLab、Unlit HLSL、顶点/片元、渲染状态；MSR-04 Clip Space 裁剪/光栅化/插值，MSR-05 深度与 UV；**Shader 独立编写及故障排查** |
| 4 | 11/14–11/20 | D13–D16、T03、MSR-06 | URP Asset、Renderer Data、Camera 与 Pass、RendererFeature 生命周期；MSR-06 导入 FBX Mesh 并整合软件渲染管线；**URP 架构图 + 软件渲染器综合验收** |
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


## 3A. Mini Software Renderer 配套实战（2026-10-27 新增）

**调整决定：** 在现有 D01–D40 + T01–T08 主线旁增加 6 个有验收标准的 Mini Software Renderer（MSR）实践阶段。主线目标不删减，软件渲染器不代替 HLSL、GPU、URP、Render Graph 或性能分析课程。完整阶段、实现顺序、验收和参考资料见 [MINI_SOFTWARE_RENDERER_PLAN.md](https://github.com/kitterr/URP_lesson/blob/study/urp-ta-course-memory-2026/MINI_SOFTWARE_RENDERER_PLAN.md)。

- MSR-01：CPU Framebuffer 与屏幕空间三角形；先验证 CPU 如何写像素。
- MSR-02：Mesh 顶点/索引与 Object→World；手写三角形后读取 Unity 已导入的 Mesh。
- MSR-03：Software Camera、View/Projection、透视除法与 Viewport Transform。
- MSR-04：Clip Space 裁剪、光栅化、重心坐标与属性插值。
- MSR-05：Depth Buffer、遮挡、UV、纹理采样和透视校正插值。
- MSR-06：导入 FBX Mesh，整合管线并与 Unity GPU 渲染结果做差异分析。

**工程边界：** 不手写 FBX 文件解析器；使用 Unity FBX Importer 加载资产。渲染计算主要由 C# 在 CPU 上执行，Texture2D 仅用于展示结果。每个阶段预计 2–4 小时，可拆分学习；按每周约 20 小时预算嵌入现有课程。先讲原因、图示、必要公式、C# 语法/API，再动手验收；每次只教一个阶段。


## 4. 版本核对规则
- D01 必须从实际 `ProjectSettings/ProjectVersion.txt`、`Packages/manifest.json` 和 Package Manager 验证并**锁定真实 Unity Editor、URP Package、Graphics API**；仓库与学习者本地可能不一致。
- 仅以锁定版本的官方 API 为准，主线使用 Unity 6 URP Render Graph；关闭 Compatibility Mode (Render Graph Disabled)。旧版 `Execute()`/传统 `Blit()` 示例只能用来比较，不作为现代课程代码。
- 如需对照 URP 源码，记录相应 Package 版本/commit 和具体文件路径，避免误把新版 main 当作实际安装的源码。

## 5. 能力验收：课程内部 L8
毕业项目计 100 分：架构和时序 20、Render Graph 依赖与生命周期 25、Shader/视觉正确性 15、Camera/对象筛选与集成 10、调试和性能对照 15、代码/文档/工程权衡 15。目标 >=80，且不允许有严重的资源/时序错误。此分数只证明这套课程定义的独立实战能力，**不等价于业界 8 年经验或可以重写完整 SRP**。

## 6. 当前状态（截至 2026-10-27）
- 用户将 2026-10-26 重新定义为 Day 1 / D01；D01 四个阶段已通过验收。
- D01 环境：Unity Editor 6000.6.7f1；URP Package 17.6.0；Windows；Auto Graphics API 已勾选；Graphics API 列表由用户记为“D12”，具体全称未进一步确认。
- D01 阶段二：Graphics 默认管线与 Quality/PC 管线资源均为 PC_RPAsset；Windows 默认质量等级和 Editor 当前质量等级均为 PC。
- D01 阶段三：测试场景已保存，Game View 正常，未出现新的红色错误。场景文件/截图未确认已提交到 Git。
- D01 阶段四：用户提供的运行时日志显示 Current Quality Level: PC、SRP Enabled: True、Active Render Pipeline Asset: PC_RPAsset、Asset Type 为 UniversalRenderPipelineAsset；无新红色错误。
- D02 已进行 CPU/GPU、Camera Culling、几何裁剪、透视除法和视口变换等基础讨论。用户报告 Shader 编译成功、材质应用成功、Transform 实验正常，并表示理解 TransformObjectToWorld 的作用。
- **当前课程调整：** 用户提出实现一个加载 FBX、建立 Camera 并自行完成矩阵变换到屏幕的小项目。已决定新增 Mini Software Renderer 配套实战，详见 MINI_SOFTWARE_RENDERER_PLAN.md。该计划尚未实现，不能把任何 MSR 阶段标记为完成。
- **MSR-01 最新进度（2026-10-27）：** 已完成并验收：Texture2D + Color32[] CPU 像素缓冲、四象限颜色写入、SetPixels32/Apply/RawImage 显示链路、1D 索引与 2D 坐标互转（例如 width=256、i=513 得到 x=1、y=2）、安全 SetPixel(x,y,color) 的边界检查。测试证据来自用户实际运行反馈：四象限正常显示；纯洋红色测试确认显示链路可用；合法坐标写入白点成功；SetPixel(-1,0,...) 被拦截并输出警告、没有数组越界异常；恢复合法坐标后画面正常。课程日志已保存至 [MSR_LOG_2026-10-27.md](https://github.com/kitterr/URP_lesson/blob/study/urp-ta-course-memory-2026/MSR_LOG_2026-10-27.md)。日志记录的是学习进度，未收到 Unity 截图或项目源码，不声称这些内容已上传。
- **教学方式（用户明确要求）：** 对复杂知识进行详细、循序渐进的讲解，解释为什么需要、数据输入/输出、内部步骤、易混淆概念和常见错误；提供相关图示、文章/视频链接和观看重点。每次给 C# 代码必须讲解相关语法、执行顺序及 Unity API 的输入/输出和调用时机。
- 版本配置与仓库配置可能存在差异；未核实前不擅自修改项目设置。公司旧项目仍是 Unity 2022.3.62f1 + URP，不默认兼容 Unity 6 教学代码。
- 计划时间：北京时间每天 10:00 推送；工作日每天 3 小时，周末两天合计约 5 小时（默认每天 2.5 小时）。课程按验收推进，不为赶日期跳过概念。
- **下一步：继续 MSR-01 当前阶段的代码整理与资源生命周期检查**：移除不必要的 Update() 每帧重复绘制、删除未使用的 bg 字段，检查运行时 Texture2D 的释放；暂不进入三角形绘制，直到用户明确继续并完成这一步。


## 6A. MSR-01 实际学习日志（2026-10-27）

### 状态：进行中（已通过的子步骤记录如下）
- **环境：** Unity 6 教学工程；此前已记录 Unity Editor 6000.6.7f1、URP 17.6.0。当前未重新核验版本。
- **已完成：** 创建 Texture2D、Color32[]；在 CPU 上生成四象限颜色并通过 SetPixels32() / Apply(false, false) 显示到 RawImage。
- **已验证的显示排错：** 全洋红色纹理能够显示，证明纹理生成/上传/RawImage 显示链路基本正常；恢复四象限后显示正确。
- **已完成：** 使用 x = i % width、y = i / width 将一维数组索引转换为二维像素坐标；用户正确回答 width=256、i=513 对应 (1,2)。
- **已完成：** 增加安全 SetPixel(x, y, color) 方法，对 x/y 的合法范围进行检查；合法坐标能显示白点，(-1,0) 被拦截并输出警告且未发生数组越界异常，恢复合法坐标后图像正常。
- **当前已通过的子步骤：** CPU 像素缓冲与显示；四象限绘制；索引/坐标转换；像素安全写入与越界拦截。
- **未完成：** MSR-01 整体尚未验收。屏幕空间三角形绘制尚未开始；不得将 MSR-01 整体标为通过。
- **当前未做的整理：** 删除重复的 Update() 绘制；删除未使用的 bg 字段；确认 Texture2D 的生命周期与 OnDestroy() 清理方式。
- **下次学习起点：** 只做当前阶段的代码整理和纹理资源生命周期检查；用户明确允许后再开始三角形绘制。
- **证据边界：** 上述结果来自用户文字反馈；当前没有收到工程截图、实际代码仓库 commit 或 Unity 场景文件，因此不记录这些未提供的证据。

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


## 9. TA 教学模式与课程进度门禁（新增，2026-10-29）

正式规则见 [TA_TEACHING_RULES.md](https://github.com/kitterr/URP_lesson/blob/study/urp-ta-course-memory-2026/TA_TEACHING_RULES.md)。此规则适用于全部每日 TA 课程，优先于按日历推进的默认排课：

- **分阶段教学：** 每次消息只讲当前一个阶段，讲解、操作、验收后停止等待用户反馈；通过后才继续下一阶段。不一次性发完当天所有阶段。
- **缺课不跳课：** 若前一天没有当前阶段的明确通过记录，次日 10:00 继续同一课程/同一阶段，并明确提醒“课程进度已落后；当前阶段尚无通过记录，今天继续本阶段，不进入下一阶段”。日期可以继续，但课程内容不能自动升级。
- **完成记录门禁：** 已发送、已阅读、代码中存在功能均不等于通过。必须有用户实际操作/回答、教师验收和明确日志记录。部分子步骤通过只更新子步骤，不代表整个阶段通过。
- **当前执行状态（2026-10-29）：** 用户说明今天没有空学习。MSR-01 当前阶段“现有代码整理与运行时 Texture2D 生命周期/OnDestroy 清理检查”仍未完成/未验收。下一次推送必须重复这一阶段并提醒进度已落后；不得进入三角形绘制。
- 自动化任务与 GitHub 不会自动同步。只有工具写入成功后才能说已同步。


## 10. MSR-01 生命周期检查进度（2026-10-29）

- 用户实际运行反馈：移除 `SoftwareFramebufferDemo` 组件后，RawImage 的纹理引用成功清空，Console 没有任何异常。当前 `OnDestroy()` 子步骤已验收通过：先检查 `output != null && output.texture == framebuffer` 并解除 RawImage 引用，再 `Destroy(framebuffer)`，最后清空 `framebuffer` 和 `pixels`。详情见 [MSR_LOG_2026-10-27.md](https://github.com/kitterr/URP_lesson/blob/study/urp-ta-course-memory-2026/MSR_LOG_2026-10-27.md)。
- 用户决定保留空的 `Update()` 作为未来测试入口。尊重此决定，不再要求删除；记录为有意保留，而不是未完成缺陷。
- MSR-01 已于 2026-10-29 整体验收通过：用户确认修改三角形第二个顶点后，显示形状发生变化；并完成了前述像素缓冲、坐标、边界、生命周期及叉乘原理的问答/运行反馈。下一阶段进入 MSR-02，先学习 Mesh 的顶点数组与三角形索引数据；具体证据见最新学习日志。


## 13. 像素坐标复习状态（2026-10-29）
用户表示已理解 `BgColored()` 中非正方形 framebuffer 的行坐标应使用 `i / width` 而不是 `i / height`；未报告实际运行对照，因此仅记为概念理解确认，不声称运行时测试通过。用户明确说明三角形绘制已经学过；叉乘符号、三边测试、area/顶点绕序、像素中心采样、包围盒和边界检查的口头复习基本正确。MSR-01 已整体验收通过：用户正确识别 `DrawTriangle` 输入为屏幕像素空间，并实际反馈改变顶点后画面发生变化。当前课程进入 MSR-02，按用户要求跳过基础 Mesh 导入/数据读取，转向手写几何的坐标变换与屏幕边界处理；不得一次性讲完整个 MSR-02。


## 14. MSR-01 三角形顶点变化验收（2026-10-29）
用户正确回答：把第二个顶点从 `(100,120)` 改为 `(80,60)`，三角形外轮廓及覆盖像素会变化，原因是顶点位置改变。口头概念验收通过。此题为预测题，本轮没有报告实际改坐标后运行的观察结果，因此在完成一次简短实测前，不标记 MSR-01 整体通过。用户已报告将第二个顶点改为 `(80,60)` 后，三角形画面确实发生变化，因此该实测已完成。MSR-01 整体验收通过；下一步进入 MSR-02 的第一个子阶段：理解 Mesh 顶点数组与三角形索引的对应关系。


## 15. MSR-01 完成与 MSR-02 开始（2026-10-29）
- MSR-01 整体状态：通过。用户实际报告将三角形第二个顶点改为 `(80,60)` 后，画面形状发生变化；其他子步骤与叉乘原理复习也已完成。
- 下一阶段 MSR-02：手写几何的坐标空间、Object→World 与屏幕映射；基础 Mesh 数据读取按用户要求跳过。
- 当前只开展 MSR-02 第一个子阶段：理解 `Mesh.vertices` 与 `Mesh.triangles` 的数据关系。先建立顶点数组与索引的直觉，再操作；本次不提前讲矩阵或完整 Object→World 变换。
- 课程门禁仍有效：用户反馈实际操作/回答后再验收，验收通过才进入 MSR-02 下一个子阶段。


## 16. MSR-02 平移概念状态（2026-10-29）
用户正确理解：只改变 `objectPosition` 时，模型随其 Pivot 整体移动。解释前提是当前手写 CPU 渲染器只实现平移，未实现旋转/缩放；`objectPosition` 表示局部原点/Pivot 的世界位置。该子阶段尚待实际运行确认：分别使用 `(0,0,0)` 与 `(0.5,0.25,0)`，确认形状与大小不变、整体位置变化。未收到结果前不标记通过，也不进入旋转。


## 18. MSR-02 课程剪裁与越界处理（2026-10-29）
- 用户具备 3D 模型制作和贴图绘制经验，明确要求跳过 Unity Mesh 导入及基础 `Mesh.vertices` / `Mesh.triangles` 数据读取教学；未来项目实际需要时，助手提供可用读取器和调用说明。
- 用户已理解 `WorldToScreen` 的中心偏移逻辑，不重复讲解。已理解链路概念：局部顶点 → 通过 Pivot 世界位置平移到世界坐标 → 通过 `pixelsPerUnit` 映射到 framebuffer；平移对照实验由用户选择跳过，不记为实测通过。
- 当前教学点为屏幕越界保护：保留原始顶点坐标，夹紧三角形包围盒的像素遍历范围，而不是逐个夹紧 A/B/C 顶点。用户尚未回答对应验收题，因此当前子阶段未通过。


## 19. MSR-02 屏幕边界概念验收通过（2026-10-29）
- 用户正确说明：保留 `a.x = -20`，因为 boundary/AABB 阶段已将遍历范围限制在屏幕内；在有效像素上仍根据原始三角形边判断覆盖，因此可正确绘制屏幕内可见部分。
- 该子阶段口头验收通过；没有单独进行越界场景的运行测试，不标记为运行实测。
- 区分：包围盒范围限制不是三维几何裁剪；Clip Space/near-plane clipping 后续另讲。
- 当前下一子阶段：旋转变换的直觉，比较平移加偏移与旋转改变相对方向；只讲一个子阶段，不直接展开完整矩阵推导。
