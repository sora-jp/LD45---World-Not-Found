using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundedRect : BaseMeshEffect
{
    public override void ModifyMesh(VertexHelper vh)
    {
        var rt = GetComponent<RectTransform>();
        float h = rt.rect.height;
        float aspect = rt.rect.width / rt.rect.height;
        for (int i = 0; i < vh.currentVertCount; i++)
        {
            UIVertex v = new UIVertex();
            vh.PopulateUIVertex(ref v, i);
            v.uv1 = new Vector2(aspect, h);
            vh.SetUIVertex(v, i);
        }
    }
}
