using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour {
    public static SceneLoadManager scene_load_manager_instance;
    public Fade Fade_UI_Control;
    public bool SceneChanging = false;
    public float holdingTimer;


    private void Awake() {
        if (scene_load_manager_instance == null) {
            scene_load_manager_instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!SceneChanging)
        {
            Rewind_To_Start();
            Rewind_To_Current();
            ToIntro();
            Skip_Intro();
        }
    }


    private void Rewind_To_Start()
    {
        if (CurrentSceneIndex() >= 1)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                holdingTimer += Time.deltaTime;

            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                holdingTimer = 0;
            }
            if (holdingTimer > 2f)
            {
                holdingTimer = 0;
                Start_Scene_Load(0);
            }
        }
    }


    private void Rewind_To_Current()
    {

        if (CurrentSceneIndex() >= 2)
        {
            if (Input.GetKey(KeyCode.R))
            {
                holdingTimer += Time.deltaTime;

            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                holdingTimer = 0;
            }
            if (holdingTimer > 2f)
            {
                holdingTimer = 0;
                CurrentSceneLoad(2);
            }
        }
    }


    private void Skip_Intro()
    {
        if (CurrentSceneIndex() == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                holdingTimer += Time.deltaTime;

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                holdingTimer = 0;
            }
            if (holdingTimer > 2f)
            {
                holdingTimer = 0;
                NextSceneLoad(2);
                
            }
        }
    }
    private void ToIntro()
    {
        if (CurrentSceneIndex() == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("Start", true);
                NextSceneLoad(2);
            }
        }
    }

    public void NextSceneLoad(int time) {
        if (!(CurrentSceneIndex() == SceneManager.sceneCountInBuildSettings - 1))
        {
            StartCoroutine(LoadScene(CurrentSceneIndex() + 1, time));
        }
        else
            StartCoroutine(LoadScene(0, time));
    }

    public void CurrentSceneLoad(int time) {

        StartCoroutine(LoadScene(CurrentSceneIndex(), time));
    }
    public void Start_Scene_Load(int time)
    {
        
        StartCoroutine(LoadScene(0, time));
    }
    
    IEnumerator LoadScene(int buildIndex, float Time) {
        SceneChanging = true;
        Fade_UI_Control.Fade_in = true;
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(buildIndex);
        AudioManager.instance.ChangeBackSound(buildIndex);
        GameManager.Game_Manager_Instance.Initialize_GameData();
    }

    public int CurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
