interface IGame {
    int PlayerChoose();
    OppType CheckVictory(); //TODO: make checkVictory more efficient by giving it the current move
    bool IsMoveLegal(int move);
    void MakeMove(int move, OppType opp);
    void RevertMove();
    bool IsTie();
    int OptionsCount();
    void Draw();
}