using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Pixl_Sport;

namespace UserMenus
{
    class NoRunning: Rule
    {
        
            Vector2 position = new Vector2();
            TeamMember current;

          public NoRunning(GameManager Manager, Judgement punishment)
        {
            ruleName = "Stay right where you are";
            manager = Manager;
            assignedJudgement = punishment;
        }

              public NoRunning(GameManager Manager)
        {
            ruleName = "No Running";
            manager = Manager;
        }



          public override void Check()
          {

              if (manager.Ball.Possessor != null && manager.Ball.Possessor != current)
              {
                  current = manager.Ball.Possessor;
                  position = current.Position;

              }
              if (current != null &&current.HasBall && (current.Position - position).Length()>1)
              {
                  Enforce(current.Team, current);
              }
              if (current != null) position = current.Position;

          }
    }
}
