using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calendex.Code.ButtonInformation_Namespace
{
    public class ButtonInformations        //Das hier sind nur Infos!!          //HIER WAR EIN FEHLER: Als diese Klasse die base.Button Klasse geerbt hat, habe ich mir die eigenschaften vom den Button genommen und nicht von der eigentlichen Klasse.
    {
        private string _shortdayname;
        public string ShortDayName
        {

            get { return _shortdayname; }

            set
            {
                char[] char_array = value.ToCharArray();
                char[] NEW_char_array = new char[2];
                string NEW_string = string.Empty;

                int i = 0;

                foreach (char item in char_array)
                {
                    if (i == 2)
                    {
                        break;
                    }
                    NEW_char_array[i] += item;
                    i++;
                }


                foreach (char item in NEW_char_array)
                {
                    NEW_string += item;

                }
                _shortdayname = NEW_string;
            }
        }

        public Brush ButtonsBackgroundColor { get; set; }
        public Brush PriorityColors { get; set; }

        public Grid Position { get; set; }

        public int CollumnDefinitions { get; set; }

        public int RowDefinitions { get; set; }

        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }




        public ButtonInformations()
        {

            // Eigentlich könnte ich das hier weglassen, sonst macht es wie immer, es macht Automatisch nur ich zeige den Konstruktor an
            // Lasse dich davon nicht ablenken.
        }


        // Es gibt 3 Methoden die gleich sind zur auswahl
        /// <summary>
        /// Ist für die Infomationen vom UI und die Position im Grid bzw. "KalenderRaster" zuständig
        /// </summary>
        /// <param name="day">Das ist der Tag, normalerweiße wird er nur auf 2 Buchstaben begrentzt und angezeigt wird</param>
        /// <param name="backgroundcolor">Das ist der Hintegrund vom dem "Button"</param>
        /// <param name="prioritycolors"> Diese Farben zeigen die Priorität an Wichtigkeit an, also für die Buttons </param>
        /// <param name="Position"> Mit Position wird damit gemeint an welchen Grid die Ganzen objekte hinzugefügt werden sollen.</param>
        /// <param name="CollumnDefinitions">Position vom Spalten | Fängt von Null an</param>
        /// <param name="RowDefinitions">Position von Zeilen | Fängt von Null an</param>
        //Das Standard
        public void Add(string day, Brush backgroundcolor, Brush prioritycolors, Grid position, int collumnDefinitions, int rowdefinitions)
        {
            ShortDayName = day;
            ButtonsBackgroundColor = backgroundcolor;
            PriorityColors = prioritycolors;
            Position = position;
            CollumnDefinitions = collumnDefinitions;
            RowDefinitions = rowdefinitions;

            ColumnSpan = rowdefinitions;
            RowSpan = rowdefinitions;
        }

        /// <summary>
        /// Ist für die Infomationen vom UI und die Position im Grid bzw. "KalenderRaster" zuständig
        /// </summary>
        /// <param name="Day">Das ist der Tag, normalerweiße wird er nur auf 2 Buchstaben begrentzt und angezeigt wird</param>
        /// <param name="backgroundcolor">Das ist der Hintegrund vom dem "Button"</param>
        /// <param name="prioritycolors"> Diese Farben zeigen die Priorität an Wichtigkeit an, also für die Buttons </param>
        /// <param name="Position"> Mit Position wird damit gemeint an welchen Grid die Ganzen objekte hinzugefügt werden sollen.</param>
        /// <param name="CollumnDefinitions">Position vom Spalten | Fängt von Null an</param>
        /// <param name="RowDefinitions">Position von Zeilen | Fängt von Null an</param>
        /// <param name="ColumnSpan">Wie viele man Spalten auf einmal nimmt | Man zählt normal</param>
        /// <param name="RowSpan">Wie viele man Zeilen auf einmal nimmt | Man zählt normal</param>
        //Die Vollständige + Mit Grid.Span
        public void Add(string day, Brush backgroundcolor, Brush prioritycolors, Grid position, int collumndefinitions, int rowdefinitions, int columnspan, int rowspan)
        {
            ShortDayName = day;
            ButtonsBackgroundColor = backgroundcolor;
            PriorityColors = prioritycolors;
            Position = position;
            CollumnDefinitions = collumndefinitions;
            RowDefinitions = rowdefinitions;

            ColumnSpan = columnspan;
            RowSpan = rowspan;
        }

        /// <summary>
        /// Ist für die Infomationen vom UI und die Position im Grid bzw. "KalenderRaster" zuständig
        /// </summary>
        /// <param name="Day">Das ist der Tag, normalerweiße wird er nur auf 2 Buchstaben begrentzt und angezeigt wird</param>
        /// <param name="backgroundcolor">Das ist der Hintegrund vom dem "Button"</param>
        /// <param name="Position"> Mit Position wird damit gemeint an welchen Grid die Ganzen objekte hinzugefügt werden sollen.</param>
        /// <param name="CollumnDefinitions">Position vom Spalten | Fängt von Null an</param>
        /// <param name="RowDefinitions">Position von Zeilen | Fängt von Null an</param>
        // Die Nur ohne "Prioritätsfarben"
        public void Add(string day, Brush backgroundcolor, Grid position, int collumndefinitions, int rowdefinitions, int columnspan, int rowspan)
        {
            ShortDayName = day;
            ButtonsBackgroundColor = backgroundcolor;
            Position = position;
            CollumnDefinitions = collumndefinitions;
            RowDefinitions = rowdefinitions;

            ColumnSpan = columnspan;
            RowSpan = rowspan;
        }
    }
}