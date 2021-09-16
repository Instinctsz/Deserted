using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

public class BoatSceneEvents : MonoBehaviour
{
    public List<GameEvent> events;
    public Image blackImage;
    public Image whiteImage;
    public AudioSource audio;
    public AudioSource audioThunder;
    public AudioSource audioThunderImpact;
    public AudioClip clipFinalThunderImpact;


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
            DialogueManager.instance.ShowText("(You): Oh no... I fell asleep during my fishing trip.", 4f);  
        }));

        events.Add(new GameEvent(20f, () =>
        {
            DialogueManager.instance.ShowText("I should check where I am currently at.", 4f);
        }));

    }

    public void OnMapPickup()
    {
        if (mapPickedUp) return;

        events.Add(new GameEvent(3f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("Damn... Looks like I'm in the middle of nowhere", 4f);
        }));

        events.Add(new GameEvent(12f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("I better change course soon, or I'll never get back home.", 4f);
        }));

        mapPickedUp = true;
    }

    public void OnSteeringWheelTurn()
    {
        if (!mapPickedUp) return;

        audioThunder.Play();

        events.Add(new GameEvent(0f + timePassed, () =>
        {
            DialogueManager.instance.ShowText("The weather is not looking good, I need to move fast.", 3f);
        }));

        events.Add(new GameEvent(10f + timePassed, () =>
        {
            audioThunderImpact.Play();
            whiteImage.DOFade(1f, 0.2f).OnComplete(() => { whiteImage.DOFade(0, 0.5f); });
        }));

        events.Add(new GameEvent(11.5f + timePassed, () =>
        {
            
            DialogueManager.instance.ShowText("Ahh! That one was close!", 1.5f);
        }));

        events.Add(new GameEvent(20f + timePassed, () =>
        {
            audioThunderImpact.clip = clipFinalThunderImpact;
            audioThunderImpact.Play();
            DialogueManager.instance.ShowText("Ahhhhhh!", 3f);
            whiteImage.DOFade(1f, 0.4f);

            TweenAudioVolume(audio, 0);
            TweenAudioVolume(audioThunder, 0);
        }));

        events.Add(new GameEvent(25f + timePassed, () =>
        {
            SceneManager.LoadScene("IslandScene");
        }));
    }

    private void TweenAudioVolume(AudioSource source, float value)
    {
        DOTween.To(() => source.volume, x => source.volume = x, value, 5);
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
