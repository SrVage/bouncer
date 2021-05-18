using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject _ball = null;
    private void Awake()
    {
        _ball = GameObject.Find("Sphere");
        transform.position = _ball.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!_ball.GetComponent<Ball>()._arr) Destroy(gameObject);
        else
        {
            transform.forward = _ball.GetComponent<Ball>().undir.normalized;
            transform.localScale = new Vector3(1, 1, 1 /*_ball.GetComponent<Ball>().undir.magnitude/20*/);
        }

        /*transform.LookAt(_ball.GetComponent<Ball>().undir);*/

            //transform.rotation = Quaternion.Euler(_ball.GetComponent<Ball>().undir);
    }
}
