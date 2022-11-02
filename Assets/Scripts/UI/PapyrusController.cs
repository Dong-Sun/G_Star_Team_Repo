using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapyrusController : MonoBehaviour
{
    [SerializeField] Animator tutorial;
    bool isActive = false;
    private void Update() {
        transform.rotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !isActive) {
            tutorial.SetBool("Active", true);
            isActive = true;
            Debug.Log("Open");
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && isActive) {
            tutorial.SetBool("Active", false);
            isActive = false;
            Debug.Log("Close");
        }
            
    }
}
