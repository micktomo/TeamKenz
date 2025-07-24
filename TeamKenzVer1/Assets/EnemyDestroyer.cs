using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    [SerializeField] Camera _mainCam;
    [SerializeField] float _destroyCamOffset = 10f;
    void Start()
    {
        if (_mainCam == null)
        {
            _mainCam = Camera.main;
        }
    }

    void Update()
    {
        if (_mainCam == null) return;

        // Camera‚Ì‰º’[‚ÌyÀ•W‚ğæ“¾
        float camBottom = _mainCam.transform.position.y - _mainCam.orthographicSize;

        if ( transform.position.y < camBottom - _destroyCamOffset )
        {
            Destroy(gameObject);
        }
    }
}
