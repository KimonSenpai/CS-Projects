using Universology.Puple;

namespace Universology.Parsing.ConditionParsing.ConditionTree {
    public class Mult:ConditionTree {
        private ConditionTree _left, _right;

        public Mult( ConditionTree left, ConditionTree right ) {
            _left = left;
            _right = right;
        }

        public override bool Check(MatrixType puple) {
            return _left.Check( puple ) && _right.Check( puple );
        }
    }
}