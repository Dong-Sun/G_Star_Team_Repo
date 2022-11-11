using UnityEngine;
public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] GameObject arrow;
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    bool switching = true;
    bool isActive = true;
    [SerializeField] bool spawnFallSound = false;

    private void Update() {
        if (!switching) {
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        }
    }

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
        GameManager.Game_Manager_Instance.Player_Manager.Holding_Block = true;
        transform.SetParent(playerTarget);
        switching = false;
        arrow.SetActive(false);
    }
    private void UnHoldBlock() {
        GameManager.Game_Manager_Instance.Player_Manager.Holding_Block = false;
        transform.SetParent(floor);
        switching = true;
        arrow.SetActive(true & isActive);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Lava>() != null) {
            isActive = false;
            UnHoldBlock();
            AudioManager.instance.OneShotEvent("fallRockLava");
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerInteraction>() != null) {
            UnHoldBlock();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.name == "FloorBase") {
            isActive = false;
            arrow.SetActive(true & isActive);
            this.gameObject.layer = 0;
        }
        if (collision.transform.parent != null) {
            if (collision.transform.parent.name == "1stFloor" && spawnFallSound) {
                spawnFallSound = false;
                AudioManager.instance.OneShotEvent("fallRockFloor");
            }
        }
    }
}