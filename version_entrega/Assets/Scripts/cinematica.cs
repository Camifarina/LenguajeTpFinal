using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO.Ports;  //Descomentar cuando se conecta el arduino

public class cinematica : MonoBehaviour
{
    //public SerialPort puerto = new SerialPort("COM5", 9600);  //Descomentar cuando se conecta el arduino
    public bool matar = false;
    private float time = 0;
    void Start()
    {
        //puerto.ReadTimeout = 30;  //Descomentar cuando se conecta el arduino
        //puerto.Open();  //Descomentar cuando se conecta el arduino
    }
    void Update()
    {
        /*Descomentar cuando se conecta el arduino
        try
        {
            if (puerto.IsOpen)
            {
                string dato_recibido = puerto.ReadLine();
                if (dato_recibido.Equals("A"))
                {
                    matar = true;
                }
            }
        }
        catch (System.Exception ex1)
        {
            ex1 = new System.Exception();
        }
        */
        time += Time.deltaTime;
        if (time >= 40 || Input.GetKeyDown(KeyCode.A) || matar)
        {
            matar = false;
            SceneManager.LoadScene(1);
        }
    }
}
