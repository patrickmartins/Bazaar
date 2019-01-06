using PM.Bazaar.Domain.Validators;
using System;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Entities
{
    public class Image : Entity
    {
        public Guid Hash { get; private set; }
        public byte[] Bytes { get; private set; }

        protected Image() { }

        public Image(byte[] bytes, Guid hash)
        {
            Bytes = bytes;
            Hash = hash;
        }

        public Image(int id, byte[] bytes, Guid hash) : this(bytes, hash)
        {
            Id = id;
        }

        public override IResult IsValid()
        {
            var validator = new PictureValidator();

            return validator.Validate(this);
        }
    }
}
