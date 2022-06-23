using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI1_CORE
{
    /// <summary>
    /// Class selects all elements of given categories that intersect with given solid.
    /// </summary>
    public class IntersectionWithSolidSelection
    {
        #region constructor

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="IntersectionWithElementSelection"/> class.
        /// </summary>
        public IntersectionWithSolidSelection()
        {

        }

        #endregion

        #region public methods

        /// <summary>
        ///  Main method of the Selection class.
        /// </summary>
        /// <param name="doc">The current document.</param>
        /// <param name="element">The element for intersiction.</param>
        /// <param name="categories">The categories of elements for intersection.</param>
        /// <returns></returns>
        public List<Element> Selection(Document doc, Element element, params BuiltInCategory[] categories)
        {
            GeometryElement geometryElement = element.get_Geometry(new Options());
            Solid solid = null;
            foreach (GeometryObject geometryObject in geometryElement)
            {
                solid = geometryObject as Solid;
                if (solid != null)
                {
                    break;
                }
            }

            List<Element> elements = new List<Element>();
            foreach (BuiltInCategory category in categories)
            {
                var elementsOfCategory = new FilteredElementCollector(doc)
                    .OfCategory(category)
                    .WhereElementIsNotElementType()
                    .WherePasses(new ElementIntersectsSolidFilter(solid))
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
