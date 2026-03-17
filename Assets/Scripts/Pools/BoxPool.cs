using UnityEngine;
using UnityEngine.Pool;
using Behaviors;

namespace Pools
{

    public class BoxPool: MonoBehaviour
    {
        public static BoxPool Current;

        [SerializeField] private GameObject Prefab;
        [SerializeField] private int DefaultCapacity = 10;
        [SerializeField] private int MaxSize = 100;

        protected ObjectPool<BoxBehavior> Pool;

        protected virtual void Awake()
        {
            Pool = new ObjectPool<BoxBehavior>(
                createFunc: () => {
                    var obj = Instantiate(Prefab);
                    var behavior = obj.GetComponent<BoxBehavior>();
                    return behavior;
                },
                actionOnGet: obj => {
                    obj.InTruck = false;
                    obj.gameObject.SetActive(false);
                },
                actionOnRelease: obj => obj.gameObject.SetActive(false),
                actionOnDestroy: obj => Destroy(obj.gameObject),
                defaultCapacity: DefaultCapacity,
                maxSize: MaxSize
            );
            Current = this;
        }

        public BoxBehavior Get() => Pool.Get();

        public void Release(BoxBehavior obj) => Pool.Release(obj);
    }
}
