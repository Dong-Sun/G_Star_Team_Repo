using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaster : MonoBehaviour
{
    [SerializeField] Lever[] levers;
    [SerializeField] Key myKey;
    bool isActive = false;
    void Start()
    {
        if (myKey == null)
            myKey = transform.GetChild(0).GetComponent<Key>();
    }

    void Update()
    {
        if(levers.Length > 0 && !myKey.GetKey) {
            if (!myKey.gameObject.activeSelf) {
                isActive = true;
                foreach(Lever lever in levers) {
                    isActive = isActive & lever.Switching;
                }
                myKey.gameObject.SetActive(isActive);
            }
        }
    }
}
