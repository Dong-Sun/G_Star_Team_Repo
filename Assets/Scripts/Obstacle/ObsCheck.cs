using UnityEngine;

public class ObsCheck : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Obstacle obs = other.GetComponent<Obstacle>();
        if (obs != null) {
            obs.Work();
        }
    }
}
