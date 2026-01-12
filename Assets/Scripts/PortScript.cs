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
    private PortScript[] portArr;
	private PriceInfo[] lowestKnownPrices;
	private PriceInfo[] highestKnownPrices;
     
    // Start is called before the first frame update
    void Start()
    {
        int initialPop = Random.Range(100, 5000);
        market = new Market("Test", 100, initialPop); 

        int[] goodArr = new int[8];
 
        int good = Random.Range(1,4);
        int prodMethod = Random.Range(1,Market.prodMethodCount - 1);
        goodArr[0] = good;

        market.AddGood(new MarketGood((GoodsType)good, 5f, /*(ProductionMethod)prodMethod*/ProductionMethod.Subsistance));

        for (int i = 0; i < 7; i++)
        { 
            while (System.Array.IndexOf(goodArr, good) != -1 ) {
                good = Random.Range(1,Market.goodsTypeCount);
            }
            prodMethod = Random.Range(1,Market.prodMethodCount - 1);
            goodArr[i+1] = good;

            market.AddGood(new MarketGood((GoodsType)good, 5f, (ProductionMethod)prodMethod));
        }

        // market.AddGood(new MarketGood(GoodsType.Wheat, 5f, ProductionMethod.TwoPerPop));
        // market.AddGood(new MarketGood(GoodsType.Meat, 5f , ProductionMethod.TwoPerPop));


        Debug.Log(market.DebugPrintState());
        
        GameObject[] portObjArr = GameObject.FindGameObjectsWithTag("Port");

        portArr = new PortScript[portObjArr.Length];

        for(int i = 0; i < portObjArr.Length; i++)
        {
            portArr[i] = portObjArr[i].GetComponent<PortScript>();
        }
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

    //both of these are O(N^2) so if we could figure out a faster way to look up goods by type i would love that
    int FindMinGoodPrice (GoodsType goodType)
    {
        float minPrice = 100f;
        int index = -1;
        for(int i = 0; i < portArr.Length; i++)
        {
            Good thisGood = portArr[i].market.GetGood(goodType);
            if (thisGood == null)
            {
                continue;
            }
            if (thisGood.Price < minPrice)
            {
                index = i;
                minPrice = thisGood.Price;
            }
        }

        return index;
    }

    int FindMaxGoodPrice (GoodsType goodType)
    {
        float maxPrice = 100f;
        int index = -1;
        for(int i = 0; i < portArr.Length; i++)
        {
            Good thisGood = portArr[i].market.GetGood(goodType);
            if (thisGood == null)
            {
                continue;
            }
            if (thisGood.Price > maxPrice)
            {
                index = i;
                maxPrice = thisGood.Price;
            }
        }

        return index;
    }
}
