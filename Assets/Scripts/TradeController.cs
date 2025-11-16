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
        protected string name, description;
        protected float price;
        protected int qty;
        protected GoodsCategory goodsCategory;

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
            this.goodsCategory = GoodsCategory.Undefined;
            this.description = "";
        }

        public string Name 
        {
            get
            {
                return this.name;
            }
        }

        public int Quantity 
        {
            get
            {
                return this.qty;
            }
        }

        public float Price
        {
            get
            {
                return this.price;
            }
        }

        public GoodsCategory Category
        {
            get
            {
                return this.goodsCategory;
            }
        }
    }

    public class MarketGood : Good
    {
        public int production, demand;
        private float basePrice;
        
        public MarketGood(string _name, float initialPrice, int initialQty, GoodsCategory _goodsCategory, int initialProduction) : base(_name, initialPrice, initialQty, _goodsCategory)
        {
            this.basePrice = initialPrice;
            this.production = initialProduction;
            this.demand = 20;
        }

        public void CalculatePrice()
        {
            if (this.qty == 0) {
                this.price = this.basePrice * 5.0f;
                return;
            }
            this.price = this.basePrice * Mathf.Clamp((this.demand / this.qty), 0. 1f, 3f);//experiment without the clamps
        }

        public void UpdateQty() {
            this.qty += this.production;
            this.qty -= this.demand;
            this.qty = Mathf.Max(this.qty, 0);
        }

    }

    public class Market
    {
        public const float updateRate = 1f;

        private List<MarketGood> goods;
        private int liquidCurrency;
        private string name;

        public Market(string _name, int startingCurrency)
        {
            this.name = _name;
            this.liquidCurrency = startingCurrency;
            this.goods = new List<MarketGood>();
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

        public List<MarketGood> Goods
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

        public MarketGood GetGood(int index)
        {
            return goods[index];
        }

        public void AddGood(MarketGood newGood)
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
                if (goods[i].Name == goodName)
                {
                    goods.RemoveAt(i);
                    break;
                } 
            }
        }

        public void UpdateMarket() 
        {
            for (int i = 0; i < goods.Count; i++) {
                goods[i].UpdateQty();
                goods[i].CalculatePrice();
            }
        }

        public void SortGoodsByName()
        {
            List<MarketGood> sorted = new List<MarketGood>();

            for (int i = 0; i < goods.Count; i++)
            {
                bool inserted = false;
                for (int o = 0; o < i; o++)
                {
                    if (goods[i].Name[0] < sorted[o].Name[0])
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

        public string DebugPrintState () {
            string DebugString = this.Name + " state:";
            for (int i = 0; i < this.goods.Count; i++) {
                MarketGood thisGood = goods[i]; 
                
                DebugString += "\n" + thisGood.Name + ": Qty: " + thisGood.Quantity + " - Price: " + thisGood.Price.ToString(".00#");
            }  

            return DebugString;
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