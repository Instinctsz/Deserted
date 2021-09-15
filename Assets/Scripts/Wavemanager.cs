using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavemanager : MonoBehaviour
{
    public static Wavemanager instance;

    private MeshFilter meshFilter;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        meshFilter = GetComponent<MeshFilter>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public Vector3 FindClosestVertex(Vector3 position)
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        Vector3 closestVector = Vector3.zero;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            Debug.Log("looping through vertex: " + vertex);

            if (Vector3.Distance(vertex, position) < Vector3.Distance(closestVector, position)) {
                closestVector = vertex;
            }
        }

        return closestVector;
    }
}
