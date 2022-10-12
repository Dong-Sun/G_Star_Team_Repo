using UnityEngine;
using DataStruct;
public class HoldingBlock : MonoBehaviour, Interact {
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform floor;
    bool switching = false;
    public void Work() {
        Debug.Log("HoldingBlock");
        switching = !switching;
        if (switching) {
            if (playerTarget != null) {
                PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Block;
                transform.SetParent(playerTarget);
            }
        }
        else {
            if (floor != null) {
                PlayerManager.Player_Manager_Instance.player_animator_parameter = Player_Animator_Parameter.Idle;
                transform.SetParent(floor);
            }
        }
    }
}