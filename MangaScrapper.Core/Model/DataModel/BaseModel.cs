using System;

namespace MangaScrapper.Core.Model.DataModel
{
    /// <summary>
    /// Model for Site, Series & Chapter Lists
    /// </summary>
    sealed class BaseModel : IEquatable<BaseModel>
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public bool Equals(BaseModel other)
        {
            if (other == null)
                return false;

            return string.Equals(Name, other.Name)
                   && string.Equals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals(obj as BaseModel);
        }

        /***
         * Overriding it so it can be used
         * in hash-based collections
         */
        public override int GetHashCode()
        {
            //running as unchecked as we don't care about overflow
            unchecked
            {
                // Choosing high co-primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Name) ? Name.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Url) ? Url.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(BaseModel x, BaseModel y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseModel x, BaseModel y)
        {
            return !(x == y);
        }
    }
}
