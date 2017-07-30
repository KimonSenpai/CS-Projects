
using System;


namespace Universology.Puple {
    public partial class Puple {
        
        private DateTime _date;
        private int[] _n = new int[6];
        private string _dateInLine = "";
        private CellType[] _numbers = new CellType[13];
        private CellType[] _sums = new CellType[4];

        public Puple( DateTime dateTime ) {
            _date = dateTime;
            Name = "";
            Calculate();
        }
        public Puple( string name, DateTime dateTime ) {
            _date = dateTime;
            Name = name;
            Calculate();
        }
        private void Calculate() {
            #region Calculating N1--N6

            _dateInLine += (_date.Day / 10 == 0 ? "0" : "") + _date.Day + ':';
            _dateInLine += (_date.Month / 10 == 0 ? "0" : "") + _date.Month + ':';
            _dateInLine += _date.Year + '.';

            _n[0] = _date.Day / 10 + _date.Day % 10 + _date.Month / 10 + _date.Month % 10;
            _n[0] += _SumOfDigits( _date.Year );
            _dateInLine += _n[0] + '.';

            _n[1] = _SumOfDigits( _n[0] );
            if (_n[0] == _n[1])
                _n[1] = 0;
            _dateInLine += (_n[1] == 0 ? "-" : _n[1].ToString()) + '.';

            _n[2] = _n[0] - 2 * (_date.Day / 10);
            _dateInLine += _n[2] + '.';

            _n[3] = _SumOfDigits( _n[2] );
            if (_n[2] == _n[3])
                _n[3] = 0;
            _dateInLine += (_n[3] == 0 ? "-" : _n[3].ToString()) + '(';

            _n[4] = _n[0] + _n[2];
            _dateInLine += _n[4] + '.';

            _n[5] = _n[1] + _n[3];
            _dateInLine += (_n[5] == 0 ? "-" : _n[5].ToString()) + '(';

            #endregion

            #region Calculating Digits

            _numbers[_date.Day / 10].Normal++;
            _numbers[_date.Day % 10].Normal++;

            _numbers[_date.Month / 10].Normal++;
            _numbers[_date.Month % 10].Normal++;

            foreach (char dig in _date.Year.ToString())
                _numbers[int.Parse( dig.ToString() )].Normal++;

            for (int i = 0; i < 4; i++) {
                if (_n[i] == 0)
                    continue;
                if (_n[i] == 11) {
                    _numbers[11].Normal++;
                    continue;
                }
                if (_n[i] == 10 || _n[i] == 12) {
                    _numbers[_n[i]].Normal++;
                    if (_date.Year >= 2000)
                        continue;
                }
                foreach (char dig in _n[i].ToString())
                    _numbers[int.Parse( dig.ToString() )].Normal++;
            }
            for (int i = 4; i < 6; i++) {
                if (_n[i] == 0)
                    continue;
                if (_n[i] == 11) {
                    _numbers[11].Corrupted++;
                    continue;
                }
                if (_n[i] == 10 || _n[i] == 12) {
                    _numbers[_n[i]].Corrupted++;
                    if (_date.Year >= 2000)
                        continue;
                }
                foreach (char dig in _n[i].ToString())
                    _numbers[int.Parse( dig.ToString() )].Corrupted++;
            }

            #endregion

            #region Calculating Sums

            for (int i = 0; i < 4; i++) {
                for (int j = 1; j <= 3; j++) {
                    _sums[i].Normal += _numbers[3 * i + j].Normal * (3 * i + j);
                    _sums[i].Corrupted += _numbers[3 * i + j].Corrupted * (3 * i + j);
                }
                while (_sums[i].Normal > 12)
                    _sums[i].Normal = _SumOfDigits( _sums[i].Normal );
                while (_sums[i].Corrupted > 12)
                    _sums[i].Corrupted = _SumOfDigits( _sums[i].Corrupted );
            }


            #endregion
        }


        public string Name { set; get; }


        public int N(int n) {
            if (!(1 <= n && n <= 6)) throw new Exception("Exception of Puple: Wrong N index.");
            return _n[n - 1];
        }
        public CellType Number(int n) {
            if(!(0 <= n && n <= 12)) throw new Exception("Exception of Puple: Wrong Number index.");
            return _numbers[n];
        }
        public CellType S(int n) {
            if(!(1 <= n && n <= 4)) throw new Exception("Exception of Puple: Wrong S index.");
            return _sums[n - 1];
        }
        public int LightWay => _n[1];
        public int HardWay => _n[3];

       

        private int _SumOfDigits(int val) {
            int res = 0;
            while (val > 0) {
                res += val % 10;
                val /= 10;
            }
            return res;
        }

        

        public override string ToString() {
            return _dateInLine;
        }
    }
}