﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace VBF.Compilers.Scanners.Generator
{
    public class CompactCharManager
    {
        private ushort[] m_compactCharTable;
        private readonly ushort m_minIndex = 1;
        private readonly ushort m_maxIndex;

        public CompactCharManager(ushort[] compactCharTable, ushort maxIndex)
        {
            m_compactCharTable = compactCharTable;
            Debug.Assert(maxIndex >= m_minIndex);
            m_maxIndex = maxIndex;
        }

        public int GetCompactClass(Char c)
        {
            ushort compactClass = m_compactCharTable[(int)c];

            //0 is an invalid compact class
            Debug.Assert(compactClass >= m_minIndex);
            return (int)compactClass;
        }

        public HashSet<char>[] CreateCompactCharMapTable()
        {
            HashSet<char>[] result = new HashSet<char>[m_maxIndex + 1];
            result[0] = null; //occupy the 0 position for invalid index
            for (int i = m_minIndex; i <= m_maxIndex; i++)
            {
                result[i] = new HashSet<char>();
            }

            for (int i = Char.MinValue; i <= Char.MaxValue; i++)
            {
                int index = m_compactCharTable[i];

                if (index >= m_minIndex)
                {
                    result[index].Add((char)i);
                }
            }

            return result;
        }

        public int MaxClassIndex
        {
            get
            {
                return m_maxIndex;
            }
        }

        public int MinClassIndex
        {
            get
            {
                return m_minIndex;
            }
        }
    }
}
