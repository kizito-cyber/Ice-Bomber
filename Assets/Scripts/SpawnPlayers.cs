using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Vector2[] playerPos;

    private int count;
    private void Start()
    {
        count = Random.Range(0, 4);
       
        PhotonNetwork.Instantiate(playerPrefab.name, playerPos[count], Quaternion.identity);
    }
}
