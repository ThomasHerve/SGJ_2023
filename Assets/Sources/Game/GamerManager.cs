using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private string[] Colorskin = new string[4] { "Rouge", "Orange", "Vert", "Bleu" };

    // Start is called before the first frame update
    void Start()
    {
        // DEBUG
        if (PlayerInstance.currentPlayerNumber == 0)
        {
            PlayerInstance.players[0] = new PlayerInstance();
            PlayerInstance.players[0].InputDevice = Keyboard.current;
            PlayerInstance.players[0].skin = 0;
            PlayerInstance.players[1] = new PlayerInstance();
            PlayerInstance.players[1].InputDevice = Keyboard.current;
            PlayerInstance.players[1].skin = 1;
            PlayerInstance.players[2] = new PlayerInstance();
            PlayerInstance.players[2].InputDevice = Gamepad.current;
            PlayerInstance.players[2].skin = 2;
        }
        

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
                    Debug.Log("Couleur :" + Colorskin[p.skin]);
                    Menu[i].GetComponentInChildren<TextMeshProUGUI>().text = Colorskin[p.skin];
                    if (Colorskin[p.skin] == "Rouge")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    else if (Colorskin[p.skin] == "Orange")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f, 1f);
                    else if (Colorskin[p.skin] == "Vert")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    else if (Colorskin[p.skin] == "Bleu")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
                    gm.players.Add(playerComp);
                playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[p.skin],Colorskin[p.skin]);
                keyboardTaken = true;
            }
            else if (p.InputDevice is Keyboard)
            {
                    Debug.Log("Couleur :" + Colorskin[p.skin]);
                    GameObject player = playerInputManager.JoinPlayer(i, -1, "keyboard", Keyboard.current).gameObject;
                player.GetComponent<PlayerInput>().SwitchCurrentActionMap("PlayerInput2");
                player.GetComponent<CharacterControler2D>().isKeyboard = true;
                playerComp = player.GetComponent<Player>();
                playerComp.menuPlayer = Menu[i];
                    Menu[i].GetComponentInChildren<TextMeshProUGUI>().text = Colorskin[p.skin];
                    if (Colorskin[p.skin] == "Rouge")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    else if (Colorskin[p.skin] == "Orange")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f, 1f);
                    else if (Colorskin[p.skin] == "Vert")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    else if (Colorskin[p.skin] == "Bleu")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
                    playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[p.skin], Colorskin[p.skin]);
                    gm.players.Add(playerComp);
                keyboardTaken = true;
            }
                else
            {
                GameObject player = playerInputManager.JoinPlayer(i, -1, "Gamepad", p.InputDevice).gameObject;
                playerComp = player.GetComponent<Player>();
                    Debug.Log("Couleur :" + Colorskin[p.skin]);
                    Menu[i].GetComponentInChildren<TextMeshProUGUI>().text = Colorskin[p.skin];
                    if (Colorskin[p.skin] == "Rouge")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    else if (Colorskin[p.skin] == "Orange")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 0.5f, 0f, 1f);
                    else if (Colorskin[p.skin] == "Vert")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                    else if (Colorskin[p.skin] == "Bleu")
                        Menu[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
                    playerComp.menuPlayer = Menu[i];
                    playerComp.changeSkin(sprtCorps[p.skin], sprtBras[p.skin], sprtSlowCorps[p.skin], sprtSlowBras[p.skin], Colorskin[p.skin]);
                    gm.players.Add(playerComp);
                }
            i++;
        }
    }

}

