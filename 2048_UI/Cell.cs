using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_UI
{
    public class Cell : ObservableObject
    {
        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyPropertyChanged(Number);
            }
        }

        public void DoubleValue()
        {
            Int32.TryParse(Number, out int number);
            number *= 2;
            Number = number.ToString();
        }

        public void ResetNumber()
        {
            Number = "";
        }

        public Cell()
        {
            Number = "";
        }
    }
}
