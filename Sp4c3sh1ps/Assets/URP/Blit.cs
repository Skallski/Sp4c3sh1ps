using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Blit : ScriptableRendererFeature
{
    private class BlitPass : ScriptableRenderPass
    {
        private Material _blitMaterial = null;
        private int _blitShaderPassIndex = 0;
        private FilterMode FilterMode { get; set; }
        
        private RenderTargetIdentifier Source { get; set; }
        private RenderTargetHandle Destination { get; set; }
        private RenderTargetHandle _temporaryColorTexture;
        
        private readonly string _profilerTag;
         
        public BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            _blitMaterial = blitMaterial;
            _blitShaderPassIndex = blitShaderPassIndex;
            _profilerTag = tag;
            _temporaryColorTexture.Init("_TemporaryColorTexture");
        }
         
        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            Source = source;
            Destination = destination;
        }
         
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(_profilerTag);
 
            var opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;
            
            // Can't read and write to same color target, use a TemporaryRT
            if (Destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(_temporaryColorTexture.id, opaqueDesc, FilterMode);
                Blit(cmd, Source, _temporaryColorTexture.Identifier(), _blitMaterial, _blitShaderPassIndex);
                Blit(cmd, _temporaryColorTexture.Identifier(), Source);
            }
            else
            {
                Blit(cmd, Source, Destination.Identifier(), _blitMaterial, _blitShaderPassIndex);
            }
 
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
         
        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (Destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(_temporaryColorTexture.id);
        }
    }
 
    [System.Serializable]
    public class BlitSettings
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;
        public Material blitMaterial = null;
        public int blitMaterialPassIndex = 0;
        public Target destination = Target.Color;
        public string textureId = "_BlitPassTexture";
    }
 
    public enum Target
    {
        Color,
        Texture
    }
 
    public BlitSettings settings = new BlitSettings();
    private RenderTargetHandle _renderTextureHandle;
    private BlitPass _blitPass;
 
    public override void Create()
    {
        var passIndex = settings.blitMaterial != null ? settings.blitMaterial.passCount - 1 : 1;
        settings.blitMaterialPassIndex = Mathf.Clamp(settings.blitMaterialPassIndex, -1, passIndex);
        _blitPass = new BlitPass(settings.Event, settings.blitMaterial, settings.blitMaterialPassIndex, name);
        _renderTextureHandle.Init(settings.textureId);
    }
 
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        var dest = (settings.destination == Target.Color) ? RenderTargetHandle.CameraTarget : _renderTextureHandle;
 
        if (settings.blitMaterial == null)
        {
            Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
            return;
        }
 
        _blitPass.Setup(src, dest);
        renderer.EnqueuePass(_blitPass);
    }
}