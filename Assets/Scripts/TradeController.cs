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
        Silk,
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
        protected GoodsType goodType;
        protected string description;
        protected float price;
        protected int qty; //could argue we need to have a float here? for smaller numbers? or limit production to min 1?
        protected GoodsCategory goodsCategory; //setup based on the goodsType, shouldn't be settable outside the object

        public Good(GoodsType type, float initialPrice, int initialQty)
        {
            this.goodType = type;
            this.price = initialPrice;
            this.qty = initialQty;
            SetGoodsCategory();

            this.description = "";
        }

        public Good()
        {
            this.goodType = GoodsType.Undefined;
            this.price = 0.0f;
            this.qty = 0;
            this.goodsCategory = GoodsCategory.Undefined;
            this.description = "";
        }

        public string Name 
        {
            get
            {
                switch(this.goodType)
                {
                    case GoodsType.Wheat:
                        return "Wheat";
                    case GoodsType.Rice:
                        return "Rice";
                    case GoodsType.Meat:
                        return "Meat";
                    case GoodsType.Fruit:
                        return "Fruit";
                    case GoodsType.Cotton:
                        return "Cotton";
                    case GoodsType.Wool:
                        return "Wool";
                    case GoodsType.Hemp:
                        return "Hemp";
                    case GoodsType.Silk:
                        return "Silk";
                    case GoodsType.Wood:
                        return "Wood";
                    case GoodsType.Clothes:
                        return "Clothes";
                    case GoodsType.Tools:
                        return "Tools";
                    case GoodsType.Weapons:
                        return "Weapons";
                    case GoodsType.Sheets:
                        return "Sheets";
                    case GoodsType.Utensils:
                        return "Utensils";
                    case GoodsType.Saffron:
                        return "Saffron";
                    case GoodsType.Pepper:
                        return "Pepper";
                    case GoodsType.Gold:
                        return "Gold";
                    case GoodsType.Silver:
                        return "Silver";
                    case GoodsType.Gemstones:
                        return "Gemstones";
                    case GoodsType.Fine_Cutlery:
                        return "Fine Cutlery";
                    case GoodsType.Fine_Ornaments:
                        return "Fine Ornaments";
                    default:
                        return "Undefined";
                }
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

        protected void SetGoodsCategory () 
        {
            switch(this.goodType)
            {
                case GoodsType.Wheat:
                case GoodsType.Rice:
                case GoodsType.Meat:
                case GoodsType.Fruit:
                    this.goodsCategory = GoodsCategory.Food;
                    break;
                case GoodsType.Cotton:
                case GoodsType.Wool:
                case GoodsType.Hemp:
                case GoodsType.Silk:
                    this.goodsCategory = GoodsCategory.Fabrics;
                    break;
                case GoodsType.Wood:
                case GoodsType.Clothes:
                case GoodsType.Tools:
                case GoodsType.Weapons:
                case GoodsType.Sheets:
                case GoodsType.Utensils:
                    this.goodsCategory = GoodsCategory.Manufactured;
                    break;
                case GoodsType.Saffron:
                case GoodsType.Pepper:
                    this.goodsCategory = GoodsCategory.Spices;
                    break;
                case GoodsType.Gold:
                case GoodsType.Silver:
                case GoodsType.Gemstones:
                case GoodsType.Fine_Cutlery:
                case GoodsType.Fine_Ornaments:
                    this.goodsCategory = GoodsCategory.Treasures;
                    break;
                default:
                    this.goodsCategory = GoodsCategory.Undefined;
                    break;
            }
        }

        public static float WeightPerGoodsType(GoodsType type) {
            switch(type)
                {
                case GoodsType.Wheat:
                    return 1f;
                case GoodsType.Rice:
                    return 1f;
                case GoodsType.Meat:
                    return 2f;
                case GoodsType.Fruit:
                    return 1.5f;
                case GoodsType.Cotton:
                    return 1f;
                case GoodsType.Wool:
                    return 1.5f;
                case GoodsType.Hemp:
                    return 0.5f;
                case GoodsType.Silk:
                    return 3f;
                case GoodsType.Wood:
                    return 0.5f;
                case GoodsType.Clothes:
                    return 1f;
                case GoodsType.Tools:
                    return 2f;
                case GoodsType.Weapons:
                    return 1.5f;
                case GoodsType.Sheets:
                    return 0.7f;
                case GoodsType.Utensils:
                    return 1f;
                case GoodsType.Saffron:
                    return 1f;
                case GoodsType.Pepper:
                    return 3f;
                case GoodsType.Gold:
                    return 7f;
                case GoodsType.Silver:
                    return 1f;
                case GoodsType.Gemstones:
                    return 10f;
                case GoodsType.Fine_Cutlery:
                    return 5f;
                case GoodsType.Fine_Ornaments:
                    return 7f;
                default:
                    return 0f;
            }
        }
    }

    public class MarketGood : Good
    {
        private int production;
        private float basePrice;

        //mostly deprecated        
        public MarketGood(GoodsType type, float initialPrice, int initialQty, int initialProduction) : base(type, initialPrice, initialQty)
        {
            this.basePrice = initialPrice;
            this.production = initialProduction;
        }
		
		public MarketGood(GoodsType type, float _basePrice, int population) {
			this.goodType = type;
			this.qty = 0;
			this.basePrice = _basePrice;
			this.description = "";
			
            SetGoodsCategory();
			UpdateProduction(population);
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
		
		public int TotalQuantity
		{
			get 
			{
                return this.qty + this.production;
			}
		}

        public void UpdatePrice(int demand)
        {
            if (this.TotalQuantity == 0) {
                this.price = this.basePrice * 5.0f;
                return;
            }
            this.price = this.basePrice * demand / this.TotalQuantity;//Mathf.Clamp((this.demand / this.TotalQuantity), 0.1f, 3f);//experiment without the clamps
        }

        public void UpdateQty(int demand) 
        {
            this.qty += this.production;
            this.qty -= demand;
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
    }

    public class Market
    {
        public const float updateRate = 1f;

        private List<MarketGood> goods;
        private float liquidCurrency;
        private int population;
        private string name;
		private bool foodShortage; //either use to set up unqiue famine logic or just to show the player that there is a famine going on.

        private int[] demand = new int[System.Enum.GetNames(typeof(GoodsCategory)).Length];
        // private int[] localProductionModifiers = new int[System.Enum.GetNames(typeof(GoodsType)).Length];

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

        public void UpdateDemand (int population = 0)
		{
            demand[(int)GoodsCategory.Undefined] = 0;
            demand[(int)GoodsCategory.Food] = population;
            demand[(int)GoodsCategory.Fabrics] = population;
            demand[(int)GoodsCategory.Manufactured] = Mathf.RoundToInt(population * 2.5f);
            demand[(int)GoodsCategory.Spices] = Mathf.RoundToInt(population * 0.5f);
            demand[(int)GoodsCategory.Treasures] = Mathf.RoundToInt(population * 0.5f);
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
				
                goods[i].UpdateQty(1);
            }
			
			if (population > popsFed) {
				foodShortage = true;
			}
			
			UpdatePopulation();
			
			for (int i = 0; i < goods.Count; i++) {
                goods[i].UpdateProduction(population);
				goods[i].UpdatePrice(1);
            }

            UpdateDemand();
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
                
                DebugString += "\n" + thisGood.Name + ":\n\tQty:"  + thisGood.Quantity/* + " \n\tDemand:" + thisGood.Demand*/ + " \n\tProduction:" + thisGood.Production + "\n\tPrice:" + thisGood.Price.ToString(".00#");
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