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
    public GameObject[] Menu;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<Sprite> sprtBras;
    [SerializeField]
    private List<Sprite> sprtCorps;
    [SerializeField]
    private List<Sprite> sprtSlowCorps;
    [SerializeField]
    private List<Sprite> sprtSlowBras;
    [SerializeField]
    private GameManager gm;
    private string[] Colorskin = new string[4] { "rouge", "orange", "vert", "bleu" };

    // Start is called before the first frame update
    void Start()
    {
        // DEBUG
        //if (PlayerInstance.players[0] == null)
        //{

        //    PlayerInstance.players[0] = new PlayerInstance();
        //    PlayerInstance.players[0].InputDevice = Keyboard.current;
        //    PlayerInstance.players[1] = new PlayerInstance();
        //    PlayerInstance.players[1].InputDevice = Keyboard.current;
        //    PlayerInstance.players[2] = new PlayerInstance();
        //    PlayerInstance.players[2].InputDevice = Gamepad.current;
        //}
        //

        int i = 0;
        playerInputManager = GameObject.FindGameObjectWithTag("inputManager").GetComponent<PlayerInputManager>();
        bool keyboardTaken = false;
        Player playerComp;
        foreach (PlayerInstance  p in PlayerInstance.players)
        {
            if(p is not null)
            if (p.InputDevice is Keyboard && !keyboardTaken)
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "keyboard", p.InputDevice).gameObject;
                player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerInput");
                player.GetComponent<CharacterControler2D>().isKeyboard = true;
                playerComp = player.GetComponent<Player>();
                playerComp.menuPlayer = Menu[i];
                gm.players.Add(playerComp);
                playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[i],Colorskin[i]);
                keyboardTaken = true;
            }
            else if (p.InputDevice is Keyboard)
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "keyboard", Keyboard.current).gameObject;
                player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerInput2");
                player.GetComponent<CharacterControler2D>().isKeyboard = true;
                playerComp = player.GetComponent<Player>();
                playerComp.menuPlayer = Menu[i];
                    playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[i], Colorskin[i]);
                    gm.players.Add(playerComp);
                keyboardTaken = true;
            }
                else
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "Gamepad", p.InputDevice).gameObject;
                playerComp = player.GetComponent<Player>();
                playerComp.menuPlayer = Menu[i];
                    playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[p.skin], Colorskin[p.skin]);
                    gm.players.Add(playerComp);
                }
            i++;
        }
    }

}

