using GameLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _2048_UI
{
    public class GameGrid : ObservableObject
    {
        private ObservableCollection<Cell> _rowA;
        public ObservableCollection<Cell> RowA
        {
            get { return _rowA; }
            set
            {
                _rowA = value;
                NotifyPropertyChanged("RowA");
            }
        }

        private ObservableCollection<Cell> _rowB;
        public ObservableCollection<Cell> RowB
        {
            get { return _rowB; }
            set
            {
                _rowB = value;
                NotifyPropertyChanged("RowB");
            }
        }

        private ObservableCollection<Cell> _rowC;
        public ObservableCollection<Cell> RowC
        {
            get { return _rowC; }
            set
            {
                _rowC = value;
                NotifyPropertyChanged("RowC");
            }
        }

        private ObservableCollection<Cell> _rowD;
        public ObservableCollection<Cell> RowD
        {
            get { return _rowD; }
            set
            {
                _rowD = value;
                NotifyPropertyChanged("RowD");
            }
        }

        private int _points;
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
                NotifyPropertyChanged("Points");
            }
        }

        public GameGrid()
        {
            RowA = new ObservableCollection<Cell> { new Cell(), new Cell(), new Cell(), new Cell() };
            RowB = new ObservableCollection<Cell> { new Cell(), new Cell(), new Cell(), new Cell() };
            RowC = new ObservableCollection<Cell> { new Cell(), new Cell(), new Cell(), new Cell() };
            RowD = new ObservableCollection<Cell> { new Cell(), new Cell(), new Cell(), new Cell() };
            InsertRandomNumbers(2);
        }

        private void InsertRandomNumbers(int amount)
        {
            List<ObservableCollection<Cell>> allCells = new List<ObservableCollection<Cell>> { RowA, RowB, RowC, RowD };
            List<Cell> emptyCells = new List<Cell>();

            foreach (ObservableCollection<Cell> column in allCells)
            {
                foreach (Cell cell in column)
                {
                    if (cell.Number.Equals(""))
                    {
                        emptyCells.Add(cell);
                    }
                }
            }

            if (emptyCells.Count == 0) { return; } // game lost

            for (int i = 0; i < amount; i++)
            {
                int position;
                Random random = new Random((int)DateTime.Now.Ticks);
                lock (random)
                {
                    position = random.Next(0, emptyCells.Count);
                }

                emptyCells[position].Number = GetRandomNumber();

            }

        }

        private string GetRandomNumber()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            if (random.Next(1, 11) < 6)
            {
                return 2.ToString();
            }
            else
            {
                return 4.ToString();
            }
        }

        private void Move(Direction direction, List<List<Cell>> columns)
        {
            foreach (List<Cell> column in columns)
            {
                for (int i = 0; i < column.Count; i++)
                {
                    if (column[i].Number.Equals(""))
                    {
                        for (int j = i + 1; j < column.Count; j++)
                        {
                            if (!column[j].Number.Equals(""))
                            {
                                column[i].Number = column[j].Number;
                                column[j].Number = "";
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Merge(Direction direction)
        {
            List<Cell> columnA = null;
            List<Cell> columnB = null;
            List<Cell> columnC = null;
            List<Cell> columnD = null;

            if (direction == Direction.UP)
            {
                columnA = new List<Cell> { RowA[0], RowB[0], RowC[0], RowD[0] };
                columnB = new List<Cell> { RowA[1], RowB[1], RowC[1], RowD[1] };
                columnC = new List<Cell> { RowA[2], RowB[2], RowC[2], RowD[2] };
                columnD = new List<Cell> { RowA[3], RowB[3], RowC[3], RowD[3] };
            }
            else if (direction == Direction.DOWN)
            {
                columnA = new List<Cell> { RowD[0], RowC[0], RowB[0], RowA[0] };
                columnB = new List<Cell> { RowD[1], RowC[1], RowB[1], RowA[1] };
                columnC = new List<Cell> { RowD[2], RowC[2], RowB[2], RowA[2] };
                columnD = new List<Cell> { RowD[3], RowC[3], RowB[3], RowA[3] };
            }
            else if (direction == Direction.LEFT)
            {
                columnA = new List<Cell> { RowA[0], RowA[1], RowA[2], RowA[3] };
                columnB = new List<Cell> { RowB[0], RowB[1], RowB[2], RowB[3] };
                columnC = new List<Cell> { RowC[0], RowC[1], RowC[2], RowC[3] };
                columnD = new List<Cell> { RowD[0], RowD[1], RowD[2], RowD[3] };
            }
            else if (direction == Direction.RIGHT)
            {
                columnA = new List<Cell> { RowA[3], RowA[2], RowA[1], RowA[0] };
                columnB = new List<Cell> { RowB[3], RowB[2], RowB[1], RowB[0] };
                columnC = new List<Cell> { RowC[3], RowC[2], RowC[1], RowC[0] };
                columnD = new List<Cell> { RowD[3], RowD[2], RowD[1], RowD[0] };
            }

            List<List<Cell>> columns = new List<List<Cell>> { columnA, columnB, columnC, columnD };

            Move(direction, columns);

            foreach (List<Cell> column in columns)
            {
                for (int i = 0; i < column.Count; i++)
                {
                    if (i != 3 && column[i].Number.Equals(column[i + 1].Number) && !column[i].Number.Equals(""))
                    {
                        Int32.TryParse(column[i].Number, out int number);
                        Points += number;
                        // if points = 1024 then you win.
                        column[i].DoubleValue();
                        column[i + 1].ResetNumber();
                    }
                }
            }

            Move(direction, columns);
            InsertRandomNumbers(1);

            NotifyGridChanges(); // I do not understand why i have to call this when its in the setter.
        }

        private void NotifyGridChanges()
        {
            NotifyPropertyChanged("RowA");
            NotifyPropertyChanged("RowB");
            NotifyPropertyChanged("RowC");
            NotifyPropertyChanged("RowD");
        }
    }
}
