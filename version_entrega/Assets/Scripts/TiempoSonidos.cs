using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoSonidos : MonoBehaviour
{
    public float TiempoSonido;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TiempoSonido);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
