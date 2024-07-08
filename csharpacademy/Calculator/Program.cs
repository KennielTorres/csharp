using System.Text.RegularExpressions;
using CalculatorLibrary;


namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            bool exitMenu = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                string? mainMenuOption = "";
                string? historyMenuOption = "";
                bool pastResult = false;
                int index;
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                while(!exitMenu){
                    Console.WriteLine("Perform operation or see history?");
                    Console.WriteLine("1. Perform operation");
                    Console.WriteLine("2. See history");
                    Console.WriteLine("Your option? ");
                    mainMenuOption = Console.ReadLine();

                    switch (mainMenuOption){
                        case "1":
                            exitMenu = true;
                            break;
                        case "2":
                            Console.WriteLine("");
                            calculator.GetOperationHistory();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadLine();
                            // History menu: Clear history, or use past operation result.
                            Console.WriteLine("Mange your history");
                            Console.WriteLine("1. Clear history");
                            Console.WriteLine("2. Use past result");
                            Console.WriteLine("3. Exit to main menu");
                            Console.WriteLine("Your option? ");
                            historyMenuOption = Console.ReadLine();

                            switch (historyMenuOption){
                                case "1":
                                    calculator.DeleteOperationHistory();
                                    break;
                                case "2":
                                    pastResult = true;
                                    Console.WriteLine("Indicate the index of the result you would like to use.");
                                    Console.Write("\t*Can be seen on the left of the history screen: ");
                                    try{
                                        index = Convert.ToInt32(Console.ReadLine());
                                        cleanNum1 = calculator.GetPastResult(index);
                                    }
                                    catch{
                                        Console.WriteLine("This is not valid input. Please enter an integer value: ");
                                        index = Convert.ToInt32(Console.ReadLine());
                                        cleanNum1 = calculator.GetPastResult(index);
                                    }
                                    exitMenu = true;
                                    break;
                                case "3":
                                    break;
                            }
                            break;
                    }
                }
                
                // If past result is not used
                if (!pastResult) {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || ! Regex.IsMatch(op, "[a|s|m|d]"))
                {
                   Console.WriteLine("Error: Unrecognized input.");
                }
                else
                { 
                   try
                   {
                       result = calculator.DoOperation(cleanNum1, cleanNum2, op); 
                       if (double.IsNaN(result))
                       {
                           Console.WriteLine("This operation will result in a mathematical error.\n");
                       }
                       else Console.WriteLine("Your result: {0:0.##}\n", result);
                   }
                   catch (Exception e)
                   {
                       Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                   }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") {
                    endApp = true;
                }
                else{
                    exitMenu = false;
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}