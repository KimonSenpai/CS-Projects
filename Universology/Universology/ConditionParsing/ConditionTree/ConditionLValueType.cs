using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Universology.Parsing.ConditionParsing.ConditionTree {
    public class ConditionLValueType {
        
        public enum LValueType {
            Digit,
            Sum,
            Way
        }

        public readonly LValueType Type;

        public readonly int Number;

        public readonly bool Normal;

        /// <summary>
        /// Class' constructor.
        /// </summary>
        /// <param name="type">Type of left value.</param>
        /// <param name="number">Digit, or index of sum[1,4], or way(0 ~ LW, 1 ~ HW).</param>
        /// <param name="normal">True if normal, false if currapted(if type == Way, isn't necessary).</param>
        public ConditionLValueType(LValueType type, int number, bool normal = true) {
            if (!Enum.IsDefined(typeof(LValueType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(LValueType));
            if (!(number >= 0&&number <= 12))
                throw new ArgumentOutOfRangeException(nameof(number));
            Type = type;
            Number = number;
            Normal = normal;
        }

        public bool Check() {
            throw new NotImplementedException();
        }


    }
}