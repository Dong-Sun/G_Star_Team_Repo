using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    GameObject g;
    private void OnTriggerEnter(Collider other) {
        g = other.gameObject;
        if (g.GetComponent<HoldingBlock>()) {
            g.transform.SetParent(null);
        }
        else if (TryGetComponent<PlayerManager>(out PlayerManager playermanger) == true) {
            GameManager.Game_Manager_Instance.Player_Die();
        }
    }
}