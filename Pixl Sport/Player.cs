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

       
        private Team team;
        public TeamMember CurrentCharacter;

        

        public Player(Team team)
        {
            this.team = team;
            CurrentCharacter = team.Members.ElementAt(0);
            input = new InputController(InputController.InputMode.Player1);

        }


        public void Update(GameTime t)
        {
          //  if (!CurrentCharacter.HasBall) foreach (TeamMember TM in team.Members) if(TM.HasBall) CurrentCharacter = TM;
            input.Update();
            if (CurrentCharacter.HasBall && input.IsAButtonNewlyPressed()) CurrentCharacter.Pass(new Vector2(100, 0));
            CurrentCharacter.Position += input.LStickPosition();

        }

        


    }
}
