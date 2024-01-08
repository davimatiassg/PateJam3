using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Volume", 0.5f);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("cityScene");
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        audioSource.PlayOneShot(clickSound);
        Application.Quit();
    }

    public void SliderChange(float value) {
        PlayerPrefs.SetFloat("Volume", value);
    }
}
