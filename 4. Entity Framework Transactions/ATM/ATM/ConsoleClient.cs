using System.Linq;

namespace ATM
{
    using System;

    class ConsoleClient
    {
        static void Main(string[] args)
        {
            var context = new ATMEntities1();
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Console.Write("Enter card number to withdraw money from it: ");
                    string cardNumber = Console.ReadLine();
                    var cardAccount = context.CardAccounts.FirstOrDefault(c => c.CardNumber == cardNumber);
                    if (cardAccount == null)
                    {
                        throw new Exception("Invalid card number!");
                    }

                    Console.Write("Enter PIN: ");
                    string PIN = Console.ReadLine();

                    if (PIN != cardAccount.CardPIN)
                    {
                        throw new Exception("Invalid PIN!");
                    }

                    Console.Write("Enter amount: ");
                    decimal amount = Decimal.Parse(Console.ReadLine());

                    if (amount > cardAccount.CardCash || amount == null)
                    {
                        throw new Exception("You dont have so much money or invalid amount!");
                    }

                    cardAccount.CardCash -= amount;
                    context.TransactionHistories.Add(new TransactionHistory()
                    {
                        CardNumber = cardNumber,
                        TransactionDate = DateTime.Now,
                        Amount = amount
                    });
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
