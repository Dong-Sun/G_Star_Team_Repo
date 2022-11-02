using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapyrusController : MonoBehaviour
{
    [SerializeField] Animator tutorial;
    private void Update() {
        transform.rotation = (Camera.main.transform.rotation * Quaternion.Euler(0, 1f, 0));
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            tutorial.SetBool("Active", true);
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            tutorial.SetBool("Active", false);
    }
}
