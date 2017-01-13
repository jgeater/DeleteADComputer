
using System;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.DirectoryServices.AccountManagement;

namespace DeleteADComputer
{
    class Program
    {
        static void Main(string[] args)
        {

            //check for two paremters
            if (args.Length != 1)
            {
                System.Console.WriteLine("Invalid command Line options");
                System.Console.WriteLine("");
                System.Console.WriteLine("deletes a coputer in AD");
                System.Console.WriteLine("");
                System.Console.WriteLine("Usage DeleteADComputer.exe <computername>");
                System.Console.WriteLine("");
                System.Console.WriteLine("Example:");
                System.Console.WriteLine(@"DeleteADComputer.exe computer1");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            // find the computer in question
            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, args[0]);

            //if not found exit
            if (computer == null)
            {
                Console.WriteLine("machine not found");
                Environment.Exit(53);
            }

            // if found - delete it
            if (computer != null)
            {
                //Console.WriteLine(computer);
                try
                { computer.Delete(); }

                //Catch any failures during the delete
                catch (System.UnauthorizedAccessException)
                {
                    Console.WriteLine("Access is Denied");
                    System.Environment.Exit(5);
                }

                // Handle InvalidOperationException.
                catch (InvalidOperationException InvOpEx)
                {
                    Console.WriteLine(InvOpEx.Message);
                    System.Environment.Exit(2);
                }

                // Handle COMException.
                catch (COMException COMEx)
                {
                    Console.WriteLine(COMEx.Message);
                    System.Environment.Exit(3);
                }

            }
        }
    }
}

