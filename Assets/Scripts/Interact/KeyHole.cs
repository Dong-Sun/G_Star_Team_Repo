using UnityEngine;
using UnityEngine.Events;

public class KeyHole : MonoBehaviour, Interact {
    [SerializeField] UnityEvent quest;
    public GameObject KeyObj;
    [SerializeField] GameObject arrow;
    bool oneTake = true;

    private void Update() {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key && oneTake) {
            arrow.SetActive(true);
            oneTake = false;
        }
    }

    public void Work() {
        if (GameManager.Game_Manager_Instance.Get_Stage_Key) {
            arrow.SetActive(false);
            KeyObj.SetActive(true);
            quest.Invoke();
            StartCoroutine(GameManager.Game_Manager_Instance.End_Animation_Coroutine());
            GameManager.Game_Manager_Instance.Get_Stage_Key = false;
        }
    }
}
