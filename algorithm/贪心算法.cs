using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algorithm
{
    internal class 贪心算法
    {
        /*===========贪心算法(集合覆盖问题)==============
               贪心算法介绍#
               贪婪算法(贪心算法)是指在对问题进行求解时，在每一步选择中都采取最好或者最优(即最有利)的选择，
               从而希望能够导致结果是最好或者最优的算法
               贪婪算法所得到的结果不一定是最优的结果(有时候会是最优解)，但是都是相对近似(接近)最优解的结果

               应用场景-集合覆盖问题#
               问题详情#
   　　          假设存在下面需要付费的广播台，以及广播台信号可以覆盖的地区。
                 如何选择最少的广播台，让所有的地区都可以接收到信号

               思路分析#
                   目前并没有算法可以快速计算得到准备的值， 使用贪婪算法，则可以得到非常接近的解，并且效率高。
                   选择策略上，因为需要覆盖全部地区的最小集合:
                   遍历所有的广播电台, 找到一个覆盖了最多未覆盖的地区的电台(此电台可能包含一些已覆盖的地区，但没有关系）
                   将这个电台加入到一个集合中(比如 ArrayList), 想办法把该电台覆盖的地区在下次比较时去掉。
                   重复第 1 步直到覆盖了全部的地区
                */

        [Test]
        public void Test()
        {
            //假设存在下面需要付费的广播台，以及广播台信号可以覆盖的地区。 如何选择最少的广播台，让所有的地区都可以接收到信号
            //创建广播电台,放入到Map
            Dictionary<string, List<string>> broadcasts = new Dictionary<string, List<string>>();

            //将各个电台放入到broadcasts
            List<string> hashSet1 = new List<string>
            {
                "北京",
                "上海",
                "天津"
            };

            List<string> hashSet2 = new List<string>
            {
                "广州",
                "北京",
                "深圳"
            };

            List<string> hashSet3 = new List<string>
            {
                "成都",
                "上海",
                "杭州"
            };

            List<string> hashSet4 = new List<string>
            {
                "上海",
                "天津"
            };

            List<string> hashSet5 = new List<string>
            {
                "杭州",
                "大连"
            };

            //加入到字典集合
            broadcasts.Add("Radio1", hashSet1);
            broadcasts.Add("Radio2", hashSet2);
            broadcasts.Add("Radio3", hashSet3);
            broadcasts.Add("Radio4", hashSet4);
            broadcasts.Add("Radio5", hashSet5);

            //allAreas 存放所有的地区
            List<string> allAreas = new List<string>
            {
                "北京",
                "上海",
                "天津",
                "广州",
                "深圳",
                "成都",
                "杭州",
                "大连"
            };

            //创建ArrayList, 存放选择的电台集合
            List<string> selList = new List<string>();

            //定义一个临时的集合， 在遍历的过程中，存放遍历过程中的电台覆盖的地区和当前还没有覆盖的地区的交集
            List<string> tempList = new List<string>();

            //定义给maxKey ， 保存在一次遍历过程中，能够覆盖最大未覆盖的地区对应的电台的key
            //如果maxKey 不为null , 则会加入到 selList
            string maxKey = null;

            // 如果allAreas 不为0, 则表示还没有覆盖到所有的地区
            // 每进行一次while（每次把maxkey电台放入selList后）,需要清空maxkey电台中的地区
            while (allAreas.Count != 0)
            {
                maxKey = null;
                //遍历 broadcasts, 取出对应key  这个for是用来找到最优的maxkey的
                foreach (string key in broadcasts.Keys)
                {
                    // 这里获得的key 应该是k1 k2

                    //每进行一次for（每次往temp临时集合中存完数据 下次再使用前要把临时集合中的数据清空）
                    tempList.Clear();
                    //当前这个key能够覆盖的地区 把地区放到areas中 通过key 取value 也就是地区
                    List<string> areas = broadcasts[key];
                    //把地区放到tempList中
                    tempList.AddRange(areas);
                    //求出tempList和allAreas 集合的交集, 交集会赋给 tempList
                    tempList = tempList.Intersect(allAreas).ToList();
                    //如果当前这个集合包含的未覆盖地区的数量，比maxKey指向的集合地区还多
                    //就需要重置maxKey   意思就是把地区最多的电台赋给maxkey
                    // tempList.Count > broadcasts[maxKey].Count 体现出贪心算法的特点,每次都选择最优的
                    // tempList.Count > 0 说明还未覆盖完所有地区 因为是和 allAreas还有交集
                    if (tempList.Count > 0 && (maxKey == null || tempList.Count > broadcasts[maxKey].Count))
                    {
                        maxKey = key;
                    }
                }
                //maxKey != null, 就应该将maxKey 加入selList
                if (maxKey != null)
                {
                    selList.Add(maxKey);
                    //将maxKey指向的广播电台覆盖的地区，从 allAreas 去掉
                    allAreas.RemoveAll(p => broadcasts[maxKey].Contains(p));
                }
            }
            //得到的选择结果是Radio1,Radio2,Radio3,Radio5
            Console.WriteLine("得到的选择结果是" + string.Join(",", selList));
        }
    }
}