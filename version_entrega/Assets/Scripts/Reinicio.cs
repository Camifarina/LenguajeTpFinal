using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO.Ports;  //Descomentar cuando se conecta el arduino

public class Reinicio : MonoBehaviour
{
    //public SerialPort puerto = new SerialPort("COM5", 9600);  //Descomentar cuando se conecta el arduino
    public bool matar = false;
    public float time;
    public Transform controladorSenal;
    public GameObject senal_izq;
    void Start()
    {
        //puerto.ReadTimeout = 30;  //Descomentar cuando se conecta el arduino
        //puerto.Open();  //Descomentar cuando se conecta el arduino
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 90f); // Rotación de -90 grados en el eje Z para 2D
            Instantiate(senal_izq, controladorSenal.position, rotation);
            controladorSenal.position -= new Vector3(0f, Random.Range(-0.01f, 0.01f), 0f);
        }
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
        if (Input.GetKeyDown(KeyCode.A) || matar)
        {
            SceneManager.LoadScene(0);
        }
        matar = false;
    }
}

