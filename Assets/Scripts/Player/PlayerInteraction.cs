using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    Obstacle obstacle;
    Interact interact;

    void Update() {
        Interacting();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.TryGetComponent<Obstacle>(out obstacle))
            obstacle.Work();
        else if (col.TryGetComponent<Interact>(out interact)) {
            Debug.Log("Get Interact");
        }
    }

    private void OnTriggerExit(Collider col) {
        if (obstacle != null)
            obstacle = null;
        else if (interact != null)
            interact = null;
    }

    private void Interacting() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (interact != null) {
                interact.Work();
            }
        }
        else if (Input.GetKeyUp(KeyCode.E)) {
            if (interact != null) {

            }
        }
    }

}