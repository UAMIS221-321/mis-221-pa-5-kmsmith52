namespace mis_221_pa_5_kmsmith52
{
    public class TrainerUtility
    {
         private Trainer[] trainers;

        //arg constructor
        public TrainerUtility(Trainer[] trainers) {
            this.trainers = trainers;
        }

        //adds Trainer objects from trainers.txt to trainers array
        public void GetAllTrainersFromFile() {
            //open
            StreamReader inFile = new StreamReader("trainers.txt");

            //process
            Trainer.SetCount(0);
            string line = inFile.ReadLine();
            while(line != null) {
                string[] temp = line.Split("#");
                trainers[Trainer.GetCount()] = new Trainer(int.Parse(temp[0]), temp[1], temp[2], temp[3]);
                Trainer.IncCount();
                line = inFile.ReadLine();
            }

            //close
            inFile.Close();
        }

        //Creates Trainer object and adds it to trainers array and trainers.txt
        public void AddTrainer() {
            Console.Clear();
            Trainer newTrainer = new Trainer();
            System.Console.WriteLine("PLease enter the trainer's ID:");
            newTrainer.SetTrainerId(ValidTrainerId(Console.ReadLine()));
            Console.Clear();
            Console.WriteLine("Please enter the trainer's name:");
            newTrainer.SetTrainerName(Console.ReadLine());
            Console.Clear();
            System.Console.WriteLine("Please enter the trainer's mailing address:");
            newTrainer.SetMailingAddress(Console.ReadLine());
            Console.Clear();
            System.Console.WriteLine("Please enter the trainer's email address:");
            newTrainer.SetEmailAddress(Console.ReadLine());
            Console.Clear();

            trainers[Trainer.GetCount()] = newTrainer;
            Trainer.IncCount();

            Save();
        }

        //removes Trainer object from trainers array and trainers.txt
        public void DeleteTrainer() {
            Console.Clear();
            System.Console.WriteLine("What is the ID of the trainer you want to delete?:");
            string searchVal = Console.ReadLine();
            int foundIndex = Find(searchVal);

            if(foundIndex != -1) {
                for(int i = foundIndex; i < Trainer.GetCount() - 1; i++) {
                    trainers[i] = trainers[i + 1];
                }

                Trainer.DecCount();
                Save();
            }

            else {
                Console.Clear();
                System.Console.WriteLine("Trainer not found.");
            }
        }

        //allows user to edit Trainer object in trainers array and trainers.txt
        public void UpdateTrainer() {
            Console.Clear();
            System.Console.WriteLine("What is the ID of the trainer you want to update?:");
            string searchVal = Console.ReadLine();
            int foundIndex = Find(searchVal);

            if(foundIndex != -1) {
                Console.Clear();
                System.Console.WriteLine("Please enter the trainer ID:");
                trainers[foundIndex].SetTrainerId(ValidTrainerId(Console.ReadLine()));
                Console.Clear();
                System.Console.WriteLine("Please enter the trainer's name:");
                trainers[foundIndex].SetTrainerName(Console.ReadLine());
                Console.Clear();
                System.Console.WriteLine("Please enter the trainer's mailing address:");
                trainers[foundIndex].SetMailingAddress(Console.ReadLine());
                Console.Clear();
                System.Console.WriteLine("Please enter the trainer's email address:");
                trainers[foundIndex].SetEmailAddress(Console.ReadLine());
                Console.Clear();

                Save();
            }

            else {
                Console.Clear();
                System.Console.WriteLine("Trainer not found.");
            }
        }

        //saves edits to trainers array to trainers.txt
        private void Save() {
            StreamWriter outFile = new StreamWriter("trainers.txt");

            for(int i = 0; i < Trainer.GetCount(); i++) {
                outFile.WriteLine(trainers[i].ToFile());
            }

            outFile.Close();
        }

        //finds index of Trainer object in trainers array based on searchVal
        private int Find(string searchVal) {
            int n;
            if(int.TryParse(searchVal, out n) == false) {
                while(int.TryParse(searchVal, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the trainer's ID:");
                    searchVal = Console.ReadLine();
                    int.TryParse(searchVal, out n);
                }
            }
            int intVal = int.Parse(searchVal);

            for(int i = 0; i < Trainer.GetCount(); i++) {
                if(trainers[i].GetTrainerId() == intVal) {
                    return i;
                }
            }

            return -1;
        }

        //determines if trainerId is valid 
        //that is, whether or not it is an int and if it as already been taken
        public string ValidTrainerId(string input) {
            int n;
            for(int i = 0; i < Trainer.GetCount(); i++) {
                while(int.TryParse(input, out n) == false || input == trainers[i].GetTrainerId().ToString()) {
                    Console.Clear();
                    if(int.TryParse(input, out n) == false) {
                        Console.WriteLine("Please enter a number for the trainer's ID:");
                    }
                    else if(input == trainers[i].GetTrainerId().ToString()) {
                        Console.WriteLine("This trainer ID has already been taken, please choose another:");
                    }
                    input = Console.ReadLine();
                }
            }
            return input;
        }
    }
}