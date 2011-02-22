using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace UserMenus
{
    class CloseMenuButton:MenuItem
    {
        MenuManager manager;

        public CloseMenuButton(MenuManager Manager)
        {
            manager = Manager;
          
            id = "Close Menu";
        }



        public override void execute()
        {   manager.CloseMenu();
        }




    }
}

   