using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChaseAct : MonoBehaviour
{
    public List<GameObject> Monsters;

    [SerializeField] int actNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Monsters.Count; i++)
        {
            Monsters[i].GetComponent<ChasePlayer>().act = actNum;
        }
    }
}
