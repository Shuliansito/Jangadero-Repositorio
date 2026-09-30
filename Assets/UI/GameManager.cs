using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject consumablePrefab;
    [SerializeField]
    private List<GameObject> lst_consumables=new();
    [SerializeField]
    private GameObject enemyPrefab;
    private List<GameObject> lst_enemies=new();
    private PlayerMovement pm;


    private void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
    }
    private void Start()
    {
        StartGame(1);
        pm = player.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        

    }

    /// <summary>
    /// Start game in given level
    /// </summary>
    /// <param name="level"></param>
    public void StartGame(int level)
    {
        //Starting
        player.transform.position = new Vector3(0f, 1.8f, 0f);

        foreach(GameObject item in lst_consumables)
        {
            Destroy(item);
            
        }
        foreach (GameObject item in lst_enemies)
        {
            Destroy(item);

        }

        lst_consumables.Add(Instantiate(consumablePrefab, new Vector3(1.6f, 1.1f, 7.5f), Quaternion.identity));
        lst_consumables.Add(Instantiate(consumablePrefab, new Vector3(0.5f, 1.1f, 7.5f), Quaternion.identity));


    }
}
