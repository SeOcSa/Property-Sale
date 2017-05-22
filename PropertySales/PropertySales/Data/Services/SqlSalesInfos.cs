using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public class SqlSalesInfos: ISales
    {
        private readonly PropertyDbContext _context;

        public SqlSalesInfos(PropertyDbContext context)
        {
            _context = context;
        }

        public void Add(SaleDetails sale)
        {
            _context.SaleInfo.Add(sale);
            _context.SaveChanges();
        }
    }
}
