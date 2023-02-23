using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.MultiPlayerGame.Shared;

public class Lobby:MonoBehaviour
{
   
    public void OnPlayerJoin(GameObject player)
    {
        InputDevice id = player.GetComponent<PlayerInput>().GetDevice<InputDevice>();



        if (PlayerInstance.currentPlayerNumber >= 2)
        {
            //GameObject.FindGameObjectWithTag("SelectMenu").GetComponent<Menus>().launchText.SetActive(true);
        }
            
    }

    public void OnPlayerLeft(PlayerInstance player)
    {

        if (PlayerInstance.currentPlayerNumber < 2)
        {
            //GameObject.FindGameObjectWithTag("SelectMenu").GetComponent<Menus>().launchText.SetActive(false);
        }

    }

    public void GetCancelAction(InputAction.CallbackContext context)
    {
        PlayerInstance pi = PlayerInstance.players.FirstOrDefault(p => p.InputDevice == context.control.device);
        if (pi != null)
        {
            OnPlayerLeft(pi);
        }
        else
        {
            //GameObject.FindGameObjectWithTag("SelectMenu").GetComponent<Menus>().ReturnToMainScreen();
        }
    }

}
