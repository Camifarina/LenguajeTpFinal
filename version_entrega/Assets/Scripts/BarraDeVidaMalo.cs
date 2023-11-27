
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeVidaMalo : MonoBehaviour
{
    private Animator Animator;
    public PlayerController raulcito;
    public Malo malo;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (raulcito.maloMuere == 1)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(1));
        }
        else if (raulcito.maloMuere == 2)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(2));
        }
        else if (raulcito.maloMuere == 3)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(3));
        }


        if (raulcito.mSinMasc == 1)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(1));
        }
        else if (raulcito.mSinMasc == 2)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(2));
        }
        else if (raulcito.mSinMasc == 3)
        {
            Animator.SetInteger("Mvidas", Mathf.RoundToInt(3));
        }
    }
}
