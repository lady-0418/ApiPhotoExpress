using Microsoft.EntityFrameworkCore;

namespace ApiPhotoExpress.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options)
         : base(options)
        {
        }
        public DbSet<EventosModel> Evento { get; set; }
    }
}
