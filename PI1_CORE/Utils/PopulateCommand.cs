using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PI1_CORE
{
    public static class PopulateCommand
    {
        public static void PopulateKeyValueList(FilteredElementCollector elements, System.Windows.Forms.ComboBox cmb)
        {
            var list = new List<KeyValuePair<string, ElementId>>();
            foreach (var element in elements)
            {
                list.Add(new KeyValuePair<string, ElementId>(element.Name, element.Id));
            }

            cmb.DataSource = new BindingSource(list, null);
            cmb.DisplayMember = "Key";
            cmb.ValueMember = "Value";
        }
    }
}
