using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class SteeringWheelEvent : MonoBehaviour
{

    public float minimalAngleTurn = 50f;
    public UnityEvent onSteeringWheelTurned;

    private bool angleReached = false;

    private CircularDrive circularDrive;

    // Start is called before the first frame update
    void Start()
    {
        circularDrive = GetComponent<CircularDrive>();
    }

    // Update is called once per frame
    void Update()
    {
        if (angleReached) return;

        if(Mathf.Abs(circularDrive.outAngle) >= minimalAngleTurn)
        {
            onSteeringWheelTurned?.Invoke();
            angleReached = true;
        }   
    }
}
