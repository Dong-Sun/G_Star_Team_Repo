using UnityEngine;

public class QuestController : MonoBehaviour {
    [SerializeField] GameObject questBar;
    [SerializeField] Quest[] quests;
    private void Update() {
        if (SceneLoadManager.scene_load_manager_instance.SceneChanging)
            questBar.SetActive(false);
        else
            questBar.SetActive(true);
    }


    public void CompletionQuest(int index) {
        quests[index].count--;
    }
}
