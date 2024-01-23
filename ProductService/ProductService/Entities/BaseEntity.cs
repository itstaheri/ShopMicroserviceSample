namespace ProductService.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
        public void Delete()=> IsDeleted = true;
        public void UnDelete()=> IsDeleted = false;

        public Guid Id { get;private set; }
        public DateTime CreateDate { get;private set; }
        public bool IsDeleted { get;private set; }
    }
}
