using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Empressions
{
    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")
        {
        }
    }

    public class AlphabetAttribute : RegularExpressionAttribute
    {
        public AlphabetAttribute() : base("^(?=.*[a-zA-Z]).+$")
        {
        }
    }

    public class DigitAttribute : RegularExpressionAttribute
    {
        public DigitAttribute() : base("^(?=.*[0-9]).+$")
        {
        }
    }

    public class AlphabetOnlyAttribute : RegularExpressionAttribute
    {
        public AlphabetOnlyAttribute() : base("^[a-zA-Z ]+$")
        {
        }
    }

    public class AlphabetAndDigitOnly : RegularExpressionAttribute 
    {
        public AlphabetAndDigitOnly() : base("^[a-zA-Z0-9]+$")
        {
        }
    }
}
