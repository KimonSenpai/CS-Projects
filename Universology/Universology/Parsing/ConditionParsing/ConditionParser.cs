
using Universology.Parsing.ConditionParsing.ConditionTree;
using Tree = Universology.Parsing.ConditionParsing.ConditionTree.ConditionTree;

namespace Universology.Parsing.ConditionParsing {
    class ConditionParser {

        private string _buffer;
        private int _pointer = 0;
        private Tree _tree;

        public static Tree Parse(string condition) {
            ConditionParser parser = new ConditionParser();
            parser._buffer = condition;
        }

        private Tree Value() {
            int len = 0;


            return new Condition(_buffer.Substring(_pointer,len));
        }
    }
}
