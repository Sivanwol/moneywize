using AutoBogus;
using Bogus;
using Domain.Entities;
namespace Domain.Persistence.Mock.Configs;

public sealed class AppUserMockConfig : AutoFaker<ApplicationUser> {
    public AppUserMockConfig() {
        Randomizer.Seed = new Random(8675309);
        RuleFor(fake => fake.Id, fake => fake.Random.Guid().ToString());
        RuleFor(fake => fake.FirstName, fake => fake.Name.FirstName());
        RuleFor(fake => fake.LastName, fake => fake.Name.LastName());
        RuleFor(fake => fake.Email, fake => fake.Person.Email);
        RuleFor(fake => fake.CountryId, fake => fake.Random.Decimal(110, 210));
        RuleFor(fake => fake.DisplayLanguageId, fake => fake.Random.Decimal(110, 210));
        RuleFor(fake => fake.EmailConfirmed, fake => true);
        RuleFor(fake => fake.TwoFactorEnabled, fake => true);
        RuleFor(fake => fake.Address, fake => fake.Address.FullAddress());
        RuleFor(fake => fake.PhoneNumber, fake => fake.Person.Phone);
        RuleFor(fake => fake.PhoneNumberConfirmed, fake => true);
        RuleFor(fake => fake.LockoutEnabled, fake => false);
        //RuleFor(fake => fake.LockoutEnd, fake => fake.Date.Future());
        RuleFor(fake => fake.AccessFailedCount, fake => fake.Random.Number(1, 1000));
        RuleFor(fake => fake.PasswordHash, fake => fake.Random.String2(128));
    }
}