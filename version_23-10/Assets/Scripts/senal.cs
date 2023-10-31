using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senal : MonoBehaviour
{
    private Transform raul;
    private PlayerController raulcito;
    // Start is called before the first frame update
    void Start()
    {
        raul = GameObject.Find("Player").transform;
        raulcito = raul.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
            Destroy(gameObject);
    }
}
