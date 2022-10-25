using UnityEngine;
public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    bool switching = true;
    public void Work() {
        if (switching) {
            if (playerTarget != null) {
                HoldBlock();
            }
        }
        else {
            if (floor != null) {
                UnHoldBlock();
            }
        }
    }
    private void HoldBlock() {
        Debug.Log("HoldBlock");
        PlayerManager.Player_Manager_Instance.Holding_Block = true;
        transform.SetParent(playerTarget);
        switching = false;
    }
    private void UnHoldBlock() {
        Debug.Log("UnHoldBlock");
        PlayerManager.Player_Manager_Instance.Holding_Block = false;
        transform.SetParent(floor);
        switching = true;   
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Lava>() != null) {
            UnHoldBlock();
            Destroy(other);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerInteraction>() != null) {
            UnHoldBlock();
        }
    }
}