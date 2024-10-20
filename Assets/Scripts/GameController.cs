using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    AudioClip _buttonPressSound;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void FireButton()
    {
        PlayButtonPressSound();
        PlayerBehaviour.instance.Shoot();
    }

    public void Restart()
    {
        Debug.Log("GAME RESTART");
        ScoreManager.instance.DisableGOScreen();
        SceneManager.LoadScene(1);
    }

    void PlayButtonPressSound()
    {
        SoundManager.instance.PlayAudioClip(_buttonPressSound);
    }

    public IEnumerator SoundRoutine()
    {
        PlayButtonPressSound();
        yield return new WaitForSeconds(1.0f);
    }
}
