using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartingScreenHandler : MonoBehaviour
{
    public TMP_InputField inputName;
    public Image blackScreen;

    public void RegisterName()
    {
        if (inputName.text != "")
            GameManager.SetName(inputName.text);

        TransitionNextScene();
    }

    private async void TransitionNextScene()
    {
        await blackScreen.DOColor(Color.black, 2f).AsyncWaitForCompletion();
        SceneManager.LoadScene("BoatScene");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
