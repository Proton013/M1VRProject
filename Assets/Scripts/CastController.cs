using UnityEngine;

public class CastController : MonoBehaviour
{
    [SerializeField] private ProjectileController projectileController = default;
    private Transform _wand = default;


    [SerializeField] private float velocityUpperThreshold = 20f;
    [SerializeField] private float velocityLowerThreshold = 0.1f;
    
    private Vector3 _tipPos;
    private Vector3 _tipLastPos;
    private Rigidbody _tipRigidbody;
    private bool _isFastEnough = false;
    
    private float _coolDown = 0.2f;
    private float _count = 0f;

    private void Start()
    {
        _wand = transform;
        _tipRigidbody = _wand.transform.Find("Tip").GetComponent<Rigidbody>();
        _tipPos = _wand.transform.Find("Tip").position;
        _tipLastPos = _tipPos;
    }

    private void Update()
    {
        float tipVelocity = UpdateTipVelocity();
        if (_count >= _coolDown)
        {
            CastProjectile(tipVelocity);
        }

        _count += Time.deltaTime;
    }

    private void CastProjectile(float tipVelocity)
    {
        // should be fast enough 
        if (tipVelocity >= velocityUpperThreshold) _isFastEnough = true;
        // once fast enough, return to low speed for cast
        if (_isFastEnough && tipVelocity <= velocityLowerThreshold) // && tipVelocity != 0 
        {
            projectileController.SpawnProjectiles(); // * _wandRigidbody.velocity.magnitude ?
            _count = 0f;
            _isFastEnough = false;
        }
        /*
        // Debug input
        if (Input.GetKeyDown("space"))
        {
            projectileController.SpawnProjectiles();
        }
        */
    }
    
    // instantaneous velocity
    private float UpdateTipVelocity()
    {
        _tipPos = _tipRigidbody.position;
        float velocity = (_tipPos - _tipLastPos).magnitude /Time.deltaTime;
        _tipLastPos = _tipPos;

        return velocity;
    }
}
