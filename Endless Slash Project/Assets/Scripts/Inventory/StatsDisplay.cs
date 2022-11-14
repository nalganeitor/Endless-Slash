using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI damage, defense, health;

    [SerializeField] PlayerManager playerManager;

    private int totalDamage;
    private int totalDefense;
    private int totalHealth;

    void Update()
    {
        damage.text = playerManager.totalDamage.ToString();
        defense.text = playerManager.totalDefense.ToString();
        health.text = playerManager.totalHealth.ToString();
    }
}
