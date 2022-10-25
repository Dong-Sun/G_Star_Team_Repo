using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager playermanger)) {
            if (!GameManager.Game_Manager_Instance.Game_Stop)
                playermanger.Player_Dying();
        }
    }
}