using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    [SerializeField] Transform player;

    void Update() {
        transform.position = player.position;
    }
}
