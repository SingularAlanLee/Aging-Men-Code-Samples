using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cardScript : MonoBehaviour
{
    GameObject ITGO;
    [SerializeField]int matchID = 0;
    bool selected, matched, assignedSpawn;
    [SerializeField]GameObject selectIndicator;
    GameManagerScript GMScript;
    [SerializeField] AudioSource cardSFXSource;
    [SerializeField] AudioClip flipSFX;

    void Start()
    {
        //assign random cardmat color
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        GMScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        ITGO = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(!selected || matched)
        {
            selectIndicator.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        if(!matched)
        {
            ITGO.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        ITGO.SetActive(false);
    }

    void OnMouseDown()
    {
        if(!matched)
        {
            cardSFXSource.Play(0);
            if(selected == false)
            {
                selected = true;
                ITGO.SetActive(true);
                GMScript.AssignSelected(gameObject);
                selectIndicator.SetActive(true);
            }

            else
            {
                selected = false;
                ITGO.SetActive(false);
                selectIndicator.SetActive(false);
                if(GMScript.GetSelected1() == this.gameObject)
                {
                    GMScript.UnassignSelected(gameObject);
                }
                else if(GMScript.GetSelected2() == this.gameObject)
                {
                    GMScript.UnassignSelected(gameObject);
                }
            }
        }
    }

    public void Deselect()
    {
        selected = false;
    }

    public void MarkMatched()
    {
        matched = true;
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
    }

    public int GetMatchID()
    {
        return matchID;
    }
}
