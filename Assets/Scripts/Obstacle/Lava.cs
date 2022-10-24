using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<HoldingBlock>(out HoldingBlock holdBlock)==true) {
            holdBlock.transform.SetParent(null);
            Destroy(this);
        }
        else if (other.TryGetComponent<PlayerManager>(out PlayerManager playermanger) == true) {
            if (GameManager.Game_Manager_Instance.Game_Stop ==false)
                playermanger.Player_Dying();
        }
    }
}