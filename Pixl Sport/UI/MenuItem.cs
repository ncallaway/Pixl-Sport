using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UserMenus
{
    class MenuItem
    {
        protected String id;
        public String Id { get { return id; } }
        protected Color color;


        public MenuItem(String ID)
        {
            id = ID;
            color = Color.Black;
        }

        public MenuItem()
        {
            id = "Default ID";

            color = Color.White;

        }


      

        public virtual void execute()
        {
            // Various Implementation things go here.


        }

        public virtual void Draw(SpriteBatch batch, SpriteFont font,  int x_pos, int y_pos)
        {

            batch.DrawString(font, id, new Vector2(x_pos, y_pos), color);

        }


    }
}
