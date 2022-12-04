class TicTacToe : IGame {

    OppType[,] _board;
    int _nToWin;
    Stack<int> _moveStack;

    public TicTacToe(int width, int height, int nToWin) {
        _board = new OppType[width, height]; //automatically filled with 0 (OppType.None)
        _nToWin = nToWin;
        _moveStack = new Stack<int>();
    }

    (int X, int Y) iToXY(int i) {
        return (i % _board.GetLength(0), i / _board.GetLength(0));
    }

    public int PlayerChoose() {
        int rtn;

        while (true) {
            Console.Write("Please enter X and Y values [1-3] seperated by a space: ");
            var input = Console.ReadLine();

            if (input == null) {
                Console.WriteLine("Make sure to input a value.");
                continue;
            }

            var parts = input.TrimEnd().Split(' ');

            if (parts.Length != 2) {
                Console.WriteLine("Make sure to input 2 integers.");
                continue;
            }

            int x, y;

            if (!Int32.TryParse(parts[0], out x) || !Int32.TryParse(parts[1], out y)) {
                Console.WriteLine("Make sure both values are integers.");
                continue;
            }

            x -= 1;
            y -= 1;

            rtn = x + _board.GetLength(0) * y;

            if (!_board.IsInBounds(x, y) || !IsMoveLegal(rtn)) {
                Console.WriteLine("Make sure the values are in bounds and point to an empty space.");
                continue;
            }

            break;
        }

        return rtn;
    }

    public OppType CheckVictory() {
        var move = _moveStack.First();
        var pos = iToXY(move);
        var cellTypeToCheck = _board.ByI(move);

        foreach (var dir in new (int X, int Y)[]{(1, 0), (0, 1), (1, 1), (1, -1)}) {
            for (int i = 1 - _nToWin; i <= 0; i++) {
                var win = true;
                for (int j = 0; j < _nToWin; j++) {
                    (int X, int Y) crnt = (pos.X  + dir.X * (i + j), pos.Y + dir.Y * (i + j));
                    
                    if (!_board.IsInBounds(crnt.X, crnt.Y) || _board[crnt.X, crnt.Y] != cellTypeToCheck) {
                        win = false;
                        break;
                    }
                }
                if (win) {
                    return cellTypeToCheck;
                }
            }
        }

        return OppType.None;
    }

    public bool IsMoveLegal(int move) {
        return _board.ByI(move) == OppType.None;
    }

    public void MakeMove(int move, OppType opp) {
        _board.ByI(move) = opp;
        _moveStack.Push(move);
    }

    public void RevertMove() {
        var move = _moveStack.Pop();
        _board.ByI(move) = OppType.None;
    }

    public bool IsTie() {
        for (int x = 0; x < _board.GetLength(0); x++) {
            for (int y = 0; y < _board.GetLength(1); y++) {
                if (_board[x, y] == OppType.None) {
                    return false;
                }
            }
        }

        return true;
    }

    public int OptionsCount() {
        return _board.Length;
    }

    public void Draw() {
        for (int y = 0; y < _board.GetLength(1); y++) {
            for  (int x = 0; x < _board.GetLength(0); x++) {
                Console.Write(_board[x, y] switch {
                    OppType.Player => "O",
                    OppType.Com => "X",
                    _ => " "
                } + (x != _board.GetLength(0) - 1 ? "|" : ""));
            }
            Console.WriteLine();
            if (y != _board.GetLength(1) - 1) {
                for  (int x = 0; x < _board.GetLength(0); x++) {
                    Console.Write("-" + (x != _board.GetLength(0) - 1 ? "+" : ""));
                }
                Console.WriteLine();
            }
        }
    }
}