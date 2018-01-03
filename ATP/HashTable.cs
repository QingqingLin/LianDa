using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CBTC
{
    class HashTable
    {
        public Hashtable ht = new Hashtable();
        public Hashtable ht_1 = new Hashtable();
        public Hashtable ht_2 = new Hashtable();    

        #region 区段距离哈希表
        public void sectionHashTable()
        {
            ht_1.Add("1-1", 100);
            ht_1.Add("1-2", 80);
            ht_1.Add("2-1", 40);
            ht_1.Add("2-2", 20);
            ht_1.Add("1", 100);
            ht_1.Add("2", 20);
        }
        #endregion

        #region 应答器道岔标号哈希表
        public void switchHashTable()
        {
            ht.Add("111-1", "1-0");
            ht.Add("111-2", "3-5");
            ht.Add("111-3", "1-0");
            ht.Add("111-4", "3-5");
            ht.Add("110-1", "4-6");
            ht.Add("110-2", "2-3");
            ht.Add("110-3", "4-6");
            ht.Add("110-4", "2-3");
            ht.Add("204-1", "2-4");
            ht.Add("204-2", "2-4");
            ht.Add("204-3", "2-4");
            ht.Add("207-1", "1-1");
            ht.Add("207-2", "1-1");
            ht.Add("207-3", "1-1");
            ht.Add("106-1", "6-8");
            ht.Add("106-2", "6-8");
            ht.Add("106-3", "6-8");
            ht.Add("118-1", "8-10");
            ht.Add("118-2", "10-2");
            ht.Add("118-3", "8-10");
            ht.Add("118-4", "10-2");
            ht.Add("119-1", "7-9");
            ht.Add("119-2", "9-11");
            ht.Add("119-3", "7-9");
            ht.Add("119-4", "9-11");
            ht.Add("107-1", "5-7");
            ht.Add("107-2", "5-7");
            ht.Add("107-3", "5-7");
            ht.Add("405-1", "1-12");
            ht.Add("405-2", "1-12");
            ht.Add("405-3", "1-12");
            ht.Add("408-1", "2-13");
            ht.Add("408-2", "2-13");
            ht.Add("408-3", "2-13");
        }
        #endregion

        #region MA终点是否道岔哈希表
        public void MAIsSwitch()
        {
            ht_2.Add("111", "111DG");
            ht_2.Add("110", "110DG");
            ht_2.Add("204", "204DG");
            ht_2.Add("207", "207DG");
            ht_2.Add("106", "106DG");
            ht_2.Add("118", "118DG");
            ht_2.Add("119", "119DG");
            ht_2.Add("107", "107DG");
            ht_2.Add("405", "405DG");
            ht_2.Add("408", "408DG");
        }
        #endregion
    }
}
