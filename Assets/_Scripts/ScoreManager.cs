using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshPro scoreTXT;
    static int combo;

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        combo = 0;
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
    }
}
