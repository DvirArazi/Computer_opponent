using System.IO.Compression;

static class Utils {
    public static Random Random = new Random();

    public static T[,] FillArray2D<T>(T value, int lengthX, int lengthY) {
        var rtn = new T[lengthX, lengthY];

        for (int x = 0; x < lengthX; x++) {
            for (int y = 0; y < lengthY; y++) {
                rtn[x, y] = value;
            }
        }

        return rtn;
    }

    public static bool IsInBounds<T>(this T[,] arr, int x, int y) {
        return x >= 0 && x < arr.GetLength(0) && y >= 0 && y < arr.GetLength(1);
    }

    public static ref T ByI<T>(this T[,] arr, int i) {
        return ref arr[i % arr.GetLength(0), i / arr.GetLength(0)];
    }

    public static bool RandBool() {
        Random rand = new Random();
        return rand.NextDouble() > 0.5 ? true : false;
    }

    public static string getInput(string message ,(Func<string, bool> cond, string msg)[] errors) {
        Console.Write(message + ": ");
        while(true) {
            var input = Console.ReadLine();
            if (input == null) {
                Console.Write("Input cannot be null: ");
                continue;
            }

            var invalid = false;
            while(true) {
                foreach (var err in errors) {
                    if (err.cond(input)) {
                        Console.Write(err.msg + ": ");
                        invalid = true;
                        break;
                    }
                }
                if (invalid) break;

                return input;
            }
        }
    }
}