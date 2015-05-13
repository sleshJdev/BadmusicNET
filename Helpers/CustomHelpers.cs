using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Helpers
{
    public class CustomHelpers
    {
        private const int accuracy = 2;
        public static  string FormattingStr(string size)
        {
            int count = 0,
                leng = size.Length;
            if (leng > 3)
            {

                while ((size[count] != ',') && (count < leng))
                    count++;

                size = size.Remove(count + accuracy, leng - (count + accuracy) - 1);
            }

            return size;
        }
    }
}