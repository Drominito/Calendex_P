using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Calendex.Code.PriorityMiniButtons_Namespace
{
    internal class PriorityMiniButton
    {
        public void PriorityButtons(Brush prioritycolors)
        {
            Rectangle PriorityRectangle = new Rectangle();
            PriorityRectangle.MaxWidth = 35;
            PriorityRectangle.Width = 10;
            PriorityRectangle.Fill = prioritycolors;
        }
    }
}