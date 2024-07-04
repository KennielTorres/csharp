
string? userOption;
Random rand = new Random();
bool canPlay;
int a;
int b;
int answer;
List<string> gameHistory = new List<string>();
DateTime today;

do{
    Console.WriteLine("Welcome to the Math Game!");
    Console.WriteLine("Pick one of the following options:");
    Console.WriteLine("1. Add\n2. Subtract\n3. Multiply\n4. Divide\n5. View Game History\n6. Quit");
    userOption = Console.ReadLine();
    Console.WriteLine("");

    switch(userOption){
        case "1":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} + {b} = ");
                try{
                    answer = Convert.ToInt32(Console.ReadLine());
                    if(answer != a+b){
                        canPlay = false;
                        Console.WriteLine("Game Over! Press any key to return to the menu.");
                        Console.ReadLine();
                    }
                }
                catch{
                    Console.WriteLine("Invalid input. Press any key to return to the menu.");
                    Console.ReadLine(); 
                    break;
                }
                Console.WriteLine("");
            } 
            break;
        case "2":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} - {b} = ");
                try{
                    answer = Convert.ToInt32(Console.ReadLine());
                    if(answer != a-b){
                        canPlay = false;
                        Console.WriteLine("Game Over! Press any key to return to the menu.");
                        Console.ReadLine();
                    }
                }
                catch{
                    Console.WriteLine("Invalid input. Press any key to return to the menu.");
                    Console.ReadLine(); 
                    break;
                }
                Console.WriteLine("");
            }
            break;
        case "3":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} * {b} = ");
                try{
                    answer = Convert.ToInt32(Console.ReadLine());
                    if(answer != a*b){
                        canPlay = false;
                        Console.WriteLine("Game Over! Press any key to return to the menu.");
                        Console.ReadLine();
                    }
                }
                catch{
                    Console.WriteLine("Invalid input. Press any key to return to the menu.");
                    Console.ReadLine(); 
                    break;
                }
                Console.WriteLine("");
            }
            break;
        case "4":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(2,100);
                while(a%b!=0){
                    b = rand.Next(2,100);
                }
                Console.WriteLine($"{a} / {b} = ");
                try{
                    answer = Convert.ToInt32(Console.ReadLine());
                    if(answer != a/b){
                        canPlay = false;
                        Console.WriteLine("Game Over! Press any key to return to the menu.");
                        Console.ReadLine();
                    }
                }catch{
                    Console.WriteLine("Invalid input. Press any key to return to the menu.");
                    Console.ReadLine(); 
                    break;
                }
                Console.WriteLine("");
            }
            break;
        case "5":
            Console.WriteLine("Game History:");
            if (gameHistory.Count == 0){
                Console.WriteLine("No games played yet.");
            }
            foreach(string game in gameHistory){
                Console.WriteLine(game);
            }
            Console.WriteLine("");
            break;
        case "6":
            Console.WriteLine("Goodbye!");
            Console.WriteLine("");
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            Console.WriteLine("");
            break;
    }

}while(userOption!="6");

gameHistory.Clear();
