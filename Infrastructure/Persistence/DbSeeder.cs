using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Генератор данных для БД
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(TestTaskDbContext db)
    {
        if (await db.Patients.AnyAsync()) return;

        var (doctors, patients, encounters, conditions, observations) = DataGenerator.GenerateData();

        await db.Practitioners.AddRangeAsync(doctors);
        await db.Patients.AddRangeAsync(patients);
        await db.Encounters.AddRangeAsync(encounters);
        await db.Conditions.AddRangeAsync(conditions);
        await db.Observations.AddRangeAsync(observations);

        await db.SaveChangesAsync();
    }
}