
using UnityEngine;
using UnityEngine.UI;

public class SoftwareFramebufferDemo : MonoBehaviour
{
    [Min(1)] public int width = 128;
    [Min(1)] public int height = 128;
    public RawImage output;

    [Header("Transform")]
    [Range(0.1f, 1f)] public float scale = 1f;
    public float rotationSpeed = 6f;

    private Texture2D framebuffer;
    private Color32[] pixels;

    // 原始顶点永远不被矩阵计算结果覆盖
    private Vector2[] originalPoints;
    private Vector2[] points;

    private float angleDegrees = 0f;

    private void Start()
    {
        if (output == null)
        {
            Debug.LogError("RawImage 没有指定", this);
            enabled = false;
            return;
        }

        if (width <= 0 || height <= 0)
        {
            Debug.LogError("Framebuffer 的宽高必须大于 0", this);
            enabled = false;
            return;
        }

        // 创建 CPU Framebuffer
        framebuffer = new Texture2D(width, height, TextureFormat.RGBA32, false);
        framebuffer.filterMode = FilterMode.Point;
        framebuffer.wrapMode = TextureWrapMode.Clamp;

        pixels = new Color32[width * height];

        // 原始顶点：当前使用屏幕像素坐标
        originalPoints = new Vector2[]
        {
            new Vector2(10f, 10f),
            new Vector2(100f, 120f),
            new Vector2(0f, 111f)
        };

        points = new Vector2[originalPoints.Length];

        // RawImage 只需绑定一次纹理
        output.texture = framebuffer;

        RenderFrame();
    }

    private void Update()
    {
        // 帧率无关的角度累积
        angleDegrees = Mathf.Repeat(angleDegrees + rotationSpeed * Time.deltaTime, 360f);

        // 本实验的 framebuffer 尺寸在 Play 期间保持不变
        if (width != framebuffer.width || height != framebuffer.height)
        {
            Debug.LogWarning("请退出 Play 模式后再修改 Framebuffer 尺寸", this);
            enabled = false;
            return;
        }

        RenderFrame();
    }

    private void RenderFrame()
    {
        // 清除上一帧颜色
        BgColored();

        // 构建本帧完整变换矩阵
        Matrix4x4 transform = BuildTransformMatrix();

        // 每次都从原始顶点重新计算
        for (int i = 0; i < originalPoints.Length; i++)
        {
            Vector3 source = new Vector3(originalPoints[i].x, originalPoints[i].y, 0f);
            Vector3 result = transform.MultiplyPoint3x4(source);
            points[i] = new Vector2(result.x, result.y);
        }

        DrawTriangle(points[0], points[1], points[2], new Color32(0, 0, 0, 255));
        ApplyBuffer();
    }

    private Matrix4x4 BuildTransformMatrix()
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        // 旋转矩阵 R
        Matrix4x4 rotation = Matrix4x4.identity;
        rotation.m00 = cos;
        rotation.m01 = -sin;
        rotation.m10 = sin;
        rotation.m11 = cos;

        // 缩放矩阵 S
        Matrix4x4 scaleMatrix = Matrix4x4.identity;
        scaleMatrix.m00 = scale;
        scaleMatrix.m11 = scale;

        // 旋转和缩放的中心：Framebuffer 中心
        Vector3 pivot = new Vector3(width * 0.5f, height * 0.5f, 0f);

        Matrix4x4 toOrigin = Matrix4x4.Translate(-pivot);
        Matrix4x4 fromOrigin = Matrix4x4.Translate(pivot);

        // 从右向左执行：移到原点 → 缩放 → 旋转 → 移回
        return fromOrigin * rotation * scaleMatrix * toOrigin;
    }

    private void BgColored()
    {
        Color32 red = new Color32(255, 0, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 blue = new Color32(0, 0, 255, 255);
        Color32 yellow = new Color32(255, 255, 0, 255);

        for (int i = 0; i < pixels.Length; i++)
        {
            int x = i % width;
            int y = i / width;

            if (x < width / 2 && y < height / 2)
                pixels[i] = red;
            else if (x >= width / 2 && y < height / 2)
                pixels[i] = green;
            else if (x < width / 2 && y >= height / 2)
                pixels[i] = blue;
            else
                pixels[i] = yellow;
        }
    }

    private void ApplyBuffer()
    {
        framebuffer.SetPixels32(pixels);
        framebuffer.Apply(false, false);
    }

    private void SetPixel(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogError("坐标超出范围");
            return;
        }

        int index = y * width + x;
        pixels[index] = new Color32(0, 0, 0, 255);
    }

    private float Edge(Vector2 a, Vector2 b, Vector2 p)
    {
        return (b.x - a.x) * (p.y - a.y)
             - (b.y - a.y) * (p.x - a.x);
    }

    private void DrawTriangle(Vector2 a, Vector2 b, Vector2 c, Color32 color)
    {
        float area = Edge(a, b, c);

        if (Mathf.Approximately(area, 0f))
        {
            Debug.LogWarning("三角形面积为零，无法绘制。", this);
            return;
        }

        int minX = Mathf.Max(0, Mathf.FloorToInt(Mathf.Min(a.x, Mathf.Min(b.x, c.x))));
        int maxX = Mathf.Min(width - 1, Mathf.CeilToInt(Mathf.Max(a.x, Mathf.Max(b.x, c.x))));
        int minY = Mathf.Max(0, Mathf.FloorToInt(Mathf.Min(a.y, Mathf.Min(b.y, c.y))));
        int maxY = Mathf.Min(height - 1, Mathf.CeilToInt(Mathf.Max(a.y, Mathf.Max(b.y, c.y))));

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                Vector2 p = new Vector2(x + 0.5f, y + 0.5f);

                float e0 = Edge(a, b, p);
                float e1 = Edge(b, c, p);
                float e2 = Edge(c, a, p);

                bool inside = area > 0f
                    ? e0 >= 0f && e1 >= 0f && e2 >= 0f
                    : e0 <= 0f && e1 <= 0f && e2 <= 0f;

                if (inside)
                    pixels[y * width + x] = color;
            }
        }
    }

    private void OnDestroy()
    {
        if (output != null && output.texture == framebuffer)
            output.texture = null;

        if (framebuffer != null)
            Destroy(framebuffer);

        framebuffer = null;
        pixels = null;
        points = null;
        originalPoints = null;
    }
}
