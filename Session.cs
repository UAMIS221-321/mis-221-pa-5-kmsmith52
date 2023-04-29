namespace mis_221_pa_5_kmsmith52
{
    public class Session
    {
        //instance variables
        private int sessionId;
        private string customerName;
        private string customerEmail;
        private string trainingDate;
        private int trainerId;
        private string trainerName;
        private string status;

        //class variable
        static private int count;

        //no arg constructor
        public Session() {

        }

        //arg constructors
        public Session(int sessionId, string customerName, string customerEmail, string trainingDate, int trainerId, string trainerName) {
            this.sessionId = sessionId;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.trainingDate = trainingDate;
            this.trainerId = trainerId;
            this.trainerName = trainerName;
            this.status = "booked";
        }

        public Session(int sessionId, string customerName, string customerEmail, string trainingDate, int trainerId, string trainerName, string status) {
            this.sessionId = sessionId;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.trainingDate = trainingDate;
            this.trainerId = trainerId;
            this.trainerName = trainerName;
            this.status = status;
        }

        public int GetSessionId() {
            return sessionId;
        }

        public string GetCustomerName() {
            return customerName;
        }

        public string GetCustomerEmail() {
            return customerEmail;
        }

        public string GetTrainingDate() {
            return trainingDate;
        }

        public int GetTrainerId() {
            return trainerId;
        }

        public string GetTrainerName() {
            return trainerName;
        }
        public string GetStatus() {
            return status;
        }

        static public int GetCount() {
            return Session.count;
        }

        //sets sessionId, tests if input is int
        public void SetSessionId(string sessionId) {
            int n;
            if(int.TryParse(sessionId, out n) == false) {
                while(int.TryParse(sessionId, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the session's ID:");
                    sessionId = Console.ReadLine();
                    int.TryParse(sessionId, out n);
                }
            }
            this.sessionId = int.Parse(sessionId);
        }

        public void SetCustomerName(string customerName) {
            this.customerName = customerName;
        }

        public void SetCustomerEmail(string customerEmail) {
            this.customerEmail = customerEmail;
        }

        //sets trainingDate, only accepts input of format MM/DD/YYYY
        public void SetTrainingDate(string trainingDate) {
            DateTime value;
            char[] input = trainingDate.ToCharArray();
            if(input.Length != 10 || input[2] != '/' || input[5] != '/' || !DateTime.TryParse(trainingDate, out value)) {
                while(input.Length != 10 || input[2] != '/' || input[5] != '/' || !DateTime.TryParse(trainingDate, out value)) {
                    Console.Clear();
                    Console.WriteLine("Please enter the date in the format of MM/DD/YYYY:");
                    trainingDate = Console.ReadLine();
                    input = trainingDate.ToCharArray();
                }
            }
            this.trainingDate = trainingDate;
        }

        //sets trainingId, tests if input is int
        public void SetTrainerId(string trainerId) {
             int n;
            if(int.TryParse(trainerId, out n) == false) {
                while(int.TryParse(trainerId, out n) == false) {
                    Console.Clear();
                    Console.WriteLine("Please enter a number for the trainer's ID:");
                    trainerId = Console.ReadLine();
                }
            }
            this.trainerId = int.Parse(trainerId);
        }

        public void SetTrainerName(string trainerName) {
            this.trainerName = trainerName;
        }

        //sets status, only accepts "booked," "completed," and "cancelled" as input
        public void SetStatus(string status) {
            while(status.ToUpper() != "BOOKED" && status.ToUpper() != "COMPLETED" && status.ToUpper() != "CANCELLED") {
                Console.Clear();
                Console.WriteLine("Please enter either 'Completed' or 'Cancelled' to indicate whether the session has been completed or cancelled/no-showed, respectively.");
                status = Console.ReadLine();
            }

           this.status = status;
        }
        
        static public void SetCount(int count) {
            Session.count = count;
        }

        static public void IncCount() {
            Session.count++;
        }

        static public void DecCount() {
            Session.count--;
        }

        public override string ToString()
        {
            return $"Session {sessionId} with trainer {trainerName} (Trainer ID: {trainerId}), which had been booked by {customerName} (Customer Email Address: {customerEmail}) for {trainingDate}, is {status}.";
        }

        public string ToFile() {
            return $"{sessionId}#{customerName}#{customerEmail}#{trainingDate}#{trainerId}#{trainerName}#{status}";
        }
    }
}