using Acme.Tarot.Cards;
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
            //...
            //});

            builder.Entity<TarotCard> (b => {
                b.ToTable (TarotConsts.DbTablePrefix + "TarotCards", TarotConsts.DbSchema);
                b.ConfigureByConvention (); //auto configure for the base class props
                //...
                b.Property (x => x.Name).IsRequired ().HasMaxLength (TarotCardConsts.MaxNameLength);
                b.Property (x => x.Descript).HasMaxLength (TarotCardConsts.MaxNameLength);
                b.Property (x => x.CardFrontImgUrl).HasMaxLength (TarotCardConsts.MaxURLLength);
                b.Property (x => x.CardContentText).HasMaxLength (TarotCardConsts.MaxNameLength);
                b.HasMany (x => x.TarotCardCollections)
                    .WithMany (x => x.TarotCards)
                    .UsingEntity<CardBindCollection> (
                        j => j
                        .HasOne (co => co.TarotCardCollection)
                        .WithMany (cb => cb.CardBindCollections)
                        .HasForeignKey (co => co.TarotCardCollecitonId),
                        j => j
                        .HasOne (c => c.TarotCard)
                        .WithMany (cb => cb.CardBindCollections)
                        .HasForeignKey (c => c.TarotCardId),
                        j => j.HasKey (t => new { t.TarotCardId, t.TarotCardCollecitonId })
                    );
            });

            builder.Entity<TarotCardCollection> (b => {
                b.ToTable (TarotConsts.DbTablePrefix + "TarotCardCollections", TarotConsts.DbSchema);
                b.ConfigureByConvention (); //auto configure for the base class props
                // ...
                b.Property (x => x.Name).IsRequired ().HasMaxLength (TarotCardCollectionConsts.MaxDescriptLength);
                b.Property (x => x.Descript).HasMaxLength (TarotCardCollectionConsts.MaxDescriptLength);
                b.Property (x => x.CardFrontImgUrl).HasMaxLength (TarotCardCollectionConsts.MaxURLLength).HasDefaultValue ("123");
                b.Property (x => x.CardBackImgUrl).HasMaxLength (TarotCardCollectionConsts.MaxURLLength).HasDefaultValue ("123");
            });

        }
    }
}