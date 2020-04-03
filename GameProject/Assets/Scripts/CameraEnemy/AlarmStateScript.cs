using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class Enemy
{
    public GameObject enemyType;
    public Transform spawnLocation;
}
public class AlarmStateScript : MonoBehaviour
{
    public EventTrigger.TriggerEvent customCallback;

    private BoxCollider2D enemyAlertCollider;
    public List<Enemy> enemySpawnList;

    // Start is called before the first frame update
    void Start()
    {

        BaseEventData eventData = new BaseEventData(EventSystem.current);
        //eventData.selectedObject = this.gameObject;

        enemyAlertCollider = gameObject.GetComponent<BoxCollider2D>();
        
        if (enemySpawnList.Count != 0)
        {
            foreach (Enemy enemy in enemySpawnList)
            {
                Instantiate(enemy.enemyType, new Vector3(enemy.spawnLocation.position.x, enemy.spawnLocation.position.y, 0), Quaternion.identity);
            }
        }

        customCallback.Invoke(eventData);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Triggeröi vihollisten Attack-state
            Debug.Log("Vihollinen AlertColliderin sisällä!");
        }
    }
}
