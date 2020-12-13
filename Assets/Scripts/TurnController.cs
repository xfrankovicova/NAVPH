using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private Dictionary<int, Character> rullers;
    [SerializeField]
    private List<CharacterData> rullersData;
    // Start is called before the first frame update
    private static TurnController _instance;
    public static TurnController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Character GetRuller(int id)
    {
        if (rullers.ContainsKey(id))
        {
            return rullers[id];
        }
        else
        {
            return null;
        }
    }



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Debug.Log(_instance.ToString());
    }

    public void SetUpController() 
    {
        rullers = new Dictionary<int, Character>();
        foreach (var item in rullersData)
        {
            rullers.Add(item.id, new Character(item));
        }
        rullers[currentPlayerId].setPlayer();
    }

    private void Update()
    {
        if (moveDone)
        {
            StartCoroutine(MakeTurns());
        }
    }

    public bool moveDone = true;
    public int moveCount = 0;
    public int currentPlayerId = 1;
    public bool playerMoveDone = false;

    private IEnumerator MakeTurns()
    {
        moveDone = false;
        moveCount++;
        foreach (var item in rullers.Values)
        {
            Debug.Log(item.Data.id.ToString() + ". character turn");
            if (item.Data.id == currentPlayerId)
            {
                playerMoveDone = false;
                yield return new WaitUntil(() => playerMoveDone);
            }
            item.TakeTurn();
        }
        moveDone = true;
    }
}

