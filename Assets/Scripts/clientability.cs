using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class clientability : MonoBehaviour
{
    public static clientability Instance { get; private set; }
    private static readonly object _lock = new object();
    [SerializeField] public Animator timer;
    [SerializeField] GameManager gamemanager;
    [SerializeField] playerslimeed player;

    

    void Awake() {
        lock (_lock) {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void getability(client thisclient) {
        switch (thisclient.name.ToLower()) { // use ToLower() for consistent case comparison
            case "dracula":
                gamemanager.TriggerTakeTime();
                timer.SetTrigger("dracula");
                timer.SetTrigger("taketime");
                break;
            case "erica":
                movment.Instance.fuckupwalking();
                gamemanager.TriggerTakeTime();
                timer.SetTrigger("dracula");
                timer.SetTrigger("taketime");
                break;
            case "blobby":
                movment.Instance.fuckupwalking();
                player.slime();
                break;
            case "murray":
                movment.Instance.freezz();
                player.stone();
                break;
            case "frank":
            case "eunice":
                if (abjectonkitchen.Instance.thingsInKitchen.Count > 0) {
                    SOobjects randomobj = abjectonkitchen.Instance.GetRandomFromKitchen();
                    abjectonkitchen.Instance.thingsInKitchen.Remove(randomobj);
                    randomobj.getcounter().CleartheCounter();
                    randomobj.setoncounter(null);
                    Destroy(randomobj.gameObject);
                    //play effect
                }
                break;
        }
    }
}
