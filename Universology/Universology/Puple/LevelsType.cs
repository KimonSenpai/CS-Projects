namespace Universology.Puple {
    public class LevelsType {

        public readonly int[][] DigitsOfLevel = {
            new[] {10, 11},
            new[] {2, 4, 7},
            new[] {3, 6},
            new[] {1, 5},
            new[] {8},
            new[] {9},
            new[] {12, 11}
        };

        private Puple.CellType[][] _digitCount = new Puple.CellType[7][];

        public LevelsType(MatrixType matrix) {

            for (int i = 0; i < 7; i++) {
                _digitCount[i] = new Puple.CellType[DigitsOfLevel[i].Length];

                for (int j = 0; j < DigitsOfLevel[i].Length; j++) 
                    _digitCount[i][j] = matrix.Number( DigitsOfLevel[i][j] );
                
            }

        }

    }
}