using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixl_Sport;

namespace UserMenus
{
    class NewGameItem: MenuItem
    {

        private GameManager GM;

        public NewGameItem(GameManager gM)
        {
            GM = gM;
            id = "Start New Game";

        }


        public override void execute()
        {
            GM.MenuM.CloseMenu();
            GM.StartGame();

        }

    }
}
