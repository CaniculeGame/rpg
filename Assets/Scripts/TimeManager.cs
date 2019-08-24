using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 0f;
    public float slowdownLengthreprise = 2f;
    private bool slowmotionActivated = false;

    private float oldTimeScale = 0;
    private bool estEnPause = false;
    private float normalFixedDeltaTime = 0;

    void Update()
    {
        if (estEnPause == false)
        {
            if (slowdownLength > 0)
            {
                Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
            else if (slowmotionActivated == false && slowdownLengthreprise > 0) //reprise du cours normal apres 2 sec
            {
                Time.timeScale += (1f / slowdownLengthreprise) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
        }
    }

    public void Initialiser()
    {
        estEnPause = false;
        Time.timeScale = 1;
        slowmotionActivated = false;
        normalFixedDeltaTime = Time.fixedDeltaTime;
    }

    public bool EstEnPause(){return estEnPause;}

    public void Pause()
    {
        if (estEnPause == false)
        {
            Debug.Log("Pause");
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            estEnPause = true;
        }
        else if(estEnPause == true)
        {
            Debug.Log("UnPause");
            Time.timeScale = oldTimeScale;
            estEnPause = false;
        }
    }

    public void MettrePause()
    {
        if (estEnPause == false)
        {
            Debug.Log("Pause");
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            estEnPause = true;
        }
    }

    public void UnPause()
    {
        if (estEnPause == true)
        {
            Debug.Log("UnPause");
            Time.timeScale = oldTimeScale;
            estEnPause = false;
        }
    }

    public void DoSlowmotion()
    {
        if (!slowmotionActivated)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            slowmotionActivated = true;
        }
    }

    public void StopSlowMotion()
    {
        if (slowmotionActivated)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = normalFixedDeltaTime;
            slowmotionActivated = false;
        }
    }

}