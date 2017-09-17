using System.Collections.Generic;
using System.IO;

namespace AnalyticalSkills.SafestPlace.DataAccess
{
    /// <summary> Allows access to the file system. </summary>
    public class FileReadOnlyAccess : IFileReadonlyAccess
    {
        public IEnumerable<string> ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
            // yield return "1, 4, 50,50,50,10,10,10,30,30,30,90,90,90";
            //yield return "1, 1, 3,3,3";
        }
    }
}