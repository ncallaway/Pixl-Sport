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
        private float arcApex;
        private bool apexReached;


        public BoundingBox Bounds
        {
            get
            {
                return new BoundingBox(new Vector3(position - Vector2.One, height), new Vector3(position + 2 * Vector2.One,height = 2f));
            }
        }

        private Vector2 direction;
        public Vector2 Direction { get { return direction; } }


        private float velocity;

        public enum BallState
        {
            Held,
            Flying,
            Bouncing,
            Dead


        }

        public BallState State;
        public TeamMember Possessor;

        public Ball()
        {
            height = 0f;
            Random rand = new Random(5);
            direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
            direction.Normalize();
            velocity = 1f;
            arcApex = 2f;
            State = BallState.Flying;
        }



        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize, 2 * (int)scaleSize);

            batch.Draw(pixels, destination, COLOR_BALL);
        }


        public void Update ( GameTime t)
        {
           



            switch (State){
                case BallState.Flying:
                    //position += direction*velocity;
                    if (!apexReached)
                    {
                        //height+=.4f;
                        if (height > arcApex) apexReached = true;
                    }
                    height -=.2f;
                    
                    if(height <= 1f) 
                    {
                    //State = BallState.Bouncing;
                    Random rand = new Random();
                    direction = new Vector2((float)rand.NextDouble(), (float)(rand.NextDouble()));
                    direction.Normalize();
                    SendFlying(direction, velocity / 2, arcApex / 2);
                   }
                    break;
                case BallState.Held:
                    break;
                case BallState.Bouncing:
                    //position += direction * velocity;
                    if (!apexReached)
                    {
                        height++;
                        if (height > arcApex) apexReached = true;
                    }
                    if (apexReached) height--;
                    if (height <= 1f)
                    {
                        State = BallState.Bouncing;
                        Random rand = new Random();
                        direction = new Vector2((float)rand.NextDouble(), (float)(rand.NextDouble()));
                        direction.Normalize();
                        velocity /= 2;
                        apexReached = false;
                        arcApex /= 2;
                        if (velocity <= .5f) State = BallState.Dead;
                    }
                    
                    break;

            
            }
       }

        public void Clear()
        {
            direction = Vector2.Zero;
            apexReached = false;
            height = .2f;

        }


        public void SendFlying(Vector2 direction,float strength, float apex)
        {
            height = 2.1f;
            arcApex = 3;
            velocity = strength/3;
            this.direction = direction;
            this.direction.Normalize();

            

            State = BallState.Flying;
        }



    }
}
