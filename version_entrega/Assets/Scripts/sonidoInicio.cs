using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidoInicio : MonoBehaviour
{
    public GameObject fondoInicio;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fondoInicio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
