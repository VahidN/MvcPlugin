using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CommonEntities;

namespace MvcPluginMasterApp.Plugin2.DomainClasses
{
    public class Article
    {
        public int Id { set; get; }

        public string Title { set; get; }

        public string Body { set; get; }


        [ForeignKey("UserId")]
        public virtual User User { set; get; }
        public int UserId { set; get; }
    }

    public class ArticlesConfig : EntityTypeConfiguration<Article>
    {
        public ArticlesConfig()
        {
            this.ToTable("Plugin2_Articles");
            this.HasKey(article => article.Id);
            this.Property(article => article.Title).IsRequired().HasMaxLength(500);
            this.Property(article => article.Body).IsOptional().IsMaxLength();
        }
    }
}