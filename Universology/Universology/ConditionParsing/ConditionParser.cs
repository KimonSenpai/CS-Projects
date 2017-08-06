using System;
using Universology.ConditionParsing.ConditionTree;
using Universology.Parsing.ConditionParsing.ConditionTree;
using Tree = Universology.Parsing.ConditionParsing.ConditionTree.ConditionTree;

namespace Universology.ConditionParsing {
    static class ConditionParser {
        
        public static Tree Parse(string condition) {
            condition += '\0';

            var it = condition.GetEnumerator();

            var res = Equation(it);

            return res.tree;

        }

        private static (Tree tree, CharEnumerator newIter) Equation(CharEnumerator iter) {

            var bad = (null as Tree, iter);
            var it = iter.Clone() as CharEnumerator;

            if (it == null) return bad;

            var leftValue = Term(it);

            if (leftValue.tree == null) return bad;

            it = leftValue.newIter;

            if (it.Current != '+') return leftValue;
            if (!it.MoveNext()) return bad;

            var rightValue = Equation(it);

            if (rightValue.tree == null) return bad;
            it = rightValue.newIter;

            return (new Add(leftValue.tree, rightValue.tree), it);

        }

        private static (Tree tree, CharEnumerator newIter) Term(CharEnumerator iter) {

            var bad = (null as Tree, iter);
            var it = iter.Clone() as CharEnumerator;

            if (it == null) return bad;

            var leftValue = Factor(it);

            if (leftValue.tree == null) return bad;

            it = leftValue.newIter;

            if (it.Current != '*') return leftValue;
            if (!it.MoveNext()) return bad;

            var rightValue = Term(it);

            if (rightValue.tree == null) return bad;
            it = rightValue.newIter;

            return (new Mult(leftValue.tree, rightValue.tree), it);

        }

        private static (Tree tree, CharEnumerator newIter) Factor(CharEnumerator iter) {

            var bad = (null as Tree, iter);
            var it = iter.Clone() as CharEnumerator;
            (Tree tree, CharEnumerator newIter) res;

            if (it == null) return bad;

            if (it.Current == '(') {
                if (!it.MoveNext()) return bad;

                res = Equation(it);
                if (res.tree == null) return bad;
                it = res.newIter;

                if (it.Current != ')') return bad;
                if (!it.MoveNext()) return bad;
            }
            else {
                res = Value(it);
                it = res.newIter;
            }

            return (res.tree, it);

        }

        private static (Tree tree, CharEnumerator newIter) Value(CharEnumerator iter) {

            var bad = (null as Tree, iter);
            var it = iter.Clone() as CharEnumerator;
            Condition condition;

            if (it == null || it.Current != '[') return bad;

            string conditionalString = "";

            do {

                conditionalString += it.Current;
                if (!it.MoveNext()) return bad;

            } while (it.Current != ']');

            conditionalString += ']';

            if (!it.MoveNext())
                throw new Exception(
                    "Something strange with iter.MoveNext in ConditionParser.cs ConditionParser.Value function.");

            try {
                condition = new Condition(conditionalString);
            }
            catch (ArgumentException) {
                return bad;
            }
            catch (Exception) {
                throw new Exception("Something strange with Condition constructor in ConditionParser.cs ConditionParser.Value function.");
            }

            return (condition, it);
        }

    }
}
