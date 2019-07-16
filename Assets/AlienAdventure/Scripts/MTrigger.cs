using UnityEngine;


public class MTrigger : MonoBehaviour
{
    public delegate void function(Collider collider);

    public function onEnter;
    public function onStay;
    public function onExit;


    void Awake()
    {
        Init();
    }

    private void Init()
    {
        onEnter = OnEnter;
        onStay = OnStay;
        onExit = OnExit;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        onEnter(collider);
    }

    void OnTriggerStay(Collider collider)
    {
        onStay(collider);
    }

    void OnTriggerExit(Collider collider)
    {
        onExit(collider);
    }

    private void OnEnter(Collider collider)
    {
        //Debug.Log("OnEnter");
    }

    private void OnStay(Collider collider)
    {
        //Debug.Log("OnStay");
    }

    private void OnExit(Collider collider)
    {
        //Debug.Log("OnExit");
    }
}
