using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLivingRoom : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Living Room");
    }
}
