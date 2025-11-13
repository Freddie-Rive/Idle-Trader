using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trade;

public class PortScript : MonoBehaviour
{

    public Market market;
     
    // Start is called before the first frame update
    void Start()
    {
        market = new Market("Test", 100);

        market.AddGood(new Good("Goods", 10.0f, 3, GoodsCategory.Treasures));

        Debug.Log(market.GetGood(0).name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
