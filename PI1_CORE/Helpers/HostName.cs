using Autodesk.Revit.DB;

namespace PI1_CORE.Helpers
{
    public static class HostName
    {
        public static string GetHostName(Category category)
        {
            ElementId categoryId = category.Id;
            BuiltInCategory biCategory = (BuiltInCategory)categoryId.IntegerValue;

            string hostName = string.Empty;
            switch (biCategory)
            {
                case BuiltInCategory.OST_StructuralColumns:
                    return hostName = "Несущая колонна";
                case BuiltInCategory.OST_StructuralFraming:
                    return hostName = "Несущий каркас";
                case BuiltInCategory.OST_Floors:
                    return hostName = "Пол";
                case BuiltInCategory.OST_Walls:
                    return hostName = "Стена";
                case BuiltInCategory.OST_StructuralFoundation:
                    return hostName = "Фундамент несущей конструкции";
                case BuiltInCategory.OST_Stairs:
                    return hostName = "Лестницы";
                default:
                    return null;
            }
        }
    }
}
