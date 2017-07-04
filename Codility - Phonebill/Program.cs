namespace Codility___Phonebill
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            // The phoneBill is delivered as a long string.
            const string phoneBill = "00:01:06,400-234-090\n" +
                                     "00:05:01,301-080-080\n" +
                                     "00:05:00,400-234-090\n" +
                                     "00:00:01,300-234-090\n" +
                                     "00:01:06,301-080-080\n";

            Console.WriteLine($" \n The phone bill in cents: {CalculatePhoneBill(phoneBill)}");
            Console.ReadKey();
        }

        private static int CalculatePhoneBill(string phoneBillAsString)
        {
            var phoneBill = phoneBillAsString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var listOfPhoneCalls = new List<Phonecall>();

            const int zeroSeconds = 0;
            const int threeCents = 3;
            const int hundredFiftyCents = 150;
            var fiveMinutes = TimeSpan.Parse("00:05:00");

            foreach (var call in phoneBill)
            {
                // Get the values from the string as Time and PhoneNumber.
                var callDuration = TimeSpan.Parse(call.Split(',')[0]);
                var phoneNumber = int.Parse(call.Split(',')[1].Replace("-", ""));

                // Calculate the cost of each call, given by description.
                int costPerCall;

                // If the duration is shorter the 5 minutes multiply with 3 cents per second.
                if (callDuration < fiveMinutes)
                {
                    costPerCall = (int)callDuration.TotalSeconds * threeCents;
                }

                // If the duration is exactly 5 minutes multiply by 150 cents per minute.
                // If the duration exceedes 5 minutes multiply by 150, for every started minute.
                else
                {
                    costPerCall = callDuration.Seconds == zeroSeconds
                        ? (int)callDuration.TotalMinutes * hundredFiftyCents
                        : ((int)callDuration.TotalMinutes + 1) * hundredFiftyCents;
                }

                // Store the amount of the callduration for every unique phonenumber and the cost of every call.
                // Searches in the list if a phonenumber already exists, if it does, it will add the values to the correct phonenumber, else it will add it to the list.
                var phoneNumberResult = listOfPhoneCalls.SingleOrDefault(p => p.PhoneNumber == phoneNumber);

                if (phoneNumberResult == null)
                {
                    listOfPhoneCalls.Add(new Phonecall { PhoneNumber = phoneNumber, Duration = callDuration, Cost = costPerCall });
                }
                else
                {
                    phoneNumberResult.Duration += callDuration;
                    phoneNumberResult.Cost += costPerCall;
                }
            }

            // Find the longest duration for a phonenumber and subtract the cost from the bill.
            // If there is multiple phoneNumbers with the same duration, it will pick the phoneNumber with the lowest value, and subtract the amount from the list of calls.
            var maxDurationPhoneCalls = listOfPhoneCalls.FindAll(d => d.Duration == listOfPhoneCalls.Max(maxD => maxD.Duration));

            listOfPhoneCalls.Remove(maxDurationPhoneCalls.Count == 1
                ? maxDurationPhoneCalls.ElementAt(0)
                : maxDurationPhoneCalls.Find(p => p.PhoneNumber == maxDurationPhoneCalls.Min(minP => minP.PhoneNumber)));

            // Returns the amount in cents from all calls e.g. 900 cents.
            return listOfPhoneCalls.Sum(c => c.Cost);
        }
    }
}
