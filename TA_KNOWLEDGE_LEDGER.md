# TA 知识掌握台账 — Unity 6 URP Rendering TA

> 更新日期：2026-11-17。用于跨对话区分“教过什么”和“用户是否掌握”，避免把只提过的术语当成已学会。详细日期和证据见对应 GPU/MSR 学习日志。

## 1. 状态定义

| 状态 | 含义 |
|---|---|
| 未引入 | 尚未教学 |
| 已提及 | 只提到名称、简短说明或出现在题目中 |
| 已系统教学 | 原理、数据流、用途和边界已完整讲解 |
| 理解已确认 | 用户通过解释、推理或针对性问答确认理解 |
| 实践已验收 | 用户实际运行/操作并提供足够证据，检查通过 |
| 主动跳过/延后 | 用户选择暂不实践；不代表失败，也不代表已经掌握 |

状态可并列记录。例如“理论已系统教学；实践主动延后”。不能用一个笼统的“完成”覆盖不同证据层级。

## 2. GPU-01：GPU Pipeline 与 Shader 数据流

| 知识点 | 当前状态 | 记录与边界 |
|---|---|---|
| CPU 提交 Draw Call、Vertex Shader、Primitive Assembly、Rasterization、Fragment Shader 的基本职责 | 理解已确认 | 已讲解 GPU 图形流水线，用户完成相关基础问答 |
| Attributes 与 HLSL Semantic 对应 Mesh 顶点数据 | 理解已确认 | POSITION / NORMAL / TEXCOORD 等语义已讲解 |
| Houdini string Attribute 不能直接作为普通 GPU 顶点输入 | 理解已确认 | 应在导出链映射为数值 ID/向量等 Mesh 通道，CPU 侧保留名称与 ID 映射 |
| Varyings 从 Vertex Shader 传到 Fragment Shader | 理解已确认、实践已验收 | 用户运行 URP Unlit Shader，以 UV 驱动颜色渐变并提供截图 |
| UV 通道与 TEXCOORD 语义映射 | 已系统教学 | UV4 与 TEXCOORD3 的对应关系已讲解 |
| 非线性 sin(uv.x * 10) 在 Vertex / Fragment 阶段计算的差异 | 已系统教学、实践已验收（有限范围） | 用户在 Unity 默认 Plane 网格上运行两种方案并提供截图；没有完成低细分 Quad 的严格对照，不能声称已量化插值误差 |
| 透视正确插值的基础直觉 | 已系统教学（基础层） | CPU 光栅化原理课程已覆盖；GPU HLSL 插值修饰符与具体 Semantic 细节仍需补充 |
| noperspective / centroid / sample 等 HLSL 插值修饰符 | 未系统教学 | 第三轮 Q2 属于超出教学范围的诊断题；用户选 centroid，正确概念为 noperspective 对应非透视校正屏幕线性插值 |
| GPU Instancing 基础与 Instancing Buffer / 每实例属性 | 未系统教学 | 第三轮 Q1 用户正确选择 Instancing Buffer，属于推理正确，不等于已经教学或实践 |
| UnityPerMaterial / CBUFFER 布局与 SRP Batcher 关系 | **实践已验收（GPU-04-B）** | 已独立实现材质属性，故意移动到 CBUFFER 外验证 Inspector 的 Not Compatible，恢复 Compatible；详见 GPU_LOG_2026-11-15.md |

## 3. GPU-02：Depth Test、ZWrite 与绘制顺序

| 知识点 | 当前状态 | 记录与边界 |
|---|---|---|
| ZTest / ZWrite 基本职责 | 实践已验收 | 已完成红/蓝 Quad 的 A/B/C 对照 |
| ZTest LEqual、Always 与 ZWrite Off 的效果 | 实践已验收（对应实验状态） | 不等于所有深度状态组合都已学会 |
| Frame Debugger 检查 Draw Event 与绘制顺序 | 实践已验收 | 用户确认红色先绘制、蓝色后绘制 |
| 普通不透明物体通常倾向于 Front-to-Back 排序 | 已系统讲解（概念层） | 讨论了原因与局限；没有完成可控的顺序 A/B 性能实验 |
| Camera.opaqueSortMode、Render Queue 控制思路 | 已提及/基础讲解 | 没有独立编写控制脚本或完成真实顺序对照 |
| 透明物体 Back-to-Front 混合顺序与排序约束 | 已提及，未系统教学 | 需在透明渲染专题完整讲解 |
| 透明物体 ZWrite On 的遮挡风险 | 未系统教学 | 第三轮 Q3 为诊断题；正确机制是先绘制的透明片元可能写深度并阻挡之后更远的透明层 |

## 4. GPU-03：深度预处理、Early-Z 与 Overdraw

| 知识点 | 当前状态 | 记录与边界 |
|---|---|---|
| Reversed-Z 基本方向与精度动机 | 已系统教学、理解已确认 | 近处深度通常接近 1，远处接近 0；未单独做精度量化实验 |
| Depth Prepass 的位置与用途 | 已系统教学、实践已验收 | 用户在 Frame Debugger 观察 Draw Depth Only / DepthPrepass 位于 Opaque Color Pass 前 |
| Depth Prepass 可独立于 Depth Priming 存在 | 理解已确认、实践已验收 | Force Prepass 下，Depth Priming Disabled 时仍发现预通道 |
| Depth Priming Auto 与 Disabled 的差异 | 实践已验收（有限范围） | Auto 下预通道仍存在、Opaque 正常；用户观察 Disabled 的 ZTest LessEqual、Auto 的 ZTest Equal |
| Depth Priming 的 ZWrite 状态 | 未验证 | 用户截图没有提供完整 ZWrite 对照，不得记录为已验证 |
| Early-Z 基本原理 | 已系统教学 | 作为 GPU 硬件尽可能提前进行深度拒绝的优化讲解；没有测量真实硬件 Early-Z 命中/拒绝次数 |
| Early-Z 与 Depth Prepass / Depth Priming 的区别 | 已系统教学、理解已确认 | 已明确三者分别属于 GPU 硬件优化、URP 独立深度 Pass、URP 利用预通道深度的策略 |
| Overdraw 基本概念 | 已系统教学 | 已区分几何覆盖、最终可见表面与实际 Fragment Shader 执行次数 |
| Rendering Debugger Overdraw 可视化 | 已尝试，结果不具判别力 | Opaque 模式下五个 Quad 重叠仍显示蓝色/计数 1；不能由此推断真实 Shader 执行次数或 Early-Z 行为 |
| Overdraw / Early-Z 合成性能对照 | 主动跳过/延后 | 用户决定未来在真实项目场景中按具体瓶颈学习工具，不再为课程制造五层 Quad 基准测试 |
| GPU Frame Time 与 FPS 的基础关系 | 已系统教学（基础层） | 已讲解 ms 与理论帧率倒数关系；未进行实测 |
| VSync 对 GPU 优化收益与显示帧率的影响 | 未系统教学 | 第三轮 Q8 用户答对诊断题，但不能因此标记为已教学 |

## 5. 尚未系统教学的重点清单

以下主题未来应在相关课程中先讲解，再用于正式测验：

1. **GPU Instancing**：实例化绘制机制、实例 ID、Instancing Buffer、每实例属性、与 SRP Batcher 的区别和适用条件。
2. **HLSL 插值控制**：linear/perspective-correct、noperspective、nointerpolation、centroid、sample；何时使用及平台限制。
3. **透明渲染**：2026-11-16 已在 GPU-04-E 系统讲解并确认核心概念；部分独立实测、复杂几何与平台约束仍待实践，详见第 9 节。
4. **DepthOnly / Forward 一致性**：Alpha Clipping、顶点动画、位移、双面/剔除等如何确保预通道与颜色 Pass 的几何和裁剪行为一致。
5. **SV_Depth**：输出机制、与深度测试的关系、为什么可能限制传统 Early-Z、与 Depth Priming Equal 的冲突风险。
6. **clip/discard**：2026-11-16 已确认 Alpha=0 与 clip 丢弃的核心区别；Early-Z 和 GPU 平台细节仍待深入，避免绝对化结论。
7. **性能测量**：GPU Frame Time、CPU/GPU bound、VSync、分辨率缩放、采样波动、GPU Profiler/RenderDoc 的证据边界。

## 6. 最新诊断测验快照（2026-11-10）

第三轮 GPU-01～03 综合进阶测验：**5/8**。由于多题涉及尚未系统教学的知识，本轮仅作为诊断，不作为正式课程成绩。

- 答对：GPU Instancing Buffer（Q1，正确推理但未教学）；DepthOnly/Forward Alpha Clipping 一致性（Q4，正确推理但未教学）；SV_Depth 与 Equal 深度匹配（Q5，正确推理但未教学）；分辨率双轴翻倍的像素数量变化（Q7）；VSync 下 GPU Frame Time 下降仍有价值（Q8，正确推理但机制未教学）。
- 答错：noperspective 与 centroid（Q2，未系统教学）；透明粒子 ZWrite On（Q3，未系统教学）；Early-Z 限制因素（Q6，相关限制此前仅被提及）。
- 不把这些失分记为用户已教知识的退步；应先补齐课程，再安排正式测验。

## 7. 记录规则

- 新知识教学后更新对应状态；用户完成实际操作后再升级为实践已验收。
- 测验题如超出教学范围，标记为诊断题并从正式成绩中剔除。
- 用户主动跳过的实验记为主动跳过/延后，不反复要求补做。
- 新对话优先读取本台账与最新学习日志；不得仅凭模型记忆推断用户掌握程度。


## 8. GPU-04：ShaderLab/HLSL、光照、SH 与 Shader Variant（2026-11-15 新增）

| 知识点 | 当前状态 | 记录与边界 |
|---|---|---|
| Unlit Shader、Attributes/Varyings、TransformObjectToHClip、Frame Debugger 找到 Draw Call | **实践已验收** | GPU-04-A，用户编译并让 Cube 显示红色 |
| _BaseColor / UnityPerMaterial / SRP Batcher | **实践已验收** | GPU-04-B，Material Compatible↔Not Compatible 实验 |
| Texture2D / SamplerState / UV | 代码审查通过；部分运行待确认 | GPU-04-C1 单独运行/Batch 未全部确认 |
| Tiling / Offset | 概念与视觉实验通过；部分待确认 | GPU-04-C2 Clamp/Batch 未独立确认 |
| Bilinear / Trilinear / Mipmap | 已系统教学，概念理解确认 | GPU-04-C3 精细视觉对照未全部确认 |
| Anisotropic Filtering | **实践已验收** | GPU-04-C4 倾斜地面 Aniso 1x/16x 对照 |
| 世界空间法线可视化 | 有运行验证，最终细节待确认 | GPU-04-D1 Sphere/Cube 可见；TEXCOORD 和 frag normalize 修正后旋转运行未全部单独验收 |
| 非均匀缩放与逆转置法线矩阵 | **理解与实践已验收** | GPU-04-D2 Unity 对照 WorldDir vs WorldNormal |
| Lambert 漫反射、dot/saturate | **实践已验收** | GPU-04-D3 用户编写 Shader，反馈实际运行符合预期 |
| 直接光 + 固定 Ambient 光 | 主要公式、视觉已验收；细节待确认 | GPU-04-D4 修正 `baseColor * (directLight + ambientLight)`；该节 SRP Batcher 未独立核查 |
| SampleSH 基础调用 | **运行已确认** | GPU-04-D5 替换手写环境光后正常显示 |
| SH 基函数/系数/L0-L1-L2 | **基础理解已确认** | 用户掌握基函数变化与系数 RGB 作用，不再重复简单计算测验 |
| Unity SH 数据变量、SHEvalLinearL0L1 / SHEvalLinearL2 | 已系统教学，源码实验未验收 | GPU-04-D5 Gradient 方向验证和源码拆分独立实验亦未验收，不把全节标为通过 |
| Vertex SH、Pixel SH、Mixed 机制 | 已系统教学 | GPU-04-D6 Inspector 模式切换成功；画质/性能实测待确认 |
| Toggle + shader_feature_local Shader Keyword | **实践已验收** | Inspector 成功在两模式之间切换 |
| Local Keyword + Variant 在 Frame Debugger 中检查 | **实践已验收** | GPU-04-D7 Vertex Draw Call 有 TA_SH_VERTEX；Pixel Draw Call 展开 Keywords 无该项 |
| SRP Batch 可见 | 绘制状态已观察 | 不能推出真实 GPU 性能收益 |
| CPU/GPU Frame Time、Profiler、Vertex vs Pixel SH A/B 性能 | **GPU-04-D8 主动跳过/延后** | 用户决定后续结合真实复杂 URP 项目的实际性能瓶颈学习；目前无可靠 GPU 耗时测量 |

2026-11-15 时点的恢复指针是 `GPU_LOG_2026-11-15.md`、`TA_HANDOFF_2026-11-15.md`；已被第 9 节的 2026-11-16 最新指针覆盖。单独输入 `1` 表示继续；跳过已理解的简单算术反复验收。


## 9. GPU-04-E / F：渲染状态、Fresnel、Scene Depth（2026-11-16 最新）

| 知识点 | 当前状态 | 记录与边界 |
|---|---|---|
| GPU-04-E1 Cull 与 ZTest | **概念理解已确认** | 屏幕空间三角形绕序 vs 法线、Always 与绘制顺序；Unity 独立运行未验收 |
| GPU-04-E2 ZWrite/Depth | **概念理解已确认** | 普通透明通常 LEqual + ZWrite Off，Always 非默认方案；实验未确认 |
| GPU-04-E3 透明排序 | **概念理解已确认** | Renderer 级远近排序和相交面局部排序错误；未逐项运行验收 |
| GPU-04-E4 Alpha/Additive/Premultiply Blend | **概念理解已确认** | Fragment 内 RGB*Alpha 与加法混合；实验未确认 |
| GPU-04-E5 clip/Alpha Test | **概念理解已确认** | 片元丢弃与深度更新；实验未确认 |
| GPU-04-E6 Stencil Ref/Comp/Pass/Fail/ZFail | **概念理解已确认** | Equal/NotEqual 排除区域；实验未确认 |
| GPU-04-E7 Stencil+Depth X-Ray | **概念理解已确认** | Greater 深度判断配合模板过滤；平台 Reversed-Z 实际状态及复杂 Mesh 仍需验证 |
| GPU-04-F1 Fresnel dot(N,V)、反转与 pow | **概念通过** | 用户正确描述中心/边缘亮度反转；自写 Shader 的法线输入错误已指导修复，F1 独立 Unity 运行未确认 |
| GPU-04-F2 Fresnel + Alpha Blend 护盾 | **实践已验收** | 用户回答 Alpha=0 与 clip 区别，明确确认 Unity 实际运行效果符合预期 |
| GPU-04-F3 Depth Buffer vs _CameraDepthTexture | **原理理解已确认** | 可采样场景深度不一定与当前写入的 Depth Buffer 是同一个资源 |
| GPU-04-F3 屏幕 UV、RawDepth、LinearEyeDepth 与 View 深度 | **已系统教学，整体数据流理解已确认** | GetNormalizedScreenSpaceUV、SampleSceneDepth、_ZBufferParams、-positionVS.z；尚未独立 Unity Debug 验证 |
| GPU-04-F3 depthDiff / intersection 权重 | **理解已确认** | 用户自行复述：设定 Width 范围，眼空间深度差越小，权重越大；屏幕空间近似而非三维 Mesh 相交检测 |
| GPU-04-F3 TA_FresnelDepth Shader/四个 Debug 模式 | **参考代码已提供，Unity 编译/运行待验收** | Final/RawDepth/EyeDepth/Intersection；未取得用户运行结果 |
| GPU-04-D8 GPU 性能分析 | **主动跳过/延后** | 留到真实项目，无性能结论 |

**2026-11-16 历史恢复指针**：当时 F3 尚待运行；后续用户截图与 Debug 反馈已更新，以下第 10 节为当前状态。


## 10. GPU-04-F3/F4、GPU-05 与 C# 前置能力（2026-11-17 当前有效）

| 模块 | 状态 | 证据与边界 |
|---|---|---|
| GPU-04-F3 Scene Depth 与交界发光 | **实践已验收** | 用户上传交界发光截图，后续确认 Debug/效果正确；没有逐模式独立截图，不扩大证据表述 |
| GPU-04-F4 Depth Prepass / Copy Depth / Depth Priming | **实践已验收（当前项目配置）** | Frame Debugger 对照截图：在关闭 Features/Priming 后 After Opaques 的 Opaque→CopyDepth、Force Prepass 的 DepthOnly→Opaque |
| GPU-05-A URP / Render Graph 架构 | **已系统教学** | 讲过 Asset/Renderer/Feature/Pass、依赖、记录与执行，尚未进行独立 C# 实践验收 |
| GPU-05-B ZoomPass Renderer Feature | **参考代码已提供，暂停/未验收** | 用户明确反映读不懂代码，没有提供 Unity 编译或 Render Graph Viewer 结果 |
| C# 类型/变量/类/字段/参数/方法 | **CSharp-01 简单代码静态验收通过** | ZoomPass 构造函数、字段、SetScale/GetScale/ResetScale；重置方法第二次改对。Unity 实机未确认 |
| C# 对象/构造函数/继承/override | **基础对象创建与带参构造函数代码通过；继承/override 待专项训练** | 已写 new ZoomPass、构造函数与实例方法；Unity Create() 框架回调机制尚未实战验收 |
| Unity API 阅读、泛型/ref-out/Lambda/using | **需系统补强** | 不能因为之前的参考代码出现过就标记理解 |
| GPU-04-D8 Profiling | **主动延后** | 留到真实项目，无性能数据 |

**2026-11-17 晚间之前的恢复指针**：当时下一节为 CSharp-01；当前已进入下方第 11 节，以最新状态为准。


## 11. 2026-11-17 晚间 C# 学习增量（当前有效）

| 分节/知识点 | 最新证据等级 | 限制与证据 |
|---|---|---|
| CSharp-01：class / 字段 / 方法 / 参数 / 返回值 / 带参构造函数 | **基础静态代码验收通过** | 用户自己提交 ZoomPass/CSharp01Demo，并将 ResetScale(float) 修正为无参数复位 1.0f；实际未证明在 Unity 编译运行；Start() 未同时 reset a、b |
| CSharp-02：值类型/引用类型、共享对象与复制 | **理解已确认** | 用户自己解释 a=b 的值/引用差别，认识字符串、数组、类、字典的类型特点；“值类型=栈”的误区已纠正 |
| CSharp-02：方法参数按值传递与引用参数 | **理解已确认** | 用户解释 Replace(a) 重新赋值内部 pass 不改变外部 a 的引用；“改对象”与“改引用变量”区别正确 |
| CSharp-02：struct 与 class、Vector3 与 Transform | **理论已系统讲解；独立代码实践待验收** | 未见用户独立提交自定义 struct/class 或 Unity 运行结果 |
| CSharp-02：ref 作为变量别名 | **已系统讲解，独立练习未验收** | 用户确认无 ref 时引用变量独立；尚未提交有 ref 的实际例子 |
| CSharp-03：ref / out / in 与方法签名、API 阅读 | **下一课；尚未验收** | 先补 ref/out/in 与陌生 API 读法，后续再深挖 OOP / 泛型 / Lambda |

**最新恢复位置**：**CSharp-03**。时间估计仅为假设：剩余约 20–35 小时有效学习与编码调试；按每周约 12–16 小时安排 C#，约 2–3 周可尝试回到 GPU-05-B，必须经实际能力验收，不把概念速度当实战速度。详见 [`CSHARP_FOUNDATION_BRIDGE_PLAN.md`](CSHARP_FOUNDATION_BRIDGE_PLAN.md) 与 [`GPU_LOG_2026-11-17.md`](GPU_LOG_2026-11-17.md)。
