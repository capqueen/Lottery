﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Lottery
{
    public class GenrateEmployees
    {
        string[] Name = new string[] { "一", "二","三","四","五","六","七","八","九","哈","" };
        string[] FirstName = new string[] { "黄","张","李","陈","史","章"};
        Random r = new Random();
        public void Genrate()
        {
            var employees = new List<Employee>();
            for (var i = 0; i < 3000; i++)
            {
                employees.Add(new Employee { 
                    Id = i,
                    Name = GetName(),
                    Zone = Random(1, 4),
                    Age = Random(1, 11)
                });
            }
           // var s = JsonConvert.SerializeObject(employees);
            using (var file = File.OpenWrite("D:/name.json"))
            {
                using (var writer = new StreamWriter(file))
                {
                    employees.ForEach(item => {
                        writer.WriteLine("," + JsonConvert.SerializeObject(item).ToLower()); 
                    });
                    
                }
            }

            using (var file = File.OpenWrite("D:/name.csv"))
            {
                using (var writer = new StreamWriter(file, System.Text.Encoding.UTF8))
                {
                    employees.ForEach(item =>
                    {
                        writer.WriteLine(string.Format("'{0},{1},{2}", item.Id, item.Name, item.Age));
                    });

                }
            }

            for(var i = 1; i < 4; i ++)
            {
                var list = employees.Where(item => item.Zone == 1).ToList();
                using (var file = File.OpenWrite("D:/name" + i + ".json" ))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        list.ForEach(item =>
                        {
                            writer.WriteLine("" + JsonConvert.SerializeObject(item).ToLower());
                        });
                    }
                }
            }
        }

        public string GetName()
        {
            return FirstName[Random(0, FirstName.Length)] + Name[Random(0, Name.Length)];
        }

        public int Random(int start, int end)
        {
            //var rand = new Random();
            return r.Next(start, end);
        }
    }


    public class Employee
    {
        /// <summary>
        /// 工号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Depart { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 是否获奖
        /// </summary>
        public bool Prized { get; set; }
        /// <summary>
        /// 分区
        /// </summary>
        public int Zone { get; set; }
    }


    public class SortArray
    {
        public void Main()
        {
            var list = new List<string>();
            list.Add("123");
            list.Add("321");
            list.Add("123");
            var result = GetSort("", list);

            result.ToList().ForEach(Console.WriteLine);
        }


        public IEnumerable<string> GetSort(string current, List<string> data)
        {
            var loc = current.Length;

            if (data.Count > loc)
            {
                var array = data[loc].ToArray();
                for (var i = 0; i < array.Length; i++)
                {
                    var it = GetSort(current + array[i], data).GetEnumerator();
                    while (it.MoveNext())
                    {
                        yield return it.Current;
                    }
                }
            }
            else
            {
                yield return current;
            }
        }
    }
}