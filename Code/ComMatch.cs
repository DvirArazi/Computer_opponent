using System.Diagnostics;

enum OppType {
    None,
    Player,
    Com,
    Tie,
}

class ComMatch {
    IGame _game;
    bool _isPlayerTurn;
    int _comDepth;

    public ComMatch(IGame game, int comDepth, bool isPlayerFirst) {
        _game = game;
        _isPlayerTurn = isPlayerFirst;
        _comDepth = comDepth;
    }

    void playerTurn() {
        _game.MakeMove(_game.PlayerChoose(), OppType.Player);
    }

    int playerOptionValue(int depth) {
        int minValue = _comDepth;

        if (depth < _comDepth) {
            for (int i = 0; i < _game.OptionsCount(); i++) {
                if (_game.IsMoveLegal(i)) {
                    _game.MakeMove(i, OppType.Player);

                    if (_game.CheckVictory() == OppType.Player) {
                        _game.RevertMove();
                        return depth;
                    }

                    int comValue = comOptionValue(depth);

                    if (minValue > comValue) {
                        minValue = comValue;
                    }

                    _game.RevertMove();
                }
            }
        }

        return minValue;
    }

    int comOptionValue(int depth) {
        if (_game.IsTie()) {
            return _comDepth + 1;
        }

        int maxValue = 0;

        for (int i = 0; i < _game.OptionsCount(); i++) {
            if (_game.IsMoveLegal(i)) {
                _game.MakeMove(i, OppType.Com);

                if (_game.CheckVictory() == OppType.Com) {
                    _game.RevertMove();
                    return _comDepth + 2;
                }

                int playerValue = playerOptionValue(depth + 1);

                if (maxValue < playerValue) {
                    maxValue = playerValue;
                }

                _game.RevertMove();
            }
        }

        return maxValue;
    }

    void comTurn() {
        List<int> options = new List<int>();
        int maxValue = 0;

        for (int i = 0; i < _game.OptionsCount(); i++) {
            if (_game.IsMoveLegal(i)) {
                _game.MakeMove(i, OppType.Com);

                int playerValue;
                if (_game.CheckVictory() == OppType.Com) {
                    playerValue = _comDepth + 1;
                } else {
                    playerValue = playerOptionValue(0);
                }

                if (maxValue < playerValue) {
                    maxValue = playerValue;
                    options = new List<int>() {i};
                } else if (maxValue == playerValue) {
                    options.Add(i);
                }

                _game.RevertMove();
            }
        }

        _game.MakeMove(options[Utils.Random.Next(0, options.Count)], OppType.Com);
    }

    public OppType checkVictory() {
        OppType winner = _game.CheckVictory();

        if (winner != OppType.None) {
            return winner;
        } else {
            return _game.IsTie() ? OppType.Tie : OppType.None;
        }
    }

    public void Run() {
        OppType winner = OppType.None;
        
        if (_isPlayerTurn) {
            _game.Draw();
        }

        Stopwatch stopwatch = new Stopwatch();

        do {
            if (_isPlayerTurn) {
                playerTurn();
            } else {
                stopwatch.Start();
                comTurn();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.Elapsed);
                stopwatch.Reset();
                _game.Draw();
            }
            _isPlayerTurn = !_isPlayerTurn;
            winner = checkVictory();
        } while (winner == OppType.None);

        if (winner == OppType.Player) {
            _game.Draw();
        }

        Console.WriteLine(
            winner == OppType.Player ? "Congratulations. You won!" :
            winner == OppType.Com ? "You lost... Better luck next time!" :
            "It's a tie. Well Played"
        );
    }
}