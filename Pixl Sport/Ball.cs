using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixl_Sport
{
    class Ball
    {
        private static readonly Color COLOR_BALL = new Color(96, 51, 17);

        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        private float height;
       
        private bool apexReached;
        private float verticalForce;
        public float VerticalF { get { return verticalForce; } }

        public bool Kicked;
        public bool Passed;

        public float h;
        public float Height { get { return h; } set { h = value; } }

        private float hotTimer;
        private float hotTime;
        public bool HotBall { get { return hotTime < hotTimer; } set { hotTime = 0; hotTimer = 500f; } }


        public BoundingBox Bounds
        {
            get
            {
                return new BoundingBox(new Vector3(position - Vector2.One, h), new Vector3(position + 2 * Vector2.One,h + 2f));
            }
        }

        private Vector2 direction;
        public Vector2 Direction { get { return direction; } }


        private float velocity;
        public float Velocity { get { return velocity; } }

        public float TimeWithBall;

        public enum BallState
        {
            Held,
            Flying,
            Bouncing,
            Dead,
            


        }

        public BallState State;
        public TeamMember Possessor;

        public Ball()
        {
            apexReached = false;
            height = 20f;
            h = 20f;
            velocity = -2f;
            
            Bounce();
        }



        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize* (int) (h /5 +1), 2 * (int)scaleSize *(int) (h /5+1));

            batch.Draw(pixels, destination, COLOR_BALL);
        }


        public void Update ( GameTime t)
        {
            hotTime += t.ElapsedGameTime.Milliseconds;
            h = Math.Max(h, 0);
         

            



            switch (State){
                case BallState.Flying:
                    verticalForce -= .01f;
                    h += verticalForce;
                    position += direction*velocity;
                    if (h < 0f) Bounce();





                    break;

                case BallState.Held:
                    position = Possessor.Position;

                    TimeWithBall += t.ElapsedGameTime.Milliseconds;
                    Position = new Vector2(position.X, position.Y - 2);
                    break;
                case BallState.Bouncing:
                        
                    break;

                    
            }

            h = Math.Max(h, 0);
       }

        public void Clear()
        {
            direction = Vector2.Zero;
            apexReached = true;
            height = 3f;
            if (Possessor != null) Possessor.HeldBall = null;
            TimeWithBall = 0f;
            State = BallState.Flying;
            Possessor = null;
            
        }


        public void SendFlying(Vector2 direction, float Velocity, float verticalF)
        {
            verticalForce = verticalF;
            velocity = Velocity;
            this.direction = direction;
            this.direction.Normalize();
            State = BallState.Flying;
        }

        


        public void Bounce()
        {
            Random rand = new Random();
            int deviation = 0;
          
              deviation = (int)(rand.NextDouble() * 270) % 270 - 135;
              verticalForce = Math.Abs(verticalForce / 5f);
              direction += new Vector2((float)Math.Cos(deviation), (float)Math.Sin(deviation));
              direction.Normalize();
              SendFlying(direction, velocity / 2f, verticalForce);
        }

    }
}
