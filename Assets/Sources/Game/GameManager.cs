using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.MultiPlayerGame.Shared;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    [Tooltip("Gameobjects references")]
    [SerializeField]
    GameObject[] obstacles;

    [SerializeField]
    GameObject[] obstacles2;

    GameObject[] currentObstacles;

    [SerializeField]
    Transform[] axisLeft;
    [SerializeField]
    Transform[] axisUp;
    [SerializeField]
    Transform[] axisRight;
    [SerializeField]
    Transform[] axisDown;

    [Tooltip("Game data")]
    [SerializeField]
    float minSpawnTime;
    [SerializeField]
    float maxSpawnTime;

    private GameState gameState;

    public List<Player> players = new List<Player>();
    public GameObject canvas; 
    public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        StartGame();
    }

    public void StartGame()
    {
        currentObstacles = obstacles;
        canvas.transform.Find("EndGame").gameObject.SetActive(false);
        canvas.transform.Find("StartGame").gameObject.SetActive(true);
        players.ForEach(p => p.Reinit());
        for(int i= 0; i< players.Count; i++)
        {
            players[i].transform.position=Spawner.transform.GetChild(i).transform.position;
            players[i].transform.rotation = Spawner.transform.GetChild(i).transform.rotation;
        }
        gameState = GameState.START;
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
        if (players.Where(p => p.vie != 0).Count() <= 1 && gameState != GameState.END)
        {
            EndGame();
            gameState = GameState.END;
        }
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

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
        switch (gameState)
        {
            case GameState.START:
                gameState = GameState.RUNNING;
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].transform.position = Spawner.transform.GetChild(i).transform.position;
                    players[i].transform.rotation = Spawner.transform.GetChild(i).transform.rotation;
                }
                break;
            case GameState.RUNNING:
                GameRun();
                break;
            case GameState.END:
                break;
        }
    }


    private float obstacleSpawnTime = 0;
    private float currentObstacleSpawnTime = 0;
    private void GameRun()
    {
        if (obstacleSpawnTime <= currentObstacleSpawnTime)
        {
            currentObstacleSpawnTime = 0;
            obstacleSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            SpawnObstacle();
        }
        else
        {
            currentObstacleSpawnTime += Time.deltaTime;
        }
    }

    private void SpawnObstacle()
    {
        // Find position
        int side = Random.Range(0, 4);
        Vector2 pos = Vector2.zero;
        Vector2 dest = Vector2.zero;
        switch (side)
        {
            case (0):
                pos = new Vector2(Random.Range(axisLeft[0].position.x, axisLeft[1].position.x), Random.Range(axisLeft[0].position.y, axisLeft[1].position.y));
                dest = new Vector2(Random.Range(axisRight[0].position.x, axisRight[1].position.x), Random.Range(axisRight[0].position.y, axisRight[1].position.y));
                break;
            case (1):
                pos = new Vector2(Random.Range(axisUp[0].position.x, axisUp[1].position.x), Random.Range(axisUp[0].position.y, axisUp[1].position.y));
                dest = new Vector2(Random.Range(axisDown[0].position.x, axisDown[1].position.x), Random.Range(axisDown[0].position.y, axisDown[1].position.y));
                break;
            case (2):
                pos = new Vector2(Random.Range(axisRight[0].position.x, axisRight[1].position.x), Random.Range(axisRight[0].position.y, axisRight[1].position.y));
                dest = new Vector2(Random.Range(axisLeft[0].position.x, axisLeft[1].position.x), Random.Range(axisLeft[0].position.y, axisLeft[1].position.y));
                break;
            case (3):
                pos = new Vector2(Random.Range(axisDown[0].position.x, axisDown[1].position.x), Random.Range(axisDown[0].position.y, axisDown[1].position.y));
                dest = new Vector2(Random.Range(axisUp[0].position.x, axisUp[1].position.x), Random.Range(axisUp[0].position.y, axisUp[1].position.y));
                break;
        }
        Instantiate(currentObstacles[Random.Range(0, obstacles.Length)], pos, Quaternion.identity).GetComponent<Obstacle>().setup(dest - pos);
    }

    public void setObstacles(int id)
    {
        if(id == 1)
        {
            currentObstacles = obstacles;
        } else
        {
            obstacles = obstacles2;
        }
    }

}


enum GameState
{
    START,
    RUNNING,
    END
}