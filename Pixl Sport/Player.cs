using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace Pixl_Sport
{
    class Player
    {
        private InputController input;
        public InputController Input {get{ return input;}}

       
        private Team team;
        private TeamMember currentCharacter;
        public TeamMember CurrentCharacter { get { return currentCharacter; }
            set {
                if (currentCharacter != null) {
                    currentCharacter.PlayerControlled = false;
                }
                currentCharacter = value;
                if (currentCharacter != null) {
                    currentCharacter.PlayerControlled = true;
                }
            } 
        }

        

        public Player(Team team)
        {
            this.team = team;
            CurrentCharacter = team.Members.ElementAt(0);
            input = new InputController(InputController.InputMode.Player1);

        }

        public Player(Team team, InputController.InputMode Input)
        {
            input = new InputController(InputController.InputMode.Player2);
            this.team = team;
            CurrentCharacter = team.Members.ElementAt(0);

        }


        public void Update(GameTime t)
        {input.Update();


            if (!CurrentCharacter.HasBall) foreach (TeamMember TM in team.Members) if(TM.HasBall) CurrentCharacter = TM;
            
            if (input.RStickPosition().Length() >= .8 && !CurrentCharacter.Tackling) if (!CurrentCharacter.HasBall) CurrentCharacter.Tackle(input.RStickPosition());
            else if (input.IsFirePressed()) CurrentCharacter.Kick(input.RStickPosition()); else CurrentCharacter.Pass(input.RStickPosition());
            if(!CurrentCharacter.Tackling) CurrentCharacter.UpdatePosition(input.LStickPosition() * TeamMember.PLAYER_SPEED);
            if (input.IsPauseMenuNewlyPressed()) team.Manager.MenuM.Pause(team); 

        }

        


    }
}
