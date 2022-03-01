using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Normal", true);
            anim.SetBool("Sad", false);
            anim.SetBool("Crazy", false);
            anim.SetBool("Bad", false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Normal", false);
            anim.SetBool("Sad", true);
            anim.SetBool("Crazy", false);
            anim.SetBool("Bad", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Normal", false);
            anim.SetBool("Sad", false);
            anim.SetBool("Crazy", true);
            anim.SetBool("Bad", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("Normal", false);
            anim.SetBool("Sad", false);
            anim.SetBool("Crazy", false);
            anim.SetBool("Bad", true);
        }
    }
}
