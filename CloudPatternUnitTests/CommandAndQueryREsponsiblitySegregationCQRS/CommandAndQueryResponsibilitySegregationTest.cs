namespace CloudPatternUnitTests.CommandAndQueryREsponsiblitySegregationCQRS
{
    /// <summary>
    /// Segregate operations that read data from operations that update data
    /// by using separate interfaces.This pattern can maximize performance, scalability, and security; 
    /// support evolution of the system over time through higher flexibility; and prevent update 
    /// commands from causing merge conflicts at the domain level
    /// </summary>
    public class CommandAndQueryResponsibilitySegregationTest
    {
        [Fact]
        public void ShouldCreateAndRetrieveEntity()
        {
            var repository = new InMemoryRepository();
            var cqrsService = new CqrsService(repository);
            var newEntity = new Entity { Id = 1, Name = "Test Entity" };

            cqrsService.CreateEntity(newEntity);

            var retrieveEntity = cqrsService.GetEntity(1);

            Assert.NotNull(retrieveEntity);
            Assert.Equal("Test Entity", retrieveEntity.Name);
        }
    }
}
