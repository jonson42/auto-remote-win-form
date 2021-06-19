using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSeleniumFromExcel.Utils
{
    public static class SplitList
    {
        public static List<List<List<string>>> SplitListProcess(List<List<string>> locations, int nSize = 30)
        {
            var list = new List<List<List<string>>>();

            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }

            return list;
        }

        public static List<List<List<string>>> SplitWithNumberThread(List<List<string>> listTotal, int nSize = 1)
        {
            var numberOfListSub = listTotal.Count / nSize;
            var numbExist = listTotal.Count % nSize;
            var list = new List<List<List<string>>>();

            for(int i = 0; i < nSize; i++)
            {
                var listSub = new List<List<string>>();
                foreach(var item in listTotal.ToList())
                {
                   if(listSub.Count< numberOfListSub)
                    {
                        listSub.Add(item);
                        listTotal.Remove(item);
                    } 
                }
                list.Add(listSub);
            }
            if (listTotal.Count > 0)
            {
                foreach(var item in listTotal.ToList())
                {
                    list[nSize - 1].Add(item);
                    listTotal.Remove(item);
                }
            }

            return list;
        }

        public static void AddHeaderForList(List<List<List<string>>> listInput)
        {
            int index = 0;
            var itemTemp = new List<string>();
            foreach(var item in listInput)
            {
                if (index == 0)
                {
                    itemTemp = item[0];
                }
                else
                {
                    item.Reverse();
                    item.Add(itemTemp);
                    item.Reverse();
                }
                index++;
            }
        }
    }
}
