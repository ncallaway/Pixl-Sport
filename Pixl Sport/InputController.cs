using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace Pixl_Sport
{
    class InputController
    {
        public enum InputMode
        {
            Player1,
            Player2
        };

        public const int NUM_SUPPORTED_PLAYERS = 2;

        private InputMode m_Mode;
        private KeyboardState m_oldState;
        private KeyboardState m_newState;

        private GamePadState[] m_oldControllerState;
        private GamePadState[] m_newControllerState;



        public InputController(InputMode mode)
        {
            m_Mode = mode;
            m_oldState = new KeyboardState();
            m_newState = new KeyboardState();

            m_oldControllerState = new GamePadState[NUM_SUPPORTED_PLAYERS];
            m_newControllerState = new GamePadState[NUM_SUPPORTED_PLAYERS];

        }

        public InputController()
        {
            m_Mode = InputMode.Player1;
            m_oldState = new KeyboardState();
            m_newState = new KeyboardState();

            m_oldControllerState = new GamePadState[NUM_SUPPORTED_PLAYERS];
            m_newControllerState = new GamePadState[NUM_SUPPORTED_PLAYERS];

        }

        public string getMode()
        {
            switch (m_Mode)
            {

                case InputMode.Player1:
                    return "Player1";
                case InputMode.Player2:
                    return "Player2";

                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }

        }

        public bool IsMoveUpPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return (m_newState.IsKeyDown(Keys.W) || m_newControllerState[0].ThumbSticks.Left.Y > 0);
                case InputMode.Player2:
                    return (m_newState.IsKeyDown(Keys.Up) || m_newControllerState[1].ThumbSticks.Left.Y > 0);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsMoveUpNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return ((m_newState.IsKeyDown(Keys.W) && m_oldState.IsKeyUp(Keys.W)) || m_newControllerState[0].ThumbSticks.Left.Y > 0 && m_oldControllerState[0].ThumbSticks.Left.Y == 0);
                case InputMode.Player2:
                    return ((m_newState.IsKeyDown(Keys.Up) && m_oldState.IsKeyUp(Keys.Up)) || m_newControllerState[1].ThumbSticks.Left.Y > 0 && m_oldControllerState[1].ThumbSticks.Left.Y == 0);
                    ;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsMoveDownPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return (m_newState.IsKeyDown(Keys.S) || m_newControllerState[0].ThumbSticks.Left.Y < 0);
                case InputMode.Player2:
                    return (m_newState.IsKeyDown(Keys.Down) || m_newControllerState[1].ThumbSticks.Left.Y < 0);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }


        public bool IsMoveDownNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return ((m_newState.IsKeyDown(Keys.S) && m_oldState.IsKeyUp(Keys.S)) || m_newControllerState[0].ThumbSticks.Left.Y < 0 && m_oldControllerState[0].ThumbSticks.Left.Y == 0);
                case InputMode.Player2:
                    return m_newState.IsKeyDown(Keys.Down) && m_oldState.IsKeyUp(Keys.Down) || (m_newControllerState[1].ThumbSticks.Left.Y < 0 && m_oldControllerState[1].ThumbSticks.Left.Y == 0);
                    ;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsMoveLeftPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return (m_newState.IsKeyDown(Keys.A) || m_newControllerState[0].ThumbSticks.Left.X < 0);
                case InputMode.Player2:
                    return (m_newState.IsKeyDown(Keys.Left) || m_newControllerState[1].ThumbSticks.Left.X < 0);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsMoveRightPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return (m_newState.IsKeyDown(Keys.D) || m_newControllerState[0].ThumbSticks.Left.X > 0);
                case InputMode.Player2:
                    return (m_newState.IsKeyDown(Keys.Right) || m_newControllerState[1].ThumbSticks.Left.X > 0);

                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }

        }

       

        public bool IsYButtonNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return m_newControllerState[0].IsButtonDown(Buttons.Y) && m_oldControllerState[0].IsButtonUp(Buttons.Y);
                case InputMode.Player2:
                    return m_newControllerState[1].IsButtonDown(Buttons.Y) && m_oldControllerState[1].IsButtonUp(Buttons.Y);
                default:

                    throw new NotImplementedException("Receeved unexpected input");
            }
        }

    

        public bool IsBButtonNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.D1) && m_oldState.IsKeyDown(Keys.D1);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.B) && m_oldControllerState[0].IsButtonDown(Buttons.B);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.NumPad7) && m_oldState.IsKeyDown(Keys.NumPad7)) || (m_newControllerState[1].IsButtonUp(Buttons.B) && m_oldControllerState[1].IsButtonDown(Buttons.B));
                    ;
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }


        public bool IsDPadLeftNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.D1) && m_oldState.IsKeyDown(Keys.D1);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.DPadLeft) && m_oldControllerState[0].IsButtonDown(Buttons.DPadLeft);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.NumPad7) && m_oldState.IsKeyDown(Keys.NumPad7)) || (m_newControllerState[1].IsButtonUp(Buttons.DPadLeft) && m_oldControllerState[1].IsButtonDown(Buttons.DPadLeft));
                    ;
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }


        public bool IsDPadRightNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.D1) && m_oldState.IsKeyDown(Keys.D1);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.DPadRight) && m_oldControllerState[0].IsButtonDown(Buttons.DPadRight);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.NumPad7) && m_oldState.IsKeyDown(Keys.NumPad7)) || (m_newControllerState[1].IsButtonUp(Buttons.DPadRight) && m_oldControllerState[1].IsButtonDown(Buttons.DPadRight));
                    ;
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }


        public bool IsDPadUpNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.D1) && m_oldState.IsKeyDown(Keys.D1);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.DPadUp) && m_oldControllerState[0].IsButtonDown(Buttons.DPadUp);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyUp(Keys.NumPad7) && m_oldState.IsKeyDown(Keys.NumPad7) || m_newControllerState[1].IsButtonUp(Buttons.DPadUp) && m_oldControllerState[1].IsButtonDown(Buttons.DPadUp);
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }

        public bool IsRightBumperNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.Tab) && m_oldState.IsKeyDown(Keys.Tab);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.RightShoulder) && m_oldControllerState[0].IsButtonDown(Buttons.RightShoulder);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.NumPad9) && m_oldState.IsKeyDown(Keys.NumPad9) || m_newControllerState[1].IsButtonUp(Buttons.RightShoulder) && m_oldControllerState[1].IsButtonDown(Buttons.RightShoulder));
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }


        public bool IsLeftBumperNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.CapsLock) && m_oldState.IsKeyDown(Keys.CapsLock);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.LeftShoulder) && m_oldControllerState[0].IsButtonDown(Buttons.LeftShoulder);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyUp(Keys.NumPad3) && m_oldState.IsKeyDown(Keys.NumPad3) || m_newControllerState[1].IsButtonUp(Buttons.LeftShoulder) && m_oldControllerState[1].IsButtonDown(Buttons.LeftShoulder);
                default:
                    throw new NotImplementedException("Receeved unexpected input");
            }

        }


        public bool IsXButtonNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.D2) && m_oldState.IsKeyDown(Keys.D2);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.X) && m_oldControllerState[0].IsButtonDown(Buttons.X);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyDown(Keys.Multiply) && m_oldState.IsKeyUp(Keys.Multiply) || m_newControllerState[1].IsButtonUp(Buttons.X) && m_oldControllerState[1].IsButtonDown(Buttons.X);
                default:
                    throw new NotImplementedException("Receeved unexpected input");

            }
        }

        public bool IsAButtonNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.F) && m_oldState.IsKeyDown(Keys.F);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonDown(Buttons.A) && m_oldControllerState[0].IsButtonUp(Buttons.A);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyDown(Keys.Subtract) && m_oldState.IsKeyUp(Keys.Subtract) || m_newControllerState[1].IsButtonDown(Buttons.A) && m_oldControllerState[1].IsButtonUp(Buttons.A);
                    ;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsAButtonNewlyReleased()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyDown(Keys.F) && m_oldState.IsKeyUp(Keys.F);
                    bool controllerNewlyReleased = m_newControllerState[0].IsButtonUp(Buttons.A) && m_oldControllerState[0].IsButtonUp(Buttons.A);
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyUp(Keys.Subtract) && m_oldState.IsKeyDown(Keys.Subtract) || m_newControllerState[1].IsButtonUp(Buttons.A) && m_oldControllerState[1].IsButtonDown(Buttons.A);
                    ;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }




        public bool IsAltFirePressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    System.Console.WriteLine("Controller right: " + m_newControllerState[0].Triggers.Right);
                    return m_newState.IsKeyDown(Keys.Space) || m_newControllerState[0].Triggers.Left > .8;
                case InputMode.Player2:
                    return m_newState.IsKeyDown(Keys.NumPad0) || m_newControllerState[1].Triggers.Left > .8;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }


        public bool IsFirePressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    System.Console.WriteLine("Controller right: " + m_newControllerState[0].Triggers.Right);
                    return m_newState.IsKeyDown(Keys.Space) || m_newControllerState[0].Triggers.Right > .8;
                case InputMode.Player2:
                    return m_newState.IsKeyDown(Keys.NumPad0);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }
        public bool IsAltFireNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyPressed = m_newState.IsKeyDown(Keys.Space) && m_oldState.IsKeyUp(Keys.Space);
                    bool controllerNewlyPressed = m_newControllerState[0].Triggers.Left > .8 && m_oldControllerState[0].Triggers.Left < .8;
                    return keyboardNewlyPressed || controllerNewlyPressed;
                case InputMode.Player2:
                    return (m_newState.IsKeyDown(Keys.NumPad0) && m_oldState.IsKeyUp(Keys.NumPad0)) || (m_newControllerState[1].Triggers.Left > .8 && m_oldControllerState[1].Triggers.Left < .8);
                    ;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }


        public bool IsFireNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyPressed = m_newState.IsKeyDown(Keys.Space) && m_oldState.IsKeyUp(Keys.Space);
                    bool controllerNewlyPressed = m_newControllerState[0].Triggers.Right > .8 && m_oldControllerState[0].Triggers.Right < .8;
                    return keyboardNewlyPressed || controllerNewlyPressed;
                case InputMode.Player2:
                    return ((m_newState.IsKeyDown(Keys.NumPad0) && m_oldState.IsKeyUp(Keys.NumPad0)) || m_newControllerState[1].Triggers.Right > .8 && m_oldControllerState[1].Triggers.Right < .8);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }
        public bool IsFireReleased()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.Space) && m_oldState.IsKeyDown(Keys.Space);
                    bool controllerNewlyReleased = m_newControllerState[0].Triggers.Right < .8;
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.NumPad0) && m_oldState.IsKeyDown(Keys.NumPad0)) || m_newControllerState[1].Triggers.Right < .8;
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }


        public bool IsAltFireReleased()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    bool keyboardNewlyReleased = m_newState.IsKeyUp(Keys.Space) && m_oldState.IsKeyDown(Keys.Space);
                    bool controllerNewlyReleased = m_newControllerState[0].Triggers.Left < .8;
                    return keyboardNewlyReleased || controllerNewlyReleased;
                case InputMode.Player2:
                    return m_newState.IsKeyUp(Keys.NumPad0) && m_oldState.IsKeyDown(Keys.NumPad0);
                default:
                    throw new NotImplementedException("Received unexpected input mode");
            }
        }

        public bool IsPauseMenuNewlyPressed()
        {
            switch (m_Mode)
            {
                case InputMode.Player1:
                    return (m_newState.IsKeyUp(Keys.Escape) && m_oldState.IsKeyDown(Keys.Escape)) || (m_newControllerState[0].IsButtonUp(Buttons.Start) && m_oldControllerState[0].IsButtonDown(Buttons.Start));
                case InputMode.Player2:
                    return (m_newState.IsKeyUp(Keys.End) && m_oldState.IsKeyDown(Keys.End)) || (m_newControllerState[1].IsButtonUp(Buttons.Start) && m_oldControllerState[1].IsButtonDown(Buttons.Start));
                default:
                    throw new NotImplementedException("Receeved unexpected input");

            }
        }


        public void Update()
        {
            m_oldState = m_newState;
            m_newState = Keyboard.GetState();

            m_oldControllerState[0] = m_newControllerState[0];
            m_oldControllerState[1] = m_newControllerState[1];

            m_newControllerState[0] = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
            m_newControllerState[1] = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Two);




        }

        public Vector2 LStickPosition()
        {

            switch (m_Mode)
            {
                case InputMode.Player1:
                    return new Vector2(m_newControllerState[0].ThumbSticks.Left.X, -m_newControllerState[0].ThumbSticks.Left.Y);
                case InputMode.Player2:
                    return new Vector2(m_newControllerState[1].ThumbSticks.Left.X, -m_newControllerState[1].ThumbSticks.Left.Y);
                default:
                    throw new NotImplementedException("Receeved unexpected input");


            }
        }

    }


}

