
string? userOption;
Random rand = new Random();
int firstNumber;
int secondNumber;
int answer;
int correctAnswer;
int randomGame;
List<string> gameHistory = new List<string>();
DateTime today;

bool playing = true;
// Main loop of the game
while (playing)
{
    playing = PlayGame();
}

bool PlayGame()
{
    Console.WriteLine("Welcome to the Math Game!");
    Console.WriteLine("Pick one of the following options:");
    Console.WriteLine("1. Add\n2. Subtract\n3. Multiply\n4. Divide\n5. Random\n6. View Game History\n7. Quit");
    userOption = Console.ReadLine();
    Console.WriteLine("");

    switch (userOption)
    {
        case "1":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            return AdditionOperation();
        case "2":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            return SubtractionOperation();
        case "3":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            return MultiplicationOperation();
        case "4":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            return DivisionOperation();
        case "5":
            today = DateTime.Now;
            gameHistory.Add($"Game Started: {today}");
            return RandomOperation();
        case "6":
            ViewGameHistory();
            return true;
        case "7":
            Console.WriteLine("Goodbye!");
            Console.WriteLine("");
            return false;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            Console.WriteLine("");
            return true;
    }
}

bool AdditionOperation()
{
    return PlayMathGame("+", (firstNumber, secondNumber) => firstNumber + secondNumber);
}

bool SubtractionOperation()
{
    return PlayMathGame("-", (firstNumber, secondNumber) => firstNumber - secondNumber);
}

bool MultiplicationOperation()
{
    return PlayMathGame("*", (firstNumber, secondNumber) => firstNumber * secondNumber);
}

bool DivisionOperation()
{
    return PlayMathGame("/", (firstNumber, secondNumber) => firstNumber / secondNumber);
}

bool RandomOperation()
{
    randomGame = rand.Next(1,5);
    switch(randomGame)
    {
        case 1:
            return AdditionOperation();
        case 2:
            return SubtractionOperation();
        case 3:
            return MultiplicationOperation();
        case 4:
            return DivisionOperation();
        default:
            return true;
    }
}

bool PlayMathGame(string operation, Func<int, int, int> calculateAnswer)
{
    bool canPlay = true;

    while (canPlay)
    {
        firstNumber = rand.Next(0, 100);
        secondNumber = rand.Next(2, 100);

        if (operation == "/")
        {
            // If the second number is not a factor of the first number, get another number
            while (firstNumber % secondNumber != 0)
            {
                secondNumber = rand.Next(2, 100);
            }
        }

        correctAnswer = calculateAnswer(firstNumber, secondNumber);

        Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ");
        try
        {
            answer = Convert.ToInt32(Console.ReadLine());
            if (answer != correctAnswer)
            {
                canPlay = false;
                Console.WriteLine("Game Over! Press any key to return to the menu.");
                Console.ReadLine();
                break;
            }
            if (userOption == "5")
            {
                return RandomOperation();
            }
        }
        catch
        {
            Console.WriteLine("Invalid input. Press any key to return to the menu.");
            Console.ReadLine();
            break;
        }
        Console.WriteLine("");
    }

    return true;
}

void ViewGameHistory()
{
    Console.WriteLine("Game History:");
    if (gameHistory.Count == 0)
    {
        Console.WriteLine("No games played yet.\n");
        return;
    }
    foreach (string game in gameHistory)
    {
        Console.WriteLine(game);
    }
    Console.WriteLine("");
}

gameHistory.Clear();
