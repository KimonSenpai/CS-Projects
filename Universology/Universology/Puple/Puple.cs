
using System;


namespace Universology.Puple {
    public partial class Puple {

        private DateTime _date;
        private Matrix _matrix;
        private Crosses _crosses;
        
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

        public Matrix Matrix => _matrix;

        public Crosses Crosses => _crosses;

        private void Calculate() {

            _matrix = new Matrix(_date);
            _crosses = new Crosses(_matrix);

            throw new NotImplementedException();

        }
        
    }
}