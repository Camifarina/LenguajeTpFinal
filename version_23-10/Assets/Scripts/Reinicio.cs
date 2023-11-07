using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reinicio : MonoBehaviour
{
    
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(0);
        }
    }
}

