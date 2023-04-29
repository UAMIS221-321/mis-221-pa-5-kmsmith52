namespace mis_221_pa_5_kmsmith52
{
    public class Report
    {
        private Session[] sessions;
        private Listing[] listings;
        private SessionUtility sessionUtility;
        private ListingUtility listingUtility;

        public Report(Session[] sessions, Listing[] listings, SessionUtility sessionUtility, ListingUtility listingUtility) {
            this.sessions = sessions;
            this.listings = listings;
            this.sessionUtility = sessionUtility;
            this.listingUtility = listingUtility;
        }

        //creates report of individual customer's booked sessions
        public void ViewCustomerSessions() {
            sessionUtility.GetAllSessionsFromFile();
            Console.WriteLine("What is the email of the customer you would like a report of?");
            string email = Console.ReadLine();
            int foundIndex = Find(email);

            if(foundIndex != -1) {
                Console.WriteLine("Report of Customer's Previous Training Sections:\n");
                for(int i = 0; i < Session.GetCount(); i++) {
                    if(sessions[i].GetCustomerEmail() == email) {
                        Console.WriteLine(sessions[i]);
                    }
                }
            }

            else {
                System.Console.WriteLine("Customer email not found.");
            }
            Console.WriteLine();
        }

        public void ViewHistoricalSessions() {
            Console.Clear();
            sessionUtility.GetAllSessionsFromFile();

            Sort();

            //displays list of all sessions, sorted by customer and date
            Console.WriteLine("List of All Sessions by Customer and Date:\n");
            for(int i = 0; i < Session.GetCount(); i++) {
                Console.WriteLine(sessions[i]);
            }

            //sessions now already in order, separated by customer then date
            //counts and displays number of sessions per customer
            Console.WriteLine("\nTotal Number of Sessions per Customer:\n");
            
            int count = 1;
            for(int j = 0; j < Session.GetCount() - 1; j++) {
                if(sessions[j].GetCustomerName() == sessions[j + 1].GetCustomerName()) {
                    count++;
                }
                else {
                    Console.WriteLine($"Customer {sessions[j].GetCustomerName()} has scheduled {count} session(s).");
                    count = 1;
                }
            }
            Console.WriteLine($"Customer {sessions[Session.GetCount() - 1].GetCustomerName()} has scheduled {count} session(s).");
            Console.WriteLine();
        }

        //provides a list of revenue by month and by year
        public void ViewRevenueReport() {
            Console.Clear();
            Console.WriteLine("Revenue Report by Month and Year:\n");
            sessionUtility.GetAllSessionsFromFile();
            listingUtility.GetAllListingsFromFile();

            int[][] trainingDates = new int[Session.GetCount()][];
            for(int x = 0; x < Session.GetCount(); x++) {
                for(int y = 0; y < 3; y++) {
                    trainingDates[x] = new int[3];
                }
            }
            trainingDates = SortByDate();

            double annualRev = 0;
            double annualRevLast = 0;
            double monthlyRev = 0;
            int lastYear = 0;
            for(int i = 0; i < Session.GetCount() - 1; i++) {
                for(int j = 0; j < Listing.GetCount(); j++) {
                    if(sessions[i].GetSessionId() == listings[j].GetListingId() &&
                    sessions[i].GetStatus().ToUpper() == "COMPLETED") {
                        monthlyRev += listings[j].GetSessionCost();
                    }
                }

                if(trainingDates[i][0] != trainingDates[i + 1][0]) {
                    Console.WriteLine($"TLAC has earned {monthlyRev.ToString("C")} in revenue for the month of {GetFullName(trainingDates[i][0])}, {trainingDates[i][2]}.");
                    annualRev += monthlyRev;
                    if (trainingDates[i][2] != lastYear) {
                        annualRevLast = annualRev;
                        lastYear = trainingDates[i][2];
                    }
                    monthlyRev = 0;
                }

                if(trainingDates[i][2] != trainingDates[i + 1][2]) {
                    annualRev += monthlyRev;
                    Console.WriteLine($"\nTLAC has earned {annualRev.ToString("C")} in revenue for the year of {trainingDates[i][2]}.\n");
                    annualRev = 0;
                }
            }

            if (trainingDates[Session.GetCount() - 1][2] != lastYear) {
                annualRevLast = annualRev;
            }

            // Add revenue from last session to monthlyRev
            int lastSessionIndex = Session.GetCount() - 1;
            if (trainingDates[lastSessionIndex][0] == trainingDates[lastSessionIndex - 1][0]) {
                for (int k = 0; k < Listing.GetCount(); k++) {
                    if (sessions[lastSessionIndex].GetSessionId() == listings[k].GetListingId() &&
                        sessions[lastSessionIndex].GetStatus().ToUpper() == "COMPLETED") {
                        monthlyRev += listings[k].GetSessionCost();
                    }
                }
            }
            else {
                Console.WriteLine($"TLAC has earned {monthlyRev.ToString("C")} in revenue for the month of {GetFullName(trainingDates[lastSessionIndex][0])}, {trainingDates[lastSessionIndex][2]}.");
                annualRevLast = annualRev;
            }
            Console.WriteLine($"TLAC has earned {monthlyRev.ToString("C")} in revenue for the month of {GetFullName(trainingDates[Session.GetCount() - 1][0])}, {trainingDates[Session.GetCount() - 1][2]}.");

            // Add monthlyRev to annualRev
            annualRev += monthlyRev;
            Console.WriteLine($"\nTLAC has earned {annualRev.ToString("C")} in revenue for the year of {trainingDates[lastSessionIndex][2]}.\n");

        }

        //returns index of element whose customerEmail matches searchVal
        private int Find(string searchVal) {
            for(int i = 0; i < Session.GetCount(); i++) {
                if(sessions[i].GetCustomerEmail() == searchVal) {
                    return i;
                }
            }

            return -1;
        }

        //sorts array by customer and date
        public void Sort() {
            for(int i = 0; i < Session.GetCount() - 1; i++) {
                int min = i;
                for(int j = i + 1; j < Session.GetCount(); j++) {
                    if((sessions[j].GetCustomerName().CompareTo(sessions[min].GetCustomerName()) < 0) || 
                    (sessions[j].GetCustomerName().CompareTo(sessions[min].GetCustomerName()) == 0 && sessions[j].GetTrainingDate().CompareTo(sessions[min].GetTrainingDate()) < 0)) {
                        min = j;
                    }
                }
                if(min != i) {
                    Swap(min, i);
                }
            }
        }

        //sorts array by date (by year, month, and day)
        public int[][] SortByDate() {
            int[][] trainingDates = new int[Session.GetCount()][];
            for(int x = 0; x < Session.GetCount(); x++) {
                for(int y = 0; y < 3; y++) {
                    trainingDates[x] = new int[3];
                }
            }

            for(int i = 0; i < Session.GetCount(); i++) {
                string[] dates = sessions[i].GetTrainingDate().Split("/");
                for(int j = 0; j < dates.Length; j++) {
                    trainingDates[i][j] = int.Parse(dates[j]);
                }  
            }

            for(int a = 0; a < Session.GetCount() - 1; a++) {
                int min = a;
                for(int b = a + 1; b < Session.GetCount(); b++) {
                    if((trainingDates[b][2] < trainingDates[min][2]) || 
                    (trainingDates[b][2] == trainingDates[min][2] && trainingDates[b][0] < trainingDates[min][0])) {
                        min = b;
                    }
                }
                if(min != a) {
                    Swap(min, a, trainingDates);
                }
            }
            return trainingDates;
        }

        //swaps elements in sessions array and their respective trainingDates in trainingDates jagged array
        private void Swap(int x, int y, int[][] trainingDates) {
            Session temp = sessions[x];
            sessions[x] = sessions[y];
            sessions[y] = temp;

            int[] temps = {trainingDates[x][0], trainingDates[x][1], trainingDates[x][2]};
            trainingDates[x] = trainingDates[y];
            trainingDates[y] = temps;
        }

        //swaps elements in sessions array
        private void Swap(int x, int y) {
            Session temp = sessions[x];
            sessions[x] = sessions[y];
            sessions[y] = temp;
        }

        //converts numerical form of month to string form of month
        static string GetFullName(int month) {
            DateTime date = new DateTime(2020, month, 1);
            return date.ToString("MMMM");
        }
    }
}