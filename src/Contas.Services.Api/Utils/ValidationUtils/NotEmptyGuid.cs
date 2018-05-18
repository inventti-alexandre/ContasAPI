using System;
using System.ComponentModel.DataAnnotations;

namespace Contas.Services.Api.Utils.ValidationUtils
{
    public class NotEmptyGuid : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((Guid) value == Guid.Empty) return false;
            return true;
        }
    }
}