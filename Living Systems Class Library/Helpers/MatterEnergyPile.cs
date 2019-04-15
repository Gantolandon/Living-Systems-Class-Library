using System;
using System.Collections;
using System.Collections.Generic;

namespace Living_Systems_Class_Library.Helpers
{
    public class MatterEnergyPile : IDictionary<string, double>
    {
        Dictionary<string, double> internalPile;

        public IDictionary<string, double> InternalPile { get => internalPile; set => internalPile = new Dictionary<string, double> (value); }

        public ICollection<string> Keys => InternalPile.Keys;

        public ICollection<double> Values => InternalPile.Values;

        public int Count => InternalPile.Count;

        public bool IsReadOnly => InternalPile.IsReadOnly;

        public double this[string key] { get => InternalPile[key]; set => InternalPile[key] = value; }

        public MatterEnergyPile()
        {
            this.InternalPile = new Dictionary<string, double>();
        }

        public MatterEnergyPile(Dictionary<string, double> pile)
        {
            this.InternalPile = pile;
        }

        public MatterEnergyPile(string key, double amount)
        {
            this.InternalPile = new Dictionary<string, double>();
            this.InternalPile.Add(key, amount);
        }

        public bool ContainsKey(string key)
        {
            return InternalPile.ContainsKey(key);
        }

        public void Add(string key, double value)
        {
            InternalPile.Add(key, value);
        }

        public bool Remove(string key)
        {
            return InternalPile.Remove(key);
        }

        public bool TryGetValue(string key, out double value)
        {
            return InternalPile.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, double> item)
        {
            InternalPile.Add(item);
        }

        public void Clear()
        {
            InternalPile.Clear();
        }

        public bool Contains(KeyValuePair<string, double> item)
        {
            return InternalPile.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, double>[] array, int arrayIndex)
        {
            InternalPile.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, double> item)
        {
            return InternalPile.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, double>> GetEnumerator()
        {
            return InternalPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InternalPile.GetEnumerator();
        }

        public bool HasAmount(KeyValuePair<string, double> amount)
        {
            return this.HasAmount(amount.Key, amount.Value);
        }

        public bool HasAmount(string key, double value)
        {
            if (value == 0.0d)
            {
                return true;
            }
            return this.ContainsKey(key) && internalPile[key] >= value;
        }

        public bool RemoveAmount(string key, double value)
        {
            if (this.HasAmount(key, value))
            {
                this[key] -= value;
                if (this[key] == 0.0d)
                {
                    this.Remove(key);
                }
                return true;
            }
            return false;
        }

        public bool RemoveAmount(KeyValuePair<string, double> amount)
        {
            return this.RemoveAmount(amount.Key, amount.Value);
        }

        public void AddAmount(string key, double value)
        {
            if (!this.HasAmount(key, value))
            {
                this.Add(key, 0.0d);
            }
            this[key] += value;
        }

        public void AddAmount(KeyValuePair<string, double> amount)
        {
            this.AddAmount(amount.Key, amount.Value);
        }

        public bool HasBulk(IDictionary<string, double> pile)
        {
            foreach (KeyValuePair<string, double> amount in pile)
            {
                if (!this.HasAmount(amount))
                {
                    return false;
                }
            }
            return true;
        }

        public bool RemoveBulk(IDictionary<string, double> pile)
        {
            foreach (KeyValuePair<string, double> amount in pile)
            {
                if (!this.HasAmount(amount))
                {
                    return false;
                }
            }
            
            foreach (KeyValuePair<string, double> amount in pile)
            {
                if (!this.RemoveAmount(amount))
                {
                    return false;
                }
            }

            return true;
        }

        public void AddBulk(IDictionary<string, double> pile)
        {
            foreach (KeyValuePair<string, double> amount in pile)
            {
                this.AddAmount(amount);
            }
        }
    }
}
