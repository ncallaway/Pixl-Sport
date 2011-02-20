using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixl_Sport
{
    class TeamMember
    {
        private String name;
        public String Name { get { return name; } }

        private String profession;
        public String Profession { get { return Profession; } }

        public bool OnField;

        private Vector2 position;
        public Vector2 Position { get { return position; } }

        private BoundingBox Bounds {
            get {
                return new BoundingBox(new Vector3(position, 0f), new Vector3(position + Vector2.One, 0f));
            } 
        }


        private Team team;
        public Team Team { get { return team; } }



        public TeamMember(Team team)
        {
            this.team = team;
        }

        public void Draw(SpriteBatch batch, Texture2D pixels)
        {
            Rectangle destination = new Rectangle((int)position.X, (int)position.Y, 2, 2);
            batch.Draw(pixels, destination, team.Color);
        }


    }
}
