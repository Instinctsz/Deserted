using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoatSceneEvents : MonoBehaviour
{
    public List<GameEvent> events;
    public Image blackImage;

    public float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        events = new List<GameEvent>();

        events.Add(new GameEvent(5f, () =>
        {
            Color transparent = new Color(0, 0, 0, 0);
            blackImage.DOColor(transparent, 30);
        }));
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        foreach (GameEvent gameEvent in events)
        {
            if (timePassed >= gameEvent.executionTime)
            {
                gameEvent.eventFunction();
                events.Remove(gameEvent);
            }
        }
        Debug.Log(timePassed);
    }
}
