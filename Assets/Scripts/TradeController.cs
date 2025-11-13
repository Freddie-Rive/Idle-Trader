using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trade
{

    public enum GoodsCategory
    {
        Undefined,
        Food,
        Fabrics,
        Spices,
        Treasures
    }

    public class Good
    {
        public string name, description;
        public float price;
        public int qty;
        public GoodsCategory goodsCategory;

        public Good(string _name, float initialPrice, int initialQty, GoodsCategory _goodsCategory)
        {
            this.name = _name;
            this.price = initialPrice;
            this.qty = initialQty;
            this.goodsCategory = _goodsCategory;

            this.description = "";
        }

        public Good()
        {
            this.name = "";
            this.price = 0.0f;
            this.qty = 0;
            this.goodsCategory = Undefined;
            this.description = "";
        }
    }

    public class MarketGood : Good
    {
        public int production, demand;
        private float basePrice;
        
        public MarketGood(string _name, float initialPrice, int initialQty, GoodsCategory _goodsCategory, int initialProduction)
        {
            Good(_name, initialPrice, initialQty, _goodsCategory);

            this.basePrice = initialPrice;
            this.production = initialProduction;
            this.demand = 0;
        }

        float CalculatePrice()
        {
            price = basePrice * Mathf.Clamp(qty / demand, 0.1, 2);//experiment without the clamps
        }

    }

    public class Market
    {
        private List<Good> goods;
        private int liquidCurrency;
        private string name;

        public Market(string _name, int startingCurrency)
        {
            this.name = _name;
            this.liquidCurrency = startingCurrency;
            this.goods = new List<Good>();
        }

        public int LiquidCurrency
        {
            get
            {
                return liquidCurrency;
            }
            set
            {
                liquidCurrency = Mathf.Max(value, 0);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<Good> Goods
        {
            get
            {
                return goods;
            }
            set
            {
                goods = value;
            }
        }

        public Good GetGood(int index)
        {
            return goods[index];
        }

        public void AddGood(Good newGood)
        {
            goods.Add(newGood);
        }

        public void RemoveGood(int index)
        {
            goods.RemoveAt(index);
        }

        public void RemoveGood(string goodName)
        {
            for (int i = 0; i < goods.Count; i++)
            {
                if (goods[i].name == goodName)
                {
                    goods.RemoveAt(i);
                    break;
                }
            }
        }

        public void SortGoodsByName()
        {
            List<Good> sorted = new List<Good>();

            for (int i = 0; i < goods.Count; i++)
            {
                bool inserted = false;
                for (int o = 0; o < i; o++)
                {
                    if (goods[i].name[0] < sorted[o].name[0])
                    {
                        sorted.Insert(o, goods[i]);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                {
                    sorted.Add(goods[i]);
                }
            }

            goods = sorted;
        }
    }

    public class TradeController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}