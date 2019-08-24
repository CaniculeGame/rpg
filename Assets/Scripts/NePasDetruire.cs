using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NePasDetruire : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

 
}
