using System;
using System.Windows;
using System.Windows.Controls;

namespace MinesGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Field[,] _fields;
        private int _numOfDiscoveredFields;
        public MainWindow()
        {
            InitializeComponent();
            NewGame();

        }

        private void NewGame()
        {
            MainGrid.Children.Clear();
            _fields = new Field[10, 10];
            _numOfDiscoveredFields = 0;

            DrawFields();
            LinkFields();
            PutTraps();
            CalculateFieldValues();
        }

        private void CalculateFieldValues()
        {
            int counter = 0;
            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    if (_fields[i, j].FieldContent.Content.ToString() == "X")
                    {
                        continue;
                    }

                    if (_fields[i, j].LeftField != null && _fields[i, j].LeftField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].TopLeftField != null && _fields[i, j].TopLeftField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].TopField != null && _fields[i, j].TopField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].TopRightField != null && _fields[i, j].TopRightField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].RightField != null && _fields[i, j].RightField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].BottomRightField != null && _fields[i, j].BottomRightField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].BottomField != null && _fields[i, j].BottomField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (_fields[i, j].BottomLeftField != null && _fields[i, j].BottomLeftField.FieldContent.Content.ToString() == "X")
                    {
                        counter++;
                    }
                    if (counter != 0)
                    {
                        _fields[i, j].FieldContent.Content = counter.ToString();
                    }
                    
                    counter = 0;
                }
            }
        }

        private void PutTraps()
        {
            Random random = new Random();
            for (int i = 0; i < 16; i++)
            {
                int rowIndex = random.Next(0, 10);
                int columnIndex = random.Next(0, 10);

                if (_fields[rowIndex, columnIndex].FieldContent.Content.ToString() == "X")
                {
                    i--;
                }
                else
                {
                    _fields[rowIndex, columnIndex].FieldContent.Content = "X";
                }
            }
        }

        private void LinkFields()
        {
            int numOfRows = _fields.GetLength(0);
            int numOfColumns = _fields.GetLength(1);

            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfColumns; j++)
                {
                    if (j > 0)
                    {
                        _fields[i, j].LeftField = _fields[i, j - 1];
                    }
                    if (i > 0 && j > 0)
                    {
                        _fields[i, j].TopLeftField = _fields[i - 1, j - 1];
                    }
                    if (i > 0)
                    {
                        _fields[i, j].TopField = _fields[i - 1, j];
                    }
                    if (i > 0 && j < numOfColumns - 1)
                    {
                        _fields[i, j].TopRightField = _fields[i - 1, j + 1];
                    }
                    if (j < numOfColumns - 1)
                    {
                        _fields[i, j].RightField = _fields[i, j + 1];
                    }
                    if (i < numOfRows - 1 && j < numOfColumns - 1)
                    {
                        _fields[i, j].BottomRightField = _fields[i + 1, j + 1];
                    }
                    if (i < numOfRows - 1)
                    {
                        _fields[i, j].BottomField = _fields[i + 1, j];
                    }
                    if (i < numOfRows - 1 && j > 0)
                    {
                        _fields[i, j].BottomLeftField = _fields[i + 1, j - 1];
                    }
                }
            }
        }

        private void DrawFields()
        {
            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    Field field = new Field();
                    _fields[i, j] = field;
                    field.FieldDiscovered += Field_FieldDiscovered;
                    MainGrid.Children.Add(field);
                    Grid.SetRow(field, i);
                    Grid.SetColumn(field, j);
                }
            }
        }

        private void Field_FieldDiscovered(object sender)
        {
            _numOfDiscoveredFields++;
            Field field = sender as Field;
            if (field.FieldContent.Content.ToString() == "X") // Field is trap
            {
                DiscoverTraps();
                MessageBox.Show("Losed game :(");
                NewGame();
            }
            else if (field.FieldContent.Content.ToString() == "")
            {
                DiscoverSafeArea(field);
                field.MainButton.Visibility = Visibility.Hidden;
                _numOfDiscoveredFields++;

                if (_numOfDiscoveredFields == 100 - 16)
                {
                    MessageBox.Show("You won!");
                }
            }
        }

        private void DiscoverSafeArea(Field field)
        {
            if (field.LeftField != null && field.LeftField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.LeftField);
            }
            if (field.TopLeftField != null && field.TopLeftField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.TopLeftField);
            }
            if (field.TopField != null && field.TopField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.TopField);
            }
            if (field.TopRightField != null && field.TopRightField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.TopRightField);
            }
            if (field.RightField != null && field.RightField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.RightField);
            }
            if (field.BottomRightField != null && field.BottomRightField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.BottomRightField);
            }
            if (field.BottomField != null && field.BottomField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.BottomField);
            }
            if (field.BottomLeftField != null && field.BottomLeftField.FieldContent.Content.ToString() != "X")
            {
                TryToDiscoverField(field.BottomLeftField);
            }
        }

        private void TryToDiscoverField(Field field)
        {
            if (field == null)
            {
                return;
            }
            if (field.MainButton.Visibility != Visibility.Hidden)
            {
                field.MainButton.Visibility = Visibility.Hidden;
                _numOfDiscoveredFields++;
                if (field.FieldContent.Content.ToString() == "")
                {
                    DiscoverSafeArea(field);
                }
                
            }
        }

        private void DiscoverTraps()
        {
            for (int i = 0; i < _fields.GetLength(0); i++)
            {
                for (int j = 0; j < _fields.GetLength(1); j++)
                {
                    if (_fields[i, j].FieldContent.Content.ToString() == "X")
                    {
                        _fields[i, j].MainButton.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
