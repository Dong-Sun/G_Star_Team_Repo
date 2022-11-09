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
        AudioManager.instance.OneShotEvent("openDoor");
    }

    public void Close_Door_Animation() {
        DoorAniamtor.SetInteger("animator_parameter", 2);
        AudioManager.instance.OneShotEvent("closeDoor");
        Invoke("Closed_Door_Animation", 1);
    }

}
