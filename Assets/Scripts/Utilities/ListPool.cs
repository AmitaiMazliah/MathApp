using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MathApp
{
    public partial class ListPool<T>
    {
        const int POOL_CAPACITY = 4;
        const int LIST_CAPACITY = 16;

        public static readonly ListPool<T> Shared = new();

        readonly List<List<T>> pool = new(POOL_CAPACITY);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> Get(int capacity)
        {
            lock (pool)
            {
                var poolCount = pool.Count;

                if (poolCount == 0)
                {
                    return new List<T>(capacity > 0 ? capacity : LIST_CAPACITY);
                }

                for (var i = 0; i < poolCount; ++i)
                {
                    var list = pool[i];

                    if (list.Capacity < capacity)
                        continue;

                    pool.RemoveBySwap(i);
                    return list;
                }

                var lastListIndex = poolCount - 1;

                var lastList = pool[lastListIndex];
                lastList.Capacity = capacity;

                pool.RemoveAt(lastListIndex);

                return lastList;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(List<T> list)
        {
            if (list == null)
                return;

            list.Clear();

            lock (pool)
            {
                pool.Add(list);
            }
        }
    }

    public static class ListPool
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> Get<T>(int capacity)
        {
            return ListPool<T>.Shared.Get(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return<T>(List<T> list)
        {
            ListPool<T>.Shared.Return(list);
        }
    }
}
