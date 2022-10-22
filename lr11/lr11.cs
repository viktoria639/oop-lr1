public class GameAccount
{
    public string UserName { get; }
    public uint CurrentRating { get; set; }
    public uint GamesCount { get; set; }
    private List<Game> gamesList;
    
    public GameAccount(string name)
    {
        UserName = name;
        CurrentRating = 1;
        GamesCount = 0;
        gamesList = new List<Game>();
    }
    public void WinGame(GameAccount opponent, uint rating)
    {
        Game game = new Game(this, opponent, rating);
        opponent.LoseGame(this, rating, game);
        gamesList.Add(game);
        GamesCount++;
        CurrentRating += rating;
    }
    public void LoseGame(GameAccount opponent, uint rating, Game game)
    {
        gamesList.Add(game);
        GamesCount++;
        if (CurrentRating > rating)
        {
            CurrentRating -= rating;
        }
        else
        {
            CurrentRating = 1;
        }
    }
    public List<Game> GetStats()
    {
        Console.WriteLine(this.UserName + " Status:\nCurrent Rating - " + this.CurrentRating + "\tGames Played - " + this.GamesCount);
        foreach (Game game in gamesList)
        {
            Console.Write("Winner - ");
            Console.Write(game.Winner.UserName);
            Console.Write("\tLoser - ");
            Console.Write(game.Loser.UserName);
            Console.Write("\tScore - ");
            Console.Write(game.Score);
            Console.WriteLine("\tID - " + game.GameId);
        }
        return gamesList;
    }
}

public class Game
{
    public GameAccount Winner { get; }
    public GameAccount Loser { get; }
    public uint Score { get; }
    private static uint id = 0;
    public uint GameId { get; }
public Game(GameAccount winner, GameAccount loser, uint score)
    {
        this.Winner = winner;
        this.Loser = loser;
        this.Score = score;
        this.GameId = id++;
    }
}

public class GamePlay
{
    public static void Main(string[] args)
    {
        GameAccount player1 = new GameAccount("mia");
        GameAccount player2 = new GameAccount("vincent");
        GameAccount player3 = new GameAccount("butch");
        play(player1, player2, 15);
        play(player1, player3, 15);
        play(player2, player1, 15);
        play(player2, player3, 15);
        play(player3, player1, 15);
        play(player3, player2, 15);
        player1.GetStats();
        player2.GetStats();
        player3.GetStats();
    }
    public static void play(GameAccount player1, GameAccount player2, uint score)
    {
        Random random = new Random();
        if (random.Next(0, 2) == 1)
        {
            player1.WinGame(player2, score);
        }
        else
        {
            player2.WinGame(player1, score);
        }
    }
}
