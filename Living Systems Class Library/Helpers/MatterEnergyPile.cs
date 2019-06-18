using System;
using System.Collections;
using System.Collections.Generic;

namespace Living_Systems_Class_Library.Helpers
{
    /// <summary>
    /// An special version of a dictionary created to store amounts of particular matter and energy in a concrete place.
    /// It's made to be usable as a normal dictionary.
    /// </summary>
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

        /// <summary>
        /// Checks if the pile has a particular amount of a particular matter energy
        /// </summary>
        /// <param name="amount">Amount of a particular matter energy (a pair: string and double)</param>
        /// <returns>true or false</returns>
        public bool HasAmount(KeyValuePair<string, double> amount)
        {
            return this.HasAmount(amount.Key, amount.Value);
        }

        /// <summary>
        /// Checks if the pile has a particular amount of a particular matter energy
        /// </summary>
        /// <param name="key">matter-energy key</param>
        /// <param name="value">matter-energy amount</param>
        /// <returns>true or false</returns>
        public bool HasAmount(string key, double value)
        {
            if (value == 0.0d)
            {
                return true;
            }
            return this.ContainsKey(key) && internalPile[key] >= value;
        }

        /// <summary>
        /// Tries to remove a particular amount of matter and energy from the pile. If there is not enough of matter-energy, it does
        /// nothing and returns false.
        /// </summary>
        /// <param name="key">matter-energy key</param>
        /// <param name="value">matter-energy amount</param>
        /// <returns>true if succeeded, false if not</returns>
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
        /// <summary>
        /// Tries to remove a particular amount of matter and energy from the pile. If there is not enough of matter-energy, it does
        /// nothing and returns false.
        /// </summary>
        /// <param name="amount">Amount of a particular matter energy (a pair: string and double)</param>
        /// <returns>true if succeeded, false if not</returns>
        public bool RemoveAmount(KeyValuePair<string, double> amount)
        {
            return this.RemoveAmount(amount.Key, amount.Value);
        }

        /// <summary>
        /// Adds a particular amount of matter and energy to the pile.
        /// </summary>
        /// <param name="key">matter-energy key</param>
        /// <param name="value">matter-energy amount</param>
        public void AddAmount(string key, double value)
        {
            if (!this.HasAmount(key, value))
            {
                this.Add(key, 0.0d);
            }
            this[key] += value;
        }

        /// <summary>
        /// Adds a particular amount of matter and energy to the pile.
        /// </summary>
        /// <param name="amount">Amount of a particular matter energy (a pair: string and double)</param>
        public void AddAmount(KeyValuePair<string, double> amount)
        {
            this.AddAmount(amount.Key, amount.Value);
        }

        /// <summary>
        /// Checks if the pile has different amounts of many types of matter and energy
        /// </summary>
        /// <param name="pile">A dictionary containing pairs of matter-energy keys and amounts</param>
        /// <returns>true if HasAmount is true for every single amount</returns>
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

        /// <summary>
        /// Tries to remove different amounts of many types of matter and energy. It fails if it can't remove even one of them.
        /// </summary>
        /// <param name="pile">A dictionary containing pairs of matter-energy keys and amounts</param>
        /// <returns>true if HasAmount and RemoveAmount is true for every single amount</returns>
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

        /// <summary>
        /// Adds different amounts of many types of matter and energy to the pile.
        /// </summary>
        /// <param name="pile">A dictionary containing pairs of matter-energy keys and amounts</param>
        public void AddBulk(IDictionary<string, double> pile)
        {
            foreach (KeyValuePair<string, double> amount in pile)
            {
                this.AddAmount(amount);
            }
        }
    }
}
