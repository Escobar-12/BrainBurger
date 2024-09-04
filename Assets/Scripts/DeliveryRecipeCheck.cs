using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryRecipeCheck : MonoBehaviour
{
    public Action OrderSpawned;
    public Action OrderCompleted;
    public static DeliveryRecipeCheck Instance { get; private set; }
    [SerializeField] allRecipes allTherecipes;
    List<RecipeSO> WaitForClientRecipe;
    public client randomClient;

    [SerializeField] Transform spawn_point;
    [SerializeField] GameManager gamemanager;
    [SerializeField] float timetospawnnextclient;
    float time;
    [SerializeField] float maxtime;
    GameObject nextclient, oldclient;
    bool isSpawningClient;
    bool delivered;
    bool abilityUsed;  // Flag to prevent ability from being triggered multiple times
    int orderdeliverd;
    [SerializeField] TextMeshProUGUI deliverd;
    [SerializeField] AudioClip served;
    AudioSource audioSource;

    void Awake() {
        Instance = this;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        orderdeliverd = 0;
        time = 5f;
        WaitForClientRecipe = new List<RecipeSO>();
        isSpawningClient = false;
        delivered = false;
        abilityUsed = false; // Initialize the flag
    }

    void Update() {
        // Timer
        if (!isSpawningClient) {
            time -= Time.deltaTime;
            if (time <= 0) {
                if (WaitForClientRecipe.Count > 0) {
                    WaitForClientRecipe.RemoveAt(0);
                    OrderSpawned?.Invoke();
                    Debug.Log("Time is up");
                }

                if (nextclient != null) {
                    if (!delivered && !abilityUsed) {
                        Debug.Log("Player delivered the wrong recipe");
                        clientability.Instance.getability(randomClient);
                        abilityUsed = true; // Mark ability as used
                    }
                    oldclient = nextclient;
                    oldclient.GetComponent<Animator>().SetTrigger("walkaway");
                    isSpawningClient = true;
                    Invoke(nameof(OnClientWalkoutComplete), timetospawnnextclient);
                } else {
                    SpawnNewClient();
                }

                time = maxtime;
            }
        }
    }

    void OnClientWalkoutComplete() {
        DestroyClient();
        SpawnNewClient();
    }

    void SpawnNewClient() {
        randomClient = clientmanager.Instance.GetRandomClient();
        Debug.Log(randomClient);
        nextclient = Instantiate(randomClient.prefab, spawn_point);
        nextclient.transform.localPosition = Vector3.zero;

        Invoke(nameof(SetupClient), 2.1f);
    }

    void SetupClient() {
        nextclient.GetComponent<Animator>().SetTrigger("reset");
        RecipeSO nextRecipe = randomClient.FaveredFood[UnityEngine.Random.Range(0, randomClient.FaveredFood.Count)];
        Debug.Log(nextRecipe.name);
        WaitForClientRecipe.Add(nextRecipe);
        OrderSpawned?.Invoke();

        isSpawningClient = false;
        delivered = false; // Reset delivered status
        abilityUsed = false; // Reset ability used flag
    }

    void DestroyClient() {
        if (oldclient != null) {
            Destroy(oldclient);
            oldclient = null;
        }
    }

    public void DeliverTheRecipe(platesSO PlateToBeDelivered) {
        if (WaitForClientRecipe.Count == 0) return;

        RecipeSO waitingrecipe = WaitForClientRecipe[0];

        if (PlateToBeDelivered.FoodOnPlate.Count > 0) {
            bool recipeFound = true;

            foreach (SOobjects wait_ingredient in waitingrecipe.RecipeList) {
                bool ingredientFound = false;

                foreach (SOobjects has_ingredient in PlateToBeDelivered.FoodOnPlate) {
                    if (wait_ingredient.tag == has_ingredient.tag) {
                        ingredientFound = true;
                        break;
                    }
                }

                if (!ingredientFound) {
                    recipeFound = false;
                    break;
                }
            }

            if (recipeFound && PlateToBeDelivered.FoodOnPlate.Count > waitingrecipe.RecipeList.Count) {
                recipeFound = false;
            }

            if (recipeFound) {
                Debug.Log("Player delivered the right recipe");
                orderdeliverd++;
                deliverd.text = orderdeliverd.ToString();
                delivered = true;
                gamemanager.TriggerAddTime();
                gameObject.GetComponent<clientability>().timer.SetTrigger("addtime");
            } else if (!abilityUsed) {
                Debug.Log("Player delivered the wrong recipe");
                clientability.Instance.getability(randomClient);
                abilityUsed = true; // Mark ability as used
            }
            audioSource.clip = served;
            audioSource.Play();

            nextclient.GetComponent<Animator>().SetTrigger("walkaway");

            WaitForClientRecipe.RemoveAt(0);
            OrderCompleted?.Invoke();
            Invoke(nameof(resettime), 1f);
            return;
        }
    }

    public void resettime() {
        time = 1f;
    }

    public List<RecipeSO> getwaitingrecipes() {
        return WaitForClientRecipe;
    }
}