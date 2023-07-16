﻿//CalenderBox.cs
//----------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Calendex.Code.ButtonInformation_Namespace;

namespace Calendex.Code.CalenderBox_Namespace
{
    public class CalenderBox : List            // Die Eigenschaften sind : Farbe, TagesAbkürzung, und die 3 Grünen Punkte an Priorität.
    {
        public int YearNumber = 365;


        public List<string> DaysBoxList = new List<string>();       // ist überflüssig, wenn du dir sicher bist, dass du es nicht brauchen wirst, dann kannst du es löschen
        public Button[] MyButtonFeld = new Button[365];      // Ich soll nicht nur auf diesen Array achten sondern auch bei der Schleife
        public Grid KPositionFeld;
        public Grid MyKalenderRasterGrid { get; set; }

        public int ProduktGrid;

        public CalenderBox()
        {
        }
        public CalenderBox(Grid MyRaster)
        {
            MyKalenderRasterGrid = MyRaster;
        }


        public void GenerateStarterButtons(string Day, Brush backgroundcolor, Brush prioritycolors, Grid KPosition)  // Das sind die Buttons, die an anfang generieren werden.
        {
            // PriorityMIniButtons ist in einem anderem Namespace


            KPositionFeld = KPosition;
            ButtonInformations DefaultButtonInfoGUI = new ButtonInformations();       // Für jeden anderen brauche ich ein ButtonIformatons klasse
            ButtonInformations ImportantButtonInfoGUI = new ButtonInformations();
            ImportantButtonInfoGUI.ButtonsBackgroundColor = prioritycolors;
            DefaultButtonInfoGUI.ButtonsBackgroundColor = backgroundcolor;



            //MEHR INFO ÜBER DIE SCHLEIFE IM NOTEPAD++ AB ZEILE 45.

            int KalenderRasterColumn = MyKalenderRasterGrid.ColumnDefinitions.Count;
            int KalenderRasterRow = MyKalenderRasterGrid.RowDefinitions.Count;
            ProduktGrid = KalenderRasterColumn * KalenderRasterRow;

            int ColumnGrid = KalenderRasterColumn;
            int RowGrid = KalenderRasterRow;

            MyButtonFeld = new Button[YearNumber];     // Max.Value == 1 Year
            


            int ColumnGridPointer = 0;
            int RowGridPointer = 0;

            int tempIForDays = 0;
            int TempJForAddingI = 0;
            int OldI = 0;

            for (int i = 0; i < MyButtonFeld.Length; i++)          // Ich soll nicht nur auf diesen Array achten sondern auch bei der Schleife
            {                                                      // Nach jedem ende sollte ein neues Button erstellt werden
                if (i > ProduktGrid) { break; }


                if (ColumnGridPointer %ColumnGrid == 0 && ColumnGridPointer != 0)
                {
                    ColumnGridPointer = 0; // Auf Nächste Spalte setzen
                    RowGridPointer++;

                    for (int j = 0; j < ColumnGrid; j++)
                    {
                        DefaultButtonInfoGUI.Add(Day, backgroundcolor, prioritycolors, KPosition, ColumnGridPointer, RowGridPointer);

                        int AddedIndex = i + 0;
                        int AdvancedAddedIndex = i + j;

                        if (j != 0)
                        {
                            MyButtonFeld[AdvancedAddedIndex] = new Button();

                            // Für die Abkürzungen verändern (Notepad++ Zeile 50-54)

                            MyButtonFeld[AdvancedAddedIndex].Content = CalenderDays((i) + j);
                            MyButtonFeld[AdvancedAddedIndex].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;

                            Grid.SetColumn(MyButtonFeld[AdvancedAddedIndex], ColumnGridPointer);
                            Grid.SetRow(MyButtonFeld[AdvancedAddedIndex], RowGridPointer);

                            KPosition.Children.Add(MyButtonFeld[AdvancedAddedIndex]);
                        }
                        else        // Diese bedienung passiert nur einmal
                        {
                            MyButtonFeld[AddedIndex] = new Button();

                            // Für die Abkürzungen verändern (Notepad++ Zeile 50-54)

                            MyButtonFeld[AddedIndex].Content = CalenderDays((i - tempIForDays) + j);
                            MyButtonFeld[AddedIndex].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;

                            Grid.SetColumn(MyButtonFeld[AddedIndex], ColumnGridPointer);
                            Grid.SetRow(MyButtonFeld[AddedIndex], RowGridPointer);

                            KPosition.Children.Add(MyButtonFeld[AddedIndex]);
                        }

                        ColumnGridPointer++;
                        tempIForDays++;
                        TempJForAddingI++;
                        //i++;
                    }
                    ColumnGridPointer = 0;
                    OldI = i;
                }
                if (OldI == i)
                {
                    i += TempJForAddingI; 
                    TempJForAddingI = 0;
                    
                    // wegen dem zweiten überspringt es zu viel, ich soll ein alten i nehmen und dann wenn der alte und der neue i nicht gleich sind, dann setze ich das zweite zurück auf 0
                    // ES kann sein, dass ich hier warscheinlich noch ein weiteres modulo nutzen muss.
                }
                else
                {
                    TempJForAddingI = 0;
                }
                

                MyButtonFeld[i] = new Button();
                MyButtonFeld[i].Content = CalenderDays(i);
                MyButtonFeld[i].Background = DefaultButtonInfoGUI.ButtonsBackgroundColor;       // ACHTE DAS BACKGROUND NICHT NULL IST.

                Grid.SetColumn(MyButtonFeld[i], ColumnGridPointer);
                Grid.SetRow(MyButtonFeld[i], RowGridPointer);

                KPosition.Children.Add(MyButtonFeld[i]);

                ColumnGridPointer++;
            }



            Debug.WriteLine("Dynamische Klassen wurden erstellt und hinzugefügt.");



        }
        public string CalenderDays(int day)     // Das hier ist eine Stadard variante, nichts fürs überarbeiten, leider auch statisch.
        {
            string[] DaysArray = new string[365];
            short Jahr = 2023;


            for (int JanuarI = 0; JanuarI < 31; JanuarI++)
            {
                DaysArray[JanuarI] = $"{JanuarI + 1}. Januar | {Jahr}";
            }



            for (int FebruarI = 31; FebruarI <= 59; FebruarI++)
            {
                DaysArray[FebruarI] = $"{FebruarI + 1 - 31}. Februar | {Jahr}";
            }



            for (int MärzI = 59; MärzI <= 90; MärzI++)
            {
                DaysArray[MärzI] = $"{MärzI + 1 - 59}. März | {Jahr}";
            }



            for (int AprilI = 90; AprilI <= 120; AprilI++)
            {
                DaysArray[AprilI] = $"{AprilI + 1 - 90}. April | {Jahr}";
            }



            for (int MaiI = 120; MaiI <= 151; MaiI++)
            {
                DaysArray[MaiI] = $"{MaiI + 1 - 120}. Mai | {Jahr}";
            }



            for (int JuniI = 151; JuniI <= 180; JuniI++)
            {
                DaysArray[JuniI] = $"{JuniI + 1 - 151}. Juni {Jahr}";
            }



            for (int JuliI = 181; JuliI <= 212; JuliI++)
            {
                DaysArray[JuliI] = $"{JuliI - 180}. Juli | {Jahr}";
            }



            for (int AugustI = 212; AugustI <= 242; AugustI++)
            {
                DaysArray[AugustI] = $"{AugustI - 210}. August | {Jahr}";
            }



            for (int SeptemberI = 242; SeptemberI <= 272; SeptemberI++)
            {
                DaysArray[SeptemberI] = $"{SeptemberI - 241}. September | {Jahr}";
            }



            for (int OktoberI = 272; OktoberI <= 302; OktoberI++)
            {
                DaysArray[OktoberI] = $"{OktoberI - 271}. Oktober | {Jahr}";
            }



            for (int NovemberI = 303; NovemberI <= 333; NovemberI++)
            {
                DaysArray[NovemberI] = $"{NovemberI - 302}. November | {Jahr}";
            }



            for (int DezemberI = 333; DezemberI <= 364; DezemberI++)
            {
                DaysArray[DezemberI] = $"{DezemberI - 333}. Dezember | {Jahr}";
            }



            bool Canemidatleyreturn = false;

            if (!Canemidatleyreturn)
            {
                for (int i = 0; i < DaysArray.Length; i++)
                {
                    if (DaysArray[i] != null)
                    {
                        Canemidatleyreturn = true;
                        break;
                    }


                }
            }
            return DaysArray[day];



        }

    }

}