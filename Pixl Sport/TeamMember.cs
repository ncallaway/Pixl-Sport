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

        private int number;
        public int Number { get { return number; } set { number = Math.Abs(value % 100); } }

        private String profession;
        public String Profession { get { return Profession; } }

        public bool OnField;

        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }

        public BoundingBox Bounds {
            get {
                return new BoundingBox(new Vector3(position- Vector2.One, 0f), new Vector3(position + 2*Vector2.One, 2f));
            } 
        }


        private Team team;
        public Team Team { get { return team; } }


        public bool HasBall = false;
        public bool PlayerControlled = false;

        public Ball HeldBall;

        private int passStrength;
        private int passAccuracy;





        public TeamMember(Team team)
        {
            this.team = team;
            passStrength = 10;
            passAccuracy = 10;
        }

        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize, 2 * (int)scaleSize);

            batch.Draw(pixels, destination, team.Color);
        }

        public void Update(GameTime t)
        {
            
            if(!PlayerControlled) PlayerControlled = false;   /// PlaceMarker for AI Calls!

            if (HasBall) HeldBall.Position = new Vector2(position.X, position.Y - 2);
         }

        public void Pass(TeamMember TM)
        {
            Pass(TM.position);
        }


        public void Pass(Vector2 target)
        {   
            HeldBall.SendFlying(target, passStrength/2, passStrength/2);
            HasBall = false;
            HeldBall = null;
        }

        public void GrabBall(Ball ball)
        {
            HeldBall = ball;
            HasBall = true;
            ball.Clear();

            ball.Possessor = this;
            ball.State = Ball.BallState.Held;
        }






    }
}
