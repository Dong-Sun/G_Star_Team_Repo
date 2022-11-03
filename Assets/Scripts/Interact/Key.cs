using UnityEngine;

public class Key : MonoBehaviour, Interact {
    private bool getKey = false;
    public bool GetKey { get => getKey; }
    private void Start() {
        AudioManager.instance.CreateKey();
    }
    private void Update() {
        transform.Rotate(Vector3.up * 60f * Time.deltaTime);
    }

    public void Work() {
        if (!GameManager.Game_Manager_Instance.Get_Stage_Key) {
            GameManager.Game_Manager_Instance.Get_Stage_Key = true;
            getKey = true;
            this.gameObject.transform.position += Vector3.down * 2;
            AudioManager.instance.GetKey();
            Destroy(this.gameObject, 3);
        }
    }
}
