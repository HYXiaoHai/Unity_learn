using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ShaderToyPassFeature : ScriptableRendererFeature
{
    [Tooltip("ShaderToy渲染用的Shader")]
    public Shader ShaderToyShader;
    private CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(name, ShaderToyShader);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // 必须将 renderer 传递给 Pass，以便在 OnCameraSetup 中获取相机颜色目标
        m_ScriptablePass.Setup(renderer);
        renderer.EnqueuePass(m_ScriptablePass);
    }

    class CustomRenderPass : ScriptableRenderPass
    {
        private string renderTag;
        private Material mat;
        private RTHandle source;
        private RTHandle destination;
        private ScriptableRenderer renderer; // 保存 renderer 引用

        public CustomRenderPass(string tag, Shader s)
        {
            renderTag = tag;
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            if (s != null)
                mat = CoreUtils.CreateEngineMaterial(s);
        }

        public void Setup(ScriptableRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // 使用 cameraColorTargetHandle 获取相机颜色目标（替代弃用的 cameraColorTarget）
            source = renderer.cameraColorTargetHandle;

            // 分配临时纹理（替代原来的 GetTemporaryRT）
            var descriptor = renderingData.cameraData.cameraTargetDescriptor;
            RenderingUtils.ReAllocateIfNeeded(ref destination, descriptor, name: "_ShaderToyTemp");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (mat == null || source == null || destination == null)
                return;

            var cmd = CommandBufferPool.Get(renderTag);

            // 使用 RTHandle 的 nameID 进行 Blit（替代 Identifier()）
            cmd.Blit(source.nameID, destination.nameID, mat);
            cmd.Blit(destination.nameID, source.nameID);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            // 释放临时纹理（替代 ReleaseTemporaryRT）
            if (destination != null)
                RTHandles.Release(destination);
        }
    }
}