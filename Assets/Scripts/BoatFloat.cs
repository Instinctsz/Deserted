using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFloat : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        Vector3 waveVertex = Wavemanager.instance.FindClosestVertex(position);

        //Debug.Log("Closest wave vertex: " + waveVertex);
        position.y = waveVertex.y;

        transform.position = position;
    }
}
