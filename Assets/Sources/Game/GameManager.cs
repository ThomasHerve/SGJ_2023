using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.MultiPlayerGame.Shared;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    List<Player> players = Enumerable.Range(0, PlayerInstance.currentPlayerNumber).Select(i => new Player()).ToList();
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        StartGame();

        //DEBUG A DEGAGER
        players.Add(new Player());
        players.Add(new Player());

    }

    void StartGame()
    {
        canvas.transform.Find("EndGame").gameObject.SetActive(false);
        canvas.transform.Find("StartGame").gameObject.SetActive(true);
        players.ForEach(p => p.vie = 100);

        //canvas.GetComponent<TextSlowWrite>().WriteText(canvas.transform.Find("StartGame").Find("StartText").GetComponent<TextMeshProUGUI>(), "Essaie d'éviter les rayonnements ! Attentions aux mutations ! Bonne chance !");
        StartCoroutine(WaitForStart());

    }
    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1);
        canvas.transform.Find("StartGame").gameObject.SetActive(false);
    }

    void CheckEndGame()
    {
        if(players.Where(p => p.vie != 0).Count() <= 1)
            EndGame();
        
    }


    public void EndGame()
    {
        Player winner = players.FirstOrDefault(p => p.vie > 0);
        if (winner != null)
            winner.score++;

        canvas.transform.Find("EndGame").gameObject.SetActive(true);

        canvas.GetComponent<TextSlowWrite>().WriteText(canvas.transform.Find("EndGame").Find("Winner").GetComponent<TextMeshProUGUI>(), "Astronaute "+winner.color+" a gagné la partie !");

        StringBuilder scoreStr = new StringBuilder();
        foreach (Player player in players.OrderByDescending(p => p.score))
            scoreStr.AppendLine("Astronaute " + player.color + " : " + player.score);
        canvas.GetComponent<TextSlowWrite>().WriteText(canvas.transform.Find("EndGame").Find("Score").GetComponent<TextMeshProUGUI>(), scoreStr.ToString() );

    }
}
