using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmSpawner : MonoBehaviour
{
	//생성할 몬스터 프리팹을 받는 변수들
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	public GameObject enemy4;

	//랜덤하게 몬스터를 꺼내기 위한 변수와 리스트
	List<GameObject> monsters = new List<GameObject>();

	//스포너의 위치를 저장하는 변수
	private Vector3 spawnerPos;

	//총 몇 마리까지 스폰시킬건지 최대치를 정하는 변수
	private int monsterCount;

	//다시 몬스터를 스폰시키는데 걸리는 시간을 정하는 변수
	public float respawnTime;

	// Start is called before the first frame update
	void Start()
	{
		//자신의 위치를 저장
		spawnerPos = transform.position;

		//각 변수마다 오브젝트가 있는지 확인하여 있을 경우 리스트에 추가
		if (enemy1 != null)
		{
			monsters.Add(enemy1);
		}

		if (enemy2 != null)
		{
			monsters.Add(enemy2);
		}

		if (enemy3 != null)
		{
			monsters.Add(enemy3);
		}

		if (enemy4 != null)
		{
			monsters.Add(enemy4);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			StartCoroutine(SpawnMonster());
		}
	}

	IEnumerator SpawnMonster()
	{
		monsterCount = Random.Range(1, 13);

		for (int i = 0; i < monsterCount; i++)
		{
			int a = Random.Range(0, 4);

			int xPos = Random.Range(-5, 5); int zPos = Random.Range(-5, 5);
			GameObject child = Instantiate(monsters[a], new Vector3(spawnerPos.x + xPos, spawnerPos.y, spawnerPos.z + zPos), Quaternion.identity);
			child.transform.SetParent(transform);
		}

		yield return new WaitForFixedUpdate();
		//Destroy(gameObject);
	}
}
