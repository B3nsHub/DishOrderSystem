using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishOrderSystemBLL;
using DishOrderUtilities;

namespace DishOrderSystemGUI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string inputString = string.Empty;
            string outputString = string.Empty;
            string message = "Please type your order below or type 'exit' to exit the system:";
            DishOrderAdministrator admin = new DishOrderAdministrator();


            Start:
            Console.WriteLine(message);
            inputString = Console.ReadLine();


            
            if (inputString != "exit")
            {
                outputString = admin.GetDishOrders(inputString);
                Console.WriteLine(outputString);
                goto Start;
            }
            
        }
    }
}
