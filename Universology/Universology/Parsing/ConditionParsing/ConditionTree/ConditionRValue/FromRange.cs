using System;

namespace Universology.Parsing.ConditionParsing.ConditionTree.ConditionRValue {
    public class FromRange:ConditionRValueType {
        private readonly int _from;

        public FromRange( int from ) {
            _from = from;
        }

        public override bool Check( int number ) {
            return _from <= number ;
        }
    }
}