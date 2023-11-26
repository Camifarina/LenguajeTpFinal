using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVidaMalo : MonoBehaviour
{
    private Animator Animator;
    public PlayerController raulcito;
    public Malo malo;
    private int golpes = 0;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raulcito.maloSinMascara == true)
        {
            golpes++;
        }
        Debug.Log("malo: "+golpes);

        if (golpes == 1)
        {
            Animator.SetInteger("Mvidas", 1);
        }
        else if (golpes == 2)
        {
            Animator.SetInteger("Mvidas", 2);
        }
        else if (golpes == 3)
        {
            Animator.SetInteger("Mvidas", 3);
        }
    }
}
