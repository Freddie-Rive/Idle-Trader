using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
	
	//Will be used by ProductionFacility to return a different amount of 
	public enum ProductionMethod
	{
		Undefined,
		EquivalentToPop,
		HalfOfPop,
		TwoPerPop,
        Subsistance,
		NoProduction
	}
	
	//ports & ships will use these to trade imperfect info on markets
	public struct PriceInfo {
		public int dayRecorded;
		public float price;
		public int portIndex;
	}

	
	//will be used by markets to produce goods. probably will be included in the MarketGood object
	public class ProductionFacility
	{
			private string name;
			private GoodsType goodType;
			private ProductionMethod productionMethod;
			private bool isUpgradable, isStackable;
			private int levels;
			
			public ProductionFacility (string _name, GoodsType _goodType,  ProductionMethod _productionMethod, bool _upgradable, bool _stackable, int _levels = 1) 
			{
				this.name = _name;
				this.goodType = _goodType;
				this.productionMethod = _productionMethod;
				this.isUpgradable  = _upgradable;
				this.isStackable = _stackable;
				this.levels = _levels;
			}
			
			public ProductionFacility() 
			{
				this.name = "Undefined";
				this.goodType = GoodsType.Undefined;
				this.productionMethod = ProductionMethod.Undefined;
				this.isUpgradable = false;
				this.isStackable = false;
				this.levels = 0;
			}
			
			//using just these two values, derive a template production facility
			// public ProductionFacility(GoodsType _goodType, ProductionMethod _productionMethod) {
                
    		// }
			
			public int Levels
			{
				get
				{
					return this.levels;
				}
			}
			
			public static int MaxEmployeeCountPerLevel = 10000;
				
			
			public int GetProduction(int employees) 
			{
				switch(this.productionMethod) 
                {
                    case ProductionMethod.Undefined:
                        return 0;
                    case ProductionMethod.EquivalentToPop:
                        return employees;
                    case ProductionMethod.HalfOfPop:
                        return employees / 2;
                    case ProductionMethod.TwoPerPop:
                        return employees * 2;
                    case ProductionMethod.Subsistance:
                        return Mathf.RoundToInt(Mathf.Log(employees,1.001f));;
                    case ProductionMethod.NoProduction:
                        return 0;
                    default:
                        return 0;
                }
			}
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
               return goodType.ToString();
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
		
		public GoodsType Type
		{
			get
			{
				return this.goodType;
			}
		}
		
		public int categoryIndex
		{
			get
			{
				return (int)this.goodsCategory;
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
        private ProductionFacility productionFacility;

        //mostly deprecated        
        public MarketGood(GoodsType type, float initialPrice, int initialQty, int initialProduction) : base(type, initialPrice, initialQty)
        {
            this.basePrice = initialPrice;
            //this.production = initialProduction;
			
			this.productionFacility = new ProductionFacility("Temp", type, ProductionMethod.EquivalentToPop, true, true, 1);
        }
		
		public MarketGood(GoodsType type, float _basePrice, ProductionMethod _productionMethod) {
			this.goodType = type;
			this.qty = 0;
			this.basePrice = _basePrice;
			this.description = "";
			
			this.productionFacility = new ProductionFacility("Temp", type, _productionMethod, true, true, 1);
			
            SetGoodsCategory();
		}
		
		public int Production
		{
			get
			{
				return this.production;
			}
		}
		
		public float BasePrice
		{
			get
			{
				return this.basePrice;
			}
		}
		
		public int TotalQuantity
		{
			get 
			{
                return this.qty + this.production;
			}
		}
		
		public int WeightedQuantity
		{
			get
			{
				return Mathf.RoundToInt(this.qty * WeightPerGoodsType(this.goodType));
			}
		}

        public void UpdatePrice(int demand, float weightedPercQty)
        {
            if (this.TotalQuantity == 0) {
                this.price = this.basePrice * 5.0f;
                return;
            }
			
			float weightedDemand = demand * weightedPercQty;
			
			//we use weighteddemand to increase price of more weighted objects in the numerator, and the actual usage of the good in the denominator since that is the amount that will change.
            this.price = this.basePrice * (weightedDemand / (this.TotalQuantity + (this.production - demand)));//Mathf.Clamp((this.demand / this.TotalQuantity), 0.1f, 3f)
        }

        public void UpdateQty(int demand) 
        {
            this.qty += this.production;
            this.qty -= demand;
            this.qty = Mathf.Max(this.qty, 0);
        }
		
		public ProductionFacility Facility 
		{
			get
			{
				return this.productionFacility;
			}
		}
				
		// Calculates Production Modifiers
		//To Do: apply local production bonuses to various regions
		public void UpdateProduction (int employees = 0)
		{
			this.production = this.productionFacility.GetProduction(employees);
		//	switch (goodsCategory)
		//	{
		//		case GoodsCategory.Undefined:
		//			this.production = 0;
		//			break;
		//		case GoodsCategory.Food:
		//			this.production = Mathf.RoundToInt(Mathf.Log(population,1.001f)); //ideally, want reducing returns as population increases. im making a malthusian game. because i hate my simulated people
		//			break;
		//		case GoodsCategory.Fabrics:
		//			this.production = Mathf.RoundToInt(Mathf.Max(population - 1000,0) * 1.2f); //1000 population requirement to start producing, then will produce slightly more than 1 per person
		//			break;
		//		case GoodsCategory.Manufactured: 
		//			this.production = Mathf.RoundToInt(Mathf.Max(population / 100)); //this is a low base level that would represent some form of artisans, to be overwritten by proper guilds / manufactories
		//			break;
		//		case GoodsCategory.Spices:
		//			this.production = Mathf.RoundToInt(Mathf.Max(population - 1000,0) * 2f); //1000 population requirement to start producing, then will produce 2 per person
		//			break;
		//		case GoodsCategory.Treasures:
		//			this.production = Mathf.RoundToInt(population * 0.5f);
		//			break;
		//		default:
		//			this.production = 0;
		//			break;
		//	}
		}
    }

    public class Market
    {
        public static int goodsCategoryCount = System.Enum.GetNames(typeof(GoodsCategory)).Length;
        public static int goodsTypeCount = System.Enum.GetNames(typeof(GoodsType)).Length;
        public static int prodMethodCount = System.Enum.GetNames(typeof(ProductionMethod)).Length;

        public const float updateRate = 1f;

        private List<MarketGood> goods;
        private float liquidCurrency;
        private int population;
        private string name;
		private bool foodShortage, unemployment; //either use to set up unqiue famine logic or just to show the player that there is a famine going on.

        private int[] demand = new int[goodsCategoryCount];
		private int[] supply = new int[goodsCategoryCount];
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

        public MarketGood GetGood(GoodsType type)
        {
            foreach(MarketGood good in goods)
            {
                if (good.Type == type)
                {
                    return good;
                }
            }
            return null;
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

        public void UpdateDemand ()
		{
            demand[(int)GoodsCategory.Undefined] = 0;
            demand[(int)GoodsCategory.Food] = this.population;
            demand[(int)GoodsCategory.Fabrics] = this.population;
            demand[(int)GoodsCategory.Manufactured] = Mathf.RoundToInt(this.population * 2.5f);
            demand[(int)GoodsCategory.Spices] = Mathf.RoundToInt(this.population * 0.5f);
            demand[(int)GoodsCategory.Treasures] = Mathf.RoundToInt(this.population * 0.5f);
		}
		
		void UpdatePopulation() {
			if (foodShortage) {
				this.population = Mathf.RoundToInt(population * 0.95f);
			} else if (unemployment) { 
                this.population = (int)Mathf.Ceil(population * 1.05f);
            } else {
				this.population = Mathf.RoundToInt(population * 1.01f);
			}
		}

        public void UpdateMarket() 
        {
			//currently has 2 for loops. Could perhaps be reduced to 1 without much important missing functionality
			foodShortage = false;
            unemployment = false;
			//int popsFed = 0; using the categorySupply for food instead
			
			//wipe existing supply
			supply = new int[goodsCategoryCount];
			
			float[] goodsPriceArr = new float[goods.Count];
			float[] categoryPriceArr = new float[goodsCategoryCount];
			
			int facilityCount = 0;
			//group supply from various goods to their categories
            for (int i = 0; i < goods.Count; i++) {
				supply[goods[i].categoryIndex] += goods[i].WeightedQuantity;
				facilityCount += goods[i].Facility.Levels;
				goodsPriceArr[i] = goods[i].Price;
				categoryPriceArr[goods[i].categoryIndex] += goods[i].Price;
            }
			
			// check if there is a famine
			if (population > supply[(int)GoodsCategory.Food]) {
				foodShortage = true;
			}
			
			int employeesPerFacility = this.population / facilityCount;

            if (employeesPerFacility > ProductionFacility.MaxEmployeeCountPerLevel)
            {
                unemployment = true;
            }
			
			//assign each individual goods weight, update its quantity
			for (int i = 0; i < goods.Count; i++) {
				float weightedPercQty = 0f;
				int demandSatisfied = 0;
				if (supply[goods[i].categoryIndex] > 0) {
					float relativePrice = goodsPriceArr[i] / categoryPriceArr[goods[i].categoryIndex]'
					float relativeWeight = (float)goods[i].WeightedQuantity / (float)supply[goods[i].categoryIndex];
					
					weightedPercQty = (float)goods[i].WeightedQuantity / (float)supply[goods[i].categoryIndex];
					demandSatisfied = Mathf.RoundToInt(demand[goods[i].categoryIndex] * weightedPercQty);
				}
				
				
				//collapse these into a single update function 
				goods[i].UpdateProduction(Mathf.Min(employeesPerFacility * goods[i].Facility.Levels, ProductionFacility.MaxEmployeeCountPerLevel * goods[i].Facility.Levels)); //currently before the population changes? might make things weird?
				goods[i].UpdateQty(Mathf.RoundToInt(demandSatisfied / Good.WeightPerGoodsType(goods[i].Type)));
				goods[i].UpdatePrice(demandSatisfied, weightedPercQty);
            }
			
			UpdatePopulation();

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
			
			string[] debugCategories = new string[goodsCategoryCount];
			bool[] categoryPopulated = new bool[goodsCategoryCount];
			
			DebugString += "\nPopulation: " + population;
			
			int facilityLevelCount = 0;
			
			for (int i = 0; i < goodsCategoryCount; i++) {
				debugCategories[i] = "\n" + (((GoodsCategory)i).ToString()) + ":";
				debugCategories[i] += "\nDemand: " + demand[i];
                debugCategories[i] += "\nSupply: " + supply[i];
				
				categoryPopulated[i] = false;
			}
			
            for (int i = 0; i < this.goods.Count; i++) {
                MarketGood thisGood = goods[i]; 
                
                debugCategories[thisGood.categoryIndex] += "\n\t" + thisGood.Name + ":\n\t\tQty:"  + thisGood.Quantity+ " \n\t\tProduction:" + thisGood.Production + "\n\t\tPrice:" + thisGood.Price.ToString(".00#");
				
				facilityLevelCount += thisGood.Facility.Levels;
				categoryPopulated[thisGood.categoryIndex] = true;
            } 

			DebugString += "\nFacilities: " + facilityLevelCount;
			DebugString += "\nEmployees per Facility: " + Mathf.Min(population / facilityLevelCount, ProductionFacility.MaxEmployeeCountPerLevel);
			
			for (int i = 0; i < goodsCategoryCount; i++) {
				if (!categoryPopulated[i]) {
					continue;
				}
				DebugString += debugCategories[i];
			}
			
			if (foodShortage) {
				DebugString += "\nFamine!";
			}
			
			if (unemployment) {
				DebugString += "\nUnemployment!";
			}

            return DebugString;
        }
    }

    public class TradeController : MonoBehaviour
    {
		public float gridSize;
        public int rowCount, columnCount, portCount;
		public PortScript[] portArr, portGrid;
		public PortScript selectedPort;

        public TextMeshProUGUI tempDebugText;
        public GameObject ui, portObj;
		
        // Start is called before the first frame update
        void Start()
        {
			int gridSize = rowCount * columnCount;
			
			//we do a little input validation :)
			portCount = Mathf.Min(portCount, gridSize);
			
			portArr = new PortScript[portCount];
			portGrid = new PortScript[gridSize];
			
			for (int i = 0; i < portCount; i++) {
				int row = Random.Range(0,rowCount);
				int column = Random.Range(0, columnCount);
				
				int indexFromCoords = (row * columnCount) + column;
				if (portGrid[indexFromCoords] != null) {
					i--;
					continue;
				}
				
				float xPos = (row - (rowCount / 2)) * gridSize + (Random.Range(0.0f, gridSize));
				float zPos = (column - (columnCount / 2)) * gridSize + (Random.Range(0.0f, gridSize));
				
				GameObject newPort = Instantiate(portObj, new Vector3(xPos, 0.0f, zPos), Quaternion.identity);
				
				portArr[i] = newPort.GetComponent<PortScript>();
			}
			
            //GameObject[] portObjArr = GameObject.FindGameObjectsWithTag("Port");

            //portArr = new PortScript[portObjArr.Length];

            //for(int i = 0; i < portObjArr.Length; i++)
            //{
             //   portArr[i] = portObjArr[i].GetComponent<PortScript>();
            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("MouseOne"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit; 

                //Debug.Log(Input.mousePosition);
                //Instantiate(new GameObject(), Input.mousePosition, Quaternion.identity);

                if (Physics.Raycast(ray, out rayHit, 100, 1<<3))
                {
                    selectedPort = rayHit.transform.gameObject.GetComponent<PortScript>();
                } 
                else
                {
                    selectedPort = null;
                }
            }

            if (selectedPort != null)
            {
                tempDebugText.text = selectedPort.market.DebugPrintState();
            }
            else
            {
                tempDebugText.text = "";
            }
        }

        public int GetIndexOfPort (GameObject input)
        {
            for(int i = 0; i < portArr.Length; i++) {
                GameObject thisObj = portArr[i].gameObject;

                if (thisObj == input)
                {
                    return i;
                }
            }
            return -1;
        }

        public PortScript GetPort(int index)
        {
            return portArr[index];
        }
    }
}