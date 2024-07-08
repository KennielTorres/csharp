using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        int operationCounter;
        List<string> operationHistory = new List<string>();

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
            operationCounter = 0;
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    operationCounter++;
                    operationHistory.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operationCounter++;
                    operationHistory.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operationCounter++;
                    operationHistory.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operationCounter++;
                        operationHistory.Add($"{num1} / {num2} = {result}");
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void GetOperationHistory()
        {
            if (operationHistory.Count == 0){
                Console.WriteLine("No operations performed yet.");
                return;
            }
            Console.WriteLine("--Operation History--");
            for(int i=0; i<operationHistory.Count; i++){
                Console.WriteLine($"{i}: " + operationHistory[i]);
            }
        }

        public void DeleteOperationHistory(){
            if (operationHistory.Count == 0){
                Console.WriteLine("No operations performed yet.");
                return;
            }
            operationHistory.Clear();
            Console.WriteLine("Operation history cleared.");
        }

        public double GetPastResult(int index){
            if(index < 0 || index >= operationHistory.Count){
                return double.NaN;
            }
            try{
                return double.Parse(operationHistory[index].Split('=')[1].Trim());
            }
            catch{
                return double.NaN;
            }
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("TotalOperations");
            writer.WriteValue(operationCounter);
            writer.WriteEndObject();
            writer.Close();
        }
    }
}