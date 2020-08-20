namespace Megaplan.API.Models
{
    using System;

    public class BaseModel : IEquatable<BaseModel>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        #region Equals

        public bool Equals(BaseModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((BaseModel)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        #endregion

    }

    public class BaseNamedModel : BaseModel, IEquatable<BaseNamedModel>
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        #region Equals

        public bool Equals(BaseNamedModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return base.Equals(other) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((BaseNamedModel)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        #endregion

    }
}