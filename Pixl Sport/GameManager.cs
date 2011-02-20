﻿using System;
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
        public void StopClock() { running = false;}

        public Team Team1;
        public Team Team2;
        public List<Team> BothTeams = new List<Team>();
        

        /* Team Variables Should go here.

        List<Players?> Example!

        End of Team Variables          */





        public GameManager() {}


        //This itterates throught the current Rules and checks them. If they are broken it enforces them.
        public void RulesCheck()
        {

            foreach (Rule R in rulesList)
            {
                R.Check();

            }
        }

        public void Update(GameTime T)
        {
            if (running) Time = T.ElapsedGameTime.Milliseconds;



        }



    }
}
