# Mini Software Renderer 配套实战计划

> 版本：v1.1 · 更新日期：2026-10-27  
> 所属课程：Unity 6 URP Rendering TA 主线  
> 配套分支：study/urp-ta-course-memory-2026  
> 目标：用一个可观察、可调试的 CPU 软件渲染器，补强坐标变换、Camera、裁剪、光栅化、插值和深度的直觉；随后把相同概念映射回 Unity GPU/URP 管线。

## 1. 为什么增加这个项目

现有课程通过 Unity Shader、URP 和 Render Graph 学习真实 GPU 渲染。软件渲染器不是替代主线，而是一个“把 GPU 内部部分工作摊开来观察”的教学实验。

在这个项目里，C# 会手动处理顶点变换、三角形裁剪、像素覆盖、插值和深度判断；Unity 主要负责导入 FBX 资源、提供运行环境，以及把 CPU 写入的 Texture2D 显示出来。这样可以直接观察一个顶点如何变成屏幕上的像素。

必须始终区分：

- **软件渲染器**：C# 在 CPU 上逐步计算并写入像素。
- **Unity GPU 渲染**：CPU 提交绘制命令，GPU 执行顶点/片元着色和硬件光栅化，URP 组织渲染 Pass。
- 两者共享大量数学原理，但执行位置、性能、资源生命周期和 API 不同。软件渲染器的实现不能直接当成 URP 生产代码。

## 2. 项目边界与实现选择

### 本项目会做什么

- 在 Unity 6 教学工程内建立独立的 SoftwareRendererLab。
- 先用手写的少量三角形验证流程，再读取 Unity 已经导入的 FBX 所对应的 Mesh 数据。
- 自己实现 Object/World/View/Clip/Screen 之间的变换。
- 自己实现裁剪、三角形光栅化、重心坐标插值和深度缓冲。
- 逐步增加 UV 与纹理采样，观察透视校正插值的作用。
- 用线框、顶点坐标、裁剪结果、深度值等 Debug 输出帮助定位错误。
- 将软件渲染结果与 Unity 正常渲染的同一模型并排比较，解释差异。

### 本项目暂时不做什么

- 不自己解析 FBX 二进制文件。先使用 Unity 的 FBX Importer，再通过 Mesh.vertices、Mesh.triangles 等 API 读取网格数据。原始 FBX 格式解析属于独立课题，不应与渲染管线学习混在一起。
- 不实现完整 PBR、阴影、抗锯齿、复杂透明排序或生产级材质系统。
- 不从零编写独立窗口系统，不把项目做成大型通用引擎。
- 不用 CPU 版本代替 HLSL、GPU Buffer、URP 或 Render Graph 课程。
- 不以像素级完全匹配 Unity 为验收标准。边缘覆盖规则、颜色空间、投影约定和深度精度都可能造成合理差异，重点是解释差异来源。

## 3. 分阶段计划

每个阶段都按“先解释原因和流程 → 看图 → 解释必要公式 → 讲 C# 语法/API → 实作 → 观察结果 → 验收”的顺序进行。每次只发当前阶段；学员提交结果后只验收当前阶段，不提前发布下一阶段教程。

| 阶段 | 主要内容 | 对应主线 | 可交付结果 |
|---|---|---|---|
| MSR-01 | CPU Framebuffer 与屏幕空间三角形 | D02、D03 | 一张可由 C# 清屏、画点并填充三角形的 Texture2D |
| MSR-02 | Mesh 顶点、索引与 Object→World | D03、D05 | 手写三角形可平移/旋转/缩放；能读出导入 Mesh 的顶点和三角形索引 |
| MSR-03 | 自己的 Software Camera、View/Projection、透视除法、Viewport | D05、D06 | 相机参数变化时，模型能正确投影到像素画布；关键空间坐标可视化 |
| MSR-04 | Clip Space 裁剪、三角形覆盖、重心坐标插值 | D03、D06、D10 | 跨越视锥边界的三角形只保留可见部分；能显示插值颜色并解释顶点如何影响像素 |
| MSR-05 | Depth Buffer、遮挡、UV 与纹理采样 | D07、D08、D10 | 近物体正确遮挡远物体；纹理正确显示；能观察普通插值与透视校正插值的区别 |
| MSR-06 | 导入 FBX Mesh、整条管线整合与 Debug 对照 | D03、D05–D08、D10 | 在 CPU 软件渲染器中显示一个导入模型；附坐标流程图、关键 Debug 图和差异分析 |

### MSR-01｜CPU Framebuffer 与屏幕空间三角形

**为什么从这里开始：** 先不引入 Camera 和矩阵，避免同时处理多个新问题。先确认 CPU 能把颜色写到像素缓冲，再将结果显示出来。

**实现顺序：**
1. 创建固定尺寸的 Texture2D 和 Color32 像素数组。
2. 实现 Clear、SetPixel/写数组、Apply，并将纹理显示在 RawImage 或简单材质上。
3. 用二维屏幕坐标画点。
4. 填充一个屏幕空间三角形；先用清楚易懂的边界判断，再讨论效率和边缘规则。

**必须解释的 C# / Unity 内容：** class、字段、数组、for 循环、方法、参数、返回值、Color32、Texture2D、Apply 的作用与调用时机。每个 API 都说明输入、输出和常见错误。

**验收：**
- 运行时看到纯色背景和指定颜色的三角形。
- 改动一个顶点坐标后，三角形按预期变化。
- 能解释：当前三角形顶点是屏幕像素坐标，不是世界坐标，也没有经过 Camera 投影。

### MSR-02｜Mesh 数据与 Object→World

**为什么做：** 模型顶点首先属于模型自身的局部坐标；要让模型出现在世界里，必须应用它的位置、旋转和缩放。

**实现顺序：**
1. 用三个手写顶点作为最小 Mesh。
2. 显示顶点编号、局部坐标和变换后的世界坐标。
3. 先用明确的 Scale → Rotation → Translation 步骤建立直觉，再写成矩阵形式。
4. 读取 Unity 导入 Mesh 的 vertices 与 triangles；先只读取、打印和可视化，不急着一次接入全部渲染流程。
5. 对照 Unity Transform 与 TransformObjectToWorld 的职责。

**必须讲清：** positionOS/positionWS 的含义、点与方向的区别、矩阵乘法的顺序、旋转/缩放/平移为什么会影响位置，以及 Mesh 顶点数组与三角形索引如何对应。

**验收：**
- 平移、旋转、缩放后，CPU 输出坐标与预期一致。
- 能解释一个三角形索引如何引用 vertices 数组中的三个顶点。
- 不把导入 Mesh 的顶点误认为已经处于世界空间。

### MSR-03｜Software Camera 与投影

**为什么做：** Object→World 只决定模型在世界中的位置；Camera 决定从哪里观察，Projection 决定三维位置如何映射到二维成像平面。

**实现顺序：**
1. 明确并画出 World、View、Clip、NDC、Screen 五种空间。
2. 实现 Camera 的位置、朝向、视野角、宽高比、近远裁剪面。
3. 实现 View Matrix 和 Perspective Projection Matrix；先解释各矩阵负责什么，再实现代码。
4. 将 Clip Space 坐标做透视除法得到 NDC，再做 Viewport Transform 映射到像素坐标。
5. 用图示标出每一步的坐标和数据流；用 Unity 的矩阵/Camera 输出作参照检查。

**技术约定：** 实现前先明确矩阵乘法约定、向量采用行向量还是列向量、相机前向、Clip Space 深度范围与屏幕 Y 轴方向。不要在未说明约定时混用不同图形 API 的投影矩阵。Unity 的 Camera.projectionMatrix 与实际 GPU 投影矩阵可能存在平台转换，比较时必须注明使用的是哪一种。

**验收：**
- 改相机位置/朝向后，模型投影按预期变化。
- 改 FOV 时，模型屏幕占比变化方向正确。
- 能说清透视除法与视口变换不是同一步：前者把齐次裁剪坐标转换为 NDC，后者将 NDC 映射到像素坐标。

### MSR-04｜Clip Space 裁剪、光栅化与插值

**为什么做：** 视锥外的整个物体可以被 CPU 的包围体剔除；但跨越视锥边界的三角形不能简单整块丢弃，必须裁掉不可见部分并生成新的边界顶点。

**实现顺序：**
1. 在 Clip Space 检查三角形是否完全在某个裁剪平面外。
2. 对跨越裁剪平面的三角形执行裁剪，生成新的顶点/三角形。
3. 对裁剪后的顶点做透视除法与视口映射。
4. 在像素范围内判断三角形覆盖关系。
5. 用重心坐标对顶点颜色等属性进行插值。
6. 展示裁剪前后线框、三角形顶点和屏幕覆盖区域。

**必须讲清：** CPU 视锥剔除通常使用 Renderer 的包围体快速排除整个物体；GPU 的几何裁剪处理单个图元与裁剪平面的相交关系。二者目标相近但粒度不同。还要解释 Clip Space 与 NDC 的区别，以及为什么不能在所有裁剪完成前就直接把顶点除以 w。

**验收：**
- 三角形完全在视锥外时不显示。
- 三角形跨越近裁剪面或屏幕边界时，只显示被保留的部分，而不是整块消失。
- 顶点颜色沿三角形内部连续变化。
- 能解释重心坐标代表什么，以及属性插值为何发生在光栅化阶段。

### MSR-05｜Depth Buffer、UV 与纹理

**为什么做：** 多个三角形覆盖同一像素时，需要决定哪个表面可见；纹理则要求将三角形顶点上的 UV 可靠地传递到每个像素。

**实现顺序：**
1. 为每个像素维护独立的深度值。
2. 每个片元先进行深度比较，通过后才更新颜色和深度。
3. 为顶点增加 UV，并实现最简单的纹理采样。
4. 对比屏幕空间线性插值 UV 与透视校正插值 UV。
5. 将遮挡错误、深度方向反转和 UV 透视失真分别做成可复现对照。

**验收：**
- 交换三角形提交顺序后，不透明物体仍按深度正确遮挡。
- 纹理能按 UV 显示。
- 能解释为什么透视投影后，直接对 UV 做普通屏幕空间线性插值通常会产生透视失真。

### MSR-06｜导入 FBX 与完整管线

**为什么做：** 前面各阶段分别验证了数据和算法；最后把它们接起来，确认理解可以处理真实网格，而不只是一个手写三角形。

**实现顺序：**
1. 通过 Unity FBX Importer 导入一个简单模型。
2. 从 MeshFilter.sharedMesh 读取顶点、三角形索引和 UV；处理模型多个子网格的情况时先记录限制，不擅自假设只有一个 submesh。
3. 依次执行 Object→World→View→Projection→Clip→Perspective Divide→Viewport→Rasterization→Depth Test→Texture。
4. 用 Debug 开关显示局部/世界/裁剪/屏幕坐标、线框、三角形编号和深度。
5. 与 Unity 正常 GPU 渲染的同一模型并排比较；出现差异时按管线阶段逐一定位。

**验收：**
- 一个 FBX 模型能由 CPU 软件渲染器绘制到 Texture2D。
- 相机移动、模型变换、裁剪和遮挡都通过基础测试。
- 提交流程图、运行截图、至少一个故障复现与修复记录。
- 能明确列出与 Unity GPU 渲染仍然不同的功能和限制。

## 4. 推荐代码结构

以下是建议结构；开始实施时以现有项目实际目录为准，不要求提前创建所有文件。

    Assets/
      SoftwareRendererLab/
        Runtime/
          SoftwareRenderer.cs
          SoftwareCamera.cs
          SoftwareMeshSource.cs
          SoftwareRasterizer.cs
          SoftwareDepthBuffer.cs
        Scenes/
          SoftwareRendererLab.unity
        Materials/
        Tests/
        Notes/

按阶段只创建当阶段需要的文件。不要在 MSR-01 就预先建立一套复杂框架。短函数和短表达式保持紧凑；新增 C# 语法必须同时解释，不能只发代码要求复制。

## 5. 与原课程的对应关系

| 原课程 | 软件渲染器提供的补强 | 原课程仍必须独立完成的内容 |
|---|---|---|
| D02 CPU/GPU、Camera、Culling | 看清 CPU 逐步处理数据的方式 | 真正 GPU 管线职责、Draw Call、URP 帧流程 |
| D03 Mesh、顶点到像素 | 顶点、索引、裁剪、像素覆盖 | Unity GPU 绘制命令、Shader 输入输出 |
| D05 向量与坐标空间 | 坐标空间可视化与转换 | 将概念映射到 Unity/HLSL API |
| D06 矩阵和投影 | 手写 View/Projection 与屏幕映射 | Unity 矩阵约定、GPU 投影差异与 Shader 实验 |
| D07 深度/模板缓冲 | CPU 深度缓冲直觉 | Unity 深度/模板状态、ZTest/ZWrite |
| D08 颜色空间与 Alpha 混合 | 可选扩展为 CPU Alpha 混合 | Linear/sRGB、GPU Blend State、URP 透明排序 |
| D09–D12 Shader/HLSL | 可用同一数据流做对照 | ShaderLab、Vertex/Fragment HLSL、真实 GPU 执行 |
| D13–D28 URP/Render Graph | 仅提供背景理解 | Renderer Feature、Pass、资源依赖、描边项目，不能由 CPU 渲染器替代 |
| D31–D40 Compute/性能/毕业项目 | 对比 CPU 与 GPU 的工作分工 | Compute Shader、Profiler、GPU 捕获和完整 URP 毕业作品 |

## 6. 嵌入原课程的节奏

不新增强制的 D 编号，不把原有 40 节和 8 次验收删除。MSR 阶段安排在相关基础课之后：

- D02–D03：先完成一帧渲染流程和 Mesh/顶点到像素的概念，再做 MSR-01。
- D05：学习向量与 Object/World 空间后做 MSR-02。
- D06：学习矩阵与投影后做 MSR-03；随后做 MSR-04 的裁剪和插值部分。
- D07–D08、D10：学习深度、颜色和 Shader 后做 MSR-05。
- D03、D05–D08、D10 的知识有足够基础后做 MSR-06，作为小项目综合验收。

每个 MSR 阶段预计 2–4 小时，总计约 14–22 小时；允许跨多个学习时段。它纳入原有每周约 20 小时的学习预算，通过减少重复的纯口头讲解、将概念直接落实为实验来安排，不要求把每天的学习时间无限延长。若实际实现超出预算，优先保证理解和验收，调整后续日期，不跳过核心知识点。

## 7. 通用验收规则

每个阶段都要有以下证据：

- 修改前预测：说出改变某参数后应该发生什么。
- 运行结果：截图或可观察画面。
- 关键数据：必要时显示顶点坐标、Clip Space 的 w、NDC、屏幕像素坐标或深度。
- 原理复述：能说明输入、处理步骤、输出和常见失败原因。
- 排错记录：至少记录实际遇到的一个错误；若没有遇到真实错误，不为了完成模板而故意伪造 Bug。

只有当前阶段通过才进入下一阶段。没有验证的功能写“待验证”，不写“已完成”。

## 8. 参考资料

优先使用资料解释当前正在学习的单个主题；每次教学时再补充具体阅读章节/观看重点，避免一次丢出大量链接。

- Unity Mesh API：https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Mesh.html
- Unity Matrix4x4 API：https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Matrix4x4.html
- Unity Texture2D.SetPixels32：https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Texture2D.SetPixels32.html
- Unity Camera API：https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Camera.html
- Scratchapixel Rasterization：https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation
- Scratchapixel Perspective-Correct Interpolation：https://www.scratchapixel.com/lessons/3d-basic-rendering/rasterization-practical-implementation/perspective-correct-interpolation

这些资料是辅助阅读；课程仍会逐段解释术语、公式、C# 语法和 API，并用实验验收，不要求学员独立啃完所有文档。

## 9. 当前执行起点

- 已有基础：用户报告 Shader 编译成功、材质应用成功、Transform 实验正常，并理解 TransformObjectToWorld 的职责。
- 课程状态：MSR-01 进行中。已通过 CPU Framebuffer/Texture2D 显示、四象限颜色绘制、数组索引与二维坐标转换、SetPixel 安全边界检查等子步骤；用户报告合法像素写入成功、(-1,0) 被警告拦截且无数组越界异常、恢复合法坐标后显示正常。
- 未完成：MSR-01 整体尚未验收；屏幕空间三角形绘制尚未开始。
- 下一步：先整理现有代码（移除不必要的 Update() 每帧重复绘制、删除未使用的 bg 字段），并讲解/验证 Texture2D 的生命周期与 OnDestroy() 清理；不要提前发布三角形绘制教程，等待用户明确继续。
- 证据边界：目前进度基于用户文字验收反馈；未收到 Unity 截图、项目文件或 Git commit，不得声称这些已上传。


## 10. 分阶段授课与缺课续接规则

每日教学必须遵守 [TA_TEACHING_RULES.md](https://github.com/kitterr/URP_lesson/blob/study/urp-ta-course-memory-2026/TA_TEACHING_RULES.md)：每次只展开当前阶段，完成操作并验收后才进入下一阶段。如果仓库/学习日志没有前一天当前阶段的明确通过记录，次日重复同一阶段并提醒“课程进度已落后”，不因日期变化自动推进。当前阶段若未完成，可跨多个日历日；不为赶进度压缩掉解释、操作或验收。