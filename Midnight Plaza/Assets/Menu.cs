using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Space)) {

            player.ToggleFreeze();
            HotelMenu = !HotelMenu;

            LoadUnloadHotel(HotelMenu);

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
}
