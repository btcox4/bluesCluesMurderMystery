using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public bool activeGame;

    public bool wordlePlayed;
    public bool cardsPlayed;
    public bool ddrrPlayed;

    public Button IP;
    public Button DC;
    public Button ST;
    
    // Start is called before the first frame update
    void Start()
    {
        activeGame = false;
        wordlePlayed = false;
        cardsPlayed = false;
        ddrrPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGame == true)
        {
            IP.interactable = false;
            DC.interactable = false;
            ST.interactable = false;
        }
        else
        {
            if (wordlePlayed == false)
            {
                IP.interactable = true;
            }
            if(cardsPlayed == false)
            {
                DC.interactable = true;
            }
            if(ddrrPlayed == false)
            {
                ST.interactable = true;
            }
        }
    }

    public void iPad()
    {
        activeGame = true;
        wordlePlayed = true;
    }

    public void match()
    {
        activeGame = true;
        cardsPlayed = true;

    }

    public void song()
    {
        activeGame = true;
        ddrrPlayed = true;
    }

    public void win()
    {
        activeGame = false;
    }
}
