using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pixl_Sport;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

namespace UserMenus
{
    class TeamMenu:Menu
    {
        private Team team;
        

        public TeamMenu(Team Team, MenuManager Parent)
        {
            team = Team;
            //foreach (Player p in Parent.Players) if (p.CurrentCharacter.Team == team) Players.Add(p);
            foreach (TeamMember TM in team.Members) addMenuItem(new TMemberItem(TM));

            parent = Parent;
            current_index = 0;
            x_position = 20;
            y_position = 110;
            y_spacing = 20;
            x_spacing = 0;
        }


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {   
            batch.Draw(image, new Rectangle(x_position-5, y_position-5, 360, y_spacing*(menuitems.Count + 1)+10), Color.Gray);

            batch.Draw(image, new Rectangle(x_position, y_position, 350, y_spacing * (menuitems.Count + 1)), Color.Black);

            batch.DrawString(font, team.TeamName, new Vector2(x_position, y_position), team.Color);
            
            foreach (MenuItem x in menuitems)
            {
                
                    int i = menuitems.IndexOf(x) + 1;
                    x.Draw(batch, font, x_position + i * x_spacing, y_position + y_spacing * i);
            }

        }



    }
}
