namespace mis_221_pa_5_kmsmith52
{
    public class Listing
    {
        //instance variables
        private int listingId;
        private string trainerName;
        private string sessionDate;
        private string sessionTime;
        private double sessionCost;
        private bool listingTaken;

        //class variable
        static private int count;

        //no arg constructor
        public Listing() {

        }

        //arg constructor
        public Listing(int listingId, string trainerName, string sessionDate, string sessionTime, double sessionCost, bool listingTaken) {
            this.listingId = listingId;
            this.trainerName = trainerName;
            this.sessionDate = sessionDate;
            this.sessionTime = sessionTime;
            this.sessionCost = sessionCost;
            this.listingTaken = listingTaken;
        }

        public int GetListingId() {
            return listingId;
        }

        public string GetTrainerName() {
            return trainerName;
        }

        public string GetSessionDate() {
            return sessionDate;
        }

        public string GetSessionTime() {
            return sessionTime;
        }

        public double GetSessionCost() {
            return sessionCost;
        }

        public bool GetListingTaken() {
            return listingTaken;
        }

        static public int GetCount() {
            return Listing.count;
        }

        public void SetListingId(string listingId) {
            this.listingId = int.Parse(listingId);
        }

        public void SetTrainerName(string trainerName) {
            this.trainerName = trainerName;
        }

        //for date and time: convert string to char array, find index of /, compare length and indexes
        public void SetSessionDate(string sessionDate) {
            DateTime value;
            char[] input = sessionDate.ToCharArray();
            if(input.Length != 10 || input[2] != '/' || input[5] != '/' || !DateTime.TryParse(sessionDate, out value)) {
                while(input.Length != 10 || input[2] != '/' || input[5] != '/' || !DateTime.TryParse(sessionDate, out value)) {
                    Console.Clear();
                    Console.WriteLine("Please enter the date in the format of MM/DD/YYYY:");
                    sessionDate = Console.ReadLine();
                    input = sessionDate.ToCharArray();
                }
            }
            this.sessionDate = sessionDate;
        }

        public void SetSessionTime(string sessionTime) {
            TimeSpan value;
            char[] input = sessionTime.ToCharArray();
            if((input.Length != 4 && input.Length != 5) || (input[1] != ':' && input[2] != ':') || !TimeSpan.TryParse(sessionTime, out value)) {
                while((input.Length != 4 && input.Length != 5) || (input[1] != ':' && input[2] != ':') || !TimeSpan.TryParse(sessionTime, out value)) {
                    Console.Clear();
                    Console.WriteLine("Please enter the time in the format of H:MM or HH:MM");
                    sessionTime = Console.ReadLine();
                    input = sessionTime.ToCharArray();
                }
            }
            this.sessionTime = sessionTime;
        }

        //sets session cost, tests if inputted string is int
        public void SetSessionCost(string sessionCost) {
            double n;
            if(double.TryParse(sessionCost, out n) == false) {
                while(double.TryParse(sessionCost, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the cost of the session:");
                    sessionCost = Console.ReadLine();
                    double.TryParse(sessionCost, out n);
                }
            }
            this.sessionCost = double.Parse(sessionCost);
        }

        //sets listingTaken, accepts only "yes" or "no" for input
        public void SetListingTaken(string listingTaken) {
            while(listingTaken.ToUpper() != "YES" && listingTaken.ToUpper() != "NO") {
                Console.Clear();
                Console.WriteLine("Please enter either 'Yes' or 'No' for whether or not the listing is taken.");
                listingTaken = Console.ReadLine();
            }

            if(listingTaken.ToUpper() == "YES") {this.listingTaken = true;}
            else if(listingTaken.ToUpper() == "NO") {this.listingTaken = false;}
        }
        
        static public void SetCount(int count) {
            Listing.count = count;
        }

        static public void IncCount() {
            Listing.count++;
        }

        static public void DecCount() {
            Listing.count--;
        }

        public override string ToString()
        {
            return $"Listing {listingId} with trainer {trainerName} is scheduled for {sessionDate} at {TimeSpan.Parse(sessionTime)} and will cost {sessionCost.ToString("C")}.";
        }

        public string ToFile() {
            return $"{listingId}#{trainerName}#{sessionDate}#{sessionTime}#{sessionCost}#{listingTaken}";
        }
    }
}