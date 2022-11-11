using UnityEngine;

public class PlayerTarget : MonoBehaviour {
    [SerializeField] Transform player;

    private void Start()
    {
        player = GameObject.Find("Look_Dir").transform;
    }
    void Update() {
        transform.position = player.position;
    }
}
