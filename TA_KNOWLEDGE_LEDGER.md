# TA 知识掌握台账 — Unity 6 URP Rendering TA

> 更新日期：2026-11-10。用于跨对话区分“教过什么”和“用户是否掌握”，避免把只提过的术语当成已学会。详细日期和证据见对应 GPU/MSR 学习日志。

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
| UnityPerMaterial / CBUFFER 布局与 SRP Batcher 关系 | 未确认系统教学 | 不得默认用户已掌握；在 GPU-04/HLSL 与 SRP Batcher 相关模块核实并补讲 |

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
3. **透明渲染**：Alpha Blend、ZTest、ZWrite、排序、深度写入造成的透明层消失问题。
4. **DepthOnly / Forward 一致性**：Alpha Clipping、顶点动画、位移、双面/剔除等如何确保预通道与颜色 Pass 的几何和裁剪行为一致。
5. **SV_Depth**：输出机制、与深度测试的关系、为什么可能限制传统 Early-Z、与 Depth Priming Equal 的冲突风险。
6. **clip/discard**：动态丢弃对 Early-Z、深度更新和不同 GPU 路径的影响；避免绝对化结论。
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
