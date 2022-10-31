using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapyrusControl : MonoBehaviour
{
    public bool Papyrus_Disable;
    Image Papyrus_Image;
    // Start is called before the first frame update
    void Start()
    {
        Papyrus_Disable = true;
        Papyrus_Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Papyrus_Action()
    {

    }
}
