using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO.Ports;

public class cinematica : MonoBehaviour
{
    //public SerialPort puerto = new SerialPort("COM5", 9600);
    public bool matar = false;
    private float time = 0;
    void Start()
    {
        //puerto.ReadTimeout = 30;
        //puerto.Open();  //Descomentar cuando se conecta el arduino
    }
    void Update()
    {
        // try
        // {
        //     if (puerto.IsOpen)
        //     {
        //         string dato_recibido = puerto.ReadLine();
        //         if (dato_recibido.Equals("A"))
        //         {
        //             matar = true;
        //         }
        //     }
        // }
        // catch (System.Exception ex1)
        // {
        //     ex1 = new System.Exception();
        // }
        //Descomentar cuando se conecta el arduino 
        time += Time.deltaTime;
        if (time >= 41 || Input.GetKeyDown(KeyCode.A) || matar)
        {
            matar = false;
            SceneManager.LoadScene(1);
        }
    }
}
