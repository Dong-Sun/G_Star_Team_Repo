using UnityEngine;
public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] GameObject arrow;
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    [SerializeField] bool spawnFallSound = false;
    bool switching = true;
    bool isActive = true;

    Ray ray;
    RaycastHit hit;
    private void Start() {

        ray.direction = Vector3.down;
    }

    private void Update() {
        if (!switching) {
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
            ray.origin = transform.position;
            if (GameManager.Game_Manager_Instance.Player_Manager.Can_Move&&GameManager.Game_Manager_Instance.Player_Manager.Player_Move.Look_Dir.position == this.transform.position) {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 2)) {
                    if (hit.collider.transform.gameObject.name == "FloorBase" || hit.collider.GetComponent<Lava>() != null) {
                        if (hit.collider.GetComponent<Lava>() != null) {
                            AudioManager.instance.OneShotEvent("fallRockLava");
                            Destroy(hit.collider.gameObject);
                        }
                        isActive = false;
                        UnHoldBlock();
                        this.gameObject.layer = 0;
                        return;
                    }
                }
            }
        }
    }
    public void Work() {
        if (switching && isActive) {
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
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerInteraction>() != null) {
            UnHoldBlock();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.parent != null) {
            if (collision.transform.parent.name == "1stFloor" && spawnFallSound) {
                spawnFallSound = false;
                AudioManager.instance.OneShotEvent("fallRockFloor");
            }
        }
    }
}