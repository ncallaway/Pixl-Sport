using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport
{
    class PlayerAI
    {
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

        public TeamAI.Instruction Instruction { get { return instruction; }
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
            instruction = TeamAI.Instruction.GetOpen;
            action = Action.NoAction;

            parentAi = player.Team.AI;
        }

        private TeamAI.Instruction instruction;
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
                case TeamAI.Instruction.AcquireBall:
                    selectActionForAcquireBall();
                    break;
                case TeamAI.Instruction.DefendArea:
                    selectActionForDefendArea();
                    break;
                case TeamAI.Instruction.GetOpen:
                    action = Action.Wait;
                    break;
            }
        }

        private void selectActionForAcquireBall()
        {
            if (player.HasBall && player.HeldBall == instructionBall) {
                /* GOT IT! */
                action = Action.Wait;
            }

            action = Action.MoveToBall;
            actionBall = instructionBall;
        }

        private void selectActionForDefendArea()
        {
            if (player.Position == actionPosition) {
                /* GOT IT! */
                action = Action.Wait;
            }

            action = Action.MoveToPosition;
            actionPosition = instructionPosition;
        }

        private void performAction(GameTime t)
        {
            switch (action) {
                case Action.MoveToBall:
                    performMoveToBall();
                    break;
                case Action.MoveToPosition:
                    performMoveToPosition();
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
            if (player.HasBall && player.HeldBall == actionBall) {
                /* ACTION ACCOMPLISHED! */
                action = Action.Wait;
                return;
            }

            /* Get Vector between us and ball! */
            Vector2 direction = actionBall.Position - player.Position;

            if (direction.LengthSquared() < TeamMember.PLAYER_SPEED * TeamMember.PLAYER_SPEED) {
                player.Position = actionBall.Position;
                return;
            }

            direction.Normalize();

            direction *= TeamMember.PLAYER_SPEED;

            player.Position += direction;
        }

        private void performMoveToPosition()
        {
            if (player.Position == actionPosition) {
                /* ACTION ACCOMPLISHED! */
                action = Action.Wait;
                return;
            }

            /* Get Vector between us and ball! */
            Vector2 direction = actionPosition - player.Position;

            if (direction.LengthSquared() < TeamMember.PLAYER_SPEED * TeamMember.PLAYER_SPEED) {
                player.Position = actionPosition;
                return;
            }

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
