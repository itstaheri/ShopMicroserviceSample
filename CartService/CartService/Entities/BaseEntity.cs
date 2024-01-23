using System.ComponentModel.DataAnnotations;

namespace CartService.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }
        public void Delete()=>IsDeleted = true;
        public void UnDelete()=>IsDeleted = false;

        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }


        
    }
}
