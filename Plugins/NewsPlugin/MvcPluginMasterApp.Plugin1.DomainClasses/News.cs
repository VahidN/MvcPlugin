using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CommonEntities;

namespace MvcPluginMasterApp.Plugin1.DomainClasses
{
    public class News
    {
        [ScaffoldColumn(false)]
        public int Id { set; get; }

        [Display(ResourceType = typeof(MvcPluginMasterApp.Plugin1.Resources.Home_Index_cshtml),
                 Name = "Title")]
        public string Title { set; get; }

        [Display(ResourceType = typeof(MvcPluginMasterApp.Plugin1.Resources.Home_Index_cshtml),
                 Name = "Body")]
        public string Body { set; get; }

        [ScaffoldColumn(false)]
        [ForeignKey("UserId")]
        public virtual User User { set; get; }

        [ScaffoldColumn(false)]
        public int UserId { set; get; }
    }

    public class NewsConfig : EntityTypeConfiguration<News>
    {
        public NewsConfig()
        {
            this.ToTable("Plugin1_News");
            this.HasKey(news => news.Id);
            this.Property(news => news.Title).IsRequired().HasMaxLength(500);
            this.Property(news => news.Body).IsOptional().IsMaxLength();
        }
    }
}