namespace NHibernateWebPersistenceExample.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
    }
}