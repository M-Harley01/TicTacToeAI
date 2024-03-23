using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public interface IScreen
    {
        public void draw();
        public void handleMouse();
    }
}
