using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
    class RulesFactory
    {
        public Rules GetRules(string type)
        {
            switch (type)
            {
                case "Wari":
                    return new WariRules();
                case "Mankala":
                    return new MankalaRules();
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
