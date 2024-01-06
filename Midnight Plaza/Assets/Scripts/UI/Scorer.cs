using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Scorer : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI textMesh;
    private void OnEnable() {
        textMesh = GetComponent<TextMeshProUGUI>();
        GameDataManager.Instance.onGainScore += UpdateScore;
    }

    public void UpdateScore(IValuable value)
    {
        score += value.ScoreValue;
        textMesh.text = string.Format("{0}", score);
    }
}
