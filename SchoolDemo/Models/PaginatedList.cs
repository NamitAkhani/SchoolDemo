using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDemo.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int Totalpages { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            Totalpages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool previouspage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool nextpage
        {
            get
            {
                return (PageIndex < Totalpages);
            }
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,int pageIndex , int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize); 
        }
    }
}
