namespace mis_221_pa_5_kmsmith52
{
    public class ListingUtility
    {
        private Listing[] listings;
        private TrainerUtility trainerUtility;

        //arg constructor
        public ListingUtility(Listing[] listings, TrainerUtility trainerUtility) {
            this.listings = listings;
            this.trainerUtility = trainerUtility;
        }

        //displays available trainers
        public void ViewRegisteredTrainers(Trainer[] trainers/*, ListingUtility listingUtility*/) {
            trainerUtility.GetAllTrainersFromFile();

            Console.WriteLine("Trainers:\n");
            for(int i = 0; i < Trainer.GetCount(); i++) {
                Console.WriteLine(trainers[i].ToString());
            }
        }

        //adds Listing objects from listings.txt to listings array
        public void GetAllListingsFromFile() {
            //open
            StreamReader inFile = new StreamReader("listings.txt");

            //process
            Listing.SetCount(0);
            string line = inFile.ReadLine();
            while(line != null) {
                string[] temp = line.Split("#");
                listings[Listing.GetCount()] = new Listing(int.Parse(temp[0]), temp[1], temp[2], temp[3], double.Parse(temp[4]), bool.Parse(temp[5]));
                Listing.IncCount();
                line = inFile.ReadLine();
            }

            //close
            inFile.Close();
        }

        //Creates Listing object and adds it to listings array and listings.txt
        public void AddListing(Trainer[] trainers) {
            trainerUtility.GetAllTrainersFromFile();
            Trainer newTrainer = new Trainer();

            Console.Clear();
            System.Console.WriteLine("PLease enter the listing's ID:");
            Listing newListing = new Listing();
            //input = Console.ReadLine();
            newListing.SetListingId(ValidListingId(Console.ReadLine()));

            Console.Clear();
            System.Console.WriteLine("Please enter the trainer's name:");
            string input = Console.ReadLine();
            while(ValidTrainerName(input, trainers, newTrainer) == false) {
                Console.Clear();
                Console.WriteLine("Please enter the name of one of the following registered trainers:\n");
                ViewRegisteredTrainers(trainers);
                input = Console.ReadLine();
            }
            newListing.SetTrainerName(input);

            Console.Clear();
            System.Console.WriteLine("Please enter the date of the session:");
            newListing.SetSessionDate(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("Please enter the time of the session:");
            newListing.SetSessionTime(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("Please enter the cost of the session:");
            newListing.SetSessionCost(Console.ReadLine());

            Console.Clear();
            System.Console.WriteLine("Please enter whether or not the listing has been taken ('Yes' or 'No'):");
            newListing.SetListingTaken(Console.ReadLine());

            Console.Clear();
            listings[Listing.GetCount()] = newListing;
            Listing.IncCount();

            Save();
        }

        //removes Listing object from listings array and listings.txt
        public void DeleteListing() {
            Console.Clear();
            System.Console.WriteLine("What is the ID of the listing you want to delete?:");
            string searchVal = Console.ReadLine();
            int foundIndex = Find(searchVal);

            if(foundIndex != -1) {
                for(int i = foundIndex; i < Listing.GetCount() - 1; i++) {
                    listings[i] = listings[i + 1];
                }
                Listing.DecCount();
                Save();
            }

            else {
                Console.Clear();
                System.Console.WriteLine("Listing not found.");
            }
        }

        //allows user to edit Listing object in listings array and listings.txt
        public void UpdateListing(Trainer[] trainers) {
            Console.Clear();
            System.Console.WriteLine("What is the ID of the listing you want to update?:");
            string searchVal = Console.ReadLine();
            int foundIndex = Find(searchVal);

            if(foundIndex != -1) {
                Console.Clear();
                Trainer newTrainer = new Trainer();
                System.Console.WriteLine("Please enter the listing's ID:");
                listings[foundIndex].SetListingId(ValidListingId(Console.ReadLine()));

                Console.Clear();
                System.Console.WriteLine("Please enter the trainer's name:");
                string input = Console.ReadLine();
                while(ValidTrainerName(input, trainers, newTrainer) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter the name of one of the following registered trainers:\n");
                    ViewRegisteredTrainers(trainers);
                    input = Console.ReadLine();
                }
                listings[foundIndex].SetTrainerName(input);

                Console.Clear();
                System.Console.WriteLine("Please enter the date of the session:");
                listings[foundIndex].SetSessionDate(Console.ReadLine());

                Console.Clear();
                System.Console.WriteLine("Please enter the time of the session:");
                listings[foundIndex].SetSessionTime(Console.ReadLine());

                Console.Clear();
                System.Console.WriteLine("Please enter the cost of the session:");
                listings[foundIndex].SetSessionCost(Console.ReadLine());

                Console.Clear();
                System.Console.WriteLine("Please enter whether or not the listing has been taken ('Yes' or 'No'):");
                listings[foundIndex].SetListingTaken(Console.ReadLine());
                Console.Clear();

                Save();
            }

            else {
                System.Console.WriteLine("Listing not found.");
            }
        }

        //tests input to insure the name is of a registered trainer
        public bool ValidTrainerName(string input, Trainer[] trainers, Trainer newTrainer) {
            for(int i = 0; i < Trainer.GetCount(); i++) {
                if(input == trainers[i].GetTrainerName()) {
                    newTrainer = trainers[i];
                    return true;
                }
            }
            return false;
        }

        //determines if listingID is valid (if its a number and if it hasn't been taken)
        public string ValidListingId(string input) {
            int n;
            for(int i = 0; i < Listing.GetCount(); i++) {
                while(int.TryParse(input, out n) == false || input == listings[i].GetListingId().ToString()) {
                    Console.Clear();
                    if(int.TryParse(input, out n) == false) {
                        Console.WriteLine("Please enter a number for the listing's ID:");
                    }
                    else if(input == listings[i].GetListingId().ToString()) {
                        Console.WriteLine("This listing ID has already been taken, please choose another:");
                    }
                    input = Console.ReadLine();
                }
            }
            return input;
        }

        //saves edits to listings array to listings.txt
        public void Save() {
            StreamWriter outFile = new StreamWriter("listings.txt");

            for(int i = 0; i < Listing.GetCount(); i++) {
                outFile.WriteLine(listings[i].ToFile());
            }

            outFile.Close();
        }

        //finds index of Listing object in listings array based on searchVal
        private int Find(string searchVal) {
            int n;
            if(int.TryParse(searchVal, out n) == false) {
                while(int.TryParse(searchVal, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the listing's ID:");
                    searchVal = Console.ReadLine();
                    int.TryParse(searchVal, out n);
                }
            }
            int intVal = int.Parse(searchVal);
            
            for(int i = 0; i < Listing.GetCount(); i++) {
                if(listings[i].GetListingId() == intVal) {
                    return i;
                }
            }

            return -1;
        }

    }
}