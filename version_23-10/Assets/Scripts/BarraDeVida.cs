using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    private Animator Animator;
    public PlayerController raulcito;
    private int golpes = 0;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raulcito.mareado == true)
        {
            golpes++;
        }
        if (golpes == 1)
        {
            Animator.SetInteger("Svidas", Mathf.RoundToInt(1));
        }
        else if (golpes == 2)
        {
            Animator.SetInteger("Svidas", Mathf.RoundToInt(2));
        }
        else if (golpes == 3)
        {
            Animator.SetInteger("Svidas", Mathf.RoundToInt(3));
        }
    }
}
