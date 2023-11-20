using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private GameBehavior _gameManager;
    public Material MainMaterial, MidMaterial, CritMaterial, HitMaterial, status;
    private bool playerDead = false;
    private double PlayerHP;
    public Transform player;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        PlayerHP = _gameManager.PHP;
        status = MainMaterial;
    }
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Renderer>().material = status;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "Enemy")
        {
            _gameManager.PHP -= 1;
            player.GetComponent<Renderer>().material = HitMaterial;
            //Debug.Log(_gameManager.HP);
            StartCoroutine(waiter());
        }
        if (_gameManager.PHP > PlayerHP * 0.7)
            status = MainMaterial;
        else if (_gameManager.PHP <= PlayerHP * 0.7 && _gameManager.PHP > PlayerHP * 0.4)
            status = MidMaterial;
        else if (_gameManager.PHP <= PlayerHP * 0.4)
            status = CritMaterial;
        if (_gameManager.PHP <= 0 && !playerDead)
        {
            Destroy(GameObject.Find("Player"));
            Debug.Log("Player dead!");
            playerDead = true;
        }
    }
}
