using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class MeleeController : MonoBehaviour
{
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float damage;
    private Transform weaponPivot;
    private BoxCollider2D damageArea;
    private Vector2 boxOffset = new Vector2(0.74f, 0);
    [SerializeField] private float meleeForce;
    [SerializeField, Range(1, 5)] private float meleeRange;
    private bool isAvablAttack = true;
    [SerializeField] private float cooldown;
    private Tween meleeAttackAnimation;

    public void Initialize(Transform weaponPivot)
    {
        this.weaponPivot = weaponPivot;
        damageArea = weaponPivot.GetComponent<BoxCollider2D>();
    }

    public void MeleeAttack()
    {
        Vector2 boxCenter = (Vector2)weaponPivot.TransformPoint(boxOffset);
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, damageArea.size, weaponPivot.eulerAngles.z, attackLayer);

        foreach (var hitted in hits)
        {
            if (hitted.TryGetComponent<IDamangeable>(out var enemy) && isAvablAttack)
            {
                PlayMeleeAttackAnimation(weaponPivot);
                enemy.GetDamage(damage);

                if (hitted.TryGetComponent<IMoveable>(out var moveable))
                {
                    moveable.isMoving = false;
                    moveable.rigidbody.velocity = Vector2.zero;
                    moveable.rigidbody.AddForce((moveable.rigidbody.position - (Vector2)transform.position).normalized * meleeForce, ForceMode2D.Impulse);
                    if (moveable != null)
                        StartCoroutine(moveable.WaitForStop());
                }
                isAvablAttack = false;
                // Debug.Log("MeleeAttack");
                StartCoroutine(Cooldown());
                // SoundSystem.Instance.Sound("Melee").Play();
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isAvablAttack = true;
    }

    public bool IsMelee()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, meleeRange, attackLayer);

        foreach (var enemy in hits)
        {
            if (enemy.TryGetComponent<IDamangeable>(out var damangeable))
            {
                return true;
            }
        }
        return false;
    }

    public void PlayMeleeAttackAnimation(Transform weapon)
    {
        IAnimateable animateable = weapon.GetComponentInParent<IAnimateable>();
        animateable.AnimationMode();
        meleeAttackAnimation = weapon.DORotate(new Vector3(0, 0, 360), 0.3f, RotateMode.FastBeyond360)
                                    .OnComplete(() => animateable.AnimationMode(false));
    }

    private void OnDestroy()
    {
        if (meleeAttackAnimation != null)
        {
            meleeAttackAnimation.Kill();
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Handles.color = Color.yellow;
    //     Handles.DrawWireDisc(transform.position, transform.forward, meleeRange);
    // }
}
