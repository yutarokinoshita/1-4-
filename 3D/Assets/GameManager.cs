using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int maxScore = 99999999;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    FirstPersonAIO firstPerson;
    [SerializeField]
    FirstPersonGunController gunCOntroller;
    [SerializeField]
    Text centerText;
    [SerializeField]
    float waitTime = 2;

    [System.NonSerialized]
    public bool gameOver = false;

    int score = 0;

    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);
            scoreText.text = score.ToString("D8");
        }
        get
        {
            return score;
        }
    }
    
    void Start()
    {
        InitGame();
        StartCoroutine(GameStart());
    }

    void InitGame()
    {
        Score = 0;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = true;
        gunCOntroller.shootEnabled = false;
    }

    public IEnumerator GameStart()
    {
        yield return new WaitForSeconds(waitTime);
        centerText.enabled = true;
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "GO!!";
        firstPerson.playerCanMove = true;
        firstPerson.enableCameraMovement = true;
        gunCOntroller.shootEnabled = true;
        yield return new WaitForSeconds(1);
        centerText.text = "";
        centerText.enabled = false;
    }

    public IEnumerator GameOver()
    {
        gameOver = true;
        firstPerson.playerCanMove = false;
        firstPerson.enableCameraMovement = false;
        gunCOntroller.shootEnabled = false;
        centerText.enabled = true;
        centerText.text = "Game Over";
        yield return new WaitForSeconds(waitTime);
        DestroyEnemies();
        centerText.text = "";
        centerText.enabled = false;
        gameOver = false;
    }

    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
