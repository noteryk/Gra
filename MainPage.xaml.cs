using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace MauiApp6
{
    public partial class MainPage : ContentPage
    {
        private const int GridSize = 5;
        private Button[,] buttons = new Button[GridSize, GridSize];
        private bool[,] buttonStates = new bool[GridSize, GridSize];

        public MainPage()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            SetupGrid();
            CreateButtons();
            ToggleButtonState(2, 2);
        }

        private void SetupGrid()
        {
            
            for (int row = 0; row < GridSize; row++)
            {
                RowDefinition rowDefinition = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                gameGrid.RowDefinitions.Add(rowDefinition);
            }

            
            for (int col = 0; col < GridSize; col++)
            {
                ColumnDefinition colDefinition = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                gameGrid.ColumnDefinitions.Add(colDefinition);
            }
        }

        private void CreateButtons()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Button button = new Button
                    {
                        BackgroundColor = Colors.White,
                        BorderColor = Colors.Black,
                        BorderWidth = 2
                    };
                    button.Clicked += OnButtonClicked;
                    buttons[row, col] = button;
                    gameGrid.Add(button, col, row);
                }
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            var position = FindButtonPosition(clickedButton);
            if (position.HasValue)
            {
                ToggleButtonAndNeighborsState(position.Value.Item1, position.Value.Item2);
                if (IsGameWon())
                {
                    DisplayAlert("Dobra robota!", "Wygrałeś!", "Pięknie!");
                    
                }
            }
        }

        private void ToggleButtonAndNeighborsState(int row, int col)
        {
            ToggleButtonState(row, col);
            ToggleButtonState(row - 1, col);
            ToggleButtonState(row + 1, col);
            ToggleButtonState(row, col - 1);
            ToggleButtonState(row, col + 1);
        }

        private void ToggleButtonState(int row, int col)
        {
            if (row >= 0 && row < GridSize && col >= 0 && col < GridSize)
            {
                buttonStates[row, col] = !buttonStates[row, col];
                buttons[row, col].BackgroundColor = buttonStates[row, col] ? Colors.DarkGreen : Colors.LightGrey;
            }
        }

        private (int, int)? FindButtonPosition(Button button)
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (buttons[row, col] == button)
                    {
                        return (row, col);
                    }
                }
            }
            return null;
        }

        private bool IsGameWon()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (buttonStates[row, col])
                    {
                        
                        return false;
                    }
                }
            }


            return true;
        }


    }
}