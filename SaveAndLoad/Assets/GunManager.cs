using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public float maxYRotation = 120f;
    public float minYRotation = 0f;
    public float maxXRotation = 60f;
    public float minXRotation = 0f;

    public float maxTimer = 1f;
    public float timer = 0f;

    public GameObject bulletGO;
    public Transform firePosition;
    public float force = 100f;

    public AudioClip fireClip;
    public AudioSource fireSource;

    private void Update()
    {
        if(GameManager.Instance.isPaused)
        {
            return;
        }

        timer += Time.deltaTime;
        if(timer>=maxTimer)
        {
            //Éä»÷
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
                timer = 0f;
            }
        }

        float xPosPrecent = Input.mousePosition.x/Screen.width;
        float yPosPrecent = Input.mousePosition.y/Screen.height;

        float xAngle = -Mathf.Clamp(yPosPrecent * maxXRotation, minXRotation, maxXRotation)+15;
        float yAngle = Mathf.Clamp(xPosPrecent * maxYRotation, minYRotation, maxYRotation)-60;

        transform.eulerAngles = new Vector3(xAngle, yAngle, 0);
    }

    void Shoot()
    {
        GameObject bulletCurrent = Instantiate(bulletGO,firePosition.position,Quaternion.identity);
        GetComponent<Animation>().Play();//Éä»÷¶¯»­
        fireSource.PlayOneShot(fireClip);
        bulletCurrent.GetComponent<Rigidbody>().AddForce(transform.forward*force);
        UIManager.Instance.AddShoot();
    }
}
