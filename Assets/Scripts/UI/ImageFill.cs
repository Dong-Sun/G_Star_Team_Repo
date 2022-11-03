using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFill : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    private void Start()
    {
        image = this.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        image.fillAmount = SceneLoadManager.scene_load_manager_instance.holdingTimer / 2;
    }
}
