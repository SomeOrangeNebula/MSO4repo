using System;
using System.Collections.Generic;
using System.Text;

namespace MSO4
{
	static class RulesDataBase
	{
		static List<String> ruleNames = new List<string> { "Wari", "Mankala" };

		public static List<string> GetRuleNames()
		{
			return ruleNames;
		}
	}
}
