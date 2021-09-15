using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TMP_Text dialogue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text, float duration)
    {
        Color newColor = dialogue.color;
        newColor.a = 255;
        DOTween.To(() => dialogue.color, x => dialogue.color = x, newColor, 5);
        dialogue.text = text;
        StartCoroutine(FadeAwayText(duration));
    }

    private IEnumerator FadeAwayText(float delay)
    {
        yield return new WaitForSeconds(delay);

        Color newColor = dialogue.color;
        newColor.a = 0;
        DOTween.To(() => dialogue.color, x => dialogue.color = x, newColor, 5);

    }
}
