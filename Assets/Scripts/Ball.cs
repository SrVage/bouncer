using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Camera _cam = null;
    [SerializeField] private Vector3 _clickPos = Vector3.zero;
    private Rigidbody rb = null;
    private MeshRenderer mr = null;
   [SerializeField] GameObject _arrow = null;
    public Vector3 undir = Vector3.zero;
    public bool _arr = false;
    private Vector3 _lastpos = Vector3.zero;
    private Vector3 _ballDir = Vector3.zero;
    public int move = 0;
    private int _lastmove = 0;
    private bool _onStart = false;
    public int _playerScore = 0;
    public int _enemyScore = 0;
    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _enemy = null;
    private bool _inAttack = false;
    public Vector3 _ballStart = Vector3.zero;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
        move = 1;
    }

    void Start()
    {
        _ballStart = new Vector3(0f, 0.5f, -12.5f);
        transform.position = _ballStart;
    }

    // Update is called once per frame
    void Update()
    { if (_inAttack) return;
        if (move == 1)
        {
            if (!_onStart)
            {
                transform.position = _ballStart;
                _onStart = true;
            }
            else
            {
                firstMouseClick();
                secondMouseClick();
            }
        }
        if (move ==2)
        {
            if (!_onStart)
            {
                _inAttack = true;
                transform.position = _ballStart;
                _onStart = true;
                //Debug.Log("strike");
                //enemyStrike();
                _enemy.gameObject.GetComponent<Kick2>().attack();
                //Invoke("enemyStrike", 0.5f);
            }
            else 
            {

                
            }

        }
        ragDollRay();

        if (move==0)
        {
            Invoke("changeMove", 1.0f);
        }
        
    }

    public void score()
    {
        if (_lastmove == 2)
            _playerScore++;
        if (_lastmove == 1)
            _enemyScore++;
    }

    public void enemyStrike()
    {
                _lastmove = 1;
                move = 0;
        //var direction = transform.position - new Vector3(Random.Range(-3f, 3f), 0.5f, 14f);
        //var undir1 = new Vector3(40 * direction.x / Screen.width, 0, 10 * direction.y / Screen.height);
        var undir1 = new Vector3(Random.Range(-10f, 10f), 0, -30);
        undir = Vector3.Normalize(undir1);
        rb.AddForce(undir * 40, ForceMode.VelocityChange);
        undir = Vector3.zero;
        _inAttack = false;
    }

    private void changeMove()
    {
        if (move != 0) return;
        if (rb.velocity.magnitude < 0.2 || transform.position.y < -20)
        {
            mr.enabled = false;
            rb.velocity = new Vector3(0, 0, 0);
            move = _lastmove;
            _onStart = false;
        }
    }

    private void firstMouseClick()
    {
        RaycastHit hit;
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            //if (hit.collider.CompareTag("Ball"))
            //{
                _clickPos = Input.mousePosition;
                _arr = true;
                var arrow = Instantiate(_arrow);
            //}
        }
    }

    private void secondMouseClick()
    {
        if (_inAttack) return;
        var direction = _clickPos - Input.mousePosition;
        var undir1 = new Vector3(10 * direction.x / Screen.width, 0, 10 * direction.y / Screen.height);
        if (Vector3.Normalize(undir1).x >-0.8 && Vector3.Normalize(undir1).x < 0.8) undir = Vector3.Normalize(undir1);
        if (Input.GetMouseButtonUp(0))
        {
            _player.gameObject.GetComponent<Kick>().attack();
            _inAttack = true;
            //rb.AddForce(undir * 40, ForceMode.VelocityChange);
            //_arr = false;
            //undir = Vector3.zero;
            //_fly = true;
            //_lastmove = 2;
            //move = 0;
        }
    }

    public void kick()
    {
        mr.enabled = true;
        rb.AddForce(undir * 40, ForceMode.VelocityChange);
        _arr = false;
        undir = Vector3.zero;
        _lastmove = 2;
        move = 0;
        _inAttack = false; ;
    }
    
    private void ragDollRay()
    {
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, _ballDir, out hit2, Mathf.Infinity))
        {
            if (hit2.collider.CompareTag("RagDoll") && /*_fly &&*/ Vector3.Distance(transform.position, hit2.collider.gameObject.transform.position)<3)
                hit2.collider.gameObject.GetComponent<RagDoll>().isKinematicOff();
        }
        _ballDir = (transform.position - _lastpos).normalized;
        _lastpos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Side"))
            _ballStart = transform.position;
    }

}
