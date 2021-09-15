using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public float executionTime;
    public Action eventFunction;

    public GameEvent(float _executionTime, Action _eventFunction)
    {
        executionTime = _executionTime;
        eventFunction = _eventFunction;
    }
}
