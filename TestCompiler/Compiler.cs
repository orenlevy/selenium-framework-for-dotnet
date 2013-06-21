using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using SeleniumScriptParser.Entities;

namespace TestCompiler
{
    /// <summary>
    /// This class compiles an assembly from a list of commands. 
    /// This assembly is packed with NUnit tests and can be used by the 
    /// NUnit console and UI applications.
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Creates an NUnit assembly.
        /// </summary>
        /// <param name="commands">The list of commands for the test.</param>
        /// <param name="testName">The name of the test.</param>
        /// <returns>true if the assembly was created.</returns>
        public static bool CreateAssembly(List<BaseCommand> commands, string testName)
        {
            CompilerParameters runParams = new CompilerParameters(AppSettings.Default.AssembliesReferences, string.Format("{0}.dll",testName));


            //generate the test.
            string source = GenerateTestCode(commands, testName);
            Debug.WriteLine(source);
            
            //compile the source to an assembly.
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerResults results = codeProvider.CompileAssemblyFromSource(runParams, source);
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                     Debug.WriteLine(error.ErrorText);   
                }
            }

            return !results.Errors.HasErrors;
        }

        /// <summary>
        /// Generate the test methods.
        /// </summary>
        /// <param name="commands">The list of commands.</param>
        /// <param name="testName">Name of the test.</param>
        /// <returns>A string containing engine commands.</returns>
        private static string GenerateTestCode(List<BaseCommand> commands, string testName)
        {
            StringBuilder source = new StringBuilder();
            StringBuilder commandString = new StringBuilder();

            foreach (BaseCommand command in commands)
            {
                if (command.Command != "close")
                    commandString.AppendFormat(".{0}(\"{1}\", \"{2}\")", command.Command, command.Target, command.Value);
                else
                    commandString.AppendFormat(".{0}()", command.Command);
            }

            source.Append(CodeSamples.Using);
            source.AppendFormat(CodeSamples.Namepace, string.Format(CodeSamples.Class, string.Format(CodeSamples.TestMethod, testName, commandString)));
            return source.ToString();
        }


    }
}
