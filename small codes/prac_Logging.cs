using Microsoft.Extensions.Logging;
namespace MyConsoleApp
{
   class Program
   {
      static void Main(string[] args)
      {
         // Configure logging
         var loggerFactory = LoggerFactory.Create(builder =>
         {
            builder.AddFilter("MyConsoleApp.Monster", LogLevel.Information) // Only logs information and errors from Monster instances
               .AddConsole();
               //.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt")); // Logs to a file
         });

         var logger = loggerFactory.CreateLogger<Program>();
         logger.LogInformation("Starting program");

         // Create a Monster instance and log some messages
         var monster = new Monster("Dark Magician");
         monster.Attack();
         monster.Defend();
         monster.SpecialAbility();

         logger.LogInformation("Program completed");
         // Create a logger factory with console provider
         /*         using var loggerFactory = LoggerFactory.Create(builder =>
                  {
                     builder.AddConsole();
                  });

                  // Create a logger for the current class
                  ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

                  // Log some messages with different levels
                  logger.LogTrace("This is a trace message.");
                  logger.LogDebug("This is a debug message.");
                  logger.LogInformation("This is an information message.");
                  logger.LogWarning("This is a warning message.");
                  logger.LogError("This is an error message.");
                  logger.LogCritical("This is a critical message.");

                  Console.WriteLine("Press any key to exit...");
                  Console.ReadKey();*/
      }
   }

   public class Monster
   {
      private string name;
      private ILogger<Monster> logger;

      public Monster(string name)
      {
         this.name = name;

         // Get a logger instance for this Monster
         logger = LoggerFactory.Create(builder => builder.AddConsole())
             .CreateLogger<Monster>();
      }

      public void Attack()
      {
         logger.LogInformation("{0} attacks!", name);
      }

      public void Defend()
      {
         logger.LogInformation("{0} defends!", name);
      }

      public void SpecialAbility()
      {
         logger.LogError("{0}'s special ability failed!", name);
      }
   }
}
