using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport
{
    class PlayerAI
    {
        public enum Instruction
        {
            /* NEUTRAL ACTIONS! */
            AcquireBall, /* NO DATA */
            CheckPlayer, /* TeamMember */

            /* DEFENSIVE ACTIONS! */
            DefendArea, /* Position, Radius */
            DefendPlayer, /* TeamMember */

            /* OFFENSIVE ACTIONS! */
            GetOpen /* Position, Radius */
        }

        public enum Action
        {
            NoAction,
            MoveNearPosition,
            MoveNearTeamMember,
            MoveToPosition,
            MoveToTeamMember,
            MoveToBall,

            CreateClearLineBetweenPlayer,
            CreateClearLineBetweenGoal,

            Wait,
        }

        public Instruction Goal { get { return instruction; }
            set
            {
                if (instruction != value) {
                    action = Action.NoAction;
                }
                instruction = value;
            }
        }

        public PlayerAI(TeamMember player)
        {
            this.player = player;
            instruction = Instruction.GetOpen;
            action = Action.NoAction;

            parentAi = player.Team.AI;
        }

        private Instruction instruction;
        private Ball instructionBall;
        private Vector2 instructionPosition;
        private float instructionRadius;
        private TeamMember instructionTeamMember;

        public Ball InstructionBall { get { return instructionBall; } set { instructionBall = value; } } 
        public Vector2 InstructionPosition { get { return instructionPosition; } set { instructionPosition = value; }}
        public float InstructionRadius { get { return instructionRadius; } set { instructionRadius = value; } }
        public TeamMember InstructionTeamMember { get { return instructionTeamMember; } set { instructionTeamMember = value; } }

        private Action action;
        private Ball actionBall;
        private Vector2 actionPosition;
        private float actionRadius;
        private TeamMember actionTeamMember;

        private TeamAI parentAi;

        private TeamMember player;

        public void Update(GameTime t)
        {
            if (evaluateCurrentAction()) {
                setActionForInstruction();
            }

            performAction(t);
        }

        private bool evaluateCurrentAction()
        {
            if (action == Action.NoAction || action == Action.Wait) {
                return true;
            }

            return false;
        }

        private void setActionForInstruction()
        {
            switch (instruction) {
                case Instruction.AcquireBall:
                    selectActionForAcquireBall();
                    break;
                case Instruction.GetOpen:
                    action = Action.Wait;
                    break;
            }
        }

        private void selectActionForAcquireBall()
        {
            action = Action.MoveToBall;
            actionBall = instructionBall;
        }

        private void performAction(GameTime t)
        {
            switch (action) {
                case Action.MoveToBall:
                    performMoveToBall();
                    break;
                case Action.NoAction:
                case Action.Wait:
                default:
                    performWait();
                    break;
            }
        }

        private void performMoveToBall()
        {
            /* Get Vector between us and ball! */
            Vector2 direction = actionBall.Position - player.Position;
            direction.Normalize();

            direction *= TeamMember.PLAYER_SPEED;

            player.Position += direction;
        }

        private void performWait()
        {
            return;
        }
    }
}
