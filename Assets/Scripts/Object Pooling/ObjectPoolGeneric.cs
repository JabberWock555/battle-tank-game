using System;
using System.Collections.Generic;
using BattleTank.Generics;
using UnityEngine;

namespace BattleTank.ObjectPool
{
    public class ObjectPoolGeneric<T> : NonMonoSingleton<ObjectPoolGeneric<T>> where T: class
    {
        [Serializable]
        private class PooledItem<K>
        {
            public K item;
            public int id;
            public bool isUsed;
        }
        [SerializeField]
        private List<PooledItem<T>> pooledItems = new();



        internal virtual T GetItem(int id)
        {
            if (pooledItems.Count < 0)
            {
                return null;
            }

            PooledItem<T> pooledItem = pooledItems.Find(newItem => newItem.isUsed == false && newItem.id == id);
            if (pooledItem != null)
            {
                pooledItem.isUsed = true;
                return pooledItem.item;
            }
            PooledItem<T> newPooledItem = new();
            newPooledItem.item = CreateItem();
            newPooledItem.id = SetId();
            newPooledItem.isUsed = true;
            pooledItems.Add(newPooledItem);
            return newPooledItem.item;
        }

        protected virtual int SetId()
        {
            return -1;
        }

        internal virtual void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(newitem => newitem.item == item);
            if (pooledItem != null)
                pooledItem.isUsed = false;
        }

        protected virtual T CreateItem()
        {
            return (T)null;
        }
    }
}