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
            List<ListNode> list = new List<ListNode>(Count);

            for (ListNode current = Head; current != null; current = current.Next)
                list.Add(current);

            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.WriteLine(Count);

                foreach (ListNode node in list)
                    sw.WriteLine($"{node.Data}:{list.IndexOf(node.Rand)}");
            }
        }

        public void Deserialize(FileStream s)
        {

            Head = new ListNode();
            ListNode current = Head;
            List<ListNode> list = new List<ListNode>();

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
                }
            }

            Tail = current.Prev;
            Tail.Next = null;

            foreach (ListNode listNode in list)
            {
                listNode.Rand = list[Convert.ToInt32(listNode.Data.Split(':')[1])];
                listNode.Data = listNode.Data.Split(':')[0];
            }
        }
    }
}
