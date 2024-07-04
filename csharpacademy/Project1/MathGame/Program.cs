
string? userOption;
Random rand = new Random();
bool canPlay;
int a;
int b;
List<string> gameHistory = new List<string>();
DateTime today = DateTime.Today;
Console.WriteLine($"Today's date: {today}");

do{
    gameHistory.Add($"{today}");
    Console.WriteLine("Welcome to the Math Game!");
    Console.WriteLine("Pick one of the following options:");
    Console.WriteLine("1. Add\n2. Subtract\n3. Multiply\n4. Divide\n5. View Game History\n6. Quit");
    userOption = Console.ReadLine();
    Console.WriteLine("");

    switch(userOption){
        case "1":
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} + {b} = ");
                try{
                    int answer = Convert.ToInt32(Console.ReadLine());
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
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} - {b} = ");
                try{
                    int answer = Convert.ToInt32(Console.ReadLine());
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
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(0,100);
                Console.WriteLine($"{a} * {b} = ");
                try{
                    int answer = Convert.ToInt32(Console.ReadLine());
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
            canPlay = true;
            while(canPlay){
                a = rand.Next(0,100);
                b = rand.Next(2,100);
                while(a%b!=0){
                    b = rand.Next(2,100);
                }
                Console.WriteLine($"{a} / {b} = ");
                try{
                    int answer = Convert.ToInt32(Console.ReadLine());
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
            foreach(string game in gameHistory){
                Console.WriteLine(game);
            }
            break;
        case "6":
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

}while(userOption!="6");

gameHistory.Clear();
