using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float range;
    
    public float maxDistance;
    public float minDistance;
    public float maxVolume;
    public float minVolume;

    // Current bullet travel distance
    public float bulletCurrentRange;
    public Vector3 bulletStartPosition;
    public void SetBulletData(float bulletDamage, float bulletRange, Vector3 bulletStartPos)
    {
        damage = bulletDamage;
        range = bulletRange;
        bulletStartPosition = bulletStartPos;
    }

    private void Update()
    {
        CalcBulletCurrentRange();
        
        if (bulletCurrentRange > range)
        {
            Debug.Log("destroy Bullet on " + bulletCurrentRange);
            Destroy(gameObject);
        }
    }

    private void CalcBulletCurrentRange()
    {
        bulletCurrentRange = Vector3.Distance(transform.position, bulletStartPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 playerPos = Managers.Instance.gameManager.player.transform.position;
        // Distance between bullet crash point and current player
        float hitPointDistanceFromPlayer = Vector3.Distance(transform.position, playerPos);

        Debug.Log("Bullet collides at" + hitPointDistanceFromPlayer+ " distance");

        // Adjust the volume according to the distance
        float volume = Mathf.Lerp(maxVolume, minVolume, (hitPointDistanceFromPlayer - minDistance) / (maxDistance - minDistance));
        Managers.Instance.audioManager.bulletSource.volume = volume;
        
        if(collision.gameObject.CompareTag("Body"))
            Managers.Instance.audioManager.PlayBullet("hit_body");
        else if(collision.gameObject.CompareTag("Concrete"))
            Managers.Instance.audioManager.PlayBullet("hit_concrete");
        else if(collision.gameObject.CompareTag("Metal"))
            Managers.Instance.audioManager.PlayBullet("hit_metal");
        else if(collision.gameObject.CompareTag("Dirt"))
            Managers.Instance.audioManager.PlayBullet("hit_dirt");
        else if(collision.gameObject.CompareTag("Glass"))
            Managers.Instance.audioManager.PlayBullet("hit_glass");
        else
            Managers.Instance.audioManager.PlayBullet("hit_concrete");
        
        Debug.Log("[collosion]  " + gameObject.name + " : destroy at " + bulletCurrentRange);
        Destroy(gameObject);
    }
}