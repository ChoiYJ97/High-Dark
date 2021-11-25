using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    static SceneManagerScript UniqueInstance;
    public static SceneManagerScript _instance
    {
        get { return UniqueInstance; }
    }

    public enum CurScene
    {
        EmptyS = 0,
        LobbyS,
        
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UniqueInstance = this;

        GoLobbyScene();
    }

    void Update()
    {
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        //에디터에 play버튼을 끄는 역활
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //빌드에서는 가능하지만 에디터에서는 안된다.
        Application.Quit();
#endif
    }

        public void GoLobbyScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }

    public void GoStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void GoBonFireScene()
    {
        SceneManager.LoadScene("BonFireScene");
    }

    public void GoTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoStoryScene()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void GoStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void GoResultScene()
    {

    }

    public void GoCharacterMakingScene()
    {

    }

}
