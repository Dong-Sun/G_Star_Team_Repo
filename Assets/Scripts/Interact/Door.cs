using UnityEngine;
public class Door : MonoBehaviour {
    Animator DoorAniamtor;
    
    private void Start() {
        if (this.tag == "Entrance")
        {
            GameManager.Game_Manager_Instance.Entrance = this;
        }
        else if(this.tag == "Exit")
        {
            GameManager.Game_Manager_Instance.Exit = this;
        }
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
