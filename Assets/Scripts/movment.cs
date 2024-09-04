using System;
using Unity.Mathematics;
using UnityEngine;

public class movment : MonoBehaviour
{
    public static movment Instance { get; private set; }

    [SerializeField] public float speed;
    [SerializeField] public float collided_speed;
    [SerializeField] public float rotation_speed;
    float xin, zin;
    [SerializeField] GameObject player_body;
    bool ismoving;
    bool canmove;
    [SerializeField] float player_hight;
    [SerializeField] float player_reduis;
    Vector3 targetpos;
    float movedis;
    Vector3 lastpos;
    ClearCounter selectedcounter;
    contaners selectedcontainer;
    cuttingcounter selectedcuttingcounter;
    trash selectedtrashcounter;
    stoveCounter selectedstovecounter;
    platecounter selectedplatescounter;
    DeliveryCounter selecteddeliverycounter;
    [SerializeField] gameinput GI;
    public Action playerevent;
    float raduis = 0.2f;
    public SOobjects ObjectONplayer;
    [SerializeField] bool hasO;
    [SerializeField] Animator anime;
    Playermovment newplayerinput;
    Vector2 newvector;
    Vector3 _smoothmovmentinput;
    Vector3 _movmentsmoothinputvector;
    Vector3 targetvector;
    public float smoothing;
    bool underability = false;

    Vector3 rightDirection;
    Vector3 leftDirection;



    void Awake()
    {
        newplayerinput = new Playermovment();
        newplayerinput.Movement.move.Enable();
        Instance = this;
    }


    void counter_interact()
    {
        if (selectedcounter != null)
        {
            selectedcounter.show();
        }
    }

    void container_interact()
    {
        if (selectedcontainer != null)
        {
            selectedcontainer.show();
        }
    }
    void cutting_interact(){
        if (selectedcuttingcounter != null){
            selectedcuttingcounter.show();
        }
    }
    void trash_interact(){
        if (selectedtrashcounter != null){
            selectedtrashcounter.show();
        }
    }
    void stove_interact(){
        if (selectedstovecounter != null){
            selectedstovecounter.show();
        }
    }
    void plates_interact(){
        if (selectedplatescounter != null){
            selectedplatescounter.show();
        }
    }
    void delivery_interact(){
        if (selecteddeliverycounter != null){
            selecteddeliverycounter.show();
        }
    }
    void Update()
    {
        movedis = speed * Time.deltaTime;
        newvector = newplayerinput.Movement.move.ReadValue<Vector2>();
        targetvector = new Vector3(-newvector.x, 0, -newvector.y);

        // Set a threshold for stopping movement
        float stopThreshold = 0.1f; // Adjust this value to fine-tune when the player should stop moving

        // Smooth the movement input
        targetpos = Vector3.SmoothDamp(targetpos, targetvector, ref _movmentsmoothinputvector, smoothing);

        // Clamp the movement to stop when it's below the threshold
        if (targetpos.magnitude < stopThreshold)
        {
            targetpos = Vector3.zero;
        }
        if (targetpos != Vector3.zero)
        {
            lastpos = targetpos;
        }

        canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * player_hight, player_reduis, targetpos, movedis);
        if (canmove)
        {
            move(speed);
        }
        else
        {
            adjustmovment();
        }
        if(!underability)
        {
            setspeedbywall();

        }

        counter_interaction();
        container_interaction();
        cuttingcounter_interaction();
        trash_interaction();
        stove_interaction();
        plates_interaction();
        Delivery_interaction();
        if(!PlayerHasSomthing()){
            anime.GetComponent<Animator>().SetBool("Iscarrying",false);
        }
    }

    void move(float moving_speed)
    {
        transform.position += targetpos.normalized * moving_speed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, lastpos, Time.deltaTime * rotation_speed);
        ismoving = targetpos.magnitude != 0;
    }

    void setspeedbywall()
    {
        leftDirection = transform.right * -1; // Left
        rightDirection = transform.right; // Right
        
        bool isNearLeftWall = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * player_hight, player_reduis, leftDirection, movedis);
        bool isNearRightWall = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * player_hight, player_reduis, rightDirection, movedis);
        
        if (isNearLeftWall || isNearRightWall)
        {
            speed = collided_speed;
        }
        else
        {
            resetwalk();
        }
    }
    void adjustmovment()
    {
        Vector3 targetposX = new Vector3(targetpos.x, 0, 0);
        Vector3 targetposZ = new Vector3(0, 0, targetpos.z);
        bool canmoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * player_hight, player_reduis, targetposX, movedis);
        bool canmoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * player_hight, player_reduis, targetposZ, movedis);

        if (canmoveX && !canmoveZ)
        {
            targetpos = targetposX;
        }
        else if (canmoveZ && !canmoveX)
        {
            targetpos = targetposZ;
        }
        // else if (canmoveX && canmoveZ)
        // {
        //     targetpos = new Vector3(targetposX.x, 0, targetposZ.z);
        // }
        else
        {
            targetpos = Vector3.zero;
        }
        move(collided_speed);
    }

    
    public void container_interaction()
    {
        RaycastHit hit_container;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_container, 1f))
        {
            if ( hit_container.transform.TryGetComponent(out contaners Container))
            {
                if (selectedcontainer != Container)
                {
                    DeselectContainer();
                    selectedcontainer?.hide();
                    selectedcontainer = Container;
                    playerevent += container_interact;
                    playerevent?.Invoke();
                    GI.I_container += selectedcontainer.interact;
                }
                
            }
            else
                {
                    DeselectContainer();
                }
        } 
        else{
                DeselectContainer();
            }
        }
    public void counter_interaction()
    {
        RaycastHit hit_counter;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_counter, 1f))
        {
            // check if it's a counter
            if (hit_counter.transform.TryGetComponent(out ClearCounter clearcounter))
            {
                if (selectedcounter != clearcounter)
                {
                    DeselectCounter();
                    selectedcounter?.hide();
                    selectedcounter = clearcounter;
                    playerevent += counter_interact;
                    GI.I_counter += selectedcounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectCounter();
            }
        }
        else
        {
            DeselectCounter();
        }
    }
    public void cuttingcounter_interaction()
    {
        RaycastHit hit_cutting;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_cutting, 1f))
        {
            // check if it's a counter
            if (hit_cutting.transform.TryGetComponent(out cuttingcounter cc))
            {
                if (selectedcuttingcounter != cc)
                {
                    DeselectCutting();
                    selectedcuttingcounter?.hide();
                    selectedcuttingcounter = cc;
                    playerevent += cutting_interact;
                    GI.I_cutting += selectedcuttingcounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectCutting();
            }
        }
        else
        {
            DeselectCutting();
        }
    }
    public void trash_interaction()
    {
        RaycastHit hit_trash;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_trash, 1f))
        {
            // check if it's a trash
            if (hit_trash.transform.TryGetComponent(out trash trashcounter))
            {
                if (selectedtrashcounter != trashcounter)
                {
                    DeselectTrash();
                    selectedtrashcounter?.hide();
                    selectedtrashcounter = trashcounter;
                    playerevent += trash_interact;
                    GI.I_trash += selectedtrashcounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectTrash();
            }
        }
        else
        {
            DeselectTrash();
        }
    }
    public void stove_interaction()
    {
        RaycastHit hit_stove;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_stove, 1f))
        {
            // check if it's a trash
            if (hit_stove.transform.TryGetComponent(out stoveCounter stovecounter))
            {
                if (selectedstovecounter != stovecounter)
                {
                    DeselectStove();
                    selectedstovecounter?.hide();
                    selectedstovecounter = stovecounter;
                    playerevent += stove_interact;
                    GI.I_stove += selectedstovecounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectStove();
            }
        }
        else
        {
            DeselectStove();
        }
    }
    public void plates_interaction()
    {
        RaycastHit hit_plates;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_plates, 1f))
        {
            // check if it's a trash
            if (hit_plates.transform.TryGetComponent(out platecounter platescounter))
            {
                if (selectedplatescounter != platescounter)
                {
                    DeselectPlates();
                    selectedplatescounter?.hide();
                    selectedplatescounter = platescounter;
                    playerevent += plates_interact;
                    GI.I_plates += selectedplatescounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectPlates();
            }
        }
        else
        {
            DeselectPlates();
        }
    }
    public void Delivery_interaction(){
        RaycastHit hit_delivery;
        if (Physics.SphereCast(transform.position, raduis, lastpos, out hit_delivery, 1f))
        {
            // check if it's a trash
            if (hit_delivery.transform.TryGetComponent(out DeliveryCounter deliverycounter))
            {
                if (selecteddeliverycounter != deliverycounter)
                {
                    DeselectDelivery();
                    selecteddeliverycounter?.hide();
                    selecteddeliverycounter = deliverycounter;
                    playerevent += delivery_interact;
                    GI.I_delivery += selecteddeliverycounter.interact;
                    playerevent?.Invoke();
                }
            }
            // if it's neither
            else
            {
                DeselectDelivery();
            }
        }
        else
        {
            DeselectDelivery();
        }
    }
    void DeselectCounter()
    {
        if (selectedcounter != null)
        {
            selectedcounter.hide();
            GI.I_counter -= selectedcounter.interact;
            playerevent -= counter_interact;
            selectedcounter = null;
        }
    }
    void DeselectDelivery()
    {
        if (selecteddeliverycounter != null)
        {
            selecteddeliverycounter.hide();
            GI.I_delivery -= selecteddeliverycounter.interact;
            playerevent -= delivery_interact;
            selecteddeliverycounter = null;
        }
    }
    void DeselectCutting()
    {
        if (selectedcuttingcounter != null)
        {
            selectedcuttingcounter.hide();
            GI.I_cutting -= selectedcuttingcounter.interact;
            playerevent -= cutting_interact;
            selectedcuttingcounter = null;
        }
    }
    void DeselectContainer()
    {
        if (selectedcontainer != null)
        {
            selectedcontainer.hide();
            playerevent -= container_interact;
            GI.I_container -= selectedcontainer.interact;
            selectedcontainer = null;
        }
    }
    void DeselectTrash()
    {
        if (selectedtrashcounter != null)
        {
            selectedtrashcounter.hide();
            playerevent -= trash_interact;
            GI.I_trash -= selectedtrashcounter.interact;
            selectedtrashcounter = null;
        }
    }
    void DeselectStove()
    {
        if (selectedstovecounter != null)
        {
            selectedstovecounter.hide();
            playerevent -= stove_interact;
            GI.I_stove -= selectedstovecounter.interact;
            selectedstovecounter = null;
        }
    }
    void DeselectPlates()
    {
        if (selectedplatescounter != null)
        {
            selectedplatescounter.hide();
            GI.I_plates -= selectedplatescounter.interact;
            playerevent -= plates_interact;
            selectedplatescounter = null;
        }
    }
    public bool walking()
    {
        return ismoving;
    }
    public bool PlayerHasSomthing(){
        return ObjectONplayer != null ;
    }
    public SOobjects GetObjectOnPlayer(){
        return ObjectONplayer;
    }
    public void Cleartheplayer(){
        // clears the counter
        ObjectONplayer = null;
    }
    public void fuckupwalking()
    {
        underability = true;
        speed = 3.5f;
        rotation_speed = 8f;
        Invoke(nameof(resetwalk), 3f);
    }
    public void freezz(){
        underability = true;
        speed = 0;
        rotation_speed = 0;
        Invoke(nameof(resetwalk), 1.5f);
    }
    void resetwalk()
    {
        underability = false;
        speed = 7f;
        rotation_speed = 13f;
    }
}
