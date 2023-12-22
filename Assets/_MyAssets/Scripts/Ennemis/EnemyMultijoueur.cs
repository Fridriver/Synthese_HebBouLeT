using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMultijoueur : NetworkBehaviour
{
    [Header("Effet de particule")]
    [SerializeField] private ParticleSystem hitMonstreEffect;

    private GunMultiplayer Gun;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private AvatarReseau[] players;
    [SerializeField] private float degatMonstres = 20f;
    private Player_VR_Multijoueur[] player_VR;
    private RaycastHit targetHit;

    private void Start()
    {
        Gun = FindObjectOfType<GunMultiplayer>();
        Gun.OnEnemyHitEvent += OnEnemyHitEvent;
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (IsServer)
        {
            players = FindObjectsOfType<AvatarReseau>();
            player_VR = FindObjectsOfType<Player_VR_Multijoueur>();
            target = players[Random.Range(0, players.Length)].RootAvatar;
        }
    }

    private void Update()
    {
        if (IsServer)
        {
            ChoosingTarget();
            PlayerIsInRange();
        }
        else
        {
            target = null;
            navMeshAgent.enabled = false;
        }
    }

    private void ChoosingTarget()
    {
        float minDistance = float.MaxValue;
        foreach (var player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = player.RootAvatar;
            }
        }

        navMeshAgent.SetDestination(target.position);

        
    }

    private void PlayerIsInRange()
    {
        
        foreach (var player in players)
        {

            if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
               // Debug.Log("Player is in Range");
                NetworkSceneTransition.Instance.ChargerScenePourTous("EndSceneMultijoueur");
            }

        }
    }

    public void OnEnemyHitEvent(RaycastHit hit)
    {
        Debug.Log("OnEnemyHitEvent");
        targetHit = hit;
        DeathEnemyClientRPC();
        DeathEnemy();
    }

    
    private void DeathEnemy()
    {
        Destroy(gameObject);
    }


    //[ServerRpc(RequireOwnership = false)]
    //public void DeathEnemyServerRPC()
    //{
    //    DeathEnemyClientRPC();
    //}

    [ClientRpc]
    public void DeathEnemyClientRPC()
    {
        Debug.Log("DeathEnemy");

        navMeshAgent.enabled = false;
        Destroy(this);
    }
}