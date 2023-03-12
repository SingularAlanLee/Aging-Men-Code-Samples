using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    GameObject selected1, selected2;
    int card1ID, card2ID;  //check IDs for match
    int randInt, secondRandInt;  //grabbing for shuffling
    int numMatches = 0;
    bool gameOver = false;
    Vector3 firstCardPos, secondCardPos;
    GameObject shuffleCard1, shuffleCard2;  //game objects of to be shuffled cards
    [SerializeField]GameObject notMatchText, titleText, gameOverText, gameCompleteText, instructionText, correctMatchText;
    [SerializeField]List<GameObject> cardParent = new List<GameObject>();

    void Start()
    {
        notMatchText.SetActive(false);
        gameOverText.SetActive(false);
        gameCompleteText.SetActive(false);
        titleText.SetActive(true);
        instructionText.SetActive(true);
        correctMatchText.SetActive(false);
        for(int i = 0; i < 16; i++)
        {
            randInt = Random.Range(0,7);
            secondRandInt = Random.Range(0,7);

            shuffleCard1 = cardParent[randInt];
            shuffleCard2 = cardParent[secondRandInt];

            firstCardPos = shuffleCard1.transform.position;
            secondCardPos = shuffleCard2.transform.position;

            shuffleCard1.transform.position = secondCardPos;
            shuffleCard2.transform.position = firstCardPos;
        }
    }

    void Update()
    {
        if(selected2 != null && selected1 != null)
        {
            card1ID = selected1.GetComponent<cardScript>().GetMatchID();
            card2ID = selected2.GetComponent<cardScript>().GetMatchID();
            if(card1ID != card2ID)
            {
                notMatchText.SetActive(true);
                correctMatchText.SetActive(false);
                selected1.GetComponent<cardScript>().Deselect();
                selected2.GetComponent<cardScript>().Deselect();
                selected1 = null;
                selected2 = null;
            }

            else if(card1ID == card2ID)
            {
                notMatchText.SetActive(false);
                correctMatchText.SetActive(true);
                selected1.GetComponent<cardScript>().MarkMatched();
                selected2.GetComponent<cardScript>().MarkMatched();
                selected1 = null;
                selected2 = null;
                numMatches++;
            }
        }

        if(numMatches == 4)
        {
            gameOverText.SetActive(true);
            gameCompleteText.SetActive(true);
            notMatchText.SetActive(false);
            instructionText.SetActive(false);

            gameOver = true;
            if(gameOver)
            {
                if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
            
        }
    }

    public void AssignSelected(GameObject temp)
    {
        if(selected1 == null)
        {
            selected1 = temp;
        }
        else if (selected2 == null)
        {
            selected2 = temp;
        }
    }
    public void UnassignSelected(GameObject temp)
    {
        if(selected1 == temp)
        {
            selected1 = null;
        }
        else if (selected2 == temp)
        {
            selected2 = null;
        }
    }

    public GameObject GetSelected1()
    {
        return selected1;
    }
    public GameObject GetSelected2()
    {
        return selected2;
    }
}