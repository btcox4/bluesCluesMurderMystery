using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TheFinalScript : MonoBehaviour
{
    public TMP_Dropdown who;
    public TMP_Dropdown where;
    public TMP_Dropdown what;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check()
    {
        if (who.value == 1 && where.value == 2 && what.value == 5)
        {
            SceneManager.LoadScene("Temp Game End");
        }
    }

}

