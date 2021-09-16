using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IslandSceneEvents : MonoBehaviour
{
    private List<GameEvent> events;
    private float timePassed = 0f;
    public Image whiteImage;

    // Start is called before the first frame update
    void Start()
    {
        events = new List<GameEvent>();

        events.Add(new GameEvent(5f, () =>
        {
            whiteImage.DOFade(0, 5);
        }));

        events.Add(new GameEvent(10f, () =>
        {
            DialogueManager.instance.ShowText("Where.... am I?", 3f);
        }));
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        foreach (GameEvent gameEvent in events.ToList())
        {
            if (timePassed >= gameEvent.executionTime)
            {
                gameEvent.eventFunction();
                events.Remove(gameEvent);
            }
        }
    }
}
