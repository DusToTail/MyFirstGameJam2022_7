Shader "Outlines/Outline_Unlit"
{
    Properties
    {
        _Thickness("Thickness", Float) = 1
        _Color("Color", Color) = (1, 1, 1, 1)
        _DepthOffset("Depth offset", Range(0,1)) = 0
        [Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)]_PrecalculatedNormals("Use UV1 normals", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline"}

        Pass
        {
            Cull Front

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS
            #pragma vertex vert
            #pragma fragment frag

            #include "Outlines.hlsl"
            ENDHLSL

        }
        
    }
}
