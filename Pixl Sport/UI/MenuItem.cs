using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserMenus
{
    class MenuItem
    {
        protected String id;


        public MenuItem(String Id)
        {
            id = Id;
        }

        public MenuItem()
        {
            id = "Default ID";
        }


        public String getId()
        {
            if (id == null) return "Null ID";
            else return id;
        }

        public virtual void execute()
        {
            // Various Implementation things go here.


        }


    }
}
