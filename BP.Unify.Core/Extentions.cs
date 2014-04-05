using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
    static class Extentions
    {
        [Extension()]
        public static bool Demote<T>(List<T> items, int itemIndex)
        {
            T item;
            if (itemIndex < items.Count - 1)
            {
                item = items[itemIndex];
                items.RemoveAt(itemIndex);
                items.Insert(itemIndex + 1, item);
                return true;
            }
            else
            {
                return false;
            }
        }

        [Extension()]
        public static bool Promote<T>(List<T> items, int itemIndex)
        {
            T item;
            if (itemIndex > 0)
            {
                item = items[itemIndex];
                items.RemoveAt(itemIndex);
                items.Insert(itemIndex - 1, item);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
