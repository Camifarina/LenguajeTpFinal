using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.IO.Ports;  //Descomentar cuando se conecta el arduino

public class Arduino : MonoBehaviour
{
    // public PlayerController player;   //Descomentar cuando se conecta el arduino
    // public SerialPort puerto;   //Descomentar cuando se conecta el arduino
    // public string portName = "COM5"; //Descomentar cuando se conecta el arduino
    public int baudRate = 9600;
    private string boton;


    void Start()
    {
        /*Descomentar cuando se conecta el arduino
        puerto = new SerialPort(portName, baudRate);
        puerto.Open();
        */
    }

    void Update()
    {
        /* Descomentar cuando se conecta el arduino
        if (puerto.IsOpen)
        {
            boton = puerto.ReadLine();
            try
            {
                if (boton == "derecha")
                {
                    Debug.Log("1");
                    //player.matar = true;
                }
            }
            catch (System.Exception ex)
            {
                ex = new System.Exception();
            }
        }
        */
    }
    void OnDestroy()
    {
        // Cerrar el puerto al salir de la aplicación
        /* Descomentar cuando se conecta el arduino
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
        */
    }
}