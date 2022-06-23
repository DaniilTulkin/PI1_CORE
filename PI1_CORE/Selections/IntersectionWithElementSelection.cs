using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace PI1_CORE
{
    /// <summary>
    /// Class selects all elements of given categories that intersect with given element.
    /// </summary>
    public class IntersectionWithElementSelection
    {
        #region constructor

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="IntersectionWithElementSelection"/> class.
        /// </summary>
        public IntersectionWithElementSelection()
        {

        }

        #endregion

        #region public methods

        /// <summary>
        /// Main method of the Selection class
        /// </summary>
        /// <param name="doc">The current document.</param>
        /// <param name="element">The element for intersiction.</param>
        /// <param name="categories">The categories of elements for intersection.</param>
        /// <returns></returns>
        public List<Element> Selection(Document doc, Element element, params BuiltInCategory[] categories)
        {           
            List<Element> elements = new List<Element>();
            foreach (BuiltInCategory category in categories)
            {
                var elementsOfCategory = new FilteredElementCollector(doc)
                    .OfCategory(category)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementIntersectsElementFilter(element))
                    .ToElements();

                foreach (Element el in elementsOfCategory)
                {
                    elements.Add(el);
                }
            }

            return elements;
        }

        #endregion
    }
}
