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

        market.AddGood(new MarketGood("Wheat", GoodsCategory.Food, 5f, market.Population));
        market.AddGood(new MarketGood("Cloth", GoodsCategory.Fabrics, 30f , market.Population));
        market.AddGood(new MarketGood("Gemstones", GoodsCategory.Treasures, 100f, market.Population));

        Debug.Log(market.DebugPrintState());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > updateMarketTime) {
            market.UpdateMarket();
			debugText.GetComponent<TextMeshProUGUI>().text = market.DebugPrintState();
            updateMarketTime += Market.updateRate;
        }
    }
}
