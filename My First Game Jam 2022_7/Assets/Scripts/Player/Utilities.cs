using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Utilities
{
    public static readonly string groundLayer = "Ground";
    public static readonly string mousePlaneLayer = "Mouse Plane";
    public static readonly string characterLayer = "Character";
    public static readonly string objectLayer = "Object";
    public static readonly string obstacleLayer = "Obstacles";

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

    public static Vector3 GetRandomPointOnGround(float minRadius, float maxRadius, Vector3 center, Vector3 size, LayerMask avoid)
    {
        int iteration = 0;
        while (iteration < 10)
        {
            float randRadius = Random.Range(minRadius, maxRadius);
            Vector2 randPosition2D = Random.insideUnitCircle;
            float x = center.x + randPosition2D.x * randRadius;
            float z = center.z + randPosition2D.y * randRadius;
            Vector3 checkPosition = new Vector3(x, center.y, z);
            if (Physics.CheckBox(checkPosition, size / 2, Quaternion.identity, avoid))
            {
                //#if UNITY_EDITOR
                //Handles.color = Color.red;
                //Handles.DrawWireCube(center, size);
                //#endif
                iteration++;
                continue;
            }
            Vector3 checkDirection = checkPosition - center;
            if (Physics.BoxCast(center, size / 2, checkDirection.normalized, Quaternion.identity, checkDirection.magnitude, LayerMask.GetMask(Utilities.groundLayer)))
            {
                //#if UNITY_EDITOR
                //Handles.color = Color.red;
                //Handles.DrawWireCube(center, size);
                //#endif
                iteration++;
                continue;
            }
            //#if UNITY_EDITOR
            //Handles.color = Color.green;
            //Handles.DrawWireCube(center, size);
            //#endif
            return checkPosition;
        }
        return Vector3.positiveInfinity;
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
