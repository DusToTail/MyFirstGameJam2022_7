using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static readonly string groundLayer = "Ground";
    public static readonly string mousePlaneLayer = "Mouse Plane";
    public static readonly string characterLayer = "Character";
    public static readonly string objectLayer = "Object";

    public static readonly string interactionActorLayer = "Interaction Actor";
    public static readonly string interactionObjectLayer = "Interaction Object";
    public static readonly string ignoreCameraLayer = "Ignore Camera";

    public static readonly string playerTag = "Player";
    public static readonly string modelTag = "Model";


    public static Vector3 GetCursorPositionOnLayers(params string[] layers)
    {
        try
        {
            LayerMask layerMask = LayerMask.GetMask(layers);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 1000f, layerMask);
            if(hit.collider == null) { return Vector3.zero; }
            return hit.point;
        }
        catch
        {
            throw new System.Exception("Utilities: Can not get cursor position with layers: " + layers);
        }

    }

    public static void ChangeLayersRecursively(GameObject target, string layerName)
    {
        target.layer = LayerMask.NameToLayer(layerName);
        for (int i = 0; i < target.transform.childCount; i++)
        {
            ChangeLayersRecursively(target.transform.GetChild(i).gameObject, layerName);
        }
    }
}
