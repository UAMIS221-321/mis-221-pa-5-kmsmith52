namespace mis_221_pa_5_kmsmith52
{
    public class Trainer
    {
        //instance variables
        private int trainerId;
        private string trainerName;
        private string mailingAddress;
        private string emailAddress;

        //class variable
        static private int count;

        //no arg constructor
        public Trainer() {

        }

        //arg constructor
        public Trainer(int trainerId, string trainerName, string mailingAddress, string emailAddress) {
            this.trainerId = trainerId;
            this.trainerName = trainerName;
            this.mailingAddress = mailingAddress;
            this.emailAddress = emailAddress;
        }

        public int GetTrainerId() {
            return trainerId;
        }

        public string GetTrainerName() {
            return trainerName;
        }

        public string GetMailingAddress() {
            return mailingAddress;
        }

        public string GetEmailAddress() {
            return emailAddress;
        }

        static public int GetCount() {
            return Trainer.count;
        }

        public void SetTrainerId(string trainerId) {
            this.trainerId = int.Parse(trainerId);
        }

        public void SetTrainerName(string trainerName) {
            this.trainerName = trainerName;
        }

        public void SetMailingAddress(string mailingAddress) {
            this.mailingAddress = mailingAddress;
        }

        public void SetEmailAddress(string emailAddress) {
            this.emailAddress = emailAddress;
        }
        
        static public void SetCount(int count) {
            Trainer.count = count;
        }

        static public void IncCount() {
            Trainer.count++;
        }

        static public void DecCount() {
            Trainer.count--;
        }

        public override string ToString()
        {
            return $"{trainerName} with trainer ID {trainerId} and email address {emailAddress} lives at {mailingAddress}.";
        }

        public string ToFile() {
            return $"{trainerId}#{trainerName}#{mailingAddress}#{emailAddress}";
        }
    }
}