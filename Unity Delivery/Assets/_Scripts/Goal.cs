using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    
    bool _finished;
    float _time;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Player" || col.transform.root.tag == "Player" ) && !_finished)
        {
            _finished = true;            
            StartCoroutine(ResetTag());
            Debug.Log(_time);
            _time = 0;
        }
    }

    private IEnumerator ResetTag()
    {
        yield return new WaitForSeconds(3);
        _finished = false;
    }
}
