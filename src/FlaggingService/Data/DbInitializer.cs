
using System.Drawing;
using FlaggingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<FlaggingDbContext>());
    }

    private static void SeedData(FlaggingDbContext? context)
    {
        context?.Database.Migrate();

        if (context != null)
        {
            if (context.Flagging.Any())
            {
                Console.WriteLine("Aleady have data - no need to seed");
                return;
            }
            SeedFlaggerData(context);
            SeedFlagData(context);
            SeedEstablishmentTypeData(context);
            SeedEstablishmentData(context);
            SeedFlaggingData(context);
        }
    }

    private static void SeedEstablishmentTypeData(FlaggingDbContext context)
    {
        var estTypes = new List<EstablishmentType>()
        {
            // Add your seed data here
            new EstablishmentType
            {
               Id = Guid.Parse("8c76781d-0318-4b82-98f6-9afeecb8ec8f"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Description = "Restaurant",
               Name = "Restaurant",
               Status = Status.Active
            },
            new EstablishmentType
            {
               Id = Guid.Parse("21f67281-a11d-4e81-bd80-d21918551689"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Description = "Club",
               Name = "Club",
               Status = Status.Active
            },
            new EstablishmentType
            {
               Id = Guid.Parse("0850cb6d-0a38-4618-b8b8-5e1693997e1a"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Description = "Resort",
               Name = "Resort",
               Status = Status.Active
            },
            new EstablishmentType
            {
               Id = Guid.Parse("0f4d73b2-41e4-4f10-ad55-55be0d113fff"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Description = "Resort",
               Name = "REsort",
               Status = Status.Active
            }
        };

        context.EstablishmentType.AddRange(estTypes);
        context.SaveChanges();
    }

    private static void SeedEstablishmentData(FlaggingDbContext context)
    {
        var establishments = new List<Establishment>()
        {
            // Add your seed data here
            new Establishment
            {
               Id = Guid.Parse("efe24fe5-b6e4-45e6-917a-73125daa58df"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Address = "900 Koedoeber road",
               ContactIds = null,
               Name = "Forest Manor",
               Owner = "Macprop",
               Status = Status.Active,
               TypeId = Guid.Parse("0f4d73b2-41e4-4f10-ad55-55be0d113fff")
            },
            new Establishment
            {
               Id = Guid.Parse("82f64570-8944-411a-861f-4503669d5d76"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Address = "900 Koedoeber road",
               ContactIds = null,
               Name = "Forest Manor",
               Owner = "Macprop",
               Status = Status.Active,
               TypeId = Guid.Parse("8c76781d-0318-4b82-98f6-9afeecb8ec8f")
            }
        };

        context.Establishments.AddRange(establishments);
        context.SaveChanges();
    }

    private static void SeedFlaggerData(FlaggingDbContext context)
    {
        var users = new List<Flagger>()
        {
            // Add your seed data here
            new Flagger
            {
               Id = Guid.Parse("2c912e86-72a0-4718-aa41-128ac771d954"),
               CreatedOn = DateTime.UtcNow.AddDays(-2),
               Username = "VeronicaN"
            },
            new Flagger
            {
               Id = Guid.Parse("7923de37-799f-4d62-b47e-f82bcb4066d0"),
               Username = "KhungileN",
               CreatedOn = DateTime.UtcNow.AddDays(-1)
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }

    private static void SeedFlagData(FlaggingDbContext context)
    {
        var flags = new List<Flag>()
        {
            // Add your seed data here
            new Flag
            {
               Id = Guid.Parse("3efb5209-a1f3-460b-bbad-3d1e024e6be1"),
               Color = Color.Green.ToString(),
               CreatedOn = DateTime.UtcNow.AddDays(2),
               Description = "Okay and good vibes",
                Significance = Significance.Okay
            },
            new Flag
            {
               Id = Guid.Parse("c90fa4f3-b358-44df-8d32-3e31126d38de"),
               Color = Color.Gold.ToString(),
               CreatedOn = DateTime.UtcNow.AddDays(5),
               Description = "This place is perfect and checks all the boxes",
                Significance = Significance.PlaceToBe
            },
            new Flag
            {
               Id = Guid.Parse("5262cae3-7e8c-4408-a876-7efac5cbe2d8"),
               Color = Color.Red.ToString(),
               CreatedOn = DateTime.UtcNow.AddDays(10),
               Description = "Everything is bad, not recomended at all",
                Significance = Significance.Boring
            },
            new Flag
            {
               Id = Guid.Parse("af20d2b8-dc34-467a-af7b-bcf2c3881bc2"),
               Color = Color.Yellow.ToString(),
               CreatedOn = DateTime.UtcNow.AddDays(1),
               Description = "Nothing Wow! But it's bearable!",
                Significance = Significance.Average
            }
        };

        context.Flags.AddRange(flags);
        context.SaveChanges();
    }

    private static void SeedFlaggingData(FlaggingDbContext context)
    {
        var flaggings = new List<Flagging>()
        {
            // Add your seed data here
            new Flagging
            {
               FlagId = Guid.Parse("3efb5209-a1f3-460b-bbad-3d1e024e6be1"),
               EstablishmentId = Guid.Parse("efe24fe5-b6e4-45e6-917a-73125daa58df"),
               FlaggedBy = Guid.Parse("2c912e86-72a0-4718-aa41-128ac771d954"),
               FlaggedOn = DateTime.UtcNow.AddDays(2),
               Comments = "Initial data from seed..."
            }
        };

        context.Flagging.AddRange(flaggings);
        context.SaveChanges();
    }
}
