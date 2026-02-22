using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Animation animation;

    public AnimationClip idleClip;
    public AnimationClip dieClip;
    public AudioSource audioSource;
 
    public int monsterType;
    private void Awake()
    {
        animation = GetComponent<Animation>();
        animation.clip = idleClip;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="bullet")
        {
            Destroy(collision.collider.gameObject);
            Died();
        }
    }
    private void OnDisable()
    {
        animation.clip = idleClip;
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(0.8f);
        GetComponentInParent<TargetManager>().UpadateMonster();
    }

    void Died()
    {
        audioSource.Play();
        animation.clip = dieClip;
        animation.Play();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        UIManager.Instance.AddScroe();
        StartCoroutine(Deactive());
    }
}
