using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Services
{
    public class SqlPropertyInfos : IPropertyInfos
    {
        private readonly PropertyDbContext _context;
        public SqlPropertyInfos(PropertyDbContext context)
        {
            _context = context;
        }
        public void Add(PropertyDetails property)
        {
            _context.PropertyInfos.Add(property);
            _context.SaveChanges();
        }
        public void ChangeState(Guid id)
        {
            var property = _context.PropertyInfos.Where(x => x.Id.Equals(id)).FirstOrDefault();
            property.IsAvailable = false;
            _context.PropertyInfos.Update(property);

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var property=_context.PropertyInfos.Where(x=>x.Id.Equals(id)).FirstOrDefault();
            _context.PropertyInfos.Remove(property);
            _context.SaveChanges();
        }
        public PropertyDetails get(Guid id)
        {
            return _context.PropertyInfos.Where(_ => _.Id.Equals(id)).FirstOrDefault();
        }
        public List<PropertyDetails> getAll()
        {
            return _context.PropertyInfos.ToList();
        }

    }
}
