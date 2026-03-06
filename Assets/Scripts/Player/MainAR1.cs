using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainAR1 : MonoBehaviour
{
	public static MainAR1 instance;

	public Transform ShootPos;
	public LineRenderer Tracer;

	private float shootDelay = 5f;
	float delay = 0;

	public GameObject[] aims = new GameObject[4];
	public GameObject gunModel; //총 모델

	Vector3 gunDefaultPos;  //반동의 영향을 받지 않은 기본 위치    
	bool isRebound = false; //반동의 영향을 받는 중인가
	float rebound = 3f;

	public ParticleSystem Muzzle;

	public GameObject bulletHole;
	public GameObject BloodEffect;

	public AudioSource shootSound;

	public bool shootable;

	public bool isShooting;

	private void Awake()
	{
		instance = this;

		shootable = true;

		Tracer.startWidth = 0.025f;
		Tracer.endWidth = 0.01f;
		Tracer.startColor = Color.red;
		Tracer.endColor = Color.red;

		gunDefaultPos = gunModel.transform.localPosition;   //기본 위치 초기화

		isShooting = false;
	}

	private void FixedUpdate()
	{
		RaycastHit hit;

		if(Input.GetMouseButtonUp(0))
			isShooting = false;

		if (shootable)
		{
			if(Input.GetMouseButton(0))
			{
				if (delay <= 0)
				{
					isShooting = true;

					delay = shootDelay;

					Vector2 reboundRay = Random.insideUnitCircle * rebound * 0.033f;    //반동이 있다면 반동만큼의 크기를 가진 원의 범위 내에서 랜덤값을 가진다.

					shootSound.Play();



					int _layerMask = (-1) - (1 << LayerMask.NameToLayer("Test") | 1 << LayerMask.NameToLayer("HealOBJ") | 1 << LayerMask.NameToLayer("Flame") | 1 << LayerMask.NameToLayer("InvisibleWall"));
					

					//랜덤값의 방향으로 레이를 쏜다. 15는 에임까지의 거리
					if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(reboundRay.x, reboundRay.y, 150)), out hit, 1000.0f, _layerMask)) 
					{
						//Tracer.SetPosition(1, hit.point);

						if (hit.collider.gameObject.tag == "Enemy")
						{
							int damage = Random.Range(20, 30);
							hit.collider.gameObject.GetComponentInParent<Zombie>().zombieHP -= damage;

							GameObject blood = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f) , Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood, 0.3f);
							GameObject blood2 = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood2, 0.3f);
						}

						if (hit.collider.gameObject.tag == "Enemy2")
						{
							int damage = Random.Range(20, 30);
							hit.collider.gameObject.GetComponentInParent<Zombie2>().zombieHP -= damage;

							GameObject blood = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood, 0.3f);
							GameObject blood2 = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood2, 0.3f);
						}

						if (hit.collider.gameObject.tag == "SlowEnemy")
						{
							int damage = Random.Range(20, 30);
							hit.collider.gameObject.GetComponentInParent<SlowZombie>().zombieHP -= damage;

							GameObject blood = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood, 0.3f);
							GameObject blood2 = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood2, 0.3f);
						}

						if (hit.collider.gameObject.tag == "SpeedEnemy")
						{
							int damage = Random.Range(20, 30);
							hit.collider.gameObject.GetComponentInParent<SpeedZombie>().zombieHP -= damage;

							GameObject blood = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood, 0.3f);
							GameObject blood2 = Instantiate(BloodEffect, new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.3f), Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(blood2, 0.3f);
						}



						if (hit.collider.gameObject.tag == "Structure")
						{
							GameObject hitHole = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
							Destroy(hitHole, 5f);
						}
					}
					else
					{
						//Tracer.SetPosition(1, transform.position + transform.forward * 1000.0f);
					}

					StartCoroutine(ShootTracer());
					AR1RecoilFunction.instance.Recoil();
					AmmoFunction.instance.AR1_Capacity -= 1;
				}
			}
		}

		if(0 < delay)
		{
			delay--;
		}
	}

	IEnumerator ShootTracer()
	{
		//Tracer.SetPosition(0, ShootPos.position);    //궤적의 시작점을 현재 기즈모의 위치로 한다.        
		//Tracer.gameObject.SetActive(true);        //궤적을 활성화       
		Muzzle.Play();

		gunModel.transform.localPosition = gunDefaultPos;   //이전 반동의 영향을 받고 있어도 총기 반동 연출을 다시 시작한다.        
		gunModel.transform.Translate(Vector3.back * 0.3f);   //반동으로 0.3만큼 뒤로 밀린다.

		foreach (GameObject aim in aims)    //에임을 발사할때마다 0.1씩 벌어지게 한다.        
		{            
			aim.transform.Translate(Vector3.up * 5f);
		}
		rebound += 3;

		if (!isRebound)
		{
			StartCoroutine(Rebound());  //반동의 영향에서 정상으로 되돌리는 코루틴 작동. 이미 켜져있다면 다시 작동 시킬 필요가 없다.        
		}

		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		//Tracer.gameObject.SetActive(false);
		//yield return new WaitForFixedUpdate();
	}

	public IEnumerator Rebound()
	{
		isRebound = true;   //코루틴이 켜져있음을 알린다.        

		while(true)        
		{            
			gunModel.transform.localPosition = Vector3.Lerp(gunModel.transform.localPosition, gunDefaultPos, Time.deltaTime * 3.0f);    //총기의 위치를 Lerp로 천천히 되돌린다.  
    
			foreach (GameObject aim in aims)
			{                
				aim.transform.localPosition = Vector3.Lerp(aim.transform.localPosition, new Vector3 (0, 0.275f, 15.0f), Time.deltaTime * 3.0f); //에임의 위치도 Lerp로 천천히 되돌린다.            
			}      
			
			rebound = Mathf.Lerp(rebound, 0, Time.deltaTime * 3.0f);  //반동으로 올라간 카메라의 각도 Lerp로 천천히 되돌린다.  
 
			if (Vector3.Distance(gunModel.transform.localPosition, gunDefaultPos) < 0.001f) //총이 거의 제자리로 돌아왔다면            
			{               
				gunModel.transform.localPosition = gunDefaultPos;   
				
				foreach (GameObject aim in aims)                
				{                    
					aim.transform.localPosition = new Vector3(0, 0.275f, 15.0f);                
				}   
				
				rebound = 0;                
				isRebound = false;      //모든 반동의 영향을 초기화 시키고 코루틴의 종료를 알리고 코루틴을 종료한다.                
				break;            
			}
			yield return null;
		}
		yield break;
	}
}
