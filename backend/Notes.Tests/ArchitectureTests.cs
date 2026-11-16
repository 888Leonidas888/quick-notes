using Notes.Application.Services;
using Notes.Domain.Entities;
using Notes.Infrastructure.Data;

namespace Notes.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Domain_Should_Not_Have_Project_Or_EfCore_Dependencies()
    {
        var domainAssembly = typeof(Note).Assembly;
        var referencedNames = domainAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.DoesNotContain("Notes.Application", referencedNames);
        Assert.DoesNotContain("Notes.Infrastructure", referencedNames);
        Assert.DoesNotContain("Notes.Api", referencedNames);
        Assert.DoesNotContain(referencedNames, name => name != null && name.StartsWith("Microsoft.EntityFrameworkCore"));
    }

    [Fact]
    public void Application_Should_Only_Depend_On_Domain()
    {
        var appAssembly = typeof(NoteService).Assembly;
        var referencedNames = appAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.Contains("Notes.Domain", referencedNames);
        Assert.DoesNotContain("Notes.Infrastructure", referencedNames);
        Assert.DoesNotContain("Notes.Api", referencedNames);
        Assert.DoesNotContain(referencedNames, name => name != null && name.StartsWith("Microsoft.EntityFrameworkCore"));
    }

    [Fact]
    public void Infrastructure_Should_Not_Depend_On_Api()
    {
        var infraAssembly = typeof(NoteDbContext).Assembly;
        var referencedNames = infraAssembly.GetReferencedAssemblies().Select(a => a.Name).ToList();

        Assert.DoesNotContain("Notes.Api", referencedNames);
    }
}
