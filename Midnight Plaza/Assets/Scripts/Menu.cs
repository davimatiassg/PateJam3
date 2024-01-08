using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform canvas;

    private bool paused = false;
    private bool HotelMenu = false;
    private PlayerBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
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
        }
        else {
            // Returning to game
            GameObject.Find("EventSystemHotel").SetActive(false);
            GameObject.Find("CameraHotel").SetActive(false);

            SceneManager.UnloadSceneAsync("InsideHotel");

            ActivateDeactivateChildren(!load);
            Scorer.Score = DataTransfer.points;
            player.UpdateStats();
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
        Application.Quit();
    }

}
