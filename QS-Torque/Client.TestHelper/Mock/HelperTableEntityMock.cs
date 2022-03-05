using Core.Entities;
using System;
using Core;

namespace TestHelper.Mock
{
    public class HelperTableEntityMock : HelperTableEntity, IQstEquality<HelperTableEntityMock>, IUpdate<HelperTableEntityMock>, ICopy<HelperTableEntityMock>
    {
        public HelperTableDescription Description;


        public bool EqualsById(HelperTableEntityMock other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(HelperTableEntityMock other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Description.Equals(other.Description);
        }

        public void UpdateWith(HelperTableEntityMock other)
        {
            throw new NotImplementedException();
        }

        public HelperTableEntityMock CopyDeep()
        {
            return new HelperTableEntityMock()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Description = this.Description != null ? new HelperTableDescription(this.Description.ToDefaultString()) : null
            };
        }
    }
}
