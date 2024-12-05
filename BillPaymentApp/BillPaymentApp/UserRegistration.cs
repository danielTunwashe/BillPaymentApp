using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillPaymentApp
{
    public class UserRegistration
    {
        User user;
        Transaction transaction;
        DateTime dateSent = DateTime.Now;
        string[] AirtlePrefixCode = new string[] { "0911", "0912", "0907", "0904", "0901", "0902", "0812", "0808", "0802", "0701"};
        string[] MtnPrefixCode = new string[] { "0703", "0706", "0704", "0707", "0803", "0806", "0810", "0813", "0814", "0816", "0903", "0906", "0913", "0916"};
        string[] GloPrefixCode = new string[] { "0915", "0905", "0815", "0811", "0807", "0805"};
        string[] EtisalatPrefixCode = new string[] { "0809", "0817", "0818", "0909", "0908", "0805"};
        Dictionary<string, string> SmartCodeStore = new Dictionary<string, string>();
        Dictionary<string,string> DecoderType = new Dictionary<string, string>();
        int[] TopUpCode = new int[] { 55555, 77777, 12121 };
        List<User> users = new List<User>();
        List<Transaction> transactions = new List<Transaction>();
        string networkProvider = "";
        public void Login()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            user = users.Find(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                Console.WriteLine($"Login Successful! ");
                Console.Write("Press Enter to continue: ");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Now welcome to the application, here are the things you can perform!");
                AppMenuTwo();
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
            }

        }

        public void Register()
        {
            try
            {
                Console.Write("Enter your Fullname: ");
                string fullname = Console.ReadLine();
                Console.Write("Enter your Address: ");
                string address = Console.ReadLine();
                Console.Write("Enter your Gender (male or female): ");
                string gender = Console.ReadLine();
                Console.Write("Enter your Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter your phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter your Password: ");
                string password = Console.ReadLine();
                Console.Write("Enter your secret pin for transactions: ");
                int.TryParse(Console.ReadLine(), out int secretPin);
                decimal accountBalance = 50000.00M;
                decimal wallet = 0.00M;
                bool isLocked = false;
                int totalLogin = 0;

                users.Add(new User
                {
                    FullName = fullname,
                    Address = address,
                    Gender = gender,
                    Username = username,
                    PhoneNumber = phoneNumber,
                    Password = password,
                    SecretPin = secretPin,
                    AccountBalance = accountBalance,
                    Wallet = wallet,
                    IsLocked = isLocked,
                    TotalLogin = totalLogin
                });

                SmartCodeStore.Add(phoneNumber,fullname);

                Console.WriteLine($"{username} your details has been registered successfully...");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void addDecoderType()
        {
            DecoderType.Add("Mtn", "GOtv");
            DecoderType.Add("Airtel", "DStv");
            DecoderType.Add("Glo", "Star-time");
            DecoderType.Add("9mobile", "Premium-time");
        }

        public void AppMenuOne()
        {
            int choice = 0;
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit the Application");
                Console.Write("Select an operation to perform: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("You entered an incorrect option");
                        break;
                }
            }
        }

        public void AppMenuTwo()
        {
            int choice = 0;
            bool running = true;
            while (running)
            {
                Console.WriteLine($"Welcome back, {user.FullName}");
                Console.WriteLine($"Account Balance: {user.AccountBalance}\t Wallet Balance: {user.Wallet}");
                Console.WriteLine("1. Transfer\t\t--------------------\t\t2.Withdraw");
                Console.WriteLine("3. Airtime\t\t---------------------\t\t4.Data");
                Console.WriteLine("5. Top-Up\t\t--------------------\t\t6.Tv");
                Console.WriteLine("7. Safebox\t\t--------------------\t\t8.Transaction History");
                Console.WriteLine("9. Profile\t\t--------------------\t\t10.Logout");
                Console.WriteLine("Choose an operation to perform: ");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Transfer();
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        Airtime();
                        break;
                    case 4:
                        Data();
                        break;
                    case 5:
                        TopUp();
                        break;
                    case 6:
                        TvSubscription();
                        break;
                    case 7:
                        SafeBox();
                        break;
                    case 8:
                        TransactionHistory();
                        break;
                    case 9:
                        Profile();
                        break;
                    case 10:
                        running = false;
                        AppMenuOne();
                        break;
                    default:
                        Console.WriteLine("Invalid Option selection. Try again...");
                        break;
                }
            }
        }

        private void Profile()
        {
            Console.WriteLine($"Here is your profile - {user.Username}: ");
            User tempUser = users.Find(x=> x.Username == user.Username);
            if (tempUser != null)
            {
                Console.WriteLine($"Name - {tempUser.FullName}\t Username: {tempUser.Username}\t Gender: {tempUser.Gender}");
                Console.WriteLine($"Address: {tempUser.Address}\t PhoneNumber: {tempUser.PhoneNumber}\t Account Balance: {tempUser.AccountBalance}");
                Console.Write("Press enter to continue");
                Console.ReadLine();
                AppMenuTwo();
            }
            else
            {
                Console.WriteLine("You do not have a record. Pls register!");
                Console.Write("Press enter to continue");
                Console.ReadLine();
                AppMenuTwo();
            }
        }

        private void TransactionHistory()
        {
            try
            {
                List<Transaction> tempTransaction = new List<Transaction>();
                Console.WriteLine("Here is your transaction history...");

                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"{transaction.Username} - {transaction.Amount} - {transaction.TransactionStatus}\n");
                }
                //foreach (var transaction in tempTransaction)
                //{
                //    Console.WriteLine(transaction.Username);
                //    Console.WriteLine(transaction.Amount);
                //    Console.WriteLine(transaction.TransactionStatus);
                //    Console.WriteLine(transaction.Remark);
                //    Console.WriteLine(transaction.DateSent);

                //    Console.WriteLine("\n\n");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SafeBox()
        {
            Console.WriteLine("Welcome to safe box where you can transfer money to your wallet in order to boost your savings!");
            Console.WriteLine("Enter the amount you want to transfer to your wallet: ");
            decimal.TryParse(Console.ReadLine(), out decimal amountToBeSentToWallet);
            if(CheckUserAccountBalance(amountToBeSentToWallet) == true)
            {
                Console.WriteLine("Enter your secret key to complete this action: ");
                int.TryParse(Console.ReadLine(), out int secretPin);
                if(secretPin == user.SecretPin)
                {
                    user.Wallet += amountToBeSentToWallet;
                    Console.WriteLine($"You have successfully transferred {amountToBeSentToWallet} to your wallet.");
                    Console.WriteLine($"Your wallet balance is: {user.Wallet}");

                    transactions.Add(new Transaction
                    {
                        Username = user.Username,
                        Amount = amountToBeSentToWallet,
                        TransactionStatus = $"Transfer to wallet account",
                        Remark = $"Added {amountToBeSentToWallet} to my wallet",
                        DateSent = DateTime.Now
                    });
                    
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Try again.");
                    Console.Write("Press enter to continue: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
                
            }
            else
            {
                Console.WriteLine("Your account balance is not sufficient for this trans action.");
                Console.Write("Press enter to continue: ");
                Console.ReadLine();
                AppMenuTwo();
            }
        }

        private void TvSubscription()
        {
            string userAnswer = "";
            decimal amount = 0.00m;
            Console.Write("Enter your smart card number (Note your smartCardNo is your Phone Number): ");
            string userSmartCardNo = Console.ReadLine();
            networkProvider = getNetworkProvider(userSmartCardNo);
            Console.WriteLine($"{SmartCodeStore[$"{userSmartCardNo}"]} - {DecoderType[networkProvider]}");

            Console.Write($"1. {DecoderType[networkProvider]} Lite / 1 Month 1,575 -----");
            Console.Write($"2. {DecoderType[networkProvider]} Jinja / 1 Month 3,300 -----");
            Console.Write($"3. {DecoderType[networkProvider]} Jolli / 1 Month 4,840 -----");
            Console.Write($"4. {DecoderType[networkProvider]} Max / 1 Month 7,200 -------");
            Console.Write($"5. {DecoderType[networkProvider]} Supa / 1 Month 9,600 ------");
            Console.Write($"6. {DecoderType[networkProvider]} Supa Plus / 1 Month 15,700");

            Console.WriteLine($"Select Plan to subscribe: ");
            int.TryParse(Console.ReadLine(), out int choice);

            switch(choice)
            {
                case 1:
                    userAnswer = $"{DecoderType[networkProvider]} Lite 1 Month plan for 1,575";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(1575.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Lite 1 Month plan",$"{DecoderType[networkProvider]} Subscription",userSmartCardNo);
                    break;
                case 2:
                    userAnswer = $"{DecoderType[networkProvider]} Jinja 1 Month plan for 3,300";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(3300.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Jinja 1 Month plan", $"{DecoderType[networkProvider]} Subscription", userSmartCardNo);
                    break;
                case 3:
                    userAnswer = $"{DecoderType[networkProvider]} Jolli 1 Month plan for 4,840";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(4840.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Jolli 1 Month plan", $"{DecoderType[networkProvider]} Subscription", userSmartCardNo);
                    break;
                case 4:
                    userAnswer = $"{DecoderType[networkProvider]} Max 1 Month plan for 7,200";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(7200.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Max 1 Month plan", $"{DecoderType[networkProvider]} Subscription", userSmartCardNo);
                    break;
                case 5:
                    userAnswer = $"{DecoderType[networkProvider]} Supa 1 Month plan for 9,600";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(9600.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Supa 1 Month plan", $"{DecoderType[networkProvider]} Subscription", userSmartCardNo);
                    break;
                case 6:
                    userAnswer = $"{DecoderType[networkProvider]} Supa Plus 1 Month plan for 15,700";
                    Console.WriteLine($"Are you sure you want to subscribe for {userAnswer}");
                    Validate(15700.00m, false, $"You have successfully subscribed to {DecoderType[networkProvider]} Supa Plus 1 Month plan", $"{DecoderType[networkProvider]} Subscription", userSmartCardNo);
                    break;
                default:
                    Console.WriteLine("You selected a wrong option. Try again.");
                    Console.Write("Press enter to continue: ");
                    Console.ReadLine();
                    AppMenuTwo();
                    break;
            }
        }

        private void TopUp()
        {
            Console.WriteLine("Enter a TopUp amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal TopUpAmount);

            if (TopUpAmount > 0.00m)
            {
                Console.WriteLine($"Are you sure you want to add: {TopUpAmount} to your account");
                Console.WriteLine("You can either TopUp your account via entering the code '*223#' and follow the prompt or you can enter card details to retrieve money directly from you bank");
                Console.WriteLine("1. TopUp through code\t\t2. TopUp via card details");
                int.TryParse(Console.ReadLine(), out int choice);

                if (choice == 1)
                {
                    Console.WriteLine("Enter the code '*223#' and follow the prompt OR send to our account number '02908765'");
                    Console.WriteLine("If done Enter the code details given after successful payment: ");
                    Console.WriteLine("1. Enter Code\t\t 2. Go back to Menu");
                    int.TryParse(Console.ReadLine(), out int codeChoice);

                    if (codeChoice == 1)
                    {
                        Console.Write("Enter the code given: ");
                        int.TryParse(Console.ReadLine(), out int userCode);
                        if (TopUpCode.Contains(userCode))
                        {
                            PrintDotAnimation();
                            Validate(TopUpAmount, true, "Top up my account with amount ", "TopUp/Deposit", user.FullName);
                            Console.WriteLine("TopUp Successful..");
                            Console.WriteLine($"Your New Account Balance is: {user.AccountBalance}");
                            Console.Write("Press enter to continue: ");
                            Console.ReadLine();
                            AppMenuTwo();
                        }
                    }
                    else if(codeChoice == 2)
                    {
                        AppMenuTwo();
                    }
                    else
                    {
                        Console.WriteLine("Invalid CodeSelection Option Entered: ");
                        Console.Write("Press enter to continue: ");
                        Console.ReadLine();
                        AppMenuTwo();
                    }
                }
                else if(choice == 2) 
                {
                    Console.Write("Enter you card number: ");
                    long.TryParse(Console.ReadLine(), out long userCardNo);
                    Console.Write("Enter Card Full-Name: ");
                    string userCardFullName = Console.ReadLine();
                    Console.Write("Enter your card three digit CVV: ");
                    int.TryParse(Console.ReadLine(), out int userCardCVV);
                    Console.Write("Enter your card pin: ");
                    int.TryParse(Console.ReadLine(), out int userCardPin);

                    PrintDotAnimation();

                    Random rand = new Random();
                    int serviceYesOrNo = rand.Next(0, 2);

                    if (serviceYesOrNo == 1)
                    {
                        Validate(TopUpAmount, true, "Top up my account with amount ", "TopUp/Deposit", user.FullName);
                        Console.WriteLine("TopUp Successful..");
                        Console.WriteLine($"Your New Account Balance is: {user.AccountBalance}");
                        Console.Write("Press enter to continue: ");
                        Console.ReadLine();
                        AppMenuTwo();
                    }
                    else
                    {
                        Console.WriteLine("TopUp Unsuccessful due to network issues.... Try again!");
                        Console.Write("Press enter to continue: ");
                        Console.ReadLine();
                        AppMenuTwo();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid TopUp Options Entered: ");
                    Console.Write("Press enter to continue: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
            }
            else
            {
                Console.WriteLine("Invalid Amount entered");
                Console.Write("Press enter to continue: ");
                Console.ReadLine();
                AppMenuTwo();
            }
        }
        public void Data()
        {
            Console.WriteLine("Are you purchasing data for your self or for others (Enter 'S' for self and 'O' for others)");
            char.TryParse(Console.ReadLine(), out char option);

            if (option == 'S')
            {
                Console.Write("Enter the amount of data you want to purchase: ");
                decimal.TryParse(Console.ReadLine(), out decimal amountToBePurchased);

                if (CheckUserAccountBalance(amountToBePurchased) == true)
                {
                    Console.WriteLine($"Are you sure you want to purchase data worth of {amountToBePurchased}");
                    Console.Write("Press enter to continue: ");
                    Console.ReadLine();
                    Validate(amountToBePurchased, false, "Purchased Data (For Self)", "Data Purchase", user.PhoneNumber);

                }

            }
            else if (option == 'O')
            {
                string networkProvider = "";
                Console.WriteLine("1. 1.8GB - 14days - 500");
                Console.WriteLine("2. 1GB - 30days - 270");
                Console.WriteLine("3. 50MB - 1day - 50");
                Console.WriteLine("4. 3.9GB - 30days - 1000");
                Console.WriteLine("5. 200MB - 14days - 60");
                
                Console.Write("Enter the AMOUNT (eg 1000 or 500) of data you want to purchase: ");
                decimal.TryParse(Console.ReadLine(), out decimal amountToBePurchased);

                if (CheckUserAccountBalance(amountToBePurchased) == true)
                {
                    Console.Write("Enter the recipient phone number: ");
                    string recipientPhoneNumber = Console.ReadLine();
                    Console.Write("Select a data plan: ");
                    string userSelection = Console.ReadLine();
                    switch (userSelection)
                    {
                        case "1":
                            Console.WriteLine("Are you sure want to purchase 1.8GB for 14days for 500 naira");
                            Console.Write("Press enter to continue the operation: ");
                            Console.ReadLine();
                            Validate(500.00m,false,$"You have successfully a data plan (for {recipientPhoneNumber})","Data Purchase",recipientPhoneNumber);
                            break;
                        case "2":
                            Console.WriteLine("Are you sure want to purchase 1GB for 30days for 270 naira");
                            Console.Write("Press enter to continue the operation: ");
                            Console.ReadLine();
                            Validate(270.00m, false, $"You have successfully a data plan (for {recipientPhoneNumber})", "Data Purchase", recipientPhoneNumber);
                            break;
                        case "3":
                            Console.WriteLine("Are you sure want to purchase 50MB for 1day for 50 naira");
                            Console.Write("Press enter to continue the operation: ");
                            Console.ReadLine();
                            Validate(50.00m, false, $"You have successfully a data plan (for {recipientPhoneNumber})", "Data Purchase", recipientPhoneNumber);
                            break;
                        case "4":
                            Console.WriteLine("Are you sure want to purchase 3.9GB for 30days for 1000 naira");
                            Console.Write("Press enter to continue the operation: ");
                            Console.ReadLine();
                            Validate(1000.00m, false, $"You have successfully a data plan (for {recipientPhoneNumber})", "Data Purchase", recipientPhoneNumber);
                            break;
                        case "5":
                            Console.WriteLine("Are you sure want to purchase 200MB for 14days for 60 naira");
                            Console.Write("Press enter to continue the operation: ");
                            Console.ReadLine();
                            Validate(60.00m, false, $"You have successfully a data plan (for {recipientPhoneNumber})", "Data Purchase", recipientPhoneNumber);
                            break;
                        default:
                            Console.WriteLine("Invalid option selected: ");
                            break;

                    }      

                }
            }
            else
            {
                Console.WriteLine("Invalid option selected. Try again.");
                Console.Write("Press enter to continue the operation: ");
                Console.ReadLine();
                AppMenuTwo();
            }
        }

        public void Validate(decimal amountUsed, bool transactionenter, string remark, string transactionStatus, string recipientDetails)
        {
            if(transactionenter == true)
            {
                Console.Write("\nEnter your secret pin: ");
                int.TryParse(Console.ReadLine(), out int secretPin);
                if (secretPin == user.SecretPin)
                {
                    user.AccountBalance = user.AccountBalance + amountUsed;
                    transactions.Add(new Transaction
                    {
                        Username = user.Username,
                        Amount = amountUsed,
                        TransactionStatus = $"{transaction}",
                        Remark = $"{remark} worth of {amountUsed}",
                        DateSent = DateTime.Now
                    });

                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.Username} - {transaction.Amount} - {transaction.TransactionStatus}\n");
                    }
                    Console.WriteLine($"{transactionStatus} of {amountUsed} to {recipientDetails} successful.");
                    Console.Write("Press enter to continue the operation: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Try again.");
                    Console.Write("Press enter to continue the operation: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
            }
            else
            {
                Console.Write("Enter your secret pin: ");
                int.TryParse(Console.ReadLine(), out int secretPin);
                if (secretPin == user.SecretPin)
                {
                    user.AccountBalance = user.AccountBalance - amountUsed;
                    transactions.Add(new Transaction
                    {
                        Username = user.Username,
                        Amount = amountUsed,
                        TransactionStatus = $"{transaction}",
                        Remark = $"{remark} worth of {amountUsed}",
                        DateSent = DateTime.Now
                    });
                    Console.WriteLine($"{remark} worth of {amountUsed} to {recipientDetails}");
                    Console.Write("Press enter to continue the operation: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Try again.");
                    Console.Write("Press enter to continue the operation: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }
            }
            

        }

        private void Airtime()
        {
            Console.WriteLine("Are you purchasing airtime for your self or for others (Enter 'S' for self and 'O' for others)");
            char.TryParse( Console.ReadLine(), out char option);
            
            if(option == 'S')
            {
                Console.Write("Enter the amount of airtime you want to purchase: ");
                decimal.TryParse(Console.ReadLine(), out decimal amountToBePurchased);

                if(CheckUserAccountBalance(amountToBePurchased) == true)
                {
                    Console.WriteLine($"Are you sure you want to purchase airtime worth of {amountToBePurchased}");
                    Console.WriteLine("Press enter to continue: ");
                    Console.ReadLine();
                    Validate(amountToBePurchased, false, "Purchased an Airtime (For Self)", "Airtime Purchase", user.PhoneNumber);
           
                }

            }
            else if(option == 'O')
            {
                Console.Write("Enter the amount of airtime you want to purchase: ");
                decimal.TryParse(Console.ReadLine(), out decimal amountToBePurchased);

                if(CheckUserAccountBalance(amountToBePurchased) == true)
                {
                    Console.WriteLine("Enter the recipient phone number: ");
                    string recipientPhoneNumber = Console.ReadLine();

                    networkProvider = getNetworkProvider(recipientPhoneNumber);

                    Console.WriteLine($"{recipientPhoneNumber} => {networkProvider}");
                    Console.WriteLine($"Are you sure you want to send an airtime worth of {amountToBePurchased} to " +
                        $"{recipientPhoneNumber}");
                    Console.Write("Press enter to continue the operation: ");
                    Console.ReadLine();

                    Validate(amountToBePurchased, false, "Purchased an Airtime (For Others)", "Airtime Purchase", recipientPhoneNumber);
                }
            }
            else
            {
                Console.WriteLine("Invalid option selected. Try again.");
                Console.Write("Press enter to continue the operation: ");
                Console.ReadLine();
                AppMenuTwo();
            }
            
        }

        public string getNetworkProvider(string recipientPhoneNumber)
        {
            if (MtnPrefixCode.Contains(recipientPhoneNumber.Substring(0, 4)))
            {
                networkProvider = "Mtn";
            }
            else if (AirtlePrefixCode.Contains(recipientPhoneNumber.Substring(0, 4)))
            {
                networkProvider = "Airtel";
            }
            else if (GloPrefixCode.Contains(recipientPhoneNumber.Substring(0, 4)))
            {
                networkProvider = "Glo";
            }
            else
            {
                networkProvider = "9mobile";
            }
            return networkProvider;
        }

        private void Withdraw()
        {
            bool checkUserAccount = false;
            Console.Write("Enter the amount you want to withdraw: ");
            decimal.TryParse(Console.ReadLine(), out decimal amtToBeWithDrawn);

            checkUserAccount = CheckUserAccountBalance(amtToBeWithDrawn);

            if (checkUserAccount == true) 
            {
                Console.WriteLine($"Are you sure you want to withdraw {amtToBeWithDrawn} from your account");
                Console.WriteLine("Press Enter to continue with the operation: ");
                Console.ReadLine();
                Validate(amtToBeWithDrawn, false, "You have successfully withdrawn an amount ", "Withdrawal", user.FullName);   
                
            }
        }

        public bool CheckUserAccountBalance(decimal amtEntered)
        {
            if (amtEntered > user.AccountBalance)
            {
                Console.Write("Amount entered greater than balance.");
                return false;
            }
            return true;
        }

        public void Transfer()
        {
            Console.Write("Enter Recipient username: ");
            string recipientUsername = Console.ReadLine();
            Console.Write("Enter the Amount to be sent: \n");
            decimal.TryParse(Console.ReadLine(), out decimal amtToBeSent);
            

            User recipientUser = users.Find(x => x.Username == recipientUsername);

            if(recipientUser == null)
            {
                Console.WriteLine($"{recipientUsername} do not exist in this application!");
                Console.Write("Press enter to continue: ");
                Console.ReadLine();
                AppMenuTwo();
            }
            else
            {
                if (amtToBeSent > user.AccountBalance)
                {
                    Console.WriteLine("You do not have sufficient balance to make this transaction!." +
                        "\n Please Top Up your account");
                    Console.Write("Press enter to continue: ");
                    Console.ReadLine();
                    AppMenuTwo();
                }

                else
                {
                    Console.WriteLine($"Are you sure you want to transfer {amtToBeSent} to {recipientUser.FullName}\n");
                    Console.Write("press enter to continue: ");
                    Console.ReadLine();
                    Console.WriteLine("Enter you secret pin: ");
                    int.TryParse(Console.ReadLine(), out int secretPin);
                    if(secretPin == user.SecretPin)
                    {
                        recipientUser.AccountBalance += amtToBeSent;
                        user.AccountBalance = user.AccountBalance - amtToBeSent;
                        transactions.Add(new Transaction
                        {
                            Username = user.Username,
                            Amount = amtToBeSent,
                            TransactionStatus = "Transfer",
                            Remark = $"Transferred {amtToBeSent} to {recipientUser.FullName}",
                            DateSent = dateSent
                        });

                        Console.WriteLine($"You have successfully transferred {amtToBeSent} to {recipientUser.FullName}");
                        Console.Write("Press enter to continue: ");
                        Console.ReadLine();
                        AppMenuTwo();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Pin. Try again.");
                        Console.WriteLine("Press Enter to continue with the operation: ");
                        Console.ReadLine();
                        AppMenuTwo();
                    }
                }
               
            }
            

        }

        public void PrintDotAnimation(int time = 10)
        {
            for (int i = 0; i < time; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
        }
    }    
}
