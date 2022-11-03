using UnityEngine;

public class PapyrusController : MonoBehaviour {
    [SerializeField] Animator tutorial;
    bool isActive = false;
    private void Update() {
        transform.rotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !isActive) {
            tutorial.SetBool("Active", true);
            AudioManager.instance.PaperOpen();
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" && isActive) {
            tutorial.SetBool("Active", false);
            AudioManager.instance.PaperClose();
            isActive = false;
        }
    }
}
