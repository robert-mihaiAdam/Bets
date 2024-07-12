using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bets.Constants;
using Bets.Database;

namespace Bets.Clients
{
    public class Clients
    {
        private long noBets = Constants.Constants.noClientBets;
        private static long noMatches = Constants.Constants.noBets;
        private static Mutex mut = new Mutex();

        void placeBet(object obj)
        {
            BetsParameter args = (BetsParameter)obj;
            Constants.Constants.BetsOptions myPick = GetRandomEnumValue<Constants.Constants.BetsOptions>();
            string betPick = ((char)myPick).ToString();

            mut.WaitOne();

            Console.WriteLine($"thread {args.threadId}, match {args.matchId} choose {myPick}");

            BetQuotes match = args.db.BetQuotes.FirstOrDefault(quote => quote.BetId == args.matchId);
            if (match == null)
            {
                Console.WriteLine($"Thread {args.threadId} didn't find {args.matchId}");
                mut.ReleaseMutex();
                return;
            }

            switch (myPick)
            {
                case Constants.Constants.BetsOptions.A:
                    match.QuoteA += 0.01m;
                    match.QuoteB -= 0.01m;
                    match.QuoteX -= 0.01m;
                    break;
                case Constants.Constants.BetsOptions.B:
                    match.QuoteA -= 0.01m;
                    match.QuoteB += 0.01m;
                    match.QuoteX -= 0.01m;
                    break;
                case Constants.Constants.BetsOptions.X:
                    match.QuoteA -= 0.01m;
                    match.QuoteB -= 0.01m;
                    match.QuoteX += 0.01m;
                    break;
                default:
                    break;
            }

            PlacedBets placedBets = new PlacedBets { UserId = args.threadId, Type = betPick, QuoteId = match.Id };
            args.db.PlacedBets.Add(placedBets);
            args.db.SaveChanges();


            mut.ReleaseMutex();
   
        }

        public async void startBet()
        {
            Thread[] clients = new Thread[noBets];
            Random rand = new Random();
            Context db = new Context();

            for (long i = 0; i < noBets; i++)
            {
                BetsParameter bet = new BetsParameter{threadId = i, matchId = rand.NextInt64(noMatches) + 1, db = db};
                clients[i] = new Thread(new ParameterizedThreadStart(placeBet));
                clients[i].Start(bet);
            }

            for (long i = 0; i < noBets; i++)
            {
                clients[i].Join();
            }
        }

        private T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random random = new Random();
            T randomValue = (T)values.GetValue(random.Next(values.Length));
            return randomValue;
        }


    }

    internal class BetsParameter
    {
        public long threadId { get; set; }
        public long matchId { get; set; }
        public Context db { get; set; }
    }
}
