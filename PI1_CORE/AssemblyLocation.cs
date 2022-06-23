using System.Diagnostics;

namespace PI1_CORE
{
    public static class AssemblyLocation
    {
        #region public methods

        public static string GetAssemblyLocation()
        {
            StackTrace stackTrace = new StackTrace();
            var assembly = stackTrace.GetFrame(2).GetMethod().DeclaringType.Assembly;
            return assembly.Location;
        }

        #endregion
    }
}
