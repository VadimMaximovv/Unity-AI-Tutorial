using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private PlayerBehavior pb;
    void Start()
    {
        offset = transform.position - player.transform.position;
        pb = player.GetComponent<PlayerBehavior>();
    }

    void LateUpdate()
    {
        if (!pb.playerDead)
            transform.position = player.transform.position + offset;
    }
}
