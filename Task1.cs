using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка 
        public string Data;
    }

    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Dictionary<ListNode, int> dictionary = new Dictionary<ListNode, int>(Count);
            Count = 0;

            for (ListNode current = Head; current != null; current = current.Next)
                dictionary.Add(current, Count++);

            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.WriteLine(Count);

                foreach (var node in dictionary.Keys)
                {
                    sw.WriteLine(node.Data);

                    if (node.Rand != null)
                        sw.WriteLine(dictionary[node.Rand]);
                    else
                        sw.WriteLine(-1);
                }
            }
        }

        public void Deserialize(FileStream s)
        {

            Head = new ListNode();
            ListNode current = Head;
            List<ListNode> list = new List<ListNode>(Count);
            List<int> indexOfRandElements = new List<int>(Count);
            Count = 0;

            using (StreamReader sr = new StreamReader(s))
            {
                string line;
                int counter = int.Parse(sr.ReadLine());

                while ((line = sr.ReadLine()) != null)
                {
                    if (Count >= counter)
                        break;

                    Count++;
                    current.Data = line;
                    ListNode next = new ListNode();
                    current.Next = next;
                    list.Add(current);
                    next.Prev = current;
                    current = next;
                    indexOfRandElements.Add(int.Parse(sr.ReadLine()));
                }
            }

            Tail = current.Prev;
            Tail.Next = null;

            for (int i = 0; i < list.Count; i++)
            {
                if (indexOfRandElements[i] == -1)
                    continue;
                else
                    list[i].Rand = list[indexOfRandElements[i]];
            }
        }
    }
}