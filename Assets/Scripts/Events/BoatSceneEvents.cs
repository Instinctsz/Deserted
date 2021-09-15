using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class BoatSceneEvents : MonoBehaviour
{
    public List<GameEvent> events;
    public Image blackImage;
    public AudioSource audio;
    public AudioSource audioThunder;


    private float timePassed = 0f;

    private bool mapPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        events = new List<GameEvent>();
        DOTween.To(() => audio.volume, x => audio.volume = x, 1, 20);

        events.Add(new GameEvent(5f, () =>
        {
            Color transparent = new Color(0, 0, 0, 0);
            blackImage.DOColor(transparent, 30);
        }));

        events.Add(new GameEvent(10f, () =>
        {
            DialogueManager.instance.ShowText("(You): Oh no... I fell asleep during my fishing trip.", 6f);  
        }));

        events.Add(new GameEvent(20f, () =>
        {
            DialogueManager.instance.ShowText("I should check where I am currently at.", 6f);
        }));

    }

    public void OnMapPickup()
    {
        if (mapPickedUp) return;

        events.Add(new GameEvent(3f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("Damn... Looks like I'm in the middle of nowhere", 6f);
        }));

        events.Add(new GameEvent(12f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("I better change course soon, or I'll never get back home.", 6f);
        }));

        mapPickedUp = true;
    }

    public void OnSteeringWheelTurn()
    {
        //if (!mapPickedUp) return;

        Debug.Log("TESTSET");
        audioThunder.Play();

        events.Add(new GameEvent(0f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("The weather is not looking good, I need to move fast.", 6f);
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
