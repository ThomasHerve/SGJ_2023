using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Unity.MultiPlayerGame.Shared
{
    public class PlayerInstance : MonoBehaviour
    {

        #region Global player list
        /**
         * GESTION GENERALE DES JOUEURS
         * La liste des joueurs et le nombre de joueurs présents
         * 
         */
        public const int MAX_PLAYER = 4;
        public static List<PlayerInstance> players = new List<PlayerInstance>(Enumerable.Repeat<PlayerInstance>(null, MAX_PLAYER));
        public static int currentPlayerNumber = 0;


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
        }

        #endregion

        //--------------------------------------------------------------------- //

        #region Player Object
        /**
         * GESTION INDIVIDUELLE DES JOUEURS
         * Les infos captées par chacun des joueurs
         * 
         */
        [SerializeField]
        Sprite[] skinList;

        InputDevice inputDevice;
        public InputDevice InputDevice { get => inputDevice; set => inputDevice = value; }

        int number = -1;
        int skin = 0;
        public void Start()
        {
            Debug.Log("Player Launch");
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
            Debug.Log("Player added at position " + number);

            GetComponent<PlayerInput>().actions.FindAction("PlayerCancel").performed += OnPlayerCancel;
            GetComponent<PlayerInput>().actions.FindAction("PlayerSubmit").performed += OnPlayerSubmit;
            GetComponent<PlayerInput>().actions.FindAction("PlayerNavigate").performed += OnPlayerNavigate;
        }

        public void Update()
        {
        }


        private void OnPlayerCancel(InputAction.CallbackContext context)
        {
            Debug.Log("Player Removal");

            RemovePlayer(this);

            Debug.Log("Player removed at position " + number);

            Destroy(gameObject);

        }

        private void OnPlayerSubmit(InputAction.CallbackContext context)
        {

        }

        public void OnPlayerNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            if (skinList.Length == 0)
                return;

            if (context.ReadValue<Vector2>().x < 0) //gauche
                skin++;
            else //droite
                skin--;

            skin = (skin % skinList.Length + skinList.Length) % skinList.Length;


            this.transform.Find("ImageHolder").GetComponent<Image>().sprite = skinList[skin];
        }

        public void OnDestroy()
        {
            GetComponent<PlayerInput>().actions.FindAction("PlayerNavigate").performed -= OnPlayerNavigate;
            GetComponent<PlayerInput>().actions.FindAction("PlayerCancel").performed -= OnPlayerCancel;
            GetComponent<PlayerInput>().actions.FindAction("PlayerSubmit").performed -= OnPlayerSubmit;
            Destroy(GetComponent<PlayerInput>());

        }

        #endregion
    }
}