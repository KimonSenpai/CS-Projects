using System;
using System.Security.Cryptography;

namespace Universology.Puple {

    public class Crosses {

        public struct Edge {
            public Zodiac.ZodiacSigns ZodiacSign;

            public int Number1;
            public int? Number2;
        }

        private readonly Edge[,] _crosses = new Edge[3,4];

        public Crosses(Matrix matrix) {

            int index = (int) (new Zodiac(matrix.Date)).ZodiacSign;

            for(int i = 0;i < 4;i++)
            for (int j = 0; j < 3; j++) {
                Edge temp;
                temp.ZodiacSign = (Zodiac.ZodiacSigns) index;

                temp.Number1 = Zodiac.DigitsOfZodiac[index][0];
                temp.Number2 = (Zodiac.DigitsOfZodiac[index].Length == 1)
                    ? null
                    : (int?) Zodiac.DigitsOfZodiac[index][1];

                _crosses[j, i] = temp;

                index++;
                index %= 12;
            }
        }

        public Edge this[int crossIndex, int edgeIndex]{
            get {
                if (!(0 <= crossIndex && crossIndex <= 2 && 0 <= edgeIndex && edgeIndex <= 3))
                    throw new IndexOutOfRangeException("Cross exception: out of range.");

                return _crosses[crossIndex, edgeIndex];
            }
        }

    }
}
