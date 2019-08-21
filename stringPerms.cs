using System;
using System.Collections.Generic;
using System.Linq;

namespace stringPerms
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "abcde".ToCharArray();

            var perms = new List<string>();
            perms.Add(input[0].ToString());
            perms.Add(input[1].ToString());
            perms.Add($"{input[0]}{input[1]}");
            perms.Add($"{input[1]}{input[0]}");
            for(int i = 2; i < input.Length; i++)
            {
                perms.Add(input[i].ToString());
                // if(i >= input.Length - 1)
                // {
                //     break;
                // }
                var newPerms = new List<string>();
                foreach(var perm in perms)
                {
                    for(int s=0; s< perm.Length; s++)
                    {
                        newPerms.Add(perm.Insert(s,input[i].ToString()));
                    }
                }
                perms.AddRange(newPerms);
            }

            foreach(var perm in perms.OrderBy(x => x.Length).ThenBy(x => x))
            {
                Console.WriteLine(perm);
            }
        }
    }
}
