using UnityEngine;
public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] GameObject arrow;
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    bool switching = true;
    bool isActive = true;
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
        GameManager.Game_Manager_Instance.Player_Manager.Holding_Block = true;
        transform.SetParent(playerTarget);
        switching = false;
        arrow.SetActive(false);
    }
    private void UnHoldBlock() {
        Debug.Log("UnHoldBlock");
        GameManager.Game_Manager_Instance.Player_Manager.Holding_Block = false;
        transform.SetParent(floor);
        switching = true;
        arrow.SetActive(true & isActive);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Lava>() != null) {
            isActive = false;
            UnHoldBlock();
            AudioManager.instance.FallRockLava();
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerInteraction>() != null) {
            UnHoldBlock();
        }
    }
}