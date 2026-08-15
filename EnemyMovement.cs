using Mono.Cecil.Cil;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static Enemymovement;

public class Enemymovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum Enemystate
    {
        Idle,
        searching,
        chasing,

    }

    public Enemystate currentstate = Enemystate.Idle;
    public Enemystate previousstate = Enemystate.Idle;
    public Vector3 target;
    public Transform player;
    public float searchtime = 20f;
    private float searchtimer;
    public float Chasespeed = 10f;
    public float idlespeed = 5f;
    public float searchspeed = 6.7f;
    Rigidbody rb;
    CapsuleCollider enemycapsule;
    NavMeshAgent agent;
    public Vector3 lastknownposition;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
       
    }



    // Update is called once per frame
    void Update()
    {
        bool check = Playerview();
       
        if (check)
        {
            changestate(Enemystate.chasing);
        }

        enemystatefunction(check);

        enemymovement();

    }
    void enemystatefunction(bool check)
    {
        switch (currentstate)
        {
            case Enemystate.Idle:

                break;
            case Enemystate.searching:
                searchtimer -= Time.deltaTime;
                if (check)
                {
                    changestate(Enemystate.chasing);
                }
                else if (searchtimer <= 0)
                {
                    changestate(Enemystate.Idle);
                }
                break;
            case Enemystate.chasing:
                if (!check)
                {
                    changestate(Enemystate.searching);
                }
                break;


        }
    }
    void enemymovement()
    {
        if(currentstate == Enemystate.chasing)
        {
            if (agent.remainingDistance < 3f)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else if (currentstate == Enemystate.Idle)
        {
            agent.isStopped = true;
        }
        else if (currentstate == Enemystate.searching)
        {
            agent.isStopped = false;
            agent.SetDestination(lastknownposition);
        }
        
    }
    bool Playerview()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 30f)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 30f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    lastknownposition = player.position;
                    return true;

                }
                return false;
            }
        }
        return false;

    }

    void changestate(Enemystate State)
    {
        previousstate = currentstate;
        currentstate = State;
       
       

        if (State == Enemystate.searching)
        {
            searchtimer = searchtime;
        }
        
    }
}
