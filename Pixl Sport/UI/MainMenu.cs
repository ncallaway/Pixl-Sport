using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserMenus
{
    class MainMenu: Menu
    {
     public MainMenu(MenuManager Parent)
        {
            parent = Parent;
            font = parent.Font;
            image = parent.Image;
            current_index = 0;
            x_position = 312;
            y_position = 300;
            y_spacing = 20;
            x_spacing = 0;
            title = "PiXL SPORT";
            addMenuItem(new NewGameItem(parent.Manager));


     }





        }





    }

