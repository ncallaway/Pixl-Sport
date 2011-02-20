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

namespace Pixl_Sport
{



    class GameManager
    {
        List<Rule> rulesList = new List<Rule>();


        // These variables encompass the gameclock.

        private static int QUARTERTIME = 600000;
        private int time;
        public int Time { get { return time; } set { time = Math.Max(time - value, 0); } }
        public int MinTime { get { return time / 6000; } }
        public int SecTime { get { return (time / 1000) % 60; } }
        private bool running;

        public Team Team1;
        public Team Team2;
        public Field PlaySpace;
        public List<Team> BothTeams = new List<Team>();

        private Texture2D pixels;


        public GameManager() { }

        public void Initialize()
        {
            Team1.Initialize();
            Team2.Initialize();

            Team1.Color = Color.Blue;
            Team2.Color = Color.Red;
        }

        public void Load(ContentManager content)
        {
            pixels = content.Load<Texture2D>("line");
        }


        //This itterates throught the current Rules and checks them. If they are broken it enforces them.
        public void RulesCheck()
        {

            foreach (Rule R in rulesList)
            {
                if (R.Check()) R.Enforce();

            }
        }

        public void Update(GameTime T)
        {
            if (running) Time = T.ElapsedGameTime.Milliseconds;
        }

        public void Draw(GameTime t, SpriteBatch b)
        {
            PlaySpace.Draw(b);

            foreach (TeamMember m in Team1.Members) {
                m.Draw(b, pixels);
            }
        }



    }
}
