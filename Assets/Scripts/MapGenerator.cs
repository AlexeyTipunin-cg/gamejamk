using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] public Earth _earthPrefab;
    [SerializeField] public AirGun _airGun;

    [SerializeField] public PlayerInputController _playerPrefab;
    
    
    private float _earthBlockWidth;
    
    private Vector3 _lastSpawnPosition;

    private ObjectPool<Earth> _pool;
    private void Awake()
    {
        _earthBlockWidth = _earthPrefab.sizeX;
        _pool = new  ObjectPool<Earth>(() => Instantiate(_earthPrefab, transform), ActionOnGet, ActionOnRelease);

        
        GetPrefab(_playerPrefab.transform.position.x);
    }

    private void ActionOnRelease(Earth obj)
    {
        obj.isInPool = true;
        obj.gameObject.SetActive(false);
    }

    private void ActionOnGet(Earth obj)
    {
        obj.gameObject.SetActive(true);
        obj.SetColor(Random.ColorHSV());
        obj.isInPool = false;
    }

    private void GetPrefab(float  position)
    {
        for (int i = 0; i < 8; i++)
        {
            var earth = _pool.Get();
            _lastSpawnPosition = Vector3.right * position  + new Vector3(i * _earthBlockWidth, -18.4f, 0);
            Instantiate(_airGun, _lastSpawnPosition + Vector3.up * _earthPrefab.halfSizeY, Quaternion.identity);
            earth.transform.position = _lastSpawnPosition;
        }

    }

    
    // public static RaycastHit2D Raycast(
    //     Vector2 origin,
    //     Vector2 direction,
    //     float distance,
    //     int layerMask)
    // {
    private void Update()
    {
        bool raycastAhead = Physics2D.Raycast(_playerPrefab.transform.position + Vector3.right * _earthBlockWidth * 3, Vector2.down, 30, LayerMask.GetMask("Floor") );

        if (!raycastAhead)
        {
            GetPrefab(_lastSpawnPosition.x + _earthBlockWidth);
        }

        RaycastHit2D raycastBack = Physics2D.Raycast(_playerPrefab.transform.position + Vector3.left * _earthBlockWidth * 4, Vector2.down, 30, LayerMask.GetMask("Floor"));
        if (raycastBack)
        {
            var obj = raycastBack.transform.GetComponent<Earth>();
            // if (!obj.isInPool)
            // {
                _pool.Release(obj);
            // }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(_playerPrefab.transform.position+ Vector3.right * _earthBlockWidth, Vector2.down * 30);
    }
}
