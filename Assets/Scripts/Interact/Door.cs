using UnityEngine;
public class Door : MonoBehaviour {
    Animator DoorAniamtor;
    private void Start() {
        DoorAniamtor = GetComponent<Animator>();
        DoorAniamtor.SetInteger("animator_parameter", 0);
    }

    public void Closed_Door_Animation() {
        DoorAniamtor.SetInteger("animator_parameter", 0);
    }

    public void Open_Door_Aniamtion() {
        DoorAniamtor.SetInteger("animator_parameter", 1);
        AudioManager.instance.OpenDoor();
    }

    public void Close_Door_Animation() {
        DoorAniamtor.SetInteger("animator_parameter", 2);
        AudioManager.instance.CloseDoor();
        Invoke("Closed_Door_Animation", 1);
    }

}
