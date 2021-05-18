using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> GetRigidbodies = new List<Rigidbody>();
    private Animator _anim = null;
    // Start is called before the first frame update
    void Start()
    {
        GetRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        isKinematicOn();
        _anim = GetComponent<Animator>();
    }

    private void isKinematicOn()
    {
        for (int i = 0; i < GetRigidbodies.Count; i++)
        {
            GetRigidbodies[i].isKinematic = true;
        }
    }

    public void isKinematicOff()
    {
        _anim.enabled = false;
        for (int i = 0; i < GetRigidbodies.Count; i++)
        {
            GetRigidbodies[i].isKinematic = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isKinematicOff();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Ball>().score();
            StartCoroutine("Death");
            Destroy(gameObject, 3f);
        }
    }


    private IEnumerator Death()
    {
        for (int i = 0; i < 40; i++)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.Lerp(gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color, Color.gray, Time.deltaTime * 15);
            yield return new WaitForSeconds(0.03f);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ball"))
    //        isKinematicOff();
    //}

}
