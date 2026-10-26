using UnityEngine;
using UnityEngine.Rendering;

public class D01_RenderPipelineCheck : MonoBehaviour
{
    private void Start()
    {
        var pipelineAsset = GraphicsSettings.currentRenderPipeline;

        Debug.Log($"[D01] Current Quality Level: {QualitySettings.names[QualitySettings.GetQualityLevel()]}");
        Debug.Log($"[D01] SRP Enabled: {GraphicsSettings.isScriptableRenderPipelineEnabled}");

        if (pipelineAsset == null)
        {
            Debug.LogError("[D01] No active Render Pipeline Asset.");
            return;
        }

        Debug.Log($"[D01] Active Render Pipeline Asset: {pipelineAsset.name}");
        Debug.Log($"[D01] Asset Type: {pipelineAsset.GetType().FullName}");
    }
}