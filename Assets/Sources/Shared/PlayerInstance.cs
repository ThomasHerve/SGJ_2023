using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace Unity.MultiPlayerGame.Shared
{
    public class PlayerInstance : MonoBehaviour
    {

        #region Global player list
        /**
         * GESTION GENERALE DES JOUEURS
         * La liste des joueurs et le nombre de joueurs pr�sents
         * 
         */
        public const int MAX_PLAYER = 4;

        public static List<PlayerInstance> players = new List<PlayerInstance>(Enumerable.Repeat<PlayerInstance>(null, MAX_PLAYER));
        public static int currentPlayerNumber = 0;
        public static bool has2PlayerKeyboard = false;


        public static int AddPlayer(PlayerInstance player)
        {
            if (currentPlayerNumber == MAX_PLAYER)
                return -1;
            players[players.IndexOf(null)] = player;


            currentPlayerNumber++;


            return currentPlayerNumber - 1;
        }


        public static void RemovePlayer(PlayerInstance player)
        {
            if (currentPlayerNumber == 0)
                return;

            players[players.IndexOf(player)] = null;

            currentPlayerNumber--;
            if (player.inputDevice is Keyboard)
            {
                has2PlayerKeyboard = false;
            }

        }

        public static bool AreAllPlayersReady()
        {
            return players.Any(player => player != null) && players.Where(p => p != null).All(p => p.isReady);
        }

        #endregion

        //--------------------------------------------------------------------- //

        #region Player Object
        /**
         * GESTION INDIVIDUELLE DES JOUEURS
         * Les infos capt�es par chacun des joueurs
         * 
         */
        [SerializeField]
        Sprite[] skinList;

        InputDevice inputDevice;
        public InputDevice InputDevice { get => inputDevice; set => inputDevice = value; }

        int number = -1;
        int skin = 0;
        bool isReady = false;

        public void Start()
        {
            inputDevice = GetComponent<PlayerInput>().GetDevice<InputDevice>();

            number = AddPlayer(this);

            if (number == -1)
            {
                Debug.Log("Too much players");
                Destroy(this.gameObject);
                return;

            }

            GameObject placeholder = GameObject.FindGameObjectWithTag("PlayerPlaceHolder" + number);
            this.transform.SetParent(placeholder.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.transform.localScale = new Vector3(1, 1, 1);

            if (inputDevice != null)
                this.transform.Find("SelectionPanel").GetComponentInChildren<TextMeshProUGUI>().text = inputDevice.name;
            else
                this.transform.Find("SelectionPanel").GetComponentInChildren<TextMeshProUGUI>().text = "Keyboard 2";

            Debug.Log("Player added at position " + number);

            GameObject.FindGameObjectWithTag("EventSystem").GetComponent<InputSystemUIInputModule>().actionsAsset.FindAction("Cancel").Disable();

        }

        public void OnPlayerCancel(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (isReady)
            {
                isReady = false;
                return;
            }

            RemovePlayer(this);

            Destroy(gameObject);


        }

        public void OnPlayerSubmit(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            isReady = true;

        }

        public void OnPlayerNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            if (skinList.Length == 0)
                return;
            if (isReady)
                return;

            if (context.ReadValue<Vector2>().x < 0) //gauche
                skin++;
            else //droite
                skin--;

            skin = (skin % skinList.Length + skinList.Length) % skinList.Length;


            this.transform.Find("ImageHolder").GetComponent<Image>().sprite = skinList[skin];
        }

        public void OnPlayerAddOther(InputAction.CallbackContext context)
        {
            if (has2PlayerKeyboard)
                return;

            has2PlayerKeyboard = true;
            GameObject newPlayer = Instantiate(this.gameObject);
            newPlayer.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerUI2");
        }

        public void OnDestroy()
        {
            Destroy(GetComponent<PlayerInput>());

            if (currentPlayerNumber == 0)
                GameObject.FindGameObjectWithTag("EventSystem").GetComponent<InputSystemUIInputModule>().actionsAsset.FindAction("Cancel").Enable();
            
        }

        #endregion
    }
}