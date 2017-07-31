using Universology.Puple;

namespace Universology.Parsing.ConditionParsing.ConditionTree {
    public class Add:ConditionTree {
        private ConditionTree _left, _right;

        public Add(ConditionTree left, ConditionTree right) {
            _left = left;
            _right = right;
        }

        public override bool Check(Matrix puple) {
            return _left.Check(puple) || _right.Check(puple);
        }
    }
}