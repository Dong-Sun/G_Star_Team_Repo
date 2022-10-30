using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    [SerializeField] Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() {
        transform.position = player.position;
    }
}
