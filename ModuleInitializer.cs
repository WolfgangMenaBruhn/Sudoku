using Catel.IoC;
using Sudoku.Contracts.Services;
using Sudoku.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the InitializeResourceGroups method is ran as soon as the assembly is loaded.
/// </summary>
// ReSharper disable once UnusedMember.Global
// ReSharper disable once CheckNamespace
public static class ModuleInitializer
{
    public static void Initialize()
    {
        // Code added here will be executed as soon as the assembly is loaded by the .net runtime. This
        // is a great opportunity to register any services in the service locator:

        var serviceLocator = ServiceLocator.Default;

        // Register singleton instances:
        serviceLocator.RegisterType<IModelsFactoryService, ModelsFactoryService>();
        serviceLocator.RegisterType<ISudokuService, SudokuService>();

    }
}