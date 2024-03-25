using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOrEnable : MonoBehaviour
{
    public GameObject iPad;
    public GameObject button;
    public GameObject couch;
    public MiniGameManager miniGameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhenButtonClicked()
    {
        if (miniGameManager.activeGame == false)
        {
            iPad.SetActive(true);
            button.SetActive(false);
            couch.SetActive(false);
            miniGameManager.activeGame = true;
        }
    }

    public void WinButton()
    {
        iPad.SetActive(false);
        button.SetActive(false);
        couch.SetActive(true);
        miniGameManager.activeGame = false;
    }
}
