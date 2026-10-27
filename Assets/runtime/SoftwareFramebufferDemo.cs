using UnityEngine;
using UnityEngine.UI;

public class SoftwareFramebufferDemo : MonoBehaviour
{
    public int width = 128;  
    public int height = 128;
    private Texture2D framebuffer;
    private Color32[] pixels;
    public RawImage output;
    void Start()
    {
        if (output == null)
        {
            Debug.LogError("rawImage 没有指定");
        }

        Color32 yellow = new Color32(255, 255, 0, 255);
        Color32 red = new Color32(255, 0, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);
        Color32 blue = new Color32(0, 0, 255, 255);

        framebuffer = new Texture2D(width, height, TextureFormat.RGBA32, false);
        pixels = new Color32[width * height];

        BgColored();
        //SetPixel(30,30);
        //BgColored2();
        DrawTriangle(new Vector2(10, 10), new Vector2(100, 120), new Vector2(0, 111), new Color32(0, 0, 0, 255));
        ApplyBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BgColored2()
    {
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = new Color32(255, 255, 0, 255); // 黄色
        }
    }
    void BgColored()
    {
        for (int i = 0; i < pixels.Length; i++)
        { 
            int x = i % width;
            int y = i / height;
            if (x < width / 2 && y < height / 2)
            {
                pixels[i] = new Color32(255, 0, 0, 255); // 红色
            }
            else if (x >= width / 2 && y < height / 2)
            {
                pixels[i] = new Color32(0, 255, 0, 255); // 绿色
            }
            else if (x < width / 2 && y >= height / 2)
            {
                pixels[i] = new Color32(0, 0, 255, 255); // 蓝色
            }
            else
            {
                pixels[i] = new Color32(255, 255, 0, 255); // 黄色
            }
        }
    }

    void ApplyBuffer()
    {
        framebuffer.SetPixels32(pixels);
        framebuffer.Apply();
        output.texture = framebuffer;
    }

    void SetPixel(int x ,int y)
    { 
        if(x<0||x>=width||y<0||y>=height)
        {
            Debug.LogError("坐标超出范围");
            return;
        }
        int index = y * width + x;
        pixels[index] = new Color32(0, 0, 0, 255); 
    }

    private void OnDestroy()
    {
        if (framebuffer != null)
        {
            Destroy(framebuffer);
        }
        framebuffer = null;
        pixels = null;

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

}
