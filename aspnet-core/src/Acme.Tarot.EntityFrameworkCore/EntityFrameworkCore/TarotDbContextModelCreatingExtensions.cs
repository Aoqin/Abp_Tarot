using Acme.Tarot.Cards;
using Acme.Tarot.Domain.Cards;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.Tarot.EntityFrameworkCore {
    public static class TarotDbContextModelCreatingExtensions {
        public static void ConfigureTarot (this ModelBuilder builder) {
            Check.NotNull (builder, nameof (builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(TarotConsts.DbTablePrefix + "YourEntities", TarotConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
            builder.Entity<TarotCard> (b => {
                b.ToTable (TarotConsts.DbTablePrefix + "TarotCards", TarotConsts.DbSchema);
                b.ConfigureByConvention (); //auto configure for the base class props
                //...
                b.Property (x => x.Name).IsRequired ().HasMaxLength (TarotCardConsts.MaxNameLength);
                b.Property (x => x.Descript).HasMaxLength (TarotCardConsts.MaxNameLength);
                b.Property (x => x.CardFrontImgUrl).HasMaxLength (TarotCardConsts.MaxURLLength);
                b.Property (x => x.CardContentText).HasMaxLength (TarotCardConsts.MaxNameLength);
            });
        }
    }
}