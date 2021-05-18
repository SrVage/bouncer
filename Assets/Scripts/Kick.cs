using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    private Animator _anim = null;
    [SerializeField] GameObject _ball = null;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void attack()
    {

        _anim.SetTrigger("Kick");
    }

    public void ballDown()
    {
        _ball.GetComponent<MeshRenderer>().enabled = true;
    }

    public void ball()
    {
        _ball.GetComponent<Ball>().kick();
    }

    // Update is called once per frame
    void Update()
    {
        if (_ball.GetComponent<Ball>().move == 1)
        transform.position = _ball.GetComponent<Ball>()._ballStart + new Vector3(-0.5f, 0, -2f);
    }
}
