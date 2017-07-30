using System;

namespace Universology.Parsing.ConditionParsing.ConditionTree.ConditionRValue {
    public class ToRange:ConditionRValueType {
        private readonly int _to;

        public ToRange( int to ) {
            _to = to;
        }

        public override bool Check( int number ) {
            return number <= _to;
        }
    }
}