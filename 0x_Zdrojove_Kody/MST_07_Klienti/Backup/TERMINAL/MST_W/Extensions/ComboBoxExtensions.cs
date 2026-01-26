using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace Fask.MST_W.Extensions
{
	public static class ComboBoxExtensions
	{

		public static void EnumForComboBox(this ComboBox comboBox, Type enumType)
		{
			var memInfo = enumType.GetMembers().Where(a => a.MemberType == MemberTypes.Field).ToList();
			comboBox.Items.Clear();
			foreach (MemberInfo member in memInfo)
			{

				if (!string.IsNullOrEmpty(member.Name))
				{
					if (member.Name == "value__")
						continue;

					comboBox.Items.Add(member.Name);
					comboBox.SelectedIndex = 0;
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				}
			}
		}
	}
}
