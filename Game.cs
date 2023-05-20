using System.Net.Http.Headers;
using System;
using System.Linq;

//link to game presentation: https://www.loom.com/share/90d712c410da49948e431f619acb3313
//repurpose into stock market simulator
public class Game
{
    public void StartGame(){ //initial game message / instructions
        Console.WriteLine("");
        Console.WriteLine("Welcome to Urban Farmer Simulator");
        Console.WriteLine("Your goal is to outplay your competitors by making the most cash ($)");
        Console.WriteLine("Time is measured in turns and business success (placing) is measured in cash ($)");
        Console.WriteLine("During each turn, choose to go to the 1. Farm or 2. Market"); //1. Farm transforms items and gives cash upgrades; 2. Market is where you sell crops
        Console.WriteLine("Input the full name of the item you wish to interact with. Ie. type lettuce to select lettuce");
        Console.WriteLine("A winner is determined at the end of the initial game settings");
        Console.WriteLine("**************************************************************");
        Console.WriteLine("");
    }

    public void MainMenu(){
        //World Variables (turns)
        Console.WriteLine("Input number of turns (recommended 10): ");
        string turns = Console.ReadLine(); //user inputs initial number of turns
        int turnsI = Int32.Parse(turns);
        int turnInit = turnsI; //initial turn amount
        int turnInit2 = turnInit;
        int turnCur = 0;
        Console.WriteLine("Input starting amount of cash (recommended 50): ");
        string cash = Console.ReadLine(); //user inputs initial cash amount
        int cashI = Int32.Parse(cash);

        /////////////////
        //WORKING ON - random number based - affects plants
        string[] disease = {"blossom-end rot", "turnip mosaic virus", "potato mosaic virus", "carrot virus Y"}; //blossom-end rot = fertilizer Ca2+ issue with pepper/tomato, lettuce = less chance of virus but poor $ (risk/reward game aspect)
        string[] weather = {"sun", "cloud", "rain"};
        string[] naturalDisaster = {"tornado", "hail", "early frost", "drought", "masonic crop circle", "infestation"}; // drought = 10 days of sun, flood = 10 days of rain
        string[] weatherEffect = {"root rot"}; //if rain*10 days in a row - root rot potential
        //WORKING ON - purchasable
        string[] environmentalEngineering = {"irrigation", "flood drain system"}; //purchasable - irrigation prevent drought damage, flood drain system prevent rain damage
        ////////////////////

        //Computer variables
        int computer1Buy = 0; // initial computerPlay option
        int Computer1Cash = cashI; //random number for computerPlay options - Computer 2
        int computer2Buy = 0; // initial computerPlay option
        int Computer2Cash = cashI;
        string[] Computer1Inventory= {}; //Players array of items (Seed, produce, equipment)
        string[] Computer2Inventory= {}; //Players array of items (Seed, produce, equipment)
        //Player variables (cash)
        int PlayerCash = cashI;
        string[] PlayerInventory= {}; //Players array of items (Seed, produce, equipment)

        //Market Buy variables 
        string lettuceSeed = "LETTUCE SEED";
        string tomatoSeed = "TOMATO SEED";
        string pepperSeed = "PEPPER SEED";
        string carrotSeed = "CARROT SEED";
        string radishSeed = "RADISH SEED";
        string potatoSeed = "POTATO SEED";
        string lettuceSeedPrice = "1";
        string tomatoSeedPrice = "2";
        string pepperSeedPrice = "3";
        string carrotSeedPrice = "4";
        string radishSeedPrice = "5";
        string potatoSeedPrice = "6"; 
        string[,] marketSeedBuy= new string[2,6]{  //Array of seed items & price to buy and use
                                {lettuceSeed, tomatoSeed, pepperSeed, carrotSeed, radishSeed, potatoSeed}, 
                                {lettuceSeedPrice, tomatoSeedPrice, pepperSeedPrice, carrotSeedPrice, radishSeedPrice, potatoSeedPrice}, 
                                };
        string fertilizer = "FERTILIZER";
        string hormones = "HORMONES";
        string mycorrhizae = "MYCORRHIZAE";
        string hydroponic = "HYDROPONIC";
        string aeroponic= "AEROPONIC";
        string growLights = "GROW LIGHTS";
        string fertilizerPrice = "12";
        string hormonesPrice = "24";
        string mycorrhizaePrice = "18";
        string hydroponicPrice = "30";
        string aeroponicPrice = "40";
        string growLightsPrice = "25";
        string[,] marketImprovementBuy= new string[2,6]{ //Array of improvement items to buy and use
                                {fertilizer, hormones, mycorrhizae, hydroponic, aeroponic, growLights}, 
                                {fertilizerPrice, hormonesPrice, mycorrhizaePrice, hydroponicPrice, aeroponicPrice, growLightsPrice}, 
                                };

        //// Market Sell variables
        string lettuce = "LETTUCE";
        string tomato = "TOMATO";
        string pepper = "PEPPER";
        string carrot = "CARROT";
        string radish = "RADISH";
        string potato = "POTATO";
        string lettucePrice = "2";
        string tomatoPrice = "4";
        string pepperPrice = "6";
        string carrotPrice = "8";
        string radishPrice = "10";
        string potatoPrice = "12"; 
        string[,] ProduceS= new string[2,6]{  //Array of produce items to sell
                                {lettuce, tomato, pepper, carrot, radish, potato}, 
                                {lettucePrice, tomatoPrice, pepperPrice, carrotPrice, radishPrice, potatoPrice}, 
                            };

        /*UNUSED CURRENTLY - NOT ABLE TO RESELL BOUGHT IMPROVEMENT ITEMS
        string fertilizerS = "FERTILIZER";
        string hormonesS = "HORMONES";
        string mycorrhizaeS = "MYCORRHIZAE";
        string hydroponicS = "HYDROPONIC";
        string aeroponicS= "AEROPONIC";
        string growLightsS = "GROW LIGHTS";
        string fertilizerSP = "12";
        string hormonesSP = "24";
        string mycorrhizaeSP = "18";
        string hydroponicSP = "30";
        string aeroponicSP = "40";
        string growLightsSP = "25";
        string[,] ImprovementS= new string[2,6]{ //Array of improvement items
                                {fertilizerS, hormonesS, mycorrhizaeS, hydroponicS, aeroponicS, growLightsS}, 
                                {fertilizerSP, hormonesSP, mycorrhizaeSP, hydroponicSP, aeroponicSP, growLightsSP}, 
                            };*/
        
        //cash item multipliers obtained from using improvement items
        int CashMultiplier = 1; 
        int F = 1;
        int H = 1;
        int M = 1;
        int H2 = 1;
        int A = 1;
        int G = 1;
        //Placing
        string placeP;
        string placeC1;
        string placeC2;

        //MAIN GAME LOOP
        while (turnInit > 0)
        {
        //WEATHER
        //Random weatherValue = new Random(); 
        //weather = weatherValue.Next(1, 3); //computer1 random number 1-6

        //MAIN MENU - PLACING
        int[] cashPlacing = {PlayerCash, Computer1Cash, Computer2Cash}; //sorts cash placing - highest number is last element ie. 1(lowest cash) 2 3 4(highest cash)
        Array.Sort(cashPlacing); //Reverse Sort with highest number first
        
        int searchPlayerElement = PlayerCash;
        int searchComputer1Element = Computer1Cash;
        int searchComputer2Element = Computer2Cash;
        int indexPlayer = Array.IndexOf(cashPlacing, searchPlayerElement);
        int indexComputer1 = Array.IndexOf(cashPlacing, searchComputer1Element);
        int indexComputer2 = Array.IndexOf(cashPlacing, searchComputer2Element);

        if(indexPlayer==2){ //Player PLACING
            placeP = "1";
        }
        else if (indexPlayer==1){
            placeP = "2";
        }
        else if (indexPlayer==0){
            placeP = "3";
        }
        else{
            placeP = "tied";
        }

        if(indexComputer1==2){ //Computer1 PLACING
            placeC1 = "1";
        }
        else if (indexComputer1==1){
            placeC1 = "2";
        }
        else if (indexComputer1==0){
            placeC1 = "3";
        }
        else{
            placeC1 = "tied";
        }

        if(indexComputer2==2){ //Computer1 PLACING
            placeC2 = "1";
        }
        else if (indexComputer2==1){
            placeC2 = "2";
        }
        else if (indexComputer2==0){
            placeC2 = "3";
        }
        else{
            placeC2 = "tied";
        }

        Console.WriteLine("Day: " + turnCur +"/" +turnInit + "; Weather: " + "; Disaster");
        Console.Write("Player Cash($): " + PlayerCash + "; Place: " + placeP + "; Cash Multiplier: " + CashMultiplier*F*H*M*H2*A*G + "; "); //PRINTS CURRENT TURN, CASH AMOUNT, CASH MULTIPLIER
        Console.Write("Player Inventory: ");
            foreach(var e in PlayerInventory) //PRINTS PLAYER CURRENT INVENTORY
                        {
                            Console.Write(e + " ");
                        }
        Console.WriteLine("");
        Console.Write("Computer1 Cash($): " + Computer1Cash + "; Place:" + placeC1 + "; ");
        Console.Write("Computer1 Inventory: ");
            foreach(var e in Computer1Inventory) //PRINTS COMPUTER CURRENT INVENTORY
                        {
                            Console.Write(e + " ");
                        }
        Console.WriteLine("");
        Console.Write("Computer2 Cash($): " + Computer2Cash + "; Place:" + placeC2 + "; ");
        Console.Write("Computer2 Inventory: ");
            foreach(var e in Computer2Inventory) //PRINTS COMPUTER CURRENT INVENTORY
                        {
                            Console.Write(e + " ");
                        }
            Console.WriteLine("");
            Console.WriteLine("Do you wish to go to the Farm (F) or go to the Market (M)?"); 
            string userSelection = Console.ReadLine(); //AWAITING USER INPUT
            
            //MARKET - BUY LOGIC
            if (userSelection.ToUpper() == "M"){ //IF USER ENTERS M
                Console.WriteLine("What would you like to do: Buy(B) or Sell(S)");
                string userSelectionMarket1 = Console.ReadLine(); //IF USER ENTERS B OR S

                if (userSelectionMarket1.ToUpper() == "B"){ //if player presses B - Buy menu
                    Console.WriteLine("What would you like to Buy? Enter full name of item.");
                    Console.WriteLine("Cash: $" + PlayerCash); //user cash
                    Console.WriteLine("Produce items ($): "); //prints multidimensional seed item array
                    for (int i = 0; i < marketSeedBuy.GetLength(0); i++)
                    {
                        for (int j = 0; j < marketSeedBuy.GetLength(1); j++)
                        {
                            Console.Write(marketSeedBuy[i,j] + "\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Improvement items ($): "); //prints multidimensional improvement item array
                    for (int i = 0; i < marketImprovementBuy.GetLength(0); i++)
                    {
                        for (int j = 0; j < marketImprovementBuy.GetLength(1); j++)
                        {
                            Console.Write(marketImprovementBuy[i,j] + "\t");
                        }
                        Console.WriteLine();
                    }

                    //PLAYER MARKET BUY CHOICE
                    string userSelectionMarket1a = Console.ReadLine();
                    if (userSelectionMarket1a.ToUpper() == lettuceSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,0]); 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,0]);//ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == tomatoSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,1]); 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,1]); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == pepperSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,2]);
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,2]); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == carrotSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,3]); 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,3]); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == radishSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,4]); 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,4]); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == potatoSeed){
                        PlayerCash -= Int32.Parse(marketSeedBuy[1,5]); 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(marketSeedBuy[0,5]); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    
                    if (userSelectionMarket1a.ToUpper() == fertilizer){
                        PlayerCash -= 12; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(fertilizer);
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == hormones){
                        PlayerCash -= 24; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(hormones);
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == mycorrhizae){
                        PlayerCash -= 18; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(mycorrhizae); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == hydroponic){
                        PlayerCash -= 30; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(hydroponic); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == aeroponic){
                        PlayerCash -= 40; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(aeroponic); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    if (userSelectionMarket1a.ToUpper() == growLights){
                        PlayerCash -= 25; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Add(growLights); //ADDS NEW ELEMENT TO INVENTORY
                        PlayerInventory = list.ToArray();
                    }
                    else{
                        Console.WriteLine("Please type exact phrase from item table"); //IF USER TYPES INCORRECT OPTION
                    }
                }

                //COMPUTER & PLAYER MARKET SELL
                if (userSelectionMarket1.ToUpper() == "S"){  //if player presses S - Sell menu
                    //PLAYER SELL
                    Console.WriteLine("What would you like to Sell? Enter full name of item.");
                    Console.WriteLine("Cash: $" + PlayerCash);
                    Console.Write("Inventory: ");
                    foreach(var e in PlayerInventory) //PRINTS CURRENT INVENTORY
                        {
                            Console.Write(e + " ");
                        }
                    Console.WriteLine("");  
                    //store array
                    Console.WriteLine("Produce items ($): "); //PRINTS MULTIDIMENSION ARRAY TO LIST SELL ITEMS
                    for (int i = 0; i < ProduceS.GetLength(0); i++)
                    {
                        for (int j = 0; j < ProduceS.GetLength(1); j++)
                        {
                            Console.Write(ProduceS[i,j] + "\t");
                        }
                        Console.WriteLine();
                    }
                    /////
                    string userSelectionMarket1a = Console.ReadLine(); // IF USER ENTERS PRODUCE WORD
                    if (userSelectionMarket1a.ToUpper() == "LETTUCE"){
                        PlayerCash += 2*F*H*M*H2*A*G;  //INCREASE CASH BY AMOUNT*MULTIPLIER
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(lettuce); //REMOVE FROM ARRAY
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                    if (userSelectionMarket1a.ToUpper() == "TOMATO"){
                        PlayerCash += 4*F*H*M*H2*A*G; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(tomato);
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                    if (userSelectionMarket1a.ToUpper() == "PEPPER"){
                        PlayerCash += 6*F*H*M*H2*A*G; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(pepper);
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                    if (userSelectionMarket1a.ToUpper() == "CARROT"){
                        PlayerCash += 8*F*H*M*H2*A*G; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(carrot);
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                    if (userSelectionMarket1a.ToUpper() == "RADISH"){
                        PlayerCash += 10*F*H*M*H2*A*G; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(radish);
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                    if (userSelectionMarket1a.ToUpper() == "POTATO"){
                        PlayerCash += 12*F*H*M*H2*A*G; 
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove(potato);
                        PlayerInventory = list.ToArray();
                        Console.WriteLine(PlayerInventory);
                    }
                }
            }

            //FARM SELECTION
            if (userSelection.ToUpper() == "F"){
                Console.WriteLine("What would you like to do: Plant(P) or Use Equipment(U)"); //Player Selection
                string userSelectionFarm1 = Console.ReadLine();

                //PLANT LOGIC - ITEM TRANSFORM
                if (userSelectionFarm1.ToUpper() == "P"){ //if player presses P - Plant menu; SEED ITEMS TRANSFORM
                    foreach(var e in PlayerInventory){ //PLAYER INVENTORY TRANSFORM
                        for(int x = 0; x<6; x++)
                        if(PlayerInventory.Contains(marketSeedBuy[0,x])){
                        PlayerInventory[Array.IndexOf(PlayerInventory, marketSeedBuy[0,x])] = ProduceS[0,x];
                        Console.WriteLine(e + " ");
                        }
                    }
                }
                //USE EQUIPMENT LOGIC
                if (userSelectionFarm1.ToUpper() == "U"){ //if player presses U - Equipment menu; EQUIPMENT ITEMS increase cash multiple
                Console.WriteLine("Select the equipment you want to use.");
                    foreach(var e in PlayerInventory) //PRINTS CURRENT INVENTORY
                        {
                            Console.Write(e + " ");
                        }
                    Console.WriteLine("");  
                    string userSelectionU = Console.ReadLine(); //Player enters equipment item to use
                    if (userSelectionU.ToUpper() == "FERTILIZER"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("FERTILIZER"); //remove array element
                        PlayerInventory = list.ToArray();
                        F = 3; //cash multiplier increase
                    }
                    if (userSelectionU.ToUpper() == "HORMONES"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("HORMONES"); //remove array element
                        PlayerInventory = list.ToArray();
                        H = 2; //cash multiplier increase
                    }
                    if (userSelectionU.ToUpper() == "MYCORRHIZAE"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("MYCORRHIZAE"); //remove array element
                        PlayerInventory = list.ToArray();
                        M = 2; //cash multiplier increase
                    }
                    if (userSelectionU.ToUpper() == "HYDROPONIC"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("HYDROPONIC"); //remove array element
                        PlayerInventory = list.ToArray();
                        H2 = 2; //cash multiplier increase
                    }
                    if (userSelectionU.ToUpper() == "AEROPONIC"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("AEROPONIC"); //remove array element
                        PlayerInventory = list.ToArray();
                        A = 2; //cash multiplier increase
                    }
                    if (userSelectionU.ToUpper() == "GROW LIGHTS"){
                        List<string> list = new List<string>(PlayerInventory.ToList());
                        list.Remove("GROW LIGHTS"); //remove array element
                        PlayerInventory = list.ToArray();
                        G = 3; //cash multiplier increase
                    }
                    else{
                        Console.WriteLine("Please type exact phrase from item table");
                    }
                    Console.WriteLine("You have used your equipment and now receive bonus produce rewards.");
                    }
                }

                //Computer1 
                Random computerRandom = new Random(); //random number for computerPlay options
                computer1Buy = computerRandom.Next(1, 7); //computer1 random number 1-6
                computer2Buy = computerRandom.Next(1, 7); //computer2 random number 1-6
                //COMPUTER MARKET BUY CHOICE
                if(turnCur == 0 || turnCur%3 == 0){
                    if(computer1Buy ==1){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,0]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,0]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==1){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,0]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,0]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                    if(computer1Buy ==2){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,1]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,1]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==2){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,1]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,1]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                    if(computer1Buy ==3){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,2]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,2]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==3){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,2]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,2]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                    if(computer1Buy ==4){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,3]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,3]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==4){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,3]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,3]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                    if(computer1Buy ==5){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,4]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,4]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==5){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,4]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,4]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                    if(computer1Buy ==6){
                        Computer1Cash -= Int32.Parse(marketSeedBuy[1,5]); //converts string lettuceSeedPrice to int
                        List<string> list1 = new List<string>(Computer1Inventory.ToList());
                        list1.Add(marketSeedBuy[0,5]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer1Inventory = list1.ToArray();
                    }
                    if(computer2Buy==6){
                        Computer2Cash -= Int32.Parse(marketSeedBuy[1,5]); //converts string lettuceSeedPrice to int
                        List<string> list2 = new List<string>(Computer2Inventory.ToList());
                        list2.Add(marketSeedBuy[0,5]); //ADDS NEW ELEMENT TO INVENTORY
                        Computer2Inventory = list2.ToArray();
                    }
                }
                //COMPUTER INVENTORY TRANSFORM
                if(turnCur == 1 || turnCur%3 ==1){
                foreach(var e in Computer1Inventory){
                        for(int x = 0; x<6; x++)
                        if(Computer1Inventory.Contains(marketSeedBuy[0,x])){
                        Computer1Inventory[Array.IndexOf(Computer1Inventory, marketSeedBuy[0,x])] = ProduceS[0,x];
                        Console.WriteLine(e + " ");
                        }
                    }
                foreach(var e in Computer2Inventory){
                        for(int x = 0; x<6; x++)
                        if(Computer2Inventory.Contains(marketSeedBuy[0,x])){
                        Computer2Inventory[Array.IndexOf(Computer2Inventory, marketSeedBuy[0,x])] = ProduceS[0,x];
                        Console.WriteLine(e + " ");
                        }
                    }
                }
            //COMPUTER SELL
            if(turnCur == 2 || turnCur%3 ==2){
                if(Computer1Inventory.Contains("LETTUCE") || Computer2Inventory.Contains("LETTUCE")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(lettuce);
                    list2.Remove(lettuce);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 2;
                    Computer2Cash += 2;
                }
                if(Computer1Inventory.Contains("TOMATO") || Computer2Inventory.Contains("TOMATO")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(tomato);
                    list2.Remove(tomato);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 4;
                    Computer2Cash += 4;
                }
                if(Computer1Inventory.Contains("PEPPER") || Computer2Inventory.Contains("PEPPER")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(pepper);
                    list2.Remove(pepper);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 6;
                    Computer2Cash += 6;
                }
                if(Computer1Inventory.Contains("CARROT") || Computer2Inventory.Contains("CARROT")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(carrot);
                    list2.Remove(carrot);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 8;
                    Computer2Cash += 8;
                }
                if(Computer1Inventory.Contains("RADISH") || Computer2Inventory.Contains("RADISH")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(radish);
                    list2.Remove(radish);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 10;
                    Computer2Cash += 10;
                }
                if(Computer1Inventory.Contains("POTATO") || Computer2Inventory.Contains("POTATO")){   
                    List<string> list1 = new List<string>(Computer1Inventory.ToList());
                    List<string> list2 = new List<string>(Computer2Inventory.ToList());
                    list1.Remove(potato);
                    list2.Remove(potato);
                    Computer1Inventory = list1.ToArray();
                    Computer2Inventory = list2.ToArray();
                    Console.WriteLine(Computer1Inventory);
                    Console.WriteLine(Computer2Inventory);
                    Computer1Cash += 12;
                    Computer2Cash += 12;
                }
        }
            
            //TURN OUTCOME
            Console.WriteLine("");
            // decrease counter
            turnInit2--;
            turnCur++;

        //Game Ending Conditions  
        if (turnCur > turnInit){
            if (PlayerCash > Computer1Cash){
                Console.WriteLine("You have beaten your competitors, Congratulations!");
                Console.WriteLine("Would you like to play again or continue? Press 'y' to reset the game or press 'c' to continue");
            }
            // INPUT
            else if (Computer1Cash > PlayerCash){
                Console.WriteLine("Unfortunately, your competitors have gained the entire share of the market.");
                Console.WriteLine("Would you like to play again or continue? Press 'y' to reset the game or press 'c' to continue");
            }
            else{
                Console.WriteLine("You have equal position in the market. Try again to gain full control!");
                Console.WriteLine("Would you like to play again or continue? Press 'y' to reset the game or press 'c' to continue");
            }
            string nextGame = Console.ReadLine();
            if (nextGame.ToUpper() == "Y"){ //start a new game
                Console.WriteLine("");
                turnInit = -1; //removes Player from while loop to reset initial game setting variables
                turnCur = 0; //Resets turn to 0
                StartGame(); //go back to start game sequence
            }
            else if (nextGame.ToUpper() == "C"){ //continue current game (add extra turns)
                turnCur = 0; //Resets turn to 0
                Console.WriteLine("Input number of turns to continue: ");
                turns = Console.ReadLine();
                turnInit = Int32.Parse(turns);
                StartGame(); //go back to start game sequence
            }
            else  {
                AwaitNGame(); //AWAIT NEW GAME SEQUENCE
            }
        }
    }
}

    public void AwaitNGame(){ //IF PLAYER CHOOSES NOT TO PLAY NEXT GAME, WAIT UNTIL USER DOES
        Console.WriteLine("Finishing the Game");
        Console.WriteLine("Press Y to start game");
        char selection = 'y';
            selection = Console.ReadKey().KeyChar; 
            if (selection == 'y' || selection == 'Y') {
                StartGame(); //NEW GAME SEQUENCE START
            }
            else {
                Console.WriteLine("Incorrect Input");
                AwaitNGame(); //CONTINUE TO AWAIT USER CHOICE FOR NEXT GAME
            }
    }
}
