using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreTXT;
    static int combo;
    public GameObject winButt;

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        combo = 0;
    }

    public void reset()
    {
        combo = 0;
        winButt.SetActive(false);
    }

    public static void Hit()
    {
        combo += 1;
        Instance.hitSFX.Play();
    }

    public static void Miss()
    {
        combo -= 2;
        Instance.missSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTXT.text = combo.ToString();
        if (combo > 9)
        {
            winButt.SetActive(true);
        }
    }
}
