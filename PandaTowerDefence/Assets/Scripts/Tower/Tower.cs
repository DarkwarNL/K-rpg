using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
using System;

public enum TowerType
{
    AOE,
    FastFire,
    Standart,
    SlowsStun
}

[Serializable]
public class Tower : MonoBehaviour{	
	protected string _Name;
    protected string _Description;
    protected TowerType _TowerType;
    protected float _Damage;
    protected float _AttackSpeed;
    protected float _AttackRange;
    protected int _Killcount;
    protected float _Price;

    protected GameObject Model;
    protected GameObject Attack;

    protected bool _CanAttack;
    protected SphereCollider _Collider;

    protected Animator _Anim;
    public List<NavMeshAgent> enemies = new List<NavMeshAgent>();

    private Shader outline;
    private Upgrades upgrade;
    LineRenderer lineRenderer;

    void Awake()
    {
        _Collider = gameObject.GetComponent<SphereCollider>();
        _Collider.radius = _AttackRange;
        _Collider.isTrigger = true;

        _Anim = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        var temppos = transform.position;
        temppos.y = 1.2f;
        transform.position = temppos;
        upgrade = Upgrades.Instance;
    }

    protected IEnumerator Cooldown(float time)
    {
        _CanAttack = false;
        yield return new WaitForSeconds(time);
        _CanAttack = true;
    }

    void Update()
    {
        if (!_CanAttack) return;

        var temprange = gameObject.GetComponent<SphereCollider>().radius;
        gameObject.GetComponent<SphereCollider>().radius = temprange;

        if (enemies.Count >= 1)
        {
            var closestEnemy = enemies.Where(go => go).OrderBy(go => GetPathLength(go)).FirstOrDefault();
            var temppos = transform.position;
            temppos.y = 2;
            lineRenderer.SetPosition(0, temppos);
            temppos = closestEnemy.transform.position;
            temppos.y = 2;
            lineRenderer.SetPosition(1, temppos);
            if (closestEnemy != null)
            {
                transform.LookAt(closestEnemy.transform);
                var temp = transform.rotation;
                temp.z = 0;
                temp.x = 0;
                transform.rotation = temp;
            }
            else
            {
                enemies.Remove(enemies[0]);
            }
        }
    }

    float GetPathLength(NavMeshAgent agent)
    {
        if (!agent.hasPath)
            return Mathf.Infinity;
        float result = 0;
        Vector3 lastCorner = agent.path.corners.First();
        foreach (var corner in agent.path.corners)
        {
            result += Vector3.Distance(corner, lastCorner);
            lastCorner = corner;
        }
        return result;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider hit)
    {
        if (hit.GetComponent<Enemy>())
        {
            enemies.Add(hit.GetComponent<NavMeshAgent>());
        }

    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.GetComponent<Enemy>())
        {
            enemies.Remove(hit.GetComponent<NavMeshAgent>());
        }
    }


    void OnMouseExit()
    {
        Debug.Log("out");
        GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
    }

    void OnMouseDown()
    {
        Debug.Log("uhmm?");
        GetComponent<Renderer>().material.shader = outline;
        upgrade.GetComponent<Upgrades>().towerUpgrading = gameObject;

    }

    public void AddDamage(float add)
    {
        _Damage += add;
    }

    public void AddSpeed(float add)
    {
        _AttackSpeed += add;
}

    public void AddRange(float add)
    {
        _AttackRange += add;
    }

}
