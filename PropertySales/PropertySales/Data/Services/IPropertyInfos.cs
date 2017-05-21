using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services
{
    public interface IPropertyInfos
    {
        List<PropertyDetails> getAll();

        PropertyDetails get(Guid id);

        void ChangeState(Guid id);
    }
}
