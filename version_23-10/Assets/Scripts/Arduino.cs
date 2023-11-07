/* using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;




public class Arduino : MonoBehaviour
{
    //public PlayerController player;
    public SerialPort puerto;
    public string portName = "COM5"; // Asigna el nombre del puerto COM que corresponda
    public int baudRate = 9600;
    private string boton;


    void Start()
    {
        //Crea una instancia de SerialPort
        puerto = new SerialPort(portName, baudRate);
        puerto.Open();
    }

    void Update()
    {
        if (puerto.IsOpen)
        {
            boton = puerto.ReadLine();
            try
            {
                if (boton == "A")
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
    }
    void OnDestroy()
    {
        // Asegúrate de cerrar el puerto al salir de la aplicación
        if (puerto != null && puerto.IsOpen)
        {
            puerto.Close();
        }
    }
} */