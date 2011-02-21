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
        public const float PLAYER_SPEED = 1f;

        private String name;
        public String Name { get { return name; } }

        private int number;
        public int Number { get { return number; } set { number = Math.Abs(value % 100); } }

        private String profession;
        public String Profession { get { return Profession; } }

        public bool OnField;
        public bool IsOnFire;


        private int numPositionAvgFrames;
        private Vector2 windowVelocity;


        public float TimeWithBall;

        private Vector2 prevPosition;
        private Vector2 position;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Velocity { get { return windowVelocity / numPositionAvgFrames; } }

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

        private float passStrength;
        private float passAccuracy;

        private PlayerAI ai;
        public PlayerAI AI { get { return ai; } }

        public TeamMember(Team team)
        {
            this.team = team;
            ai = new PlayerAI(this);
            passStrength = 10;
            passAccuracy = 10;
            TimeWithBall = 0f;
            numPositionAvgFrames = 0;
        }

        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {   
            
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 3 * (int)scaleSize, 3 * (int)scaleSize);

            if (IsOnFire) batch.Draw(pixels, destination, Color.Orange);
            else batch.Draw(pixels, destination, team.Color);
        }

        public void Update(GameTime t)
        {
           
            if (!PlayerControlled) {
               ai.Update(t);
            }

            trackMovingAverage();

            if (HasBall)
            {
                TimeWithBall += t.ElapsedGameTime.Milliseconds;
                HeldBall.Position = new Vector2(position.X, position.Y - 2);
            }
            prevPosition = this.Position;
        }

        private void trackMovingAverage()
        {
            const int WINDOW_AVG_SIZE = 10;

            Vector2 delta = Position - prevPosition;

            if (numPositionAvgFrames < WINDOW_AVG_SIZE) {
                windowVelocity += delta;
                numPositionAvgFrames++;
            } else {
                windowVelocity -= (windowVelocity / numPositionAvgFrames);
                windowVelocity += delta;
            }
        }

        public void Pass(TeamMember TM)
        {
            Pass(TM.position);
        }


        public void Pass(Vector2 target)
        {
            Random rand = new Random();
            int deviation = 0;
           
                deviation = (int)(rand.NextDouble() * 30) % 30 - 15;
            
           

            target += new Vector2((float)Math.Cos(deviation)/passAccuracy, (float)Math.Sin(deviation)/passAccuracy);


            HeldBall.SendFlying(target,(float) passStrength / 7.5f, (float) passStrength / 10f);
            HasBall = false;
            HeldBall = null;
        }

        public void Kick(Vector2 target)
        {
            Random rand = new Random();
            int deviation = 0;
            deviation += (int)(rand.NextDouble() * 30) % 60 - 30;
            
          
            target += new Vector2((float)Math.Cos(deviation)/(passAccuracy/3), (float)Math.Sin(deviation)/(passAccuracy/3));

            HeldBall.SendFlying(target, passStrength / 5, passStrength/5);
            HasBall = false;
            HeldBall = null;
        }


        public void GrabBall(Ball ball)
        {
            HeldBall = ball;
            HasBall = true;

            TimeWithBall = 0f;
            ball.Possessor = this;
            ball.State = Ball.BallState.Held;
        }






    }
}
