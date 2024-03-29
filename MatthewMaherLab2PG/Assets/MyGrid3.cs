using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid3 : MonoBehaviour
{

    public int xSize, zSize;
    private Mesh mesh;
    private Vector3[] vertices;
    private MeshCollider meshCollider;
    float rPipe;
    public GameObject pinPrefab;
    float gradeAngle;
    Vector3 pinLocation;

    private void Awake()
    {

        //Generate the grid
        Generate();

        //tip the deformed grid
        //gradeAngle = 10;
        //transform.RotateAround(Vector3.zero, Vector3.right, gradeAngle);

        //now update the mesh collider to the new mesh
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        pinLocation = new Vector3(0.5f * xSize, -rPipe, 0.8f * zSize);
        GameObject go = Instantiate(pinPrefab);
        //Pin b = go.GetComponent<Pin>();
        //b.transform.position = pinLocation;
        ////tip pin to be at the same angle as the deformed grid
        //b.transform.RotateAround(Vector3.zero, Vector3.right, gradeAngle);
        go.transform.position = pinLocation;
        go.transform.RotateAround(Vector3.zero, Vector3.right, gradeAngle);
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        transform.position = new Vector3(xSize, 0f, 0f);
    }

    private void Generate()
    {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        //First we generate a flat grid in the x,z plane
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, z, 0);
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * zSize * 6];
        for (int ti = 0, vi = 0, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
                mesh.triangles = triangles;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void DeformMyGrid()
    {
        //Now we want to shift the y-coordinate of the mesh vertices
        //so that it has a height map Height_y(x,z)
        //For a half pipe, the height function may be a semicircle
        //So we want to use the formula for a circle r^2 = x^2 +y^2
        //to calculate the y offsets that deform the flat plane into
        //a half pipe. We want a hight function something like
        //Height_y(x,z) = mathf.sqrt(r^2-x^2), but this is not properly
        //centered on the mesh. We need to shift the x coordinate, so we
        //have Height_y(x,z) = meshf.sqrt(r^2-(x-xSize/2)^2).
        vertices = mesh.vertices;
        float xMax2 = 0.5f * xSize;
        float rPipe2 = Mathf.Pow(rPipe, 2);
        float y = 0;
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                y = Mathf.Sqrt(rPipe2 - Mathf.Pow(x - xMax2, 2));
                vertices[i] += new Vector3(0, y, 0);
            }
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}