using System;
using System.ComponentModel;
using Universology.Parsing.ConditionParsing.ConditionTree.ConditionRValue;
using Universology.Properties;


namespace Universology.Parsing.ConditionParsing.ConditionTree {
    public class Condition : ConditionTree {

        private ConditionLValueType _lValue;
        private ConditionRValueType _rValue;

        public Condition(string condition) {
            if (!Parser.Parse(this, condition))
                throw new ArgumentException(Resources.ParsingException, nameof(condition));
        }

        public override bool Check(Puple.Puple puple) {
            bool res;
            switch (_lValue.Type) {
                case ConditionLValueType.LValueType.Digit:
                    res = _rValue.Check(_lValue.Normal
                        ? puple.Number(_lValue.Number).Normal
                        : puple.Number(_lValue.Number).Corrupted);
                    break;

                case ConditionLValueType.LValueType.Sum:
                    res = _rValue.Check(_lValue.Normal
                        ? puple.S(_lValue.Number).Normal
                        : puple.S(_lValue.Number).Corrupted);
                    break;

                case ConditionLValueType.LValueType.Way:
                    res = _rValue.Check(_lValue.Number == 0 ? puple.LightWay : puple.HardWay);
                    break;

                default:
                    throw new InvalidEnumArgumentException(Resources.InvalidConditionLValueType, (int) _lValue.Type,
                        typeof(ConditionLValueType));
            }
            return res;
        }

        private static class Parser {
            public static bool Parse(Condition condition, string cond) {

                cond += '\0';

                CharEnumerator iter = cond.GetEnumerator();

                return Condition(condition, iter);

            }

            private static bool Condition(Condition condition, CharEnumerator iter) {

                CharEnumerator it = iter.Clone() as CharEnumerator;

                // ReSharper disable once UseNullPropagation . In this place it works incorrect
                if (it == null) return false;

                if (it.Current != '[') return false;
                if (!it.MoveNext()) return false;


                var r = DigitCond(condition, it);
                it = r.newIter;
                if (r.parsed) goto end;


                r = SumCond(condition, it);
                it = r.newIter;
                if (r.parsed) goto end;


                r = WayCond(condition, it);
                it = r.newIter;
                if (r.parsed) goto end;

                return false;

                end:
                if (it.Current == ']') return true;
                return false;
            }

            private static (bool parsed, CharEnumerator newIter) DigitCond(Condition condition, CharEnumerator iter) {

                var bad = (false, iter);
                var it = iter.Clone() as CharEnumerator;

                var lValue = Digit(it);
                if (!lValue.parsed) return bad;
                it = lValue.newIter;

                condition._lValue = new ConditionLValueType(ConditionLValueType.LValueType.Digit, lValue.number,
                    lValue.normal);

                foreach (var c in "==") {
                    if (it.Current != c) return bad;
                    if (!it.MoveNext()) return bad;
                }

                var rValue = DigitRValue(it);
                if (!rValue.parsed) return bad;
                it = rValue.newIter;

                condition._rValue = rValue.rValue;

                return (true, it);

            }

            private static (bool parsed, CharEnumerator newIter) SumCond(Condition condition, CharEnumerator iter) {

                var bad = (false, iter);
                var it = iter.Clone() as CharEnumerator;

                var lValue = Sum(it);
                if (!lValue.parsed) return bad;
                it = lValue.newIter;

                condition._lValue = new ConditionLValueType(ConditionLValueType.LValueType.Sum, lValue.n,
                    lValue.normal);

                foreach (var c in "==") {
                    if (it.Current != c) return bad;
                    if (!it.MoveNext()) return bad;
                }

                var rValue = SumWayRValue(it);
                if (!rValue.parsed) return bad;
                it = rValue.newIter;

                condition._rValue = rValue.rValue;

                return (true, it);

            }

            private static (bool parsed, CharEnumerator newIter) WayCond(Condition condition, CharEnumerator iter) {

                var bad = (false, iter);
                var it = iter.Clone() as CharEnumerator;

                var lValue = Way(it);
                if (!lValue.parsed) return bad;
                it = lValue.newIter;

                condition._lValue = new ConditionLValueType(ConditionLValueType.LValueType.Way, lValue.n);

                foreach (var c in "==") {
                    if (it.Current != c) return bad;
                    if (!it.MoveNext()) return bad;
                }

                var rValue = SumWayRValue(it);
                if (!rValue.parsed) return bad;
                it = rValue.newIter;

                condition._rValue = rValue.rValue;

                return (true, it);

            }

            private static (bool parsed, ConditionRValueType rValue, CharEnumerator newIter) DigitRValue(CharEnumerator iter) {

                var it = iter.Clone() as CharEnumerator;

                var r = Range(it);
                it = r.newIter;
                if (r.parsed) goto end;

                r = CurrentNumber(it);
                it = r.newIter;
                if (r.parsed) goto end;

                return (false, null, iter);

                end:
                return (true, r.rValue, it);

            }

            private static (bool parsed, ConditionRValueType rValue, CharEnumerator newIter) SumWayRValue(CharEnumerator iter) {

                var bad = (false, null as ConditionRValueType, iter);
                var it = iter.Clone() as CharEnumerator;


                if (it != null && it.Current == '~') {
                    if (!it.MoveNext()) return bad;
                    return (true, new CurrentNumber(0), it);
                }

                var r = Number(it);
                if (!r.parsed) return bad;

                return (true, new CurrentNumber(r.number), it);

            }

            private static (bool parsed, ConditionRValueType rValue, CharEnumerator newIter) Range(CharEnumerator iter) {

                (bool, ConditionRValueType, CharEnumerator) bad = (false, null, iter);
                var it = iter.Clone() as CharEnumerator;

                if (it == null) return bad;

                if (it.Current != '{') return bad;
                if (!it.MoveNext()) return bad;

                var r = ToRange(it);
                it = r.newIter;
                if (r.parsed) goto end;


                r = FromRange(it);
                it = r.newIter;
                if (r.parsed) goto end;


                r = FromToRange(it);
                it = r.newIter;
                if (r.parsed) goto end;

                return bad;

                end:
                if (it.Current != '}') return bad;
                if (!it.MoveNext()) return bad;

                return (true, r.range, it);

            }

            private static (bool parsed, ConditionRValueType range, CharEnumerator newIter) FromToRange(CharEnumerator iter) {

                (bool, ConditionRValueType, CharEnumerator) bad = (false, null, iter);
                var it = iter.Clone() as CharEnumerator;

                var range = (from:0, to:0);

                var r = Number(it);
                it = r.newIter;
                if (!r.parsed) return bad;
                range.from = r.number;

                if (it.Current != ',' || !it.MoveNext()) return bad;

                r = Number(it);
                it = r.newIter;
                if (!r.parsed) return bad;
                range.to = r.number;

                return (true, new FromToRange(range.from, range.to), it);
            }

            private static (bool parsed, ConditionRValueType range, CharEnumerator newIter) FromRange(CharEnumerator iter) {

                (bool, ConditionRValueType, CharEnumerator) bad = (false, null, iter);
                var it = iter.Clone() as CharEnumerator;

                var r = Number(it);
                it = r.newIter;
                if (!r.parsed) return bad;
                var from = r.number;

                if (it.Current != ',' || !it.MoveNext()) return bad;

                return (true, new FromRange(from), it);
            }

            private static (bool parsed, ConditionRValueType range, CharEnumerator newIter) ToRange(CharEnumerator iter) {

                var bad = (false, null as ConditionRValueType, iter);
                var it = iter.Clone() as CharEnumerator;

                if (it == null) return bad;

                if (it.Current != ',' || !it.MoveNext()) return bad;

                var r = Number(it);
                it = r.newIter;
                if (!r.parsed) return bad;
                var to = r.number;

                return (true, new ToRange(to), it);
            }

            private static (bool parsed, ConditionRValueType rValue, CharEnumerator newIter) CurrentNumber(CharEnumerator iter) {
                (bool, ConditionRValueType, CharEnumerator) bad = (false, null, iter);

                var it = iter.Clone() as CharEnumerator;

                var r = Number(it);
                it = r.newIter;
                if (!r.parsed) return bad;
                var number = r.number;

                return (true, new CurrentNumber(number), it);
            }

            private static (bool parsed, int n, CharEnumerator newIter)
                Way(CharEnumerator iter) {

                var bad = (false, 0, iter);
                var it = iter.Clone() as CharEnumerator;

                int num;

                if (it == null) return bad;
                switch (char.ToUpper(it.Current)) {
                    case 'L':
                        num = 0;
                        break;

                    case 'H':
                        num = 1;
                        break;

                    default:
                        return bad;
                }

                if (it.Current != 'W' || !it.MoveNext()) return bad;
                return (true, num, it);

            }

            private static (bool parsed, int n, bool normal, CharEnumerator newIter) Sum(CharEnumerator iter) {

                var bad = (false, 0, false, iter);
                var it = iter.Clone() as CharEnumerator;

                if (it == null) return bad;

                int n;

                var norm = it.Current != '(';

                if(!norm)
                    if (!it.MoveNext())
                        return bad;

                if (char.ToUpper(it.Current) != 'S' || !it.MoveNext()) return bad;

                if (!int.TryParse(it.Current.ToString(), out n) || !(1 <= n && n <= 4) || !it.MoveNext()) return bad;

                if(!norm)
                    if (it.Current != ')'|| !it.MoveNext() )
                        return bad;

                return (true, n, norm, it);

            }

            private static (bool parsed, int number, bool normal, CharEnumerator newIter) Digit(CharEnumerator iter) {

                var bad = (false, 0,false, iter);
                var it = iter.Clone() as CharEnumerator;

                if (it == null) return bad;

                var norm =  it.Current != '(';

                if (!norm)
                    if (!it.MoveNext())
                        return bad;

                if (!char.IsDigit(it.Current)) return bad;

                var number = int.Parse(it.Current.ToString());

                if (!it.MoveNext()) return bad;

                if (char.IsDigit(it.Current)) {

                    number *= 10;
                    number += int.Parse(it.Current.ToString());

                    if (number > 12) return bad;
                }

                if (!norm)
                    if ( it.Current != ')'||!it.MoveNext() )
                        return bad;

                return (true, number, norm, it);
            }

            private static (bool parsed, int number, CharEnumerator newIter) Number(CharEnumerator iter) {

                var bad = (false, 0, iter);
                var it = iter.Clone() as CharEnumerator;

                if (it == null) return bad;

                if (!char.IsDigit(it.Current)) return bad;

                string curNum = "";

                do {
                    curNum += it.Current;
                    if (!it.MoveNext()) return bad;
                } while (char.IsDigit(it.Current));

                return (true, int.Parse(curNum), it);
                
            }
        }
    }
}