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
        else if (g.GetComponent<PlayerMove>()) {
            Debug.Log("용암에 빠졌어용");
        }
    }
}