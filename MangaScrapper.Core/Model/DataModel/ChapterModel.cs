using System;

namespace MangaScrapper.Core.Model.DataModel
{
    sealed class ChapterModel : IEquatable<ChapterModel>
    {
        public string ImageUrl { get; set; }
        public string NextUrl { get; set; }

        public bool Equals(ChapterModel other)
        {
            if (other == null)
                return false;

            return string.Equals(ImageUrl, other.ImageUrl)
                   && string.Equals(NextUrl, other.NextUrl);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals(obj as ChapterModel);
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
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ImageUrl) ? ImageUrl.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, NextUrl) ? NextUrl.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(ChapterModel x, ChapterModel y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ChapterModel x, ChapterModel y)
        {
            return !(x == y);
        }
    }
}
