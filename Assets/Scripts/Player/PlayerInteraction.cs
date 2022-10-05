using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // private
    // Start is called before the first frame update

    Obstacle obstacle;
    //Interact interact;
    //Automatic automatic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interacting();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col != null)
        {
            col.TryGetComponent<Obstacle>(out obstacle);
            //col.TryGetComponent<Interact>(out interact);
            //col.TryGetComponent<Automatic>(out automatic);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col != null)
        {

             obstacle = null;
             //interact = null;
             //automatic = null; 
        }
    }

    private void Interacting()
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(obstacle !=null)
            {
                obstacle.Work();
            }

        }
    }
}
