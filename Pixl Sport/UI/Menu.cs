using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Pixl_Sport;



namespace UserMenus
{
    class Menu
    {
        
        protected List<MenuItem> menuitems = new List<MenuItem>();
        protected MenuItem current;
        protected Player player;
        protected MenuManager parent;
        protected SpriteFont font;
        protected Texture2D image;
        protected int current_index;
        protected int x_position;
        protected int y_position;
        protected int x_spacing;
        protected int y_spacing;
        protected String title;

        public Menu() { }

        public Menu(MenuManager Parent, Player Player)
        {
            parent = Parent;
            player = Player;
            font = parent.Font;
            image = parent.Image;
            current_index = 0;
            x_position = 400;
            y_position = 400;
            y_spacing = 20;
            x_spacing = 0;
            title = "";

        }

        public Menu(MenuManager Parent)
        {
            parent = Parent;
            font = parent.Font;
            image = parent.Image;
            current_index = 0;
            x_position = 200;
            y_position = 200;
            y_spacing = 20;
            x_spacing = 0;
            title = "";
        }


        public void addMenuItem(MenuItem menuitem)
        {
            menuitems.Add(menuitem);

        }

        public void Load(ContentManager CM)
        {

            font = CM.Load<SpriteFont>("Fonts/MenuPixlFont");
            image = CM.Load<Texture2D>("line");

        }

        public virtual void Update(GameTime gametime)
        {
            if (parent.Players.Count > 0) foreach (Player p in parent.Players)
                {
                    if (current == null) current = menuitems.ElementAt(0);


                    p.Input.Update();
                    if (p.Input.IsMoveUpNewlyPressed()|| p.Input.IsDPadUpNewlyPressed()) current_index--;
                    if (p.Input.IsMoveDownNewlyPressed()|| p.Input.IsDPadDownNewlyPressed()) current_index++;
                    if (p.Input.IsBButtonNewlyPressed()) parent.CloseMenu();
                    if (current_index >= menuitems.Count) current_index = 0;
                    if (current_index < 0) current_index = menuitems.Count - 1;
                    current = menuitems.ElementAt(current_index);
                    if (p.Input.IsAButtonNewlyPressed())
                    {
                        p.Input.Update();
                        current.execute();
                    }

                }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(image, new Rectangle(x_position - 5, y_position - 5, 410, y_spacing * (menuitems.Count+1) + 10), Color.Gray);

            batch.Draw(image, new Rectangle(x_position, y_position, 400, y_spacing * (menuitems.Count+1)), Color.Black);

            batch.DrawString(font, title, new Vector2(x_position, y_position), Color.White);
            foreach (MenuItem x in menuitems)
            {
                
                    int i = menuitems.IndexOf(x);
                    if (i == current_index) x.Draw(batch, font, x_position + (i + 1) * x_spacing + 15, y_position + y_spacing * (i + 1));
                    else x.Draw(batch, font, x_position + (i+1) * x_spacing, y_position + y_spacing * (i+1));
            }

        }
    
    
    
    }

    
}
