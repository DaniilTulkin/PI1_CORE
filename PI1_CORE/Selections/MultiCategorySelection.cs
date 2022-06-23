using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace PI1_CORE
{
    public class MultiCategorySelection
    {
        #region constructor

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="MultiCategorySelection"/> class.
        /// </summary>
        public MultiCategorySelection()
        {

        }

        #endregion

        #region public methods

        /// <summary>
        /// Selections the specified document.
        /// </summary>
        /// <param name="doc">The current document.</param>
        /// <param name="categories">The filtering categories.</param>
        /// <returns></returns>
        public List<Element> Selection(Document doc, params BuiltInCategory[] categories)
        {
            List<BuiltInCategory> listOfCategories = new List<BuiltInCategory>();
            foreach (BuiltInCategory category in categories)
            {
                listOfCategories.Add(category);
            }

            ElementMulticategoryFilter filter = new ElementMulticategoryFilter(listOfCategories);
            var elements = new FilteredElementCollector(doc)
                .WherePasses(filter)
                .WhereElementIsNotElementType()
                .ToElements();

            List<Element> list = new List<Element>();
            foreach (Element element in elements)
            {
                list.Add(element);
            }

            return list;
        }

        #endregion
    }
}
