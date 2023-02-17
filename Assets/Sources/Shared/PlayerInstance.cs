using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.MultiPlayerGame.Shared
{ 
    public class PlayerInstance
    {
        #region Global player list
        public const int MAX_PLAYER = 4;
        public static PlayerInstance[] players = new PlayerInstance[MAX_PLAYER];
        public static int currentPlayerNumber = 0;


        public static void AddPlayer(InputDevice id)
        {
            if (currentPlayerNumber == MAX_PLAYER)
                return;

            PlayerInstance newplayer = players.First(p => p == null);
            newplayer.inputDevice = id;

            currentPlayerNumber++;
        }

        public static void RemovePlayer(InputDevice id)
        {
            if (currentPlayerNumber == 0)
                return;

            PlayerInstance removedplayer = players.First(p => p.inputDevice == id);
            removedplayer.inputDevice = null;

            currentPlayerNumber--;
        }

        #endregion

        #region Player Object

        InputDevice inputDevice;
        public InputDevice InputDevice { get => inputDevice; set => inputDevice = value; }







        #endregion
    }
}