Shader "Unlit/MaskShader"
{
	Properties
	{
		_MaskColor("Mask Color", 2D) = "white" {}
		_MainTex("Skin", 2D) = "white" {}
		_MaskTex("Mask",2D) = "white" {}
	}
	SubShader{
        Pass {
			SetTexture[_MaskColor]

			SetTexture[_MaskTex] {
				combine previous * texture
            }

			SetTexture[_MaskTex] {
				constantColor[_Color]
				combine previous lerp(texture) previous
            }
		
			SetTexture[_MainTex] {
				combine previous lerp(previous) texture
			}			
		}
	}
}
