// See https://aka.ms/new-console-template for more information
using mis_221_pa_5_kmsmith52;
class PA5 {
    public static void Main(string[] args) {
        Trainer[] trainers = new Trainer[100];
        TrainerUtility trainerUtility = new TrainerUtility(trainers);
        trainerUtility.GetAllTrainersFromFile();

        Listing[] listings = new Listing[100];
        ListingUtility listingUtility = new ListingUtility(listings, trainerUtility);
        listingUtility.GetAllListingsFromFile();

        Session[] sessions = new Session[100];
        SessionUtility sessionUtility = new SessionUtility(sessions, trainerUtility, listingUtility);
        sessionUtility.GetAllSessionsFromFile();

        Report report = new Report(sessions, listings, sessionUtility, listingUtility);
        
        Console.Clear();
        string choice = "";
        choice = PA5.ValidMenuChoice(choice);

        //performs tasks based on selected menu option
        while (choice != "5") {
            if(choice == "1") {
                PA5.ManageTrainerData(trainerUtility);
            }
            else if (choice == "2") {
                PA5.ManageListingData(listingUtility, trainers);
            }
            else if (choice == "3") {
                PA5.ManageBookingData(sessionUtility, trainers, listings);
            }
            else if (choice == "4") {
                PA5.ManageReports(report);
            }
            Console.Clear();
            choice = PA5.ValidMenuChoice(choice);
        }
        Console.WriteLine("Thank you for using this program!");
    }

    //handles bad input for selecting menu options
    static string ValidMenuChoice (string choice) {
        System.Console.WriteLine("Type 1 to manage trainer data, type 2 to manage listing data, type 3 to manage customer booking data, type 4 to run reports, or type 5 to exit.");
        choice = Console.ReadLine();
        if (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5") {
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5") {
                Console.Clear();
                System.Console.WriteLine("Please enter one of the following options:");
                System.Console.WriteLine("Type 1 to manage trainer data, type 2 to manage listing data, type 3 to manage customer booking data, type 4 to run reports, or type 5 to exit.");
                choice = Console.ReadLine();
            } 
        }
        return choice;
    }

    //menu for trainer section
    static void ManageTrainerData(TrainerUtility trainerUtility) {
        Console.Clear();
        string choice = "";
        choice = ValidTrainerMenuChoice(choice);

        while (choice != "4") {
            if(choice == "1") {
               trainerUtility.AddTrainer();
            }
            else if (choice == "2") {
               trainerUtility.DeleteTrainer();
            }
            else if (choice == "3") {
              trainerUtility.UpdateTrainer();
            }
            Console.Clear();
            choice = ValidTrainerMenuChoice(choice);
        }
    }

    //menu for listing section
    static void ManageListingData(ListingUtility listingUtility, Trainer[] trainers) {
        Console.Clear();
        string choice = "";
        choice = ValidListingMenuChoice(choice);

        while (choice != "4") {
            if(choice == "1") {
               listingUtility.AddListing(trainers);
            }
            else if (choice == "2") {
               listingUtility.DeleteListing();
            }
            else if (choice == "3") {
              listingUtility.UpdateListing(trainers);
            }
            Console.Clear();
            choice = ValidListingMenuChoice(choice);
        }
    }

    //menu for booking section
    static void ManageBookingData(SessionUtility sessionUtility, Trainer[] trainers, Listing[] listings) {
        Console.Clear();
        string choice = "";
        choice = ValidBookingMenuChoice(choice);

        while (choice != "4") {
            if(choice == "1") {
               sessionUtility.ViewAvailableSessions(listings);
            }
            else if (choice == "2") {
               sessionUtility.BookSession(trainers, listings);
            }
            else if (choice == "3") {
              sessionUtility.UpdateSessionStatus();
            }
            choice = ValidBookingMenuChoice(choice);
        }        
    }

    //menu for report section
    static void ManageReports(Report report) {
        Console.Clear();
        string choice = "";
        choice = ValidReportMenuChoice(choice);

        while (choice != "4") {
            Console.Clear();
            if(choice == "1") {
               report.ViewCustomerSessions();
            }
            else if (choice == "2") {
               report.ViewHistoricalSessions();
            }
            else if (choice == "3") {
              report.ViewRevenueReport();
            }
            choice = ValidReportMenuChoice(choice);
        }
    }

    //handles bad input for selecting trainer menu options
    static string ValidTrainerMenuChoice(string choice) {
        System.Console.WriteLine("Type 1 to add trainer, type 2 to delete trainer, type 3 to update trainer data, or type 4 to exit.");
        choice = Console.ReadLine();
        if (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
                Console.Clear();
                System.Console.WriteLine("Please enter one of the following options:");
                System.Console.WriteLine("Type 1 to add trainer, type 2 to delete trainer, type 3 to update trainer data, or type 4 to exit.");
                choice = Console.ReadLine();
            } 
        }
        return choice;
    }

    ////handles bad input for selecting listing menu options
    static string ValidListingMenuChoice(string choice) {
        System.Console.WriteLine("Type 1 to add listing, type 2 to delete listing, type 3 to update listing data, or type 4 to exit.");
        choice = Console.ReadLine();
        if (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
                Console.Clear();
                System.Console.WriteLine("Please enter one of the following options:");
                System.Console.WriteLine("Type 1 to add listing, type 2 to delete listing, type 3 to update listing data, or type 4 to exit.");
                choice = Console.ReadLine();
            } 
        }
        return choice;
    }

    //handles bad input for selecting booking menu options
    static string ValidBookingMenuChoice(string choice) {
        System.Console.WriteLine("Type 1 to view available training sessions, type 2 to book a training session, type 3 to update booking status, or type 4 to exit.");
        choice = Console.ReadLine();
        if (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
                Console.Clear();
                System.Console.WriteLine("Please enter one of the following options:");
                System.Console.WriteLine("Type 1 to view available training sessions, type 2 to book a training session, type 3 to update booking data, or type 4 to exit.");
                choice = Console.ReadLine();
            } 
        }
        return choice;
    }

    //handles bad input for selecting report menu options
    static string ValidReportMenuChoice(string choice) {
        System.Console.WriteLine("Type 1 for a report of previous training sessions for a certain customer, type 2 for a report on all sessions and for the total number of sessions for each customer, type 3 for a report on revenue by month and by year, or type 4 to exit.");
        choice = Console.ReadLine();
        if (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
            while (choice != "1" && choice != "2" && choice != "3" && choice != "4") {
                Console.Clear();
                System.Console.WriteLine("Please enter one of the following options:");
                System.Console.WriteLine("Type 1 for a report of previous training sessions for a certain customer, type 2 for a report on all sessions and for the total number of sessions for each customer, type 3 for a report on revenue by month and by year, or type 4 to exit.");
                choice = Console.ReadLine();
            } 
        }
        return choice;
    }
}
