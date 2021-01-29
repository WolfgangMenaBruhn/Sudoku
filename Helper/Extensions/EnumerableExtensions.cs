﻿using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Helper.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> items, int partitionSize)
        {
            return items.Select((item, inx) => new { item, inx })
                .GroupBy(x => x.inx / partitionSize)
                .Select(g => g.Select(x => x.item));
        }
    }
}