using System.Collections;
using System.Collections.Generic;
using Unity.MultiPlayerGame.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    [SerializeField]
    private InputActionAsset keyboard1;
    [SerializeField]
    private InputActionAsset keyboard2;
    // Start is called before the first frame update
    void Start()
    {
        // DEBUG
        if (PlayerInstance.players[0] == null)
        {

            PlayerInstance.players[0] = new PlayerInstance();
            PlayerInstance.players[0].InputDevice = Keyboard.current;
            //PlayerInstance.players[1] = new PlayerInstance();
            //PlayerInstance.players[1].InputDevice = Keyboard.current;
        }
        //

        int i = 0;
        playerInputManager = GameObject.FindGameObjectWithTag("inputManager").GetComponent<PlayerInputManager>();
        bool keyboardTaken = false;
        foreach (PlayerInstance  p in PlayerInstance.players)
        {
            if(p is not null)
            if (p.InputDevice is Keyboard && !keyboardTaken)
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "Keyboard", p.InputDevice).gameObject;
                player.GetComponent<PlayerInput>().actions = keyboard1;
                keyboardTaken = true;
            }
            else if (p.InputDevice is Keyboard)
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "Keyboard", p.InputDevice).gameObject;
                player.GetComponent<PlayerInput>().actions = keyboard2;
            }
            else
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "Gamepad", p.InputDevice).gameObject;
            }
            i++;
        }
    }

}

