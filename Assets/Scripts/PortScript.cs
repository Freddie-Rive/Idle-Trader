using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trade;
using TMPro;

public class PortScript : MonoBehaviour
{

    public Market market;
    public GameObject debugText;  

    private float updateMarketTime;
     
    // Start is called before the first frame update
    void Start()
    {
        market = new Market("Test", 100);

        market.AddGood(new MarketGood("Wheat", 3.0f, 100, GoodsCategory.Treasures, 30));
        market.AddGood(new MarketGood("Wood", 4.0f, 100, GoodsCategory.Treasures, 22));
        market.AddGood(new MarketGood("Gemstones", 10.0f, 100, GoodsCategory.Treasures, 15));

        Debug.Log(market.DebugPrintState());
    }

    // Update is called once per frame
    void Update()
    {
        debugText.GetComponent<TextMeshProUGUI>().text = market.DebugPrintState();

        if (Time.time > updateMarketTime) {
            market.UpdateMarket();
            updateMarketTime += Market.updateRate;
        }
    }
}
