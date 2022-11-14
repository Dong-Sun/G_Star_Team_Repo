using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceUI : MonoBehaviour
{
    private Text text;
    private Color color;
    private float time = 0.8f;
    private float cooltime=0.8f;
    private bool Start_text_Control_Bool=false;

    private void Start()
    {
        text = GetComponent<Text>();
        color = text.color;
    }
    // Update is called once per frame
    void Update()
    {
        Blinking();

    }

    private void Blinking()
    {
        if(SceneLoadManager.scene_load_manager_instance.SceneChanging)
        {
            return;
        }
        if (Start_text_Control_Bool)
        {
            if (cooltime <= time)
            {
                color.a = 1;
                Start_text_Control_Bool = false;
                return;
            }
            time += Time.deltaTime;
        }
        else
        {
            if (time <= 0)
            {
                color.a = 0;
                Start_text_Control_Bool = true;
                return;
            }
            time -= Time.deltaTime;
        }
        color.a = time / cooltime;
        text.color = color;
    }

}
