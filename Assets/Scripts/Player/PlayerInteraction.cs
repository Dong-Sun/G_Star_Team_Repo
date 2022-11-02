using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    Interact interact;

    void Update() {
        if (GameManager.Game_Manager_Instance.Player_Manager.Can_Move == true)
            Interacting();
    }

    private void Interacting() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (interact != null) {
                interact.Work();
            }
            else if (interact == null) {
                Debug.Log("Null Interact");
            }
        }
    }

    private void OnTriggerStay(Collider col) {
        if (col.GetComponent<Interact>() != null && interact == null) {
            if (col.TryGetComponent<Interact>(out interact)) {
                Debug.Log("Get Interact");
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if (interact != null) {
            Debug.Log("Return Interact");
            interact = null;
        }
    }
}