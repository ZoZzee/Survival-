using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public int firstScene;

    public GameObject settingsPanel;
    public Animator blackScreen;

    public void NewGame()
    {
        blackScreen.Play("BlackScreenOn");
        Invoke(nameof(LoadScene), 0.5f);
    }

    public void SettingSetState(bool state)
    {
        settingsPanel.SetActive(state);
    }

    public void Exid()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(firstScene);
    }
}
