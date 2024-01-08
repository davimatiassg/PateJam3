using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private AudioClip actionMusic;
    [SerializeField] private AudioClip shopMusic;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip gameOverSound;

    private bool paused = false;
    private bool HotelMenu = false;
    private PlayerBehaviour player;

    private AudioSource audioSource;
    private float actionMusicTime;
    private float shopMusicTime;

    void Awake () {
        GameDataManager.Instance.onPlayerDie += PlayerDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        audioSource = GetComponent<AudioSource>();
        playActionMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu")) {

            player.ToggleFreeze();
            HotelMenu = !HotelMenu;

            LoadUnloadHotel(HotelMenu);

        }

        if (Input.GetButtonDown("Pause")) {
            TogglePause();
        }
    }

    void LoadUnloadHotel(bool load) {

        if (load) {
            // Loading hotel scene
            DataTransfer.points = Scorer.Score;
            ActivateDeactivateChildren(!load);
            SceneManager.LoadScene("InsideHotel", LoadSceneMode.Additive);
            
            playShopMusic();
        }
        else {
            // Returning to game
            GameObject.Find("EventSystemHotel").SetActive(false);
            GameObject.Find("CameraHotel").SetActive(false);

            SceneManager.UnloadSceneAsync("InsideHotel");

            ActivateDeactivateChildren(!load);
            Scorer.Score = DataTransfer.points;
            player.UpdateStats();

            playActionMusic();
        }
    }

    void ActivateDeactivateChildren(bool active) {

        for (int i = 0;i < transform.childCount;i ++) {
            transform.GetChild(i).gameObject.SetActive(active);
        }
        
    }

    void TogglePause() {
        if (paused) {
            ResumeGame();
        }
        else {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        
        canvas.Find("NonPause").gameObject.SetActive(false);
        canvas.Find("Pause").gameObject.SetActive(true);

        paused = true;
    }

    public void ResumeGame()
    {
        audioSource.PlayOneShot(clickSound);
        canvas.Find("NonPause").gameObject.SetActive(true);
        canvas.Find("Pause").gameObject.SetActive(false);

        Time.timeScale = 1f;
        paused = false;
    }

    public void SliderChange(float value) {
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        audioSource.PlayOneShot(clickSound);
        Application.Quit();
    }

    public void PlayerDead()
    {
        StopMusic();
        audioSource.PlayOneShot(gameOverSound);

        paused = true;
        PauseGame();
        canvas.Find("Pause").gameObject.SetActive(false);
        canvas.Find("GameOverScreen").gameObject.SetActive(true);
    }

    public void RestartGame() {
        audioSource.PlayOneShot(clickSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene("cityScene");
    }

    public void playActionMusic() { // this deserves punishment?
        shopMusicTime = audioSource.time;
        StopMusic();
        audioSource.clip = actionMusic;
        audioSource.Play();
        audioSource.time = actionMusicTime;
    }

    public void playShopMusic() { // this is deserves punishment?
        actionMusicTime = audioSource.time;
        StopMusic();
        audioSource.clip = shopMusic;
        audioSource.Play();
        audioSource.time = shopMusicTime;
    }

    public void StopMusic() {
        audioSource.Stop();
    }

}
