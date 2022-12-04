

class Program
{

    static void Main()
    {
        run();
        // test();
    }

    static void run()
    {
        string? input = "Y";

        while (input == "Y")
        {

            int gameNum = default!;
            Console.Write(
                "What game would you like to play?\n" +
                "1. Connect Four\n" +
                "2. Tic Tac Toe\n"
            );
            while (true)
            {
                input = Console.ReadLine();

                if (input == null)
                {
                    Console.Write("Make sure to input a value: ");
                    continue;
                }

                if (input != "1" && input != "2")
                {
                    Console.Write("Make sure to input either '1' or '2': ");
                    continue;
                }

                gameNum = Int32.Parse(input);

                break;
            }
            Console.WriteLine();

            bool isPlayerFirst = default!;
            Console.Write("Who should start? (P - You, C - Computer, R - Random): ");
            while (true)
            {
                input = Console.ReadLine();

                if (input == null)
                {
                    Console.Write("Make sure to input a value: ");
                    continue;
                }

                input = input.ToUpper();

                if (input != "P" && input != "C" && input != "R")
                {
                    Console.Write("Make sure to input either 'P', 'C' or 'R': ");
                    continue;
                }

                isPlayerFirst = input switch
                {
                    "P" => true,
                    "C" => false,
                    _ => Utils.RandBool()
                };

                break;
            }
            Console.WriteLine();

            ComMatch game = gameNum switch
            {
                1 => new ComMatch(new ConnectN(7, 6, 4), 3, isPlayerFirst),
                2 => new ComMatch(new TicTacToe(3, 3, 3), 4, isPlayerFirst),
                _ => default!
            };

            game.Run();

            Console.Write("Would you like to play another game? (Y - yes, N - no): ");
            Console.WriteLine();

            input = Console.ReadLine();
            if (input != null)
            {
                input = input.ToUpper();
            }
        }
    }

    static void test()
    {
        var game = new ConnectN(7, 6, 4);
        game.MakeMove(1, OppType.Com);
        game.MakeMove(1, OppType.Com);

        game.MakeMove(2, OppType.Player);
        game.MakeMove(2, OppType.Player);
        game.MakeMove(2, OppType.Player);

        game.MakeMove(3, OppType.Player);
        game.MakeMove(3, OppType.Player);
        game.MakeMove(3, OppType.Player);
        game.MakeMove(3, OppType.Com);
        game.MakeMove(3, OppType.Player);
        game.MakeMove(3, OppType.Com);

        game.MakeMove(5, OppType.Com);
        game.MakeMove(5, OppType.Com);

        game.MakeMove(2, OppType.Player);

        game.Draw();

        Console.WriteLine(game.CheckVictory());
        Console.ReadKey();
    }

}