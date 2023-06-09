using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageFactory.Pages
{

    public class RandomTelephoneGenerator
    {
        private Random _random;

        public RandomTelephoneGenerator()
        {
            _random = new Random();
        }

        public string GeneratePhoneNumber()
        {
            string phoneNumber = "";

            // Generate the area code (3 digits)
            int areaCode = _random.Next(100, 1000);
            phoneNumber += areaCode.ToString();

            // Generate the first three digits of the telephone number
            int firstDigits = _random.Next(100, 1000);
            phoneNumber += firstDigits.ToString();

            // Generate the last four digits of the telephone number
            int lastDigits = _random.Next(1000, 10000);
            phoneNumber += lastDigits.ToString();

            return phoneNumber;
        }
    }

    public class Program
    {
        public static string Main(string[] args)
        {
            RandomTelephoneGenerator generator = new RandomTelephoneGenerator();

            string phoneNumber = generator.GeneratePhoneNumber();

            return phoneNumber;
        }
    }

}
