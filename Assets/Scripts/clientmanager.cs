using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientmanager : MonoBehaviour
{
    public static clientmanager Instance { get; private set; }

    [SerializeField] private clientlistSO allClients;
    private client currentClient;

    private void Awake() {
        Instance = this;
    }

    public client GetRandomClient() {
        currentClient = allClients.ClientsList[UnityEngine.Random.Range(0, allClients.ClientsList.Count)];
        return currentClient;
    }
}
