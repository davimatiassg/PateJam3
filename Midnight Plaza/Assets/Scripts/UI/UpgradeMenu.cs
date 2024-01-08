using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpgradeMenu : MonoBehaviour
{
    // the 3 lines below are sad
    [SerializeField] private TextMeshProUGUI priceHp;
    [SerializeField] private TextMeshProUGUI priceAtk;
    [SerializeField] private TextMeshProUGUI priceSpd;

    [SerializeField] private AudioClip clickSound;
    
    private AudioSource audioSource;

    private struct healthUpgrade {
        static public int cost = 100;
        static public int healthIncrease = 2;

        static public void Update() {
            cost += 200;
        }
    }

    private struct attackUpgrade {
        static public int cost = 200;
        static public int attackIncrease = 1;

        static public void Update() {
            cost += 500;
        }
    }

    private struct speedUpgrade {
        static public int cost = 100;
        static public float speedIncrease = 20f;

        static public void Update() {
            cost += 120;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateText() {
        Scorer.Score = DataTransfer.points;

        priceHp.text = "" + healthUpgrade.cost;
        priceAtk.text = "" + attackUpgrade.cost;
        priceSpd.text = "" + speedUpgrade.cost;
    }

    public void BuyHealthUpgrade() {

        if (healthUpgrade.cost <= DataTransfer.points) {
            DataTransfer.points -= healthUpgrade.cost;

            DataTransfer.playerMaxHp += healthUpgrade.healthIncrease;

            healthUpgrade.Update();
            UpdateText();
            audioSource.PlayOneShot(clickSound);
        }   
    }

    public void BuyAttackUpgrade() {

        if (attackUpgrade.cost <= DataTransfer.points) {
            DataTransfer.points -= attackUpgrade.cost;

            DataTransfer.playerAtk += attackUpgrade.attackIncrease;

            attackUpgrade.Update();
            UpdateText();
            audioSource.PlayOneShot(clickSound);
        }   
    }

    public void BuySpeedUpgrade() {

        if (speedUpgrade.cost <= DataTransfer.points) {
            DataTransfer.points -= speedUpgrade.cost;

            DataTransfer.playerSpd += speedUpgrade.speedIncrease;

            speedUpgrade.Update();
            UpdateText();
            audioSource.PlayOneShot(clickSound);
        }   
    }
}
