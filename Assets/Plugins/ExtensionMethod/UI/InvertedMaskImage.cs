using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InvertedMaskImage : Image
{
    public override Material materialForRendering
    {
        get
        {
            UnityEngine.Debug.Log((int)CompareFunction.NotEqual);
            Material result = base.materialForRendering;
            result.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return result;

        }
    }
}