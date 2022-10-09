using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    Obstacle obstacle;
    Interact interact;

    void Update() {
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
        if (col.GetComponent<Obstacle>() != null && obstacle == null) {
            if (col.TryGetComponent<Obstacle>(out obstacle)) {
                Debug.Log("Get Obstacle");
                obstacle.Work();
            }
        }

        if (col.GetComponent<Interact>() != null && interact == null) {
            if (col.TryGetComponent<Interact>(out interact)) {
                Debug.Log("Get Interact");
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if (obstacle != null) {
            Debug.Log("Return Obstacle");
            obstacle = null;
        }

        if (interact != null) {
            Debug.Log("Return Interact");
            interact = null;
        }
    }
}