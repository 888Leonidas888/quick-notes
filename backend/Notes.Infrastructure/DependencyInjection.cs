using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Services;
using Notes.Domain.Interfaces;
using Notes.Infrastructure.Data;
using Notes.Infrastructure.Repositories;

namespace Notes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isTesting)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<NoteDbContext>(options =>
        {
            if (isTesting || string.IsNullOrWhiteSpace(connectionString))
                options.UseInMemoryDatabase("QuickNotesDb");
            else
                options.UseSqlServer(connectionString);
        });

        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<NoteService>();

        return services;
    }
}
