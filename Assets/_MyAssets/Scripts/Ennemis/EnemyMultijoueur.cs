using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMultijoueur : NetworkBehaviour
{
    [Header("Effet de particule")]
    [SerializeField] private ParticleSystem hitMonstreEffect;

    private Gun Gun;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private AvatarReseau[] players;
    [SerializeField] private float degatMonstres = 20f;
    private Player_VR_Multijoueur[] player_VR;

    private void Start()
    {
        Gun = FindObjectOfType<Gun>();
        Gun.OnEnemyHitEvent += OnEnemyHitEvent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        players = FindObjectsOfType<AvatarReseau>();
        player_VR = FindObjectsOfType<Player_VR_Multijoueur>();

        ChoosingTarget();
    }

    private void Update()
    {
        if (IsServer)
        {
            ChoosingTarget();
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

        foreach (var player in players)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                player.GetComponent<Player_VR_Multijoueur>()._sante -= degatMonstres;
                Destroy(gameObject);
            }
        }
    }

    private void OnEnemyHitEvent(GameObject obj, RaycastHit hit)
    {
        DeathEnemy(obj, hit);
        Destroy(obj);
    }

    private void DeathEnemy(GameObject obj, RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        navMeshAgent.enabled = false;
        DeathEnemyClientRPC();
        Destroy(obj);

    }

    [ClientRpc]
    public void DeathEnemyClientRPC()
    {
        Destroy(this);
    }

}
