using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	public Vector3 target;
	public float speed = 500f;
	public int damage =2;
	public void SetTarget (Vector3 _target) {
		target = _target;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
			if(transform.position == target){
				Destroy(gameObject);
				return;
			}
			transform.position = Vector3.MoveTowards(transform.position,target, speed * Time.deltaTime);
		}
	
	}
	void OnTriggerEnter(Collider other)
	{
		ReactiveTarget RT = other.gameObject.GetComponent<ReactiveTarget>();
		if(RT != null){
			RT.TakeDamage(damage);
		}
	}
}
