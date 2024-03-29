using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreation : MonoBehaviour
{
    [SerializeField] private int xSize, ySize;

    private Mesh mesh;

    private Vector3[] vertices;
    private void Awake()
    {
        StartCoroutine(Generate());
        float thisYPos = transform.position.y;
    }
    private IEnumerator Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        //Gets how many verticies will be needed for each square of the grid
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, 1f);
        for (int num = 0, yPos = 0; yPos <= ySize; yPos++)
        {
            for (int xPos = 0; xPos <= xSize; xPos++, num++)
            {
                vertices[num] = new Vector3(xPos, yPos);
                tangents[num] = tangent;
                yield return new WaitForSeconds(0.05f);
            }
        }
        mesh.vertices = vertices;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * ySize * 6];
        for (int tri = 0, vert = 0, yPos = 0; yPos < ySize; yPos++, vert++)
        {
            for (int xPos = 0; xPos < xSize; xPos++, tri += 6, vert++)
            {
                triangles[tri] = vert;
                triangles[tri + 3] = triangles[tri + 2] = vert + 1;
                triangles[tri + 4] = triangles[tri + 1] = vert + xSize + 1;
                triangles[tri + 5] = vert + xSize + 2;
                mesh.triangles = triangles;
                yield return new WaitForSeconds(0.05f);
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
}
