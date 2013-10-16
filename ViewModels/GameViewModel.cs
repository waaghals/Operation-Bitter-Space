using Hexxagon.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hexxagon.ViewModels
{
    public class HexGridViewModel
    {
        Grid HexGrid;

        public HexGridViewModel()
        {
            HexGrid = new HexagonGrid();
            
        }
    }
}
