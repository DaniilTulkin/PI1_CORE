using Autodesk.Revit.DB;

namespace PI1_CORE
{
    public class Geometry
    {
        public static Curve CurveOffset(Curve curve, double distance, bool outside=false)
        {
            XYZ vectorZ = XYZ.BasisZ;
            if (outside)
            {
                vectorZ = new XYZ(0, 0, -1);
            }

            XYZ point_1 = curve.GetEndPoint(0);
            XYZ point_2 = curve.GetEndPoint(1);
            XYZ vector = point_1 - point_2;
            XYZ vectorOffset = vector.CrossProduct(vectorZ).Normalize();
            XYZ distanceOffset = vectorOffset.Multiply(distance);

            XYZ startPoint = point_1.Add(distanceOffset);
            XYZ endPoint = point_2.Add(distanceOffset);

            return Line.CreateBound(startPoint, endPoint);
        }
    }
}
