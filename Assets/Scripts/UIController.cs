using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    
    [SerializeField] AudioClip cauldronSFX, gameOverSFX;
    private AudioSource audioSource;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        audioSource = GetComponent<AudioSource>();


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne"))
        {
            audioSource.loop = true;
            audioSource.clip = cauldronSFX;
            audioSource.Play();
        }
        
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver"))
        {
            audioSource.clip = gameOverSFX;
            audioSource.Play();
        }
    }

    public void OnClickQuitButton()
    {
        print("Quit button was clicked");
        Application.Quit();
    }

    public void OnClickStartButton()
    {
        print("Starting game");
        SceneManager.LoadScene("LevelOne");
        
    }

    public void OnClickButtonHelp()
    {
        print("Help button was clicked");
        SceneManager.LoadScene("HELP");
    }

    public void OnClickButtonBack()
    {
        print("Back button was clicked");
        SceneManager.LoadScene("MainMenu");
    }
}
