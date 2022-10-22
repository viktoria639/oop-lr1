namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount Naker = new GameAccount("Naker");
            GameAccount Joker = new GameAccount("Joker");

            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                    Naker.winGame(Joker, (new Random()).Next(5, 20));
                else
                    Joker.winGame(Naker, (new Random()).Next(5, 20));
            }

            Joker.getStats();
            Naker.getStats();
            Console.WriteLine();
        }
    }
    class GameAccount
    {
        public string userName {get;}
        private int currentRating { get; set; }
        uint gamesCount;
        readonly List<Game> gamesHistory = new List<Game>();
        public GameAccount(string userName)
        {
            this.userName = userName;
            currentRating = 1;
        }

        public void winGame(GameAccount opponentName, int rating)
        {
            if (rating <= 0) throw new ArgumentOutOfRangeException("Rating must be greater than 0");
            Game game = new Game(this, opponentName, rating);
            gamesHistory.Add(game);
            opponentName.loseGame(this,rating,game);
            currentRating += rating;
            gamesCount++;
        }

        public void loseGame(GameAccount opponentName, int rating,Game game)
        {
            if (rating > currentRating) currentRating = 1;
            else currentRating -= rating;

            gamesHistory.Add(game);
            gamesCount++;
        }

        public void getStats()
        {
            Console.WriteLine(userName+"`s status\n");
            foreach (Game game in gamesHistory)
            {
                Console.Write("Winner - ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(game.Winner.userName);
                Console.ResetColor();
                Console.Write("\tLoser - ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(game.Looser.userName);
                Console.ResetColor();
                Console.WriteLine("\tRating - " + game.rating + "\tID - " + game.ID);
            }
            Console.WriteLine("\nGames played: " + gamesCount);
            Console.WriteLine("Current rating of " + this.userName + ": " + currentRating+"\n");
        }
    }
    class Game
    {
        public readonly GameAccount Winner;
        public readonly GameAccount Looser;
        public readonly int rating;
        static uint globalID;
        public readonly uint ID;

        public Game(GameAccount firstPlayer, GameAccount secondPlayer, int rating)
        {
            Winner = firstPlayer;
            Looser = secondPlayer;
            this.rating = rating;
            ID = globalID++;
        }

    }
}