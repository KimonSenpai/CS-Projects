namespace Universology.ConditionParsing.ConditionTree.ConditionRValue {
    public class CurrentNumber:ConditionRValueType {
        private readonly int _number;
        public CurrentNumber(int number) { _number = number; }

        public override bool Check(int number) {
            return number == _number;
        }
    }
}