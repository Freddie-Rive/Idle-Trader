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
		Manufactured,
        Spices,
        Treasures
    }
	
	//replace name with a goodstype that contains alll the goodies. Probably replace goodscategory variable with a function that derives it from the goodstype
	public enum GoodsType
	{
		Undefined,
		Wheat,
		Rice,
		Meat,
		Fruit,
		Cotton,
		Wool,
		Hemp,
		Wood,
		Clothes,
		Tools,
		Weapons,
		Sheets,
		Utensils,
		Saffron,
		Pepper,
		Gold,
		Silver,
		Gemstones,
		Fine_Cutlery,
		Fine_Ornaments
	}

    public class Good
    {
        protected string name, description;
        protected float price;
        protected int qty; //could argue we need to have a float here? for smaller numbers? or limit production to min 1?
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
        private int production, demand;
        private float basePrice;
        
        public MarketGood(string _name, float initialPrice, int initialQty, GoodsCategory _goodsCategory, int initialProduction) : base(_name, initialPrice, initialQty, _goodsCategory)
        {
            this.basePrice = initialPrice;
            this.production = initialProduction;
            UpdateDemand();
        }
		
		public MarketGood(string _name, GoodsCategory _goodsCategory, float _basePrice, int population) {
			this.name = _name;
			this.qty = 0;
			this.goodsCategory = _goodsCategory;
			this.basePrice = _basePrice;
			this.description = "";
			
			UpdateProduction(population);
			UpdateDemand(population);
		}
		
		public int Production 
		{
				get
				{
					return this.production;
				}
				set
				{
					this.production = value;
				}
		}
		
		public int Demand 
		{
				get
				{
					return this.demand;
				}
				set
				{
					this.demand = value;
				}
		}
		
		public int TotalQuantity
		{
			get 
			{
					return this.qty + this.production;
			}
		}

        public void UpdatePrice()
        {
            if (this.TotalQuantity == 0) {
                this.price = this.basePrice * 5.0f;
                return;
            }
            this.price = this.basePrice * this.demand / this.TotalQuantity;//Mathf.Clamp((this.demand / this.TotalQuantity), 0.1f, 3f);//experiment without the clamps
        }

        public void UpdateQty() {
            this.qty += this.production;
            this.qty -= this.demand;
            this.qty = Mathf.Max(this.qty, 0);
        }
		
				
		// Calculates Production Modifiers
		//To Do: apply local production bonuses to various regions
		public void UpdateProduction (int population = 0)
		{
			switch (goodsCategory)
			{
				case GoodsCategory.Undefined:
					this.production = 0;
					break;
				case GoodsCategory.Food:
					this.production = Mathf.RoundToInt(Mathf.Log(population,1.001f)); //ideally, want reducing returns as population increases. im making a malthusian game. because i hate my simulated people
					break;
				case GoodsCategory.Fabrics:
					this.production = Mathf.RoundToInt(Mathf.Max(population - 1000,0) * 1.2f); //1000 population requirement to start producing, then will produce slightly more than 1 per person
					break;
				case GoodsCategory.Spices:
					this.production = Mathf.RoundToInt(Mathf.Max(population - 1000,0) * 2f); //1000 population requirement to start producing, then will produce 2 per person
					break;
				case GoodsCategory.Treasures:
					this.production = Mathf.RoundToInt(population * 0.5f);
					break;
				default:
					this.production = 0;
					break;
			}
		}
		
		public void UpdateDemand (int population = 0)
		{
			switch (goodsCategory)
			{
				case GoodsCategory.Undefined:
					this.demand = 0;
					break;
				case GoodsCategory.Food:
					this.demand = population; //everyone wants 1 food
					break;
				case GoodsCategory.Fabrics:
					this.demand = population; //everyone wants 1 fabric. for now. change later
					break;
				case GoodsCategory.Spices:
					this.demand = Mathf.RoundToInt(population * 2.5f); //everyone wants sooo many spices
					break;
				case GoodsCategory.Treasures:
					this.demand = Mathf.RoundToInt(population * 0.5f); //matches production. lets see what happens.
					break;
				default:
					this.demand = 0;
					break;
			}
		}

    }

    public class Market
    {
        public const float updateRate = 1f;

        private List<MarketGood> goods;
        private float liquidCurrency;
        private int population;
        private string name;
		private bool foodShortage; //either use to set up unqiue famine logic or just to show the player that there is a famine going on.

        public Market(string _name, int startingCurrency, int startingPopulation)
        {
            this.name = _name;
            this.liquidCurrency = startingCurrency;
            this.goods = new List<MarketGood>();
			this.population = startingPopulation;
        }

        public float LiquidCurrency
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
		
		public int Population 
		{
			get
			{
				return population;
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
		
		void UpdatePopulation() {
			if (foodShortage) {
				this.population = Mathf.RoundToInt(population * 0.8f);
			} else {
				this.population = Mathf.RoundToInt(population * 1.1f);
			}
		}

        public void UpdateMarket() 
        {
			//currently has 2 for loops. Could perhaps be reduced to 1 without much important missing functionality
			foodShortage = false;
			int popsFed = 0;
			
            for (int i = 0; i < goods.Count; i++) {
				if (goods[i].Category == GoodsCategory.Food) {
					popsFed += goods[i].TotalQuantity;
				}
				
                goods[i].UpdateQty();
            }
			
			if (population > popsFed) {
				foodShortage = true;
			}
			
			UpdatePopulation();
			
			for (int i = 0; i < goods.Count; i++) {
                goods[i].UpdateProduction(population);
				goods[i].UpdateDemand(population);
				goods[i].UpdatePrice();
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
			DebugString += "\nPopulation: " + population;
            for (int i = 0; i < this.goods.Count; i++) {
                MarketGood thisGood = goods[i]; 
                
                DebugString += "\n" + thisGood.Name + ":\n\tQty:"  + thisGood.Quantity + " \n\tDemand:" + thisGood.Demand + " \n\tProduction:" + thisGood.Production + "\n\tPrice:" + thisGood.Price.ToString(".00#");
            }  
			
			if (foodShortage) {
				DebugString += "\nFamine!";
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