using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudPatternUnitTests.CommandAndQueryREsponsiblitySegregationCQRS
{
    public class CqrsService
    {
        private readonly InMemoryRepository _repository;

        public CqrsService(InMemoryRepository repository) { _repository = repository; }

        public void CreateEntity(Entity entity)
        {
            _repository.Add(entity);
        }

        public Entity GetEntity(int id) { return _repository.GetById(id); }
    }
}
