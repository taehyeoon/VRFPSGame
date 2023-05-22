using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float damage;

    public float maxDistance;
    public float minDistance;
    public float maxVolume;
    public float minVolume;
    
    public void SetBulletData(float bulletDamage)
    {
        this.damage = bulletDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 playerPos = Managers.Instance.gameManager.player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPos);
        Debug.Log("Bullet collides at" + distance+ " distance");
        // Adjust the volume according to the distance
        float volume = Mathf.Lerp(maxVolume, minVolume, (distance - minDistance) / (maxDistance - minDistance));
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
        
        Destroy(gameObject);
    }
}