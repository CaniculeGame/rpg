using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Animator animator;
    private int vie = 100;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        vie = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.DonnerInstance.Role != GameManage.ROLE.ROLE_JOUEUR)
            return;


        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        bool shift = Input.GetKey(KeyCode.LeftShift);

        animator.SetInteger("vie", vie);
        if (vie <= 0)
            return;

       /* if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Block"))
            return;*/


        if (vertical != 0 || horizontal != 0)
        {
            Vector3 pos = this.transform.position;
            if (shift)
            {
                animator.SetInteger("state", 4);
                this.transform.Translate((-1 * vertical *2* Time.deltaTime), 0,  (-1 * horizontal *2*  Time.deltaTime));
            }
            else
            {
                this.transform.Translate(-1*vertical * Time.deltaTime, 0, (-1 * horizontal * Time.deltaTime));
                animator.SetInteger("state", 1);
            }


        }
        else if(Input.GetButton("Fire1"))
        {
            animator.SetInteger("state", 3);
        }
        else if (Input.GetButton("Fire2"))
        {
            animator.SetInteger("state", 2);
        }
        else
        {
            animator.SetInteger("state", 0);
        }

    }
}
