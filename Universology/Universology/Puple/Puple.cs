
using System;


namespace Universology.Puple {
    public partial class Puple {

        private DateTime _date;
        private MatrixType _matrix;
        private ZodiacType _zodiac;
        private CrossesType _crosses;
        
        
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

        public DateTime Date {
            get => _date;
            set {
                _date = value; 

                Calculate();
            }
        }

        public string Name { set; get; }

        public MatrixType Matrix => _matrix;

        public ZodiacType Zodiac => _zodiac;

        public CrossesType Crosses => _crosses;

        private void Calculate() {

            _matrix = new MatrixType(_date);
            _zodiac = new ZodiacType(_date);
            _crosses = new CrossesType(_matrix);
            
        }
        
    }
}