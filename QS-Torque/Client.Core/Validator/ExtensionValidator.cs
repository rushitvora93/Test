using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Client.Core.Validator
{
    public interface IExtensionValidator
    {
        bool Validate(Extension extension);
    }

    public class ExtensionValidator : IExtensionValidator
    {
        public bool Validate(Extension extension)
        {
            if (extension is null)
            {
                return true;
            }

            if (extension.Validate(nameof(Extension.InventoryNumber)) != null)
            {
                return false;
            }

            if (extension.Validate(nameof(Extension.Description)) != null)
            {
                return false;
            }

            if (extension.Validate(nameof(Extension.FactorTorque)) != null)
            {
                return false;
            }

            if (extension.Validate(nameof(Extension.Length)) != null)
            {
                return false;
            }

            if (extension.Validate(nameof(Extension.Bending)) != null)
            {
                return false;
            }

            return true;
        }
    }
}
