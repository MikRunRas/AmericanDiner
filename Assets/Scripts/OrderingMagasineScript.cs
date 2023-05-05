using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderingMagasineScript : MonoBehaviour
{
    [SerializeField]
    private GameObject OrderingBookUI;
    [SerializeField]
    private GameObject playerStats;
    [SerializeField]
    private GameObject _glockOrder;
    [SerializeField]
    private Button _glockPurchaseButton;
    [SerializeField]
    private GameObject _shotgunOrder;
    [SerializeField]
    private Button _shotgunPurchaseButton;
    [SerializeField]
    private GameObject _sMGOrder;
    [SerializeField]
    private Button _sMGPurchaseButton;
    [SerializeField]
    private Canvas canvas;
    private StatsScript playerStatsScript;

    private void Start()
    {
        playerStatsScript = canvas.GetComponent<StatsScript>();
    }

    public void CloseOrderingBook()
    {
        OrderingBookUI.SetActive(false);
        playerStats.SetActive(true);
        Time.timeScale = 1f;
    }

    public void PurchaseGlock()
    {
        if(playerStatsScript.getFunds() >= 200)
        {
            _glockOrder.SetActive(true);
            _glockPurchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased!";
            _glockPurchaseButton.interactable = false;
            playerStatsScript.reduceFunds(200);
        }
    }

    public void PurchaseShotgun()
    {
        if (playerStatsScript.getFunds() >= 350)
        {
            _shotgunOrder.SetActive(true);
            _shotgunPurchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased!";
            _shotgunPurchaseButton.interactable = false;
            playerStatsScript.reduceFunds(350);
        }
    }

    public void PurchaseSMG()
    {
        if (playerStatsScript.getFunds() >= 500)
        {
            _sMGOrder.SetActive(true);
            _sMGPurchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased!";
            _sMGPurchaseButton.interactable = false;
            playerStatsScript.reduceFunds(500);
        }
    }
}
