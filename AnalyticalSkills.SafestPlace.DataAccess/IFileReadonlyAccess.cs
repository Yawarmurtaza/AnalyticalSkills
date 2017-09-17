using System.Collections.Generic;

namespace AnalyticalSkills.SafestPlace.DataAccess
{
    /// <summary> Allows access to the file system. </summary>
    public interface IFileReadonlyAccess
    {
        IEnumerable<string> ReadAllLines(string path);
    }
}