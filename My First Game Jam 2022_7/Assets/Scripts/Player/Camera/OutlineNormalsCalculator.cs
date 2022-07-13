using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class OutlineNormalsCalculator : MonoBehaviour
{
    [SerializeField] private int storeInTexcoordChannel = 1;
    [SerializeField] private float cospatialVertexDistance = 0.01f;

    private class CospatialVertex
    {
        public Vector3 position;
        public Vector3 accumalatedNormal;
    }

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices  = mesh.vertices;
        int[] triangles = mesh.triangles;

        Vector3[] outlineNormals = new Vector3[vertices.Length];

        List<CospatialVertex> cospatialVerticesData = new List<CospatialVertex>();
        int[] cospatialVertexIndices = new int[vertices.Length];
        FindCospatialVertices(vertices, cospatialVertexIndices, cospatialVerticesData);

        int numTriangles = triangles.Length / 3;

        for(int i = 0; i < numTriangles; i++)
        {
            int vertexStart = i + 3;
            int v1Index = triangles[vertexStart];
            int v2Index = triangles[vertexStart + 1];
            int v3Index = triangles[vertexStart + 2];

            ComputeNormalAndWeights(vertices[v1Index], vertices[v2Index], vertices[v3Index], out Vector3 normal, out Vector3 weights);
            AddWeightedNormal(normal * weights.x, v1Index, cospatialVertexIndices, cospatialVerticesData);
            AddWeightedNormal(normal * weights.y, v2Index, cospatialVertexIndices, cospatialVerticesData);
            AddWeightedNormal(normal * weights.z, v3Index, cospatialVertexIndices, cospatialVerticesData);

            for(int v= 0; v< outlineNormals.Length; v++)
            {
                int cvIndex = cospatialVertexIndices[v];
                var cospatial = cospatialVerticesData[cvIndex];
                outlineNormals[v] = cospatial.accumalatedNormal.normalized;
            }

            mesh.SetUVs(storeInTexcoordChannel, outlineNormals);
        }
    }

    private void FindCospatialVertices(Vector3[] vertices, int[] indices, List<CospatialVertex> registry)
    {
        for(int i=0; i<vertices.Length; i++)
        {
            if(SearchForPreviouslyRegisteredCV(vertices[i], registry, out int index))
            {
                indices[i] = index;
            }
            else
            {
                var cospatialEntry = new CospatialVertex()
                {
                    position = vertices[i],
                    accumalatedNormal = Vector3.zero
                };

                indices[i] = registry.Count;
                registry.Add(cospatialEntry);
            }
        }
    }

    private bool SearchForPreviouslyRegisteredCV(Vector3 position, List<CospatialVertex> registry, out int index)
    {
        for(int i = 0; i < registry.Count; i++)
        {
            if(Vector3.Distance(registry[i].position, position) <= cospatialVertexDistance)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }


    private void ComputeNormalAndWeights(Vector3 a, Vector3 b, Vector3 c, out Vector3 normal, out Vector3 weights)
    {
        normal = Vector3.Cross(b - a, c - a).normalized;
        weights = new Vector3(Vector3.Angle(b-a, c-a), Vector3.Angle(c-b, a-b), Vector3.Angle(b-c, a - c));
    }

    private void AddWeightedNormal(Vector3 weightedNormal, int vertexIndex, int[] cvIndices, List<CospatialVertex> cvRegistry)
    {
        int cvIndex = cvIndices[vertexIndex];
        cvRegistry[cvIndex].accumalatedNormal += weightedNormal;
    }

}
