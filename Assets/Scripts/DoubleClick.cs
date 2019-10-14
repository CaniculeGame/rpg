using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick 
{

    public bool one_click = false;
    public bool timer_running;
    public float timer_for_double_click;
    public float delay = 0.05f;



    public bool DoubleClic()
    {

        if (!one_click)
        {
            one_click = true;
            timer_for_double_click = Time.time;
        }
        else
        {
            one_click = false;
            return true;
        }


        if (one_click)
        {
            if ((Time.time - timer_for_double_click) > delay)
                one_click = false;
        }

        return false;
    }
}
