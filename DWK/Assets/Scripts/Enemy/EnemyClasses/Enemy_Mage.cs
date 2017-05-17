using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy_Mage : Enemy
{
    public Transform LeftHand;
    public Transform RightHand;

    public GameObject BlastParticle;
    public GameObject BlackDot;
    public GameObject BlackParticle;
    public GameObject BlackExplosive;

    private GameObject[] _Particles = new GameObject[2];

    void Start ()
    {
        _AggroRange = 10;

        _Level = Random.Range(1, 4);
        _Damage = -2;
        _AttackCooldown = 2.5f;
        _AttackRange = 15f;
        _WalkSpeed = 1;
        _RunSpeed = 2f;

        SetData(15);
        
        _Particles[0] = Instantiate(BlackParticle, LeftHand, false);
        _Particles[0].transform.position = LeftHand.position;
        _Particles[1] = Instantiate(BlackParticle, RightHand, false);
        _Particles[1].transform.position = RightHand.position;
        SetParticles(false);
    }

    private void SetParticles(bool state)
    {
        foreach (GameObject particle in _Particles)
        {
            particle.SetActive(state);
        }
    }

    protected override void EnemyBehaviour()
    {   
        AnimatorStateInfo stateInfo = _Anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(AnimAttackString))
        {
            _Nav.isStopped = true;
        }
        else
            _Nav.isStopped = false;

        if (CanAttack() && stateInfo.IsName(AnimMovementString))
        {
            SetRotation();

            Vector3 pos = _Target.position - transform.position;
            var angle = Vector3.Angle(pos, transform.forward);
            
            if (angle <= 25)
            {
                _Anim.SetFloat("AttackType", Random.Range(2, 4));
                _Anim.SetBool(AnimAttackString, true);
                SetParticles(true);
            }
            else
            {
                _Anim.SetFloat("AttackType", 1);
                _Anim.SetBool(AnimAttackString, true);
                SetParticles(true);
            }
        }
        if (!_Nav.isActiveAndEnabled) return;

        if (_Nav.remainingDistance <= _Nav.stoppingDistance)
            _Nav.SetDestination(GetRandomPos(_Target.position, _AttackRange *1.5f));
    }

    protected override void Idle()
    {
        if(_Nav.remainingDistance < 2)
        {            
            _Nav.SetDestination(GetRandomPos(_StartPos, 100));
            
            _Nav.speed = _WalkSpeed;
        }
    }

    internal override void Attack(float add)
    {
        float type = _Anim.GetFloat("AttackType");
        SetParticles(false);
        if (type == 1)
        {
            SpawnSpell(BlackExplosive, transform.position + transform.forward, BlackExplosive.transform.rotation, false);
        }
        else if (type == 2)
        {
            SpawnSpell(BlackDot, transform.position + transform.forward, transform.rotation, true);
        }
         else if (type == 3)
        {
            StartCoroutine(Blast());            
        }

        _Anim.SetBool(AnimAttackString, false);
        _AttackTimer = 0;
    }

    private IEnumerator Blast()
    {
        Quaternion rot = BlastParticle.transform.rotation;

        SpawnSpell(BlastParticle, transform.position + transform.forward * 2, rot, false);
        yield return new WaitForSeconds(.3f);
        SpawnSpell(BlastParticle, transform.position + (transform.forward * 6), rot, false);
        yield return new WaitForSeconds(.3f);
        SpawnSpell(BlastParticle, transform.position + (transform.forward * 10), rot, false);
        yield return null;
    }

    void SpawnSpell(GameObject particle, Vector3 pos, Quaternion rot, bool move)
    {
        Spell spell = Instantiate(particle, pos, rot).GetComponent<Spell>();
        spell.Damage = GetDamage();
        spell.Move = move;
        Destroy(spell.gameObject, 3);
    }
}