using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShelling : MonoBehaviour
{
    private Transform target;               // ��ǥ ����
    private float launchAngle;        // �߻� ����
    private Rigidbody rb;                   // Rigidbody ������Ʈ
    private float launchForce;       // �߻� �� ����

    private float monsterDamage;
    private float TotalDamage;

    [SerializeField] Vector3 bombSize;
    [SerializeField] LayerMask bombType;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void LaunchProjectile()
    {
        TestSound.Instance.PlaySound("launch");

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = target.position;

        Vector3 displacement = targetPosition - startPosition;
        float horizontalDistance = new Vector2(displacement.x, displacement.z).magnitude;
        float verticalDistance = displacement.y;

        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // �ʱ� �ӵ� ���
        float initialSpeedSquared = (horizontalDistance * horizontalDistance) /
                                     (2 * (horizontalDistance * Mathf.Tan(angleInRadians) - verticalDistance));

        float initialSpeed = Mathf.Sqrt(initialSpeedSquared) * launchForce;

        // �ʱ� �ӵ� ���� ���
        Vector3 horizontalDirection = new Vector3(displacement.x, 0, displacement.z).normalized;
        Vector3 velocity = horizontalDirection * initialSpeed * Mathf.Cos(angleInRadians);
        velocity.y = initialSpeed * Mathf.Sin(angleInRadians);

        // AddForce�� �ʱ� �ӵ� ����
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        // ���� �ӵ� ���� ��������
        Vector3 velocity = rb.velocity;

        // �ӵ��� �ʹ� ������ ȸ�� ������Ʈ ����
        if (velocity.sqrMagnitude > 0.01f)
        {
            // ���� �ӵ��� �������� �̵� ������ ���
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);

            // Y���� �⺻ �������� ����ϵ��� ���� (90�� ȸ��)
            targetRotation *= Quaternion.Euler(-90f, 0f, 0f);

            // ��� ȸ�� ����
            transform.rotation = targetRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = ObjectPool.Instance.Spawn("BombEffect", 1);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Collider[] hitPlayer = Physics.OverlapBox(transform.position, bombSize / 2, transform.rotation, bombType);
            foreach (Collider collider in hitPlayer)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    collider.GetComponent<Player>().TakeDamage(TotalDamage);
                }
                if (collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    collider.GetComponent<Monster>().TakeDamage(monsterDamage);
                }
            }

            effect.transform.position = transform.position + new Vector3(0, -0.5f);
            ObjectPool.Instance.Remove(this.gameObject);
        }
    }

    public void SetSetting(Transform target, float launchForce, float angle, float gravitySpeed, float damage, float monDamage)
    {
        this.target = target;
        this.launchAngle = angle;
        this.launchForce = launchForce;
        this.TotalDamage = damage;
        this.monsterDamage = monDamage;

        LaunchProjectile();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bombSize);
    }
}
