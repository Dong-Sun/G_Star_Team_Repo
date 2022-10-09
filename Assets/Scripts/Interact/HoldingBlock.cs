using UnityEngine;

public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    bool switching = false;
    public void Work() {
        Debug.Log("HoldingBlock");
        switching = !switching;
        if (switching) {
            if (playerTarget != null) {
                transform.SetParent(playerTarget);
            }
        }
        else {
            if (floor != null) {
                transform.SetParent(floor);
            }
        }
    }
}