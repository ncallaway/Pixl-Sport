using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pixl_Sport.AI;

namespace Pixl_Sport
{
    class TeamMember
    {
        public const float PLAYER_SPEED = 1f;

        private String name;
        public String Name { get { return name; } }

        private int number;
        public int Number { get { return number; } set { number = Math.Abs(value % 100); } }

        private Team.Position profession;
        public Team.Position Profession { get { return Profession; } set { profession = value; } }

        public bool OnField;
        public bool IsOnFire;
        private float cantCatch;
        private float catchTimer;
        public bool CantCatch { get { return cantCatch >catchTimer ; } set { catchTimer = 0; cantCatch = 500f; } }


        private int numPositionAvgFrames;
        private Vector2 windowVelocity;





       
        public bool Tackling {get {return tackleTimer< tackleRest;}}
        private Vector2 tacklingDirection;
        private float tackleTimer;
        private float tackleMove = 100f;
        private float tackleRest = 300f;

        



        public float TimeWithBall;

        private Vector2 prevPosition;
        private Vector2 position;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 Velocity { get { return windowVelocity / numPositionAvgFrames; } }

        public BoundingBox Bounds {
            get {
                return new BoundingBox(new Vector3(position- 2*Vector2.One, 0f), new Vector3(position + 3*Vector2.One, 2f));
            } 
        }


        private Team team;
        public Team Team { get { return team; } }


        public bool HasBall {get { return HeldBall!=null;}}
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
            passStrength = 5;
            passAccuracy = 5;
            TimeWithBall = 0f;
            numPositionAvgFrames = 0;
            tackleTimer = 501f;
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
            if (CantCatch) catchTimer += t.ElapsedGameTime.Milliseconds;


            if (Tackling)
            {
                tackleTimer += t.ElapsedGameTime.Milliseconds;

                if (tackleTimer < tackleMove) UpdatePosition( 3 * tacklingDirection * TeamMember.PLAYER_SPEED);
            }
            else
            {




                if (!PlayerControlled)
                {
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
        }
            
        

        public void Stun(int time)
        {
            ai.Stun(new TimeSpan(0, 0, 0, 0, time));
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
            
           

            target = new Vector2((float)Math.Cos(deviation)/passAccuracy, (float)Math.Sin(deviation)/passAccuracy);


            HeldBall.SendFlying(target,(float) passStrength / 3f, (float) passStrength / 20f);
           
            HeldBall = null;
            CantCatch = true;
        }

        public void BallMovement(float velocity, float vertical)
        {
            Random rand = new Random();
            int deviation = 0;

            deviation = (int)(rand.NextDouble() * 360) % 360;



            Vector2 target = new Vector2((float)Math.Cos(deviation) , (float)Math.Sin(deviation));


            HeldBall.SendFlying(target, velocity, vertical);

            HeldBall = null;
            CantCatch = true;
        }

        public void Drop()
        {
            Random rand = new Random();
            int deviation = 0;

            deviation = (int)(rand.NextDouble() * 360) % 360;



            Vector2 target = new Vector2((float)Math.Cos(deviation), (float)Math.Sin(deviation));


            HeldBall.SendFlying(target, .2f , .1f);
            
            HeldBall = null;
            CantCatch = true;
        }

        public void UpdatePosition(Vector2 adjustment)
        {   Team opposing;
            if (team.Manager.Team1 == team) opposing = team.Manager.Team2;
            else opposing = team.Manager.Team1;
            bool legal = true;
            foreach (TeamMember TM in opposing.Members)
            {
                if(OnField&& TM.Bounds.Intersects(new BoundingBox( new Vector3(position + adjustment - Vector2.One, 0), new Vector3(position + adjustment + 2* Vector2.One, 2f)))) legal = false;

            }
            foreach (TeamMember TM in team.Members)
            {
                if (TM!= this && OnField && TM.Bounds.Intersects(new BoundingBox(new Vector3(position + adjustment -   Vector2.One, 0), new Vector3(position + adjustment + 2* Vector2.One, 2f)))) legal = false;

            }
            if (legal) position +=adjustment;

        }


        public void Kick(Vector2 target)
        {
            Random rand = new Random();
            int deviation = 0;
            deviation += (int)(rand.NextDouble() * 30) % 60 - 30;
            
          
            target += new Vector2((float)Math.Cos(deviation)/(passAccuracy/3), (float)Math.Sin(deviation)/(passAccuracy/3));

            HeldBall.SendFlying(target, passStrength /2, passStrength/8f);
            
            HeldBall = null;
            CantCatch = true;
        }


        public void GrabBall(Ball ball)
        {
            if (!CantCatch)
            {
                HeldBall = ball;
                

                TimeWithBall = 0f;
                ball.Possessor = this;
                ball.State = Ball.BallState.Held;
            }
        }

        public void Tackle(Vector2 direction)
        {
            tacklingDirection = direction;
            tackleTimer = 0f;
            tacklingDirection.Normalize();


        }

        public void Hit(TeamMember victim)
        {
            victim.ai.Stun(new TimeSpan(0,0,0,0,2000));
            if (victim.HasBall) victim.Drop();
        }





    }
}
