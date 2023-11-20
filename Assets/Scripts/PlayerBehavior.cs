using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private GameBehavior _gameManager;
    public float speed = 7f;
    private Rigidbody _rb;
    private SphereCollider _colSp;
    public Material MainMaterial, MidMaterial, CritMaterial, HitMaterial, status;
    public bool playerDead = false;
    private double PlayerHP;
    public bool move;
    public GameObject bullet1, bullet2, bulletForAttack;
    public float bulletSpeed1 = 20f, bulletSpeed2 = 5f, bulletSpeed;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _colSp = GetComponent<SphereCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        PlayerHP = _gameManager.PHP;
        status = MainMaterial;
        bulletForAttack = bullet1;
        bulletSpeed = bulletSpeed1;
    }
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        GetComponent<Renderer>().material = status;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.PHP -= 5;
            GetComponent<Renderer>().material = HitMaterial;
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
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && bulletForAttack != bullet1)
        {
            Debug.Log("Attack1");
            bulletForAttack = bullet1;
            bulletSpeed = bulletSpeed1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && bulletForAttack != bullet2)
        {
            Debug.Log("Attack2");
            bulletForAttack = bullet2;
            bulletSpeed = bulletSpeed2;
        }
        if (move)
        {
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject newBullet = Instantiate(bulletForAttack, this.transform.position + new Vector3(0, -0.5f, 1), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject newBullet = Instantiate(bulletForAttack, this.transform.position + new Vector3(0, -0.5f, -1), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity =  -this.transform.forward * bulletSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject newBullet = Instantiate(bulletForAttack, this.transform.position + new Vector3(-1, -0.5f, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = - this.transform.right * bulletSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject newBullet = Instantiate(bulletForAttack, this.transform.position + new Vector3(1, -0.5f, 0), this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.right * bulletSpeed;
        }
    }
}
