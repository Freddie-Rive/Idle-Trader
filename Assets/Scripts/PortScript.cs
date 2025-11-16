using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trade;

public class PortScript : MonoBehaviour
{

    public Market market;
    public GameObject debugText;  
     
    // Start is called before the first frame update
    void Start()
    {
        market = new Market("Test", 100);

        market.AddGood(new MarketGood("Wheat", 3.0f, 3, GoodsCategory.Treasures, 1));
        market.AddGood(new MarketGood("Wood", 4.0f, 3, GoodsCategory.Treasures, 1));
        market.AddGood(new MarketGood("Gemstones", 10.0f, 3, GoodsCategory.Treasures, 1));

        Debug.Log(market.DebugPrintState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
