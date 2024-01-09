namespace CloudPatternUnitTests.CommandAndQueryREsponsiblitySegregationCQRS
{
    public class InMemoryRepository
    {
        private readonly List<Entity> entities = new List<Entity>();

        public void Add(Entity entity) => entities.Add(entity);
        public Entity GetById(int id) => entities.FirstOrDefault(e => e.Id == id);
    }
}
