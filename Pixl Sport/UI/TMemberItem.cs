using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace UserMenus
{
    class TMemberItem: MenuItem
    {
        TeamMember subject;

        public TMemberItem(TeamMember Subject)
        {
            subject = Subject;
            id = subject.Name;
            color = subject.Team.Color;

        }
        



        public override void Draw(SpriteBatch batch, SpriteFont font, int xpos, int ypos)
        {
            if(subject.OnField)batch.DrawString(font, subject.Name + "  " + subject.Number + "  " + subject.Profession.ToString(), new Vector2(xpos, ypos), subject.Team.Color);
            else batch.DrawString(font, subject.Name + "  " + subject.Number + "  " + subject.Profession.ToString(), new Vector2(xpos, ypos), Color.White);
           

        }


    }
}
