namespace mis_221_pa_5_kmsmith52
{
    public class SessionUtility
    {
        private Session[] sessions;
        private ListingUtility listingUtility;
        private TrainerUtility trainerUtility;

        //arg constructor
        public SessionUtility(Session[] sessions, TrainerUtility trainerUtility, ListingUtility listingUtility) {
            this.sessions = sessions;
            this.trainerUtility = trainerUtility;
            this.listingUtility = listingUtility;
        }

        //displays available sessions
        public void ViewAvailableSessions(Listing[] listings) {
            listingUtility.GetAllListingsFromFile();

            Console.WriteLine("Available Sessions:\n");
            for(int i = 0; i < Listing.GetCount(); i++) {
                if(listings[i].GetListingTaken() == false) {
                    Console.WriteLine(listings[i]);
                }
            }
            Console.WriteLine();
        }

        //adds Session objects from transactions.txt to sessions array
        public void GetAllSessionsFromFile() {
            //open
            StreamReader inFile = new StreamReader("transactions.txt");

            //process
            Session.SetCount(0);
            string line = inFile.ReadLine();
            while(line != null) {
                string[] temp = line.Split("#");
                sessions[Session.GetCount()] = new Session(int.Parse(temp[0]), temp[1], temp[2], temp[3], int.Parse(temp[4]), temp[5], temp[6]);
                Session.IncCount();
                line = inFile.ReadLine();
            }

            //close
            inFile.Close();
        }

        //Creates Session object and adds it to sessions array and transactions.txt
        public void BookSession(Trainer[] trainers, Listing[] listings) {
            listingUtility.GetAllListingsFromFile();
            trainerUtility.GetAllTrainersFromFile();
            Listing newListing = new Listing();
            Trainer newTrainer = new Trainer();

            System.Console.WriteLine("Please enter the ID of the listing you'd like to choose:");
            string input = Console.ReadLine();
            int n;
            while(!int.TryParse(input, out n) || ValidSessionId(input, listings, ref newListing) == false) {
                Console.Clear();
                Console.WriteLine("Please enter the listing ID of one of the following listings:\n");
                ViewAvailableSessions(listings);
                Console.WriteLine();
                input = Console.ReadLine();
            }

            Session newSession = new Session();
            newSession.SetSessionId(input);

            Console.Clear();
            System.Console.WriteLine("Please enter the customer's name:");
            newSession.SetCustomerName(Console.ReadLine());
            Console.Clear();
            System.Console.WriteLine("Please enter the customer's email:");
            newSession.SetCustomerEmail(Console.ReadLine());
            Console.Clear();
            newSession.SetTrainingDate(newListing.GetSessionDate());
            newSession.SetTrainerName(newListing.GetTrainerName());
            newSession.SetTrainerId(GetCorrectTrainerId(newSession.GetTrainerName(), trainers));
            newSession.SetStatus("Booked");
            newListing.SetListingTaken("yes");

            sessions[Session.GetCount()] = newSession;
            Session.IncCount();

            Save();
            listingUtility.Save();
        }

        //updates status of session
        public void UpdateSessionStatus() {
            Console.Clear();
            Console.WriteLine("What is the ID of the session you would like to update?");
            string searchVal = Console.ReadLine();
            int foundIndex = Find(searchVal);
            Console.Clear();
            
            if(foundIndex != -1) {
                Console.WriteLine("What would you like to update this session's status to ('Completed' or 'Cancelled')?");
                sessions[foundIndex].SetStatus(Console.ReadLine());
            }
            else {
                Console.WriteLine("Session not found.");
            }

            Save();
        }

        //determines of sessionId is valid (if it is an int)
        public bool ValidSessionId(string input, Listing[] listings, ref Listing newListing) {
            for(int i = 0; i < Listing.GetCount(); i++) {
                if(int.Parse(input) == listings[i].GetListingId()) {
                    newListing = listings[i];
                    return true;
                }
            }
            return false;
        }

        //gets correct trainerId from trainers array
        public string GetCorrectTrainerId(string trainerName, Trainer[] trainers) {
            for(int i = 0; i < Trainer.GetCount(); i++) {
                if(trainerName == trainers[i].GetTrainerName()) {
                    return trainers[i].GetTrainerId().ToString();
                }
            }

            return "-1";
        }

        //returns index of searchVal in sessions array
        private int Find(string searchVal) {
            int n;
            if(int.TryParse(searchVal, out n) == false) {
                while(int.TryParse(searchVal, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the session's ID:");
                    searchVal = Console.ReadLine();
                    int.TryParse(searchVal, out n);
                }
            }
            int intVal = int.Parse(searchVal);

            for(int i = 0; i < Session.GetCount(); i++) {
                if(sessions[i].GetSessionId() == intVal) {
                    return i;
                }
            }

            return -1;
        }

        //saves edits to sessions array to transactions.txt
        private void Save() {
            StreamWriter outFile = new StreamWriter("transactions.txt");

            for(int i = 0; i < Session.GetCount(); i++) {
                outFile.WriteLine(sessions[i].ToFile());
            }

            outFile.Close();
        }
    }
}