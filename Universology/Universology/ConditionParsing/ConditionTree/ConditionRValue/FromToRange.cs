using System;

namespace Universology.Parsing.ConditionParsing.ConditionTree.ConditionRValue {
    public class FromToRange:ConditionRValueType {
        private readonly int _from;
        private readonly int _to;

        public FromToRange(int from, int to) {
            if(from > to) throw new Exception("Exception of FromToRange: \"from\" is more then \"to\"");
            _from = from;
            _to = to;
        }

        public override bool Check(int number) {
            return _from <= number && number <= _to;
        }
    }
}