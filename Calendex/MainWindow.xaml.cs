//MainWindow.xaml.cs
//--------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

using Calendex.Code.ButtonInformation_Namespace;
using Calendex.Code.PriorityMiniButtons_Namespace;
using Calendex.Code.CalenderBox_Namespace;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Calendex
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RowDefinition    newRow;
        ColumnDefinition newColumn;

        public DispatcherTimer timer = new DispatcherTimer();
        public CalenderBox cb;
        public int ActualCubeNumber;
        public int IsStateChanged = 2;                 // Stell dir nicht vor das es geändert wurde, sondern ob der Fenster Maximiert wurde und ist.
        public short YearNumber = 365;
        public int GridAmount;

        public int TempPoints = 0;
        public Brush RedBrush;
        public Brush GreenBrush;
        public Brush NormalBrush;
        public MainWindow()
        {
            bool ManuallyActivating = false;
            if (ManuallyActivating)
            {
                double FastRefresh = 1 / Math.Pow(4, 20);
                int LowRefresh = 20;
                // Ein knopf für Spaß, aber am besten sollte ich da dann
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(LowRefresh);
                timer.Tick += Timer_Tick;
                timer.Start();
            }



            InitializeComponent();

            //Extra Grids für Testing
            //newRow = new RowDefinition();
            //newColumn = new ColumnDefinition();
            //newColumn.Width = new GridLength(1, GridUnitType.Star);
            //newRow.Height = new GridLength(1, GridUnitType.Star);

            cb = new CalenderBox(KalenderRaster);
            ActualCubeNumber = cb.ProduktGrid;

            //"ADDBUTTONSUI" SIND ALLE BUTTONS, ICH MUSS DANN SPÄTER MACHEN, DASS MAN HIER DIE COLUMNS UND ROWS HIER EINSTELLT UND HIER EINE SCHLEIFE MACHT.
            RedBrush = new SolidColorBrush(Colors.Red);               //Very Important Day
            GreenBrush = new SolidColorBrush(Colors.Green);           //Important Day    
            NormalBrush = new SolidColorBrush(Colors.White);
            //Weiß ist neutral und die Buttons sind automatisch immer Weiß.

            cb.GenerateStarterButtons("Monday", NormalBrush, GreenBrush, KalenderRaster);

            MainWindowAttribute.StateChanged += MainWindowAttribute_StateChanged;
            cb.MyButtonFeld[0].SizeChanged += MyButtonSizeChanged;

            int ColumnAmount = KalenderRaster.ColumnDefinitions.Count;
            int RowAmount = KalenderRaster.RowDefinitions.Count;
            GridAmount = ColumnAmount * RowAmount;

            for (int i = 0; i <= cb.ProduktGrid; i++) { cb.MyButtonFeld[i].Click += ControllTheGrid; }










           

            //  Warscheinlich überflüssig und ändert nichts, aber ich könnte mich vielleicht täuschen.

            //if (cb.KPositionFeld.Children.Count > GridAmount)
            //{
            //    Grid.SetRow(KalenderRaster, cb.KPositionFeld.Children.Count);
            //}


            //cb.DaysBoxList.Add("TAG", "HINTERGRUND", "PRIORITÄTSFARBEN");
        }

        private void MainWindowAttribute_StateChanged(object sender, EventArgs e)
        {
            WindowInfoLabel.FontSize = 45;
            IsStateChanged = 1;
        }

        private void MyButtonSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //string StringFormat = $"{cb.MyButtonFeld.}";
            //ButtonsInfoLabel.Content = StringFormat;
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            RainBow_Scroll(sender, new ScrollEventArgs(ScrollEventType.First, 3));
            //ControllTheGrid(sender, new RoutedEventArgs());
        }

        public void ControllTheGrid(object sender, RoutedEventArgs e)
        {

            SelectedIndexUI3.Content = sender.ToString();   //Mein Größter ERROR
            string EventMessage = Convert.ToString(SelectedIndexUI3.Content);
            string DoneEventMessage = "";
            for (int i = 32; i < 49; i++)
            {
                try
                {
                    DoneEventMessage += EventMessage[i];
                }
                catch
                {
                    DoneEventMessage = "";
                    try
                    {
                        for (int k = 32; k < 48; k++)
                        {
                            DoneEventMessage += EventMessage[k];
                        }

                    }
                    catch
                    {
                        DoneEventMessage = "";

                        try
                        {
                            for (int k = 32; k < 45; k++)
                            {
                                DoneEventMessage += EventMessage[k];        // Nur 14
                            }
                        }
                        catch
                        {
                            DoneEventMessage = "";

                            try
                            {

                                for (int k = 32; k < 44; k++)
                                {
                                    DoneEventMessage += EventMessage[k];        // Nur 14
                                }
                            }
                            catch
                            {
                                DoneEventMessage = "";
                                for (int k = 0; k < 8; k++)
                                {
                                    DoneEventMessage += EventMessage[k];        // Nur 14
                                }
                            }
                        }
                    }
                }

            }

            SelectedIndexUI3.Content = DoneEventMessage; // Kannst kommentar löschen

        }

        private void UpdateLoop(string Day, Brush backgroundcolor, Brush prioritycolors, Grid KPosition, bool IfShortSize)
        {

            cb.KPositionFeld = KPosition;
            ButtonInformations DefaultButtonInfoGUI = new ButtonInformations();       // Für jeden anderen brauche ich ein ButtonIformatons klasse
            ButtonInformations ImportantButtonInfoGUI = new ButtonInformations();
            ImportantButtonInfoGUI.ButtonsBackgroundColor = prioritycolors;



            //MEHR INFO ÜBER DIE SCHLEIFE IM NOTEPAD++ AB ZEILE 45.

            int KalenderRasterColumn = KalenderRaster.ColumnDefinitions.Count;
            int KalenderRasterRow = KalenderRaster.RowDefinitions.Count;
            cb.ProduktGrid = KalenderRasterColumn * KalenderRasterRow;


            int ColumnGrid = KalenderRasterColumn;
            int RowGrid = KalenderRasterRow;

            int ColumnGridPointer = 0;
            int RowGridPointer = 0;
            int secondJ = 0;
            
            if (!IfShortSize)
            {
                for (int i = 0; i < cb.MyButtonFeld.Length; i++)          // Ich soll nicht nur auf diesen Array achten sondern auch bei der Schleife
                {                                                      // Nach jedem ende sollte ein neues Button erstellt werden

                    if (i > cb.ProduktGrid) { break; }
                   


                    if (ColumnGridPointer %ColumnGrid == 0 && ColumnGridPointer != 0)
                    {
                        ColumnGridPointer = 0; // Auf Nächste Spalte setzen
                        RowGridPointer++;

                        for (int j = 0; j < ColumnGrid; j++)
                        {
                            DefaultButtonInfoGUI.Add(Day, backgroundcolor, prioritycolors, KPosition, ColumnGridPointer, RowGridPointer);

                            cb.MyButtonFeld[i] = new Button();

                            // Für die Abkürzungen verändern (Notepad++ Zeile 50-54)
                            
                            
                            
                            
                            
                                cb.MyButtonFeld[i].Content = cb.CalenderDays(i + j);
                            

                            cb.MyButtonFeld[i].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;

                            Grid.SetColumn(cb.MyButtonFeld[i], ColumnGridPointer);
                            Grid.SetRow(cb.MyButtonFeld[i], RowGridPointer);

                            KPosition.Children.Add(cb.MyButtonFeld[i]);

                            ColumnGridPointer++;
                            secondJ = j;
                        }
                        i = i + secondJ;
                        ColumnGridPointer = 0;
                    }

                    cb.MyButtonFeld[i] = new Button();
                    cb.MyButtonFeld[i].Content = cb.CalenderDays(i);
                    cb.MyButtonFeld[i].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;       // ACHTE DAS BACKGROUND NICHT NULL IST.

                    Grid.SetColumn(cb.MyButtonFeld[i], ColumnGridPointer);
                    Grid.SetRow(cb.MyButtonFeld[i], RowGridPointer);

                    KPosition.Children.Add(cb.MyButtonFeld[i]);

                    ColumnGridPointer++;
                }
            }
            else
            {
                for (int i = 0; i < cb.MyButtonFeld.Length; i++)          // Ich soll nicht nur auf diesen Array achten sondern auch bei der Schleife
                {                                                      // Nach jedem ende sollte ein neues Button erstellt werden

                    if (i > cb.ProduktGrid) { break; }


                    if (ColumnGridPointer %ColumnGrid == 0 && ColumnGridPointer != 0)
                    {
                        ColumnGridPointer = 0; // Auf Nächste Spalte setzen
                        RowGridPointer++;

                        for (int j = 0; j < ColumnGrid; j++)
                        {
                            DefaultButtonInfoGUI.Add(Day, backgroundcolor, prioritycolors, KPosition, ColumnGridPointer, RowGridPointer);

                            cb.MyButtonFeld[i] = new Button();
                            if (cb.MyButtonFeld[i].Width < 880)
                            {
                                cb.MyButtonFeld[i].Content = $"{DefaultButtonInfoGUI.ShortDayName}-{i}";
                            }
                            else
                            {
                                cb.MyButtonFeld[i].Content = cb.CalenderDays(i);      // Für die Abkürzungen verändern (Notepad++ Zeile 50-54)
                            }
                            cb.MyButtonFeld[i].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;

                            Grid.SetColumn(cb.MyButtonFeld[i], ColumnGridPointer);
                            Grid.SetRow(cb.MyButtonFeld[i], RowGridPointer);

                            KPosition.Children.Add(cb.MyButtonFeld[i]);

                            ColumnGridPointer++;
                        }
                        ColumnGridPointer = 0;
                    }

                    cb.MyButtonFeld[i] = new Button();
                    cb.MyButtonFeld[i].Content = cb.CalenderDays(i);
                    cb.MyButtonFeld[i].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;       // ACHTE DAS BACKGROUND NICHT NULL IST.

                    Grid.SetColumn(cb.MyButtonFeld[i], ColumnGridPointer);
                    Grid.SetRow(cb.MyButtonFeld[i], RowGridPointer);

                    KPosition.Children.Add(cb.MyButtonFeld[i]);

                    ColumnGridPointer++;
                }
            }

        }

        private void RainBow_Scroll(object sender, ScrollEventArgs e)
        {
            bool ManuallyActivating = true;
            double FastRefresh = 1 / Math.Pow(4, 20);
            int LowRefresh = 10;
            timer.Interval = TimeSpan.FromSeconds(FastRefresh);

            if (ManuallyActivating)
            {
                Random rnd = new Random();
                var BlueBrush = new SolidColorBrush(Colors.Blue);
                var RedBrush = new SolidColorBrush(Colors.Red);
                var GreenBrush = new SolidColorBrush(Colors.Green);
                var BlackBrush = new SolidColorBrush(Colors.Black);
                var YellowBrush = new SolidColorBrush(Colors.Yellow);
                double RandomNumber = rnd.Next(cb.MyButtonFeld.Length - 4);     // Leider geht es nur bis zu [362] irgendwo in der Schleife bei der erstellung im Raster ist was falsch, oder in der "Tages"-Methode

                for (int i = 0; i < cb.MyButtonFeld.Length + 2; i++)
                {

                    int Choosing = rnd.Next(10);




                    switch (Choosing)
                    {
                        case 1: { cb.MyButtonFeld[(int)RandomNumber].Background = BlueBrush; } break;
                        case 2: { cb.MyButtonFeld[(int)RandomNumber].Background = RedBrush; } break;
                        case 3: { cb.MyButtonFeld[(int)RandomNumber].Background = GreenBrush; } break;
                        case 4: { cb.MyButtonFeld[(int)RandomNumber].Background = BlackBrush; } break;
                        case 5: { cb.MyButtonFeld[(int)RandomNumber].Background = YellowBrush; } break;
                    }

                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ButtonInformations DefaultButtonInfoGUI = new ButtonInformations();       // Für jeden anderen brauche ich ein ButtonIformatons klasse
            DefaultButtonInfoGUI.ShortDayName = "Monday";
            string[] MonthsArray = new string[12];
            MonthsArray[0] = "Januar";
            MonthsArray[1] = "Februar";
            MonthsArray[2] = "März";
            MonthsArray[3] = "April";
            MonthsArray[4] = "Mai";
            MonthsArray[5] = "Juni";
            MonthsArray[6] = "Juli";
            MonthsArray[7] = "August";
            MonthsArray[8] = "September";
            MonthsArray[9] = "Oktober";
            MonthsArray[10] = "November";
            MonthsArray[11] = "Dezember";

            IsStateChanged++;
            if (IsStateChanged % 2 != 0)
            {
                string StringFormat = $"Width {MainWindowAttribute.ActualWidth} | Height {MainWindowAttribute.ActualHeight}";
                double ProduktWindowActualSize = MainWindowAttribute.ActualWidth * MainWindowAttribute.ActualHeight;
                double ProduktWindowMaxSize = MainWindowAttribute.MaxWidth * MainWindowAttribute.MaxHeight;
                WindowInfoLabel.Content = StringFormat;


                if (MainWindowAttribute.ActualWidth >= ProduktWindowMaxSize)
                {
                    WindowInfoLabel.FontSize = 15;
                }
                else
                {
                    WindowInfoLabel.FontSize = ProduktWindowActualSize / 20000;
                }
            }
            // Wird aber nur bei jeden zweiten pixel aktualisiert


            for (int i = 0; i < cb.ProduktGrid; i++)
            {
                if (MainWindowAttribute.Width < 880 && i %7 == 0)
                {
                    //switch cb.MyButtonFeld[i].Content = $"{i => i; i %2 == 0}";
                    //jeder block ein dazugehörigen monat, hab da kein bock mehr
                    cb.MyButtonFeld[i].Content = i;
                }
                else
                {
                    cb.MyButtonFeld[i].Content = cb.CalenderDays(i);

                }
            }
            
        }

        private void GridOptionButton_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void GridOptionButton_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void MinusOptionButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnDefinition newColumn1 = new ColumnDefinition();
            RowDefinition newRow1 = new RowDefinition();

            newColumn1.Width = new GridLength(1, GridUnitType.Star);
            newRow1.Height = new GridLength(1, GridUnitType.Star);

            


           KalenderRaster.ColumnDefinitions.RemoveAt(1);
           KalenderRaster.RowDefinitions.RemoveAt(1);


            double ActualNormalNumber = Math.Sqrt(ActualCubeNumber);
            ActualNormalNumber++;
            double CubeNumber = Math.Pow(ActualNormalNumber, 2);
            for (int i = cb.ProduktGrid; i > 0; i--) { cb.MyButtonFeld[i].Click -= ControllTheGrid; }

            UpdateLoop("Monday", NormalBrush, GreenBrush, KalenderRaster, false);

            for (int i = 0; i < cb.ProduktGrid; i++) { cb.MyButtonFeld[i].Click += ControllTheGrid; }

        }


        private void PlusOptionButton_Click(object sender, RoutedEventArgs e)
        {
            TempPoints++;
            ColumnDefinition newColumn1 = new ColumnDefinition();
            RowDefinition newRow1 = new RowDefinition();

            newColumn1.Width = new GridLength(1, GridUnitType.Star);
            newRow1.Height = new GridLength(1, GridUnitType.Star);

            KalenderRaster.ColumnDefinitions.Add(newColumn1);
            KalenderRaster.RowDefinitions.Add(newRow1);


            double ActualNormalNumber = Math.Sqrt(ActualCubeNumber);
            ActualNormalNumber++;
            double CubeNumber = Math.Pow(ActualNormalNumber, 2);
            // Es muss doch nichts nochmal entfernent weil es nichts zum enfernen gibt, und die Abonnierungs methode macht kein unterschied zum welchen es gehört

            UpdateLoop("Monday", NormalBrush, GreenBrush, KalenderRaster, false);

            for (int i = 0; i < cb.ProduktGrid; i++)
            {
                if (cb.MyButtonFeld[i] == null)
                {
                    cb.MyButtonFeld[i].Click += ControllTheGrid;
                }
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            double ActualNormalNumber = Math.Sqrt(ActualCubeNumber);
            ActualNormalNumber++;
            double CubeNumber = Math.Pow(ActualNormalNumber, 2);

            int KalenderRasterColumn = KalenderRaster.ColumnDefinitions.Count;
            int KalenderRasterRow    = KalenderRaster.RowDefinitions.Count;
            cb.ProduktGrid = KalenderRasterColumn * KalenderRasterRow;

            for (int i = cb.ProduktGrid; i > 0; i--) { cb.MyButtonFeld[i].Click -= ControllTheGrid; }

            UpdateLoop("Monday", NormalBrush, GreenBrush, KalenderRaster, false);

            for (int i = 0; i < cb.ProduktGrid; i++) { cb.MyButtonFeld[i].Click += ControllTheGrid; }
        }
    }


}