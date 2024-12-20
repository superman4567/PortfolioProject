using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class InvertedMask : Image
{
    public override Material materialForRendering
    {
        get {
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
}
