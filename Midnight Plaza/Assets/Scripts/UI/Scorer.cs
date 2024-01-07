using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Scorer : MonoBehaviour
{
    public static Scorer Instance;
    public int score;

    public static int Score
    {
        get
        {
            if(Instance == null) { return 0; }
            return Instance.score;
        }
        set
        {
            if(Instance == null) { return; }
            Instance.score = value;
            Instance.UpdateScoreText();
        }        
    }
    private TextMeshProUGUI textMesh;
    private void OnEnable() {
        if(Scorer.Instance == null){ Scorer.Instance = this; }
        else if(Scorer.Instance != this) {Destroy(this.gameObject); return; }
        textMesh = GetComponent<TextMeshProUGUI>();
        GameDataManager.Instance.onGainScore += AddScore;
    }

    public void Start() {
        score = 100;
        UpdateScoreText();
    }

    public void AddScore(IValuable value)
    {
        score += value.ScoreValue;
        UpdateScoreText();
    }

    void UpdateScoreText() {
        textMesh.text = string.Format("{0}", score);
    }

}