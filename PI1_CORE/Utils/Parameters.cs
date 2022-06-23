using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PI1_CORE
{
    /// <summary>
    /// Class with methods for editing parameters.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Gets the parameter value in case of storage type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public static dynamic GetParameterValue<T>(Parameter parameter)
        {
            StorageType storageType = parameter.StorageType;

            switch (storageType)
            {
                case StorageType.None:
                    return null;
                case StorageType.Integer:
                    return parameter.AsInteger();
                case StorageType.Double:
                    return UnitUtils.ConvertFromInternalUnits(parameter.AsDouble(), parameter.DisplayUnitType);
                case StorageType.String:
                    return parameter.AsString();
                case StorageType.ElementId:
                    return parameter.AsElementId();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Creates the shared parameter.
        /// </summary>
        /// <param name="uiapp">The current uiapp.</param>
        /// <param name="groupName">Name of the parameters group from shared parameters file.</param>
        /// <param name="parameterName">Name of the shared parameter from parameters group.</param>
        /// <param name="typeBinding"><c>true</c> for type binding and <c>false</c> for instance binding.</param>
        /// <param name="parGroup">BuiltinParameterGroup for sorting parameter.</param>
        /// <param name="categories">The categories for binding.</param>
        public static void CreateSharedParameter(UIApplication uiapp, string groupName, string parameterName,
                                          bool typeBinding=true, 
                                          BuiltInParameterGroup parGroup=BuiltInParameterGroup.INVALID,
                                          params BuiltInCategory[] categories)
        {
            Application app = uiapp.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Get the definition of the shared parameter
            Definition parDefinition = app.OpenSharedParameterFile()
                                       .Groups.get_Item(groupName)
                                       .Definitions.get_Item(parameterName);

            // Check if shared parameter definition exist.
            if (parDefinition != null)
            {
                // Create category set for binding from given categories.
                CategorySet categorySet = doc.Application.Create.NewCategorySet();
                foreach (BuiltInCategory category in categories)
                {
                    Category categoryCategory = Category.GetCategory(doc, category);
                    categorySet.Insert(categoryCategory);
                }

                // Create binding in case of tipe or instance parameter.
                object binding = null;
                if (typeBinding)
                {
                    binding = app.Create.NewTypeBinding(categorySet);
                }
                else
                {
                    binding = app.Create.NewInstanceBinding(categorySet);
                }

                // Create shared parameter.
                doc.ParameterBindings.Insert(parDefinition, binding as Binding, parGroup);
            }
        }
    }
}
