using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Footprints : MonoBehaviour
{
    public int maxFootprints = 256;
    public Vector2 footprintSize = new Vector3(0.4f, 0.8f);
    public float footprintSpacing = 0.3f;
    public float groundOffset = 0.02f;
    public LayerMask terrainLayer;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Vector2[] uvs;
    private int[] triangles;

    private int footprintCount = 0;
    private bool isLeft = false;

    private void Awake()
    {
        vertices = new Vector3[maxFootprints * 4];
        normals = new Vector3[maxFootprints * 4];
        uvs = new Vector2[maxFootprints * 4];
        triangles = new int[maxFootprints * 6];

        if (GetComponent<MeshFilter>().mesh == null)
        {
            GetComponent<MeshFilter>().mesh = new Mesh();
        }

        mesh = GetComponent<MeshFilter>().mesh;

        mesh.name = "Footprints Mesh";
    }

    public void AddFootprint(Vector3 pos, Vector3 fwd, Vector3 rht, int footprintType)
    {
        float footOffet = footprintSpacing;

        if (isLeft)
        {
            footOffet = -footprintSpacing;
        }

        Vector3[] corners = new Vector3[4];

        corners[0] = pos + (rht * footOffet) + (fwd * footprintSize.y * 0.5f) + (-rht * footprintSize.x * 0.5f);
        corners[1] = pos + (rht * footOffet) + (fwd * footprintSize.y * 0.5f) + (rht * footprintSize.x * 0.5f);
        corners[2] = pos + (rht * footOffet) + (-fwd * footprintSize.y * 0.5f) + (-rht * footprintSize.x * 0.5f);
        corners[3] = pos + (rht * footOffet) + (-fwd * footprintSize.y * 0.5f) + (rht * footprintSize.x * 0.5f);

        RaycastHit hit;
        for (int i = 0; i < 4; i++)
        {
            Vector3 rayPos = corners[i];
            rayPos.y = 1000;

            if (Physics.Raycast(rayPos, -Vector3.up, out hit, 2000, terrainLayer))
            {
                int index = (footprintCount * 4) + i;

                vertices[index] = hit.point + (hit.normal * groundOffset);

                normals[index] = hit.normal;
            }
        }

        Vector2 uvOffset;

        switch (footprintType)
        {
            case 1:
                uvOffset = new Vector2(0.5f, 1.0f);
                break;

            case 2:
                uvOffset = new Vector2(0.0f, 0.5f);
                break;

            case 3:
                uvOffset = new Vector2(0.5f, 0.0f);
                break;

            default:
                uvOffset = new Vector2(0.0f, 1.0f);
                break;
        }

        if (isLeft)
        {
            uvs[(footprintCount * 4) + 0] = new Vector2(uvOffset.x + 0.5f, uvOffset.y);
            uvs[(footprintCount * 4) + 1] = new Vector2(uvOffset.x, uvOffset.y);
            uvs[(footprintCount * 4) + 2] = new Vector2(uvOffset.x + 0.5f, uvOffset.y - 0.5f);
            uvs[(footprintCount * 4) + 3] = new Vector2(uvOffset.x, uvOffset.y - 0.5f);

            isLeft = false;
        }
        else
        {
            uvs[(footprintCount * 4) + 0] = new Vector2(uvOffset.x, uvOffset.y);
            uvs[(footprintCount * 4) + 1] = new Vector2(uvOffset.x + 0.5f, uvOffset.y);
            uvs[(footprintCount * 4) + 2] = new Vector2(uvOffset.x, uvOffset.y - 0.5f);
            uvs[(footprintCount * 4) + 3] = new Vector2(uvOffset.x + 0.5f, uvOffset.y - 0.5f);

            isLeft = true;
        }

        triangles[(footprintCount * 6) + 0] = (footprintCount * 4) + 0;
        triangles[(footprintCount * 6) + 1] = (footprintCount * 4) + 1;
        triangles[(footprintCount * 6) + 2] = (footprintCount * 4) + 2;

        triangles[(footprintCount * 6) + 3] = (footprintCount * 4) + 2;
        triangles[(footprintCount * 6) + 4] = (footprintCount * 4) + 1;
        triangles[(footprintCount * 6) + 5] = (footprintCount * 4) + 3;

        footprintCount++;

        if (footprintCount >= maxFootprints)
        {
            footprintCount = 0;
        }

        // - update mesh with new info -
        ConstructMesh();
    }

    private void ConstructMesh(){
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uvs;
    }
}
