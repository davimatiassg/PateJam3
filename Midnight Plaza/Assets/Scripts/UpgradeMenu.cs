using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpgradeMenu : MonoBehaviour
{
    // the below 3 lines are sad
    [SerializeField] private TextMeshProUGUI priceHp;
    [SerializeField] private TextMeshProUGUI priceAtk;
    [SerializeField] private TextMeshProUGUI priceSpd;

    private TextMeshProUGUI pointsText;

    private struct healthUpgrade {
        static public int cost = 10;
        static public int healthIncrease = 2;

        static public void Update() {
            cost += 5;
        }
    }

    private struct attackUpgrade {
        static public int cost = 20;
        static public int attackIncrease = 1;

        static public void Update() {
            cost += 10;
        }
    }

    private struct speedUpgrade {
        static public int cost = 10;
        static public float speedIncrease = 20f;

        static public void Update() {
            cost += 7;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pointsText = transform.Find("Points").GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateText() {
        pointsText.text= "Points: " + DataTransfer.points;

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
        }   
    }

    public void BuyAttackUpgrade() {

        if (attackUpgrade.cost <= DataTransfer.points) {
            DataTransfer.points -= attackUpgrade.cost;

            DataTransfer.playerAtk += attackUpgrade.attackIncrease;

            attackUpgrade.Update();
            UpdateText();
        }   
    }

    public void BuySpeedUpgrade() {

        if (speedUpgrade.cost <= DataTransfer.points) {
            DataTransfer.points -= speedUpgrade.cost;

            DataTransfer.playerSpd += speedUpgrade.speedIncrease;

            speedUpgrade.Update();
            UpdateText();
        }   
    }
}
