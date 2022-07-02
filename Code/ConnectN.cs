class ConnectN : IGame {

    class MoveInfo {
        public int X {init; get;}
        public int Y {init; get;}
    }

    OppType[,] _board;
    int _nToWin;
    Stack<(int X, int Y)> _moveStack;

    public ConnectN(int width, int height, int nToWin) {
        _board = new OppType[width, height];
        _nToWin = nToWin;
        _moveStack = new Stack<(int, int)>();
    }

    public bool IsMoveLegal(int move) {
        return _board[move, 0] == OppType.None;
    }

    public void MakeMove(int move, OppType opp) {
        for (int y = _board.GetLength(1) - 1; y >= 0; y--) {
            if (_board[move, y] == OppType.None) {
                _board[move, y] = opp;
                _moveStack.Push(new (move, y));
                return;
            }
        }

        throw new Exception("column is already full");
    }

    public void RevertMove() {
        var move = _moveStack.Pop();
        _board[move.X, move.Y] = OppType.None;
    }

    public bool IsTie() {
        for (int x = 0; x < _board.GetLength(0); x++) {
            if (_board[x, 0] == OppType.None) {
                return false;
            }
        }

        return true;
    }

    public OppType CheckVictory() {
        var move = _moveStack.First();
        var cellTypeToCheck = _board[move.X, move.Y];

        foreach (var dir in new (int X, int Y)[]{(1, 0), (0, 1), (1, 1), (1, -1)}) {
            for (int i = 1 - _nToWin; i <= 0; i++) {
                var win = true;
                for (int j = 0; j < _nToWin; j++) {
                    (int X, int Y) crnt = (move.X + dir.X * (i + j), move.Y + dir.Y * (i + j));
                    
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

    public int PlayerChoose() {
        string? userInput;
        int chosen = 0;

        Console.Write($"Enter a colomn (1-{_board.GetLength(0)}): ");

        while(true) {
            userInput = Console.ReadLine();

            if (userInput == null) {
                Console.Write("No Input, please enter a value: ");
            } else if (!int.TryParse(userInput, out chosen)) {
                Console.Write("Make sure to input an integer: ");
            } else if (chosen < 1) {
                Console.Write("Make sure to input a number equal to 1 or larger: ");
            } else if (chosen > _board.GetLength(0)) {
                Console.Write($"Make sure to input a number equal to {_board.GetLength(0)} or smaller: ");
            } else if (!IsMoveLegal(chosen - 1)) {
                Console.Write("Make sure the column you input is not already full: ");
            } else {
                break;
            }
        };

        return chosen - 1;
    }

    public int OptionsCount() {
        return _board.GetLength(0);
    }

    public void Draw() {
        for (int y = 0; y < _board.GetLength(1); y++) {
            for (int x = 0; x < _board.GetLength(0); x++) {  
                Console.Write(_board[x, y] switch {
                    OppType.Player => 'O',
                    OppType.Com => 'X',
                    _ => '*'
                } + " ");
            };
            Console.WriteLine();
        }

        for (int x = 0; x < _board.GetLength(0); x++) {
            Console.Write(x + 1 + " ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}