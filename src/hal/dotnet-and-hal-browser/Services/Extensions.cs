using System.Collections.Generic;
using System.Linq;

namespace Hateoas.Services
{
    public static class Extensions
    {
        public static int GetNextId<T>(this IDictionary<int, T> self)
        {
            if (self.Count == 0)
            {
                return 0;
            }

            return self.Keys.Max(id => id) + 1;
        }
    }
}
