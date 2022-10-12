using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Camera
{
    [SerializeField] float shakeAmount;
    [SerializeField] bool isShake = false;
    CameraController cameraController;
    private void Start() {
        cameraController = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cameraController.isRotate);
        if (cameraController.isRotate) {
            if (isShake) {
                transform.localPosition = initVector[(int)cameraController.direction] + Random.insideUnitSphere * shakeAmount;
            }
            else {
                transform.localPosition = initVector[(int)cameraController.direction];
            }
        }
    }
}
