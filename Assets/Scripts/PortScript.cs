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
        market = new Market("Test", 100, 1000);

        market.AddGood(new MarketGood(GoodsType.Wheat, 5f, market.Population));
        market.AddGood(new MarketGood(GoodsType.Meat, 5f , market.Population));


        Debug.Log(market.DebugPrintState());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > updateMarketTime) {
            market.UpdateMarket();
			debugText.GetComponent<TextMeshPro>().text = market.DebugPrintState();
            updateMarketTime += Market.updateRate;
        }
    }
}
