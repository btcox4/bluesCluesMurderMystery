using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOrEnable : MonoBehaviour
{
    public GameObject iPad;
    public GameObject button;
    
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
        iPad.SetActive(true);
        button.SetActive(false);
    }

    public void WinButton()
    {
        iPad.SetActive(false);
        button.SetActive(false);
    }
}
