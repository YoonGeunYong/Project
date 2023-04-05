using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public enum MonsterStates { Patrol, Notice, Attack }

	public static MonsterStates currentState;

    void Start()
	{
        currentState = MonsterStates.Patrol;
    }

	void Update()
	{
		
	}
}
