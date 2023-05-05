using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI FundsText; 
    [SerializeField]
    private TextMeshProUGUI CriminalsText; 
    [SerializeField]
    private TextMeshProUGUI CiviliansText;
    private int funds = 0;
    private int civiliansKilled = 0;
    private int thievesKilled = 0;

    private void Start()
    {
        CiviliansText.text = "";
        CriminalsText.text = "";
        CustomerScript.OnOrderCompletedEvent += UpdateFunds;
        CustomerScript.OnCivilianKilledEvent += UpdateCivilians;
        PedestrianScript.OnCivilianKilledEvent += UpdateCivilians;
        ThiefScript.OnThiefKilledEvent += UpdateThieves;
        ThiefScript.OnThiefStoleMoneyEvent += UpdateFunds;
    }

    private void UpdateFunds(int price)
    {
        funds += price;
        FundsText.text = "Current funds: $" + funds;
    }

    private void UpdateCivilians()
    {
        civiliansKilled += 1;
        CiviliansText.text = "Civilians murdered: " + civiliansKilled;
    }

    private void UpdateThieves()
    {
        thievesKilled += 1;
        CriminalsText.text = "Criminals apprehended: " + thievesKilled;
    }

    public int getFunds()
    {
        return funds;
    }

    public void reduceFunds(int price)
    {
        funds = funds - price;
        FundsText.text = "Current funds: $" + funds;
    }
}