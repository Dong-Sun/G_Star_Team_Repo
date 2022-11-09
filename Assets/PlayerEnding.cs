using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerEnding : MonoBehaviour
{
    bool OneShot =false;
    public Text Ending;
    private Color a;
    // Start is called before the first frame update
    void Start()
    {
        a = Ending.color;
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
            this.transform.position -= Vector3.right * Time.deltaTime * 0.7f;
        }
        if (this.transform.position.x < 10)
        {
            if(!OneShot)
            {
                SceneLoadManager.scene_load_manager_instance.NextSceneLoad(8);

                OneShot = true;
            }
            Invoke("EndingText", 3f);
        }
        
    }

    void EndingText()
    {
        if(OneShot)
        {
            a.a += Time.deltaTime * 0.2f;
            if(a.a>1)
            {
                a.a = 1;
            }
            Ending.color = a;
        }
    }
}
