using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x<-5)
        {
            return;
        }
        else
        {
            this.transform.position -= Vector3.right * Time.deltaTime * 0.3f;
        }
    }
}
