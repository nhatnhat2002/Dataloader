using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class KMP
    {
        public void KMPSearch(string pat, string txt, LinkedList result)
        {
            int M = pat.Length;
            int N = txt.Length;

            int[] lps = new int[M];

            int j = 0;
            LPSArray(pat, M, lps);

            int i = 0;

            while ((N - i) >= (M - j))
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    result.addLast(i - j); 
                    j = lps[j - 1];
                }
                else if (i < N && pat[j] != txt[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
        }
        public class Node
        {
            public int element;
            public Node next;
            public Node(int e, Node n)
            {
                element = e;
                next = n;
            }
        }

        public class LinkedList
        {
            public Node head;
            private Node tail;
            private int size;

            public LinkedList()
            {
                head = null;
                tail = null;
                size = 0;
            }

            public int length()
            {
                return size;
            }

            public bool isEmpty()
            {
                return size == 0;
            }

            public void addLast(int e)
            {
                Node newest = new Node(e, null);
                if (isEmpty())
                    head = newest;
                else
                    tail.next = newest;
                tail = newest;
                size = size + 1;
            }
        }

        // Tính LPS
        public void LPSArray(string pat, int M, int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0;

            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }
    }

}
