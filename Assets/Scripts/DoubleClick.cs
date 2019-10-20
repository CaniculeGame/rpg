using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick 
{

    private bool premier_clic = false;
    private bool timer_running;
    private float timer_for_double_click;
    public  float delay = 0.025f;

    public float Delay { get { return delay; } set { if (value >= 0) delay = value; } }

    public bool DoubleClic()
    {

        // premeiur clic
        if (!premier_clic)
        {
            premier_clic = true;
            timer_for_double_click = Time.time;
        }
        //deuxieme clic
        else
        {
            premier_clic = false;
            Debug.Log("sec clic");
            return true;
        }


        if (premier_clic)
        {
            if ((Time.time - timer_for_double_click) > delay)
                premier_clic = false;
        }

        return false;
    }
}
